using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace QuizFlash
{
    public partial class UserInfo : UserControl
    {

        private bool isLoading;

        public UserInfo()
        {
            InitializeComponent();
            Loaded += UserInfo_Loaded;
        }

        private async void UserInfo_Loaded(object sender, RoutedEventArgs e)
        {
            SetLoadingState(true);
            await LoadUserDataAsync();
            SetLoadingState(false);
        }

        private void SetLoadingState(bool loading)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            UserInfoGrid.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
        }

        private async Task LoadUserDataAsync()
        {
            UserInfoGrid.Visibility = Visibility.Hidden;

            Database db = new Database();

            int quizCountInt = 0;
            int classroomCountInt = 0;
            int flashcardCountInt = 0;
            int successInt = 0;
            string sql = "";

            if (!GlobalVariables.IsTeacher)
            {
                sql = "SELECT COUNT(q.id) as QuizCount FROM Quiz q JOIN Classroom c ON c.id = q.classroomId JOIN ClassroomStudents cs ON cs.classroomId = c.id WHERE cs.studentId = @StudentId";
                object quizCount = await Task.Run(() => db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId)));
                quizCountInt = Convert.ToInt32(quizCount);

                sql = "SELECT COUNT(cs.id) as ClassroomCount FROM ClassroomStudents cs WHERE cs.studentId = @StudentId";
                object classroomCount = await Task.Run(() => db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId)));
                classroomCountInt = Convert.ToInt32(classroomCount);

                sql = "SELECT COUNT(id) as FlashCardCount FROM Flashcards WHERE studentId = @StudentId";
                object flashcardCount = await Task.Run(() => db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId)));
                flashcardCountInt = Convert.ToInt32(flashcardCount);

                sql = "SELECT AVG((r.marksObtained / q.totalMarks) * 100) AS SuccessRate FROM Result r JOIN Quiz q ON r.quizId = q.id WHERE r.studentId = @StudentId GROUP BY r.studentId";
                object success = await Task.Run(() => db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId)));
                successInt = Convert.ToInt32(success);
            }
            else
            {
                sql = "SELECT COUNT(q.id) as QuizCount FROM Quiz q JOIN Classroom c ON c.id = q.classroomId WHERE c.teacherId = @TeacherId";
                object quizCount = await Task.Run(() => db.ExecuteScalar(sql, new MySqlParameter("@TeacherId", GlobalVariables.TeacherId)));
                quizCountInt = Convert.ToInt32(quizCount);

                sql = "SELECT COUNT(c.id) as ClassroomCount FROM Classroom c WHERE c.teacherId = @TeacherId";
                object classroomCount = await Task.Run(() => db.ExecuteScalar(sql, new MySqlParameter("@TeacherId", GlobalVariables.TeacherId)));
                classroomCountInt = Convert.ToInt32(classroomCount);

                sql = "SELECT COUNT(id) as StudentCount FROM Students";
                object flashcardCount = await Task.Run(() => db.ExecuteScalar(sql));
                flashcardCountInt = Convert.ToInt32(flashcardCount);
            }

            UserInfoGrid.Visibility = Visibility.Visible;

            studentName.Text = GlobalVariables.Username;
            isStudentorTeacher.Text = GlobalVariables.IsTeacher ? "Teacher" : "Student";
            classroomNo.Text = classroomCountInt.ToString();

            if (GlobalVariables.IsTeacher)
            {
                ChangeableIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.School;
                changeablepanel.Margin = new Thickness(5, 0, 25, 0);
                ChangeableTextBox.Text = "Total Students";
            }

            flashcardNo.Text = flashcardCountInt.ToString();
            quizAttempted.Text = quizCountInt.ToString();

            if (GlobalVariables.IsTeacher)
            {
                SuccessRateQuiz.Visibility = Visibility.Hidden;
            }
            else
            {
                quizProgress.Value = successInt;
                QuizPercentage.Text = successInt.ToString() + "%";
            }
        }
    }
}
