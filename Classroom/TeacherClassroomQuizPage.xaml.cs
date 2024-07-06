using LiveCharts.Wpf.Charts.Base;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
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
using ZstdSharp.Unsafe;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for TeacherClassroomQuizPage.xaml
    /// </summary>
    public partial class TeacherClassroomQuizPage : Page
    {

        private bool isLoading;
        public TeacherClassroomQuizPage()
        {
            InitializeComponent();

            AddQuizButton.Visibility = GlobalVariables.IsTeacher ? Visibility.Visible : Visibility.Collapsed;

            Loaded += TeacherClassroomQuizPage_Loaded;
        }

        private async void TeacherClassroomQuizPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetLoadingState(true);
            await LoadAsyncData();
            SetLoadingState(false);
        }

        private void SetLoadingState(bool loading)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            ClassroomQuizGrid.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
        }

        private async Task LoadAsyncData()
        {
            Database db = new Database();

            string sql;

            if (GlobalVariables.IsTeacher)
            {
                sql = "SELECT * FROM Quiz WHERE classroomId = @ClassroomId";
                DataTable result = await Task.Run(() =>db.ExecuteQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId)));
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    AddQuiz(Convert.ToInt32(result.Rows[i]["id"]), result.Rows[i]["name"].ToString(), Convert.ToInt32(result.Rows[i]["totalMarks"]), Convert.ToInt32(result.Rows[i]["totalQuestions"]), Convert.ToInt32(result.Rows[i]["dueDate"]), false);
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
                DataTable result = await Task.Run(() => db.ExecuteQuery(sql, quizParams));
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    AddQuiz(Convert.ToInt32(result.Rows[i]["id"]), result.Rows[i]["name"].ToString(), Convert.ToInt32(result.Rows[i]["totalMarks"]), Convert.ToInt32(result.Rows[i]["totalQuestions"]), Convert.ToInt32(result.Rows[i]["dueDate"]), Convert.ToBoolean(result.Rows[i]["isAttempted"]));
                }
            }
        }

        public void AddQuiz(int quizId,string quizname, int totalmarks, int questions, long validUntilEpoch, bool IsAttempted)
        {
            int index = TeacherClassroomQuizPanel.Children.Count - 1;
            QuizControl newQuiz = new QuizControl(quizId,quizname, totalmarks, questions, validUntilEpoch,IsAttempted);
            newQuiz.Margin = new Thickness(0, 15, 15, 0);
            TeacherClassroomQuizPanel.Children.Insert(index,newQuiz);

        }

        private void addquizbutton_Click(object sender, RoutedEventArgs e)
        {
            QuizDetail_Input quiz=new QuizDetail_Input();
            quiz.ShowDialog();
        }
    }
}
