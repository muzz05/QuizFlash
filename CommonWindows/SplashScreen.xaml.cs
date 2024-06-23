using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += SplashScreen_Loaded;
        }

        private async void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            // Wait asynchronously for 5 seconds
            await Task.Delay(5000);

            CheckingLoggedDevices();

            this.Close();
        }

        private void CheckingLoggedDevices()
        {
            Database db = new Database();

            // Getting the MAC Address

            string firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            // Checking if the user is already logged in

            string sql = "SELECT * FROM LoggedDevices WHERE MacAddress = @MACAddress";
            DataTable result = db.ExecuteQuery(sql, new MySqlParameter("@MACAddress", firstMacAddress));

            if (result.Rows.Count == 0)
            {
                Login loginWindow = new Login();
                loginWindow.Show();
            }
            else
            {
                int userId = Convert.ToInt32(result.Rows[0]["userId"]);
                sql = "SELECT isTeacher, name from Users WHERE id = @UserId";
                DataTable CheckingRole = db.ExecuteQuery(sql, new MySqlParameter("@UserId", userId));
                bool isTeacher = Convert.ToBoolean(CheckingRole.Rows[0]["isTeacher"]);
                string name = CheckingRole.Rows[0]["name"].ToString();

                // Adding Data to Global Variables
                GlobalVariables.UserId = userId;
                GlobalVariables.IsTeacher = isTeacher;
                GlobalVariables.Username = name;

                if (isTeacher)
                {
                    sql = "SELECT id, teacherCode FROM Teachers WHERE userId = @UserIdOfTeacher";
                    MySqlParameter TeacherUserId = new MySqlParameter();
                    TeacherUserId.ParameterName = "@UserIdOfTeacher";
                    TeacherUserId.Value = userId;
                    DataTable TeacherResult = db.ExecuteQuery(sql, TeacherUserId);

                    // Adding Data to Global Variables
                    GlobalVariables.TeacherId = Convert.ToInt32(TeacherResult.Rows[0]["id"]);
                    GlobalVariables.UserCode = TeacherResult.Rows[0]["teacherCode"].ToString();

                    Teacher teacherWindow = new Teacher(Convert.ToInt32(TeacherResult.Rows[0]["id"]), userId, name);

                    teacherWindow.Show();
                }
                else
                {
                    sql = "SELECT id, studentCode FROM Students WHERE userId = @UserIdOfStudent";
                    MySqlParameter StudentUserId = new MySqlParameter();
                    StudentUserId.ParameterName = "@UserIdOfStudent";
                    StudentUserId.Value = userId;
                    DataTable StudentResult = db.ExecuteQuery(sql, StudentUserId);

                    // Adding Data to Global Variables
                    GlobalVariables.StudentId = Convert.ToInt32(StudentResult.Rows[0]["id"]);
                    GlobalVariables.UserCode = StudentResult.Rows[0]["studentCode"].ToString();

                    Student studentWindow = new Student(Convert.ToInt32(StudentResult.Rows[0]["id"]), userId, name);


                    studentWindow.Show();
                }
            }
        }
    }
}
