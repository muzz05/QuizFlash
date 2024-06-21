using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for TeacherClassroomQuizPage.xaml
    /// </summary>
    public partial class TeacherClassroomQuizPage : Page
    {
        public TeacherClassroomQuizPage()
        {
            InitializeComponent();

            AddQuizButton.Visibility = GlobalVariables.IsTeacher ? Visibility.Visible : Visibility.Collapsed;

            // This is Dummy data
            // Example 1
            AddQuiz("Math Quiz 1", 100, 10, DateTimeOffset.Now.AddDays(7).ToUnixTimeSeconds(), false);

            // Example 2
            AddQuiz("Science Quiz 2", 50, 5, DateTimeOffset.Now.AddDays(14).ToUnixTimeSeconds(), true);

            // Example 3
            AddQuiz("History Quiz 3", 75, 15, DateTimeOffset.Now.AddDays(30).ToUnixTimeSeconds(), false);

            // Example 4
            AddQuiz("Geo Quiz 4", 60, 12, DateTimeOffset.Now.AddDays(10).ToUnixTimeSeconds(), true);

            // Example 5
            AddQuiz("Physics Quiz 5", 80, 20, DateTimeOffset.Now.AddDays(21).ToUnixTimeSeconds(), false);

            // Example 6
            AddQuiz("Chem Quiz 6", 90, 18, DateTimeOffset.Now.AddDays(25).ToUnixTimeSeconds(), true);

            // Example 7
            AddQuiz("Biology Quiz 7", 70, 14, DateTimeOffset.Now.AddDays(12).ToUnixTimeSeconds(), false);

            // Example 8
            AddQuiz("Literature Quiz 8", 85, 17, DateTimeOffset.Now.AddDays(19).ToUnixTimeSeconds(), true);

            // Example 9
            AddQuiz("Art Quiz 9", 55, 11, DateTimeOffset.Now.AddDays(16).ToUnixTimeSeconds(), false);

            // Example 10
            AddQuiz("Music Quiz 10", 65, 13, DateTimeOffset.Now.AddDays(22).ToUnixTimeSeconds(), true);


            Database db = new Database();

            string sql;

            if (GlobalVariables.IsTeacher)
            {
                sql = "SELECT * FROM Quiz WHERE classroomId = @ClassroomId";
                DataTable result = db.ExecuteQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    AddQuiz(result.Rows[i]["name"].ToString(), Convert.ToInt32(result.Rows[i]["totalMarks"]), Convert.ToInt32(result.Rows[i]["totalQuestions"]), Convert.ToInt32(result.Rows[i]["dueDate"]), false);
                }
            }
            else
            {
                sql = "SELECT q.* , EXISTS(SELECT 1 FROM StudentResponse WHERE studentId = @StudentId AND quizId = q.id) AS isAttempted FROM Quiz q WHERE q.classroomId = @ClassroomId";
                MySqlParameter[] quizParams =
                {
                    new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId),
                    new MySqlParameter("@StudentId", GlobalVariables.StudentId)
                };
                DataTable result = db.ExecuteQuery(sql, quizParams);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    AddQuiz(result.Rows[i]["name"].ToString(), Convert.ToInt32(result.Rows[i]["totalMarks"]), Convert.ToInt32(result.Rows[i]["totalQuestions"]), Convert.ToInt32(result.Rows[i]["dueDate"]), Convert.ToBoolean(result.Rows[i]["isAttempted"]));
                }
            }

        }

        public void AddQuiz(string quizname, int totalmarks, int questions, long validUntilEpoch, bool IsAttempted)
        {
            int index = TeacherClassroomQuizPanel.Children.Count - 1;
            QuizControl newQuiz = new QuizControl(quizname, totalmarks, questions, validUntilEpoch,IsAttempted);
            newQuiz.Margin = new Thickness(0, 15, 15, 0);
            TeacherClassroomQuizPanel.Children.Insert(index,newQuiz);

        }

        private void addquizbutton_Click(object sender, RoutedEventArgs e)
        {
            QuizDetail_Input quiz=new QuizDetail_Input();
            quiz.Show();
        }
    }
}
