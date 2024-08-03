using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizFlash.Components;
using System.Windows.Threading;

namespace QuizFlash
{
    public partial class QuizDisplayPage : Page
    {
        int response, correct, quizId, marksPerQuestion;
        Database database = new Database();
        private DispatcherTimer timer;
        private TimeSpan timeLeft;


        public QuizDisplayPage(int id, string name, int marksPerQuestion, int duration)
        {
            InitializeComponent();
            quizTitle.Text = name;
            quizId = id;
            this.marksPerQuestion = marksPerQuestion;

            string query = "Select * from QuestionAnswers where quizId=@quizId";
            DataTable quiz = database.ExecuteQuery(query, new MySqlParameter("@quizId", id));
            Random random = new Random();

            int i = 1;
            while (quiz.Rows.Count>0)                    
            {
                int index = random.Next(quiz.Rows.Count);
                QuizDisplayControl quizDisplayControl = new QuizDisplayControl(Convert.ToInt32(quiz.Rows[index]["id"]), quiz.Rows[index]["question"].ToString(), quiz.Rows[index]["optionA"].ToString(), quiz.Rows[index]["optionB"].ToString(), quiz.Rows[index]["optionC"].ToString(), quiz.Rows[index]["optionD"].ToString(), Convert.ToInt32(quiz.Rows[index]["correct"]),i);
                quizDisplayPanel.Children.Add(quizDisplayControl);
                quiz.Rows.RemoveAt(index);
                i++;
            }

            // Setting the Timer
            timeLeft = TimeSpan.FromMinutes(duration);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick; 

            timer.Start();

            this.Unloaded += QuizDisplayPage_Unloaded;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));

            timerText.Text = timeLeft.ToString(@"mm\:ss");

            if (timeLeft.TotalSeconds <= 0)
            {
                timer.Stop();
                SubmitQuiz();
            }

            if(timeLeft.TotalSeconds == 20)
            {
                timer.Stop();
                CustomMessageBox prompt = new CustomMessageBox("Not much time left", "Only 20 seconds left in auto submission", "Info");
                prompt.ShowDialog();
                timer.Start();
            }
        }


        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            SubmitQuiz();
        }

        private void SubmitQuiz()
        {
            int questionCount = 0, marksObtained = 0;

            foreach (var control in quizDisplayPanel.Children)
            {
                questionCount++;
                if (control is QuizDisplayControl quizDisplayControl)
                {
                    string query = "Insert into StudentResponse (quizId, questionId, studentId, isCorrect, checkedAnswer) values(@quizId,@questionId,@studentId,@correctness,@response)";
                    MySqlParameter[] parameters =
                    {
                        new MySqlParameter("@quizId",quizId),
                        new MySqlParameter("@questionId",quizDisplayControl.questionId),
                        new MySqlParameter("@studentId",GlobalVariables.StudentId),
                        new MySqlParameter("@correctness",quizDisplayControl.response==quizDisplayControl.correct),
                        new MySqlParameter("@response",quizDisplayControl.response)
                    };

                    marksObtained += quizDisplayControl.response == quizDisplayControl.correct ? marksPerQuestion : 0;

                    database.ExecuteNonQuery(query, parameters);
                }
            }

            string query1 = "Insert into Result (quizId, studentId, marksObtained) values(@quizId,@studentId,@marksObtained)";

            MySqlParameter[] parameters1 =
            {
                new MySqlParameter("@quizId",quizId),
                new MySqlParameter("@studentId",GlobalVariables.StudentId),
                new MySqlParameter("@marksObtained",marksObtained)
            };
            database.ExecuteNonQuery(query1, parameters1);

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Student student)
                {
                    student.StudentViewFrame.Content = new TeacherClassroomMainPage();
                }
            }
        }

        private void QuizDisplayPage_Unloaded(object sender, RoutedEventArgs e)
        {
            SubmitQuiz();
            CustomMessageBox prompt = new CustomMessageBox("Quiz Submitted", "Quiz has automatically been submitted", "Info");
            prompt.ShowDialog();
        }
    }
}
