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
        public QuizControl(int quizId, string quizname, int totalmarks, int questions, long validUntilEpoch, bool IsAttempted)
        {
            InitializeComponent();
            this.quizId = quizId;
            QuizName.Text = quizname;
            QuizMarks.Text = totalmarks.ToString();
            QuesCount.Text = questions.ToString();

            AttemptedBadge.Visibility =  GlobalVariables.IsTeacher || !IsAttempted ? Visibility.Collapsed: Visibility.Visible;
            quizStartButton.Visibility = GlobalVariables.IsTeacher || IsAttempted ? Visibility.Collapsed : Visibility.Visible;
            // Convert epoch timestamp to ISO 8601 string (assuming validUntilEpoch is in seconds)
            string validUntilIsoString = ConvertEpochToIsoString(validUntilEpoch);
            string validUntilDate = ConvertIsoStringToDate(validUntilIsoString); 
            ValidUntil.Text= validUntilDate;
        }

        private string ConvertEpochToIsoString(long epochTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(epochTimestamp * 1000);
            return dateTime.UtcDateTime.ToString("o");
        }
        private string ConvertIsoStringToDate(string isoString)
        {
            DateTime parsedDateTime = DateTime.Parse(isoString);
            return parsedDateTime.ToString("d");  
        }        

        private void result_page_redirect(object sender, RoutedEventArgs e)
        {            
            if(GlobalVariables.IsTeacher)
            {
                DataTable reader = database.ExecuteQuery("SELECT s.studentCode, q.marksPerQuestion, q.totalQuestions, u.name, d.name as departmentName, r.marksObtained FROM Students s JOIN Result r ON s.id = r.studentId JOIN Users u ON u.id = s.userId JOIN Quiz q ON q.id = r.quizId JOIN Department d ON u.departmentId = d.id where r.quizId=@quizId;", new MySqlParameter("quizId", quizId));
                if (reader.Rows.Count == 0)
                {
                    CustomMessageBox msg = new CustomMessageBox("No result data", "No results to show", "Error");
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
                            CustomMessageBox customMessageBox = new CustomMessageBox("Attempt it first!","You have not attempted this quiz yet", "Error");
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
            foreach(Window window in Application.Current.Windows)
            {
                if(window is Student student)
                {
                    student.StudentViewFrame.Content = new QuizDisplayPage(quizId, QuizName.Text);                    
                }
            }
        }
    }
}