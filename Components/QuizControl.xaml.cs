using QuizFlash;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Protobuf.Reflection;

namespace QuizFlash
{

    public partial class QuizControl : UserControl
    {
        int quizId;
        Database database=new Database();
        long quizEpochTime;

        // Scenarios:
        // 1. The user is a teacher and the quiz is not attempted (DONE)
        // 2. The user is a teacher and the quiz is attempted (DONE)
        // 3. The user is a student and the quiz is not attempted (DONE)
        // 4. The user is a student and the quiz is attempted (DONE)
        // 5. The quiz is expired (DONE)
        // 6. The quiz is scheduled for future (DONE)

        public QuizControl(int quizId, string quizname, int totalmarks, int questions, long startTimeEpoch, bool IsAttempted, int duration)
        {
            InitializeComponent();
            this.quizId = quizId;
            QuizName.Text = quizname;
            QuizMarks.Text = totalmarks.ToString();
            QuesCount.Text = questions.ToString();
            DurationBadge.Text = duration.ToString() + " Minutes";
            quizEpochTime = startTimeEpoch;

            AttemptedBadge.Visibility =  GlobalVariables.IsTeacher || !IsAttempted ? Visibility.Collapsed: Visibility.Visible;
            quizStartButton.Visibility = GlobalVariables.IsTeacher || IsAttempted ? Visibility.Collapsed : Visibility.Visible;

            // If the time is 5 minutes after the starting time then the quiz is expired
            if(Utilities.GetCurrentTimeInEpoch() > startTimeEpoch + (5 * 60))
            {
                AttemptedBadge.Visibility = Visibility.Visible;
                AttemptedBadge.Background = new BrushConverter().ConvertFromString("#8b3a3a") as Brush;
                textBlock.Text = "Expired";
                textBlock.MaxWidth = 50;
                quizStartButton.Visibility = Visibility.Collapsed;
            }

            if (Utilities.GetCurrentTimeInEpoch() < startTimeEpoch)
            {
                AttemptedBadge.Visibility = Visibility.Visible;
                AttemptedBadge.Background = new BrushConverter().ConvertFromString("#008080") as Brush;
                textBlock.Text = "Scheduled";
                quizStartButton.Visibility = Visibility.Collapsed;
            }

            ValidUntil.Text= Utilities.ConvertEpochToCustomString(startTimeEpoch);
        }     

        private void result_page_redirect(object sender, RoutedEventArgs e)
        {            
            if(GlobalVariables.IsTeacher)
            {
                DataTable reader = database.ExecuteQuery("SELECT s.studentCode, q.marksPerQuestion, q.totalQuestions, u.name, d.name as departmentName, r.marksObtained FROM Students s JOIN Result r ON s.id = r.studentId JOIN Users u ON u.id = s.userId JOIN Quiz q ON q.id = r.quizId JOIN Department d ON u.departmentId = d.id where r.quizId=@quizId;", new MySqlParameter("quizId", quizId));
                if (reader.Rows.Count == 0)
                {
                    CustomMessageBox msg = new CustomMessageBox("No result data", "No results to show", "Info");
                    msg.ShowDialog();                   
                    return;
                }
                foreach (Window window in Application.Current.Windows)
                {
                    if(window is Teacher teacher)
                    {
                        teacher.TeacherViewFrame.Content = new ResultTablePage(quizId, QuizName.Text);
                    }
                }
            }
            else
            {
                string query= "Select s.isCorrect, q.totalMarks, q.marksPerQuestion from StudentResponse s JOIN Quiz q ON s.quizId=q.id where s.quizId=@quizId and s.studentId=@studentId;";
                DataTable result = database.ExecuteQuery(query, new MySqlParameter[] {new MySqlParameter("@quizId",quizId), new MySqlParameter("@studentId",GlobalVariables.StudentId)});

                if(result.Rows.Count == 0)
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("Attempt it first!","You have not attempted this quiz yet", "Info");
                    customMessageBox.ShowDialog();
                }
                else
                {
                    int score = 0;
                    int totalMarks = Convert.ToInt32(result.Rows[0]["totalMarks"]);

                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        score += Convert.ToBoolean(result.Rows[i]["isCorrect"]) ? Convert.ToInt32(result.Rows[i]["marksPerQuestion"]):0;
                    }

                    CustomMessageBox customMessageBox = new CustomMessageBox("Your Result", $"You got:\n {score} out of {totalMarks}", "OK");
                    customMessageBox.ShowDialog();                        
                }
            }
        }

        private void quizRedirect(object sender, RoutedEventArgs e)
        {
            if(Utilities.GetCurrentTimeInEpoch() > quizEpochTime + (5 * 60))
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("Quiz Expired", "This quiz has expired", "Error");
                customMessageBox.ShowDialog();
                return;
            }

            foreach(Window window in Application.Current.Windows)
            {
                if(window is Student student)
                {
                    DataTable marksPerQuestion = database.ExecuteQuery("Select marksPerQuestion, duration from Quiz where id=@quizId", new MySqlParameter("@quizId",quizId));
                    student.StudentViewFrame.Content = new QuizDisplayPage(quizId, QuizName.Text, Convert.ToInt32(marksPerQuestion.Rows[0]["marksPerQuestion"]), Convert.ToInt32(marksPerQuestion.Rows[0]["duration"]));                    
                }
            }
        }
    }
}