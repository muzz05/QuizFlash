using QuizFlash;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace QuizFlash
{
    public partial class TeacherHomePage : Page
    {

        private bool isLoading;

        private string[] quotes = new string[]
        {
            "A teacher affects eternity; he can never tell where his influence stops. - Henry Adams",
            "Teaching is the greatest act of optimism. - Colleen Wilcox",
            "Good teaching is one-fourth preparation and three-fourths theater. - Gail Godwin",
            "A good teacher can inspire hope, ignite the imagination, and instill a love of learning. - Brad Henry",
            "Teaching kids to count is fine, but teaching them what counts is best. - Bob Talbert",
            "A good teacher is like a candle – it consumes itself to light the way for others. - Mustafa Kemal Atatürk",
            "The best teachers are those who show you where to look but don’t tell you what to see. - Alexandra K. Trenfor",
            "Great teachers empathize with kids, respect them, and believe that each one has something special that can be built upon. - Ann Lieberman",
            "A teacher’s job is to take a bunch of live wires and see that they are well-grounded. - Darwin D. Martin",
            "Education is not the filling of a pail, but the lighting of a fire. - W.B. Yeats"
        };


        public TeacherHomePage()
        {
            InitializeComponent();
            Loaded += TeacherHomePage_Loaded;
        }

        private async void TeacherHomePage_Loaded(object sender, RoutedEventArgs e)
        {
            SetLoadingState(true);
            await LoadDataAsync();
            SetLoadingState(false);
            AddQuote();
        }

        private void SetLoadingState(bool loading)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            MainContentTeacherHomePage.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
        }

        private async Task LoadDataAsync()
        {
            Database db = new Database();

            string sql = "SELECT * FROM LoggedDevices WHERE userId = @UserId";
            DataTable allDevices = await Task.Run(() => db.ExecuteQuery(sql, new MySqlParameter("@UserId", GlobalVariables.UserId)));

            sql = "SELECT q.name as quizName, q.startTime ,c.name as classroomName, c.id as classroomId FROM Classroom c JOIN Quiz q ON q.classroomId = c.id WHERE c.TeacherId = @TeacherId AND q.startTime > @CurrentDate";
            MySqlParameter[] resultParams =
            {
                new MySqlParameter("@TeacherId", GlobalVariables.TeacherId),
                new MySqlParameter("@CurrentDate", Utilities.GetCurrentTimeInEpoch()),
            };
            DataTable quizesResult = await Task.Run(() => db.ExecuteQuery(sql, resultParams));

            AddUserInfo();

            foreach (DataRow row in allDevices.Rows)
            {
                AddLoggedDevices(Convert.ToInt32(row["id"]), row["deviceName"].ToString(), Convert.ToInt32(row["lastLogin"]), Convert.ToInt32(row["deviceType"]));
            }

            foreach (DataRow row in quizesResult.Rows)
            {
                AddRecentQuiz(Convert.ToInt32(row["classroomId"]), row["classroomName"].ToString(), row["quizName"].ToString(), Convert.ToInt64(row["startTime"]));
            }
        }

        private void AddQuote()
        {
            Random random = new Random();
            int index = random.Next(quotes.Length);
            string randomQuote = quotes[index];

            studentQuote quo = new studentQuote(randomQuote);

            quote_panel.Children.Add(quo);
        }

        private void AddLoggedDevices(int id, string deviceName, int lastLogin, int deviceType)
        {
            loginData newDevice = new loginData(id, deviceName, lastLogin, deviceType);

            newDevice.Margin = new Thickness(0, 0, 0, 15);

            devices.Children.Add(newDevice);
        }

        private void AddRecentQuiz(int classroomId, string classroomName, string quizName, long startTime)
        {
            StudentHomepageInfoCard newQuiz = new StudentHomepageInfoCard(classroomId, classroomName, quizName, startTime);

            newQuiz.Margin = new Thickness(0, 0, 0, 15);

            infocards.Children.Add(newQuiz);
        }

        private void AddUserInfo()
        {
            UserInfo user = new UserInfo();

            user.Margin = new Thickness(0, 0, 0, 15);

            userInfo.Children.Add(user);
        }
    }
}
