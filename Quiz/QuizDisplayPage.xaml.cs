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
        int response, correct, quizId;
        Database database = new Database();
        public QuizDisplayPage(int id, string name)
        {
            InitializeComponent();
            quizTitle.Text = name;
            quizId = id;

            string query = "Select * from questionanswers where quizId=@quizId";
            DataTable quiz = database.ExecuteQuery(query, new MySqlParameter("@quizId", id));

            for (int i = 0; i < quiz.Rows.Count; i++)
            {
                QuizDisplayControl quizDisplayControl = new QuizDisplayControl(quiz.Rows[i]["question"].ToString(), quiz.Rows[i]["optionA"].ToString(), quiz.Rows[i]["optionB"].ToString(), quiz.Rows[i]["optionC"].ToString(), quiz.Rows[i]["optionD"].ToString(), Convert.ToInt32(quiz.Rows[i]["correct"]));
                quizDisplayPanel.Children.Add(quizDisplayControl);
            }
        }

        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            int questionCount = 0;
            foreach (var control in quizDisplayPanel.Children)
            {
                questionCount++;
                if (control is QuizDisplayControl quizDisplayControl)
                { string query = "Insert into studentresponse (quizId, questionId, studentId, isCorrect, checkedAnswer) values(@quizId,@questionId,@studentId,@correctness,@response)";
                    MySqlParameter[] parameters =
                    {
                        new MySqlParameter("@quizId",quizId),
                        new MySqlParameter("@questionId",questionCount),
                        new MySqlParameter("@studentId",GlobalVariables.StudentId),
                        new MySqlParameter("@correctness",quizDisplayControl.response==quizDisplayControl.correct),
                        new MySqlParameter("@response",quizDisplayControl.response)
                    };

                    database.ExecuteNonQuery(query, parameters);
                }
            }
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Student student)
                {
                    student.StudentViewFrame.Content = new TeacherClassroomQuizPage();
                }
            }
        }
    }
}
