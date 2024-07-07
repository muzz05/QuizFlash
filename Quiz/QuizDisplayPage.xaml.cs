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

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for QuizDisplayPage.xaml
    /// </summary>
    public partial class QuizDisplayPage : Page
    {
        int response, correct, quizId, marksPerQuestion;
        Database database = new Database();
        public QuizDisplayPage(int id, string name, int marksPerQuestion)
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
        }

        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            int questionCount = 0, marksObtained=0;
            bool isAllQuestionAttempted = true;

            // cheking if all the questions are attempted

            foreach (var control in quizDisplayPanel.Children)
            {
                if (control is QuizDisplayControl quizDisplayControl)
                {
                    if(quizDisplayControl.response == 1000)
                    {
                        isAllQuestionAttempted = false;
                        break;
                    }
                }
            }

            if (isAllQuestionAttempted)
            {

            

            foreach (var control in quizDisplayPanel.Children)
            {
                questionCount++;
                if (control is QuizDisplayControl quizDisplayControl)
                { string query = "Insert into StudentResponse (quizId, questionId, studentId, isCorrect, checkedAnswer) values(@quizId,@questionId,@studentId,@correctness,@response)";
                    MySqlParameter[] parameters =
                    {
                        new MySqlParameter("@quizId",quizId),
                        new MySqlParameter("@questionId",quizDisplayControl.questionId),
                        new MySqlParameter("@studentId",GlobalVariables.StudentId),
                        new MySqlParameter("@correctness",quizDisplayControl.response==quizDisplayControl.correct),
                        new MySqlParameter("@response",quizDisplayControl.response)
                    };

                    marksObtained+=quizDisplayControl.response == quizDisplayControl.correct ? marksPerQuestion : 0;

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
            else
            {
                CustomMessageBox error = new CustomMessageBox("Attempt All Question", "You cannot leave any question unsolved", "Error");
                error.ShowDialog();
            }
        }
    }
}
