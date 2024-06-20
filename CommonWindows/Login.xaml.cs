using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
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

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }


        // THIS IS FOR UI

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBoxLogin.Password) && passwordBoxLogin.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void TextPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBoxLogin.Focus();
        }


        private void TxtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(emailBoxLogin.Text) && emailBoxLogin.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void TextEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            emailBoxLogin.Focus();
        }

        // THIS IS THE LOGICAL PART
        private void RedirectToSignup(object sender, RoutedEventArgs e)
        {
            Signup signupWindow =  new Signup();
            signupWindow.Show();
            this.Close();
        }
        private void HandleLogin(object sender, RoutedEventArgs e)
        {
            Database db = new Database();

            string password = passwordBoxLogin.Password;
            string email = emailBoxLogin.Text;

            string sql = "SELECT * FROM Users WHERE email = @Email";

            MySqlParameter emailParam = new MySqlParameter();
            emailParam.ParameterName = "@Email";
            emailParam.Value = email;

            DataTable resultTable = db.ExecuteQuery(sql, emailParam);

            if (resultTable.Rows.Count == 0)
            {
                CustomMessageBox errorEmail = new CustomMessageBox("Unsuccessful Login", "Please enter a valid email address", "Error");
                errorEmail.Show();
            }
            else
            {
                DataRow row = resultTable.Rows[0];

                string dbPassword = row["password"].ToString();
                int dbUserId = Convert.ToInt32(row["id"]);
                bool isTeacher = Convert.ToBoolean(row["isTeacher"]);
                string dbUsername = row["name"].ToString();

                // Checking The Password

                if (password == dbPassword)
                {
                    if (isTeacher)
                    {
                        sql = "SELECT id FROM Teachers WHERE userId = @UserIdOfTeacher";
                        MySqlParameter TeacherUserId = new MySqlParameter();
                        TeacherUserId.ParameterName = "@UserIdOfTeacher";
                        TeacherUserId.Value = dbUserId;
                        object TeacherIdResult = db.ExecuteScalar(sql, TeacherUserId);
                        Teacher teacherWindow = new Teacher(Convert.ToInt32(TeacherIdResult), dbUserId, dbUsername);

                        // Adding Data to Global Variables
                        GlobalVariables.TeacherId = Convert.ToInt32(TeacherIdResult);

                        // Adding Data to the Logged Devices
                        AddDataToLoggedDevices(dbUserId);


                        teacherWindow.Show();
                    } 
                    else
                    {
                        sql = "SELECT id FROM Students WHERE userId = @UserIdOfStudent";
                        MySqlParameter StudentUserId = new MySqlParameter();
                        StudentUserId.ParameterName = "@UserIdOfStudent";
                        StudentUserId.Value = dbUserId;
                        object StudentIdResult = db.ExecuteScalar(sql, StudentUserId);
                        Student studentWindow = new Student(Convert.ToInt32(StudentIdResult), dbUserId, dbUsername);

                        // Adding Data to Global Variables
                        GlobalVariables.StudentId = Convert.ToInt32(StudentIdResult);

                        // Adding Data to the Logged Devices
                        AddDataToLoggedDevices(dbUserId);


                        studentWindow.Show();
                    }

                    // Adding Data to Global Variables
                    GlobalVariables.UserId = dbUserId;
                    GlobalVariables.IsTeacher = isTeacher;
                    GlobalVariables.Username = dbUsername;

                    this.Close();
                }
                else
                {
                    CustomMessageBox errorPassword = new CustomMessageBox("Unsuccessful Login", "Please enter the correct password", "Error");
                    errorPassword.Show();
                }
            }
        }

        private void AddDataToLoggedDevices(int userId)
        {
            Database db = new Database();


            // The MAC Adress

            string firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            // Getting the EPOC TIme

            DateTime currentTime = DateTime.UtcNow;
            DateTimeOffset dateTimeOffset = new DateTimeOffset(currentTime);
            long epochTime = dateTimeOffset.ToUnixTimeSeconds();

            // Database Queries

            string sql = "SELECT id,MacAddress FROM LoggedDevices WHERE userId = @UserId";
            MySqlParameter UserId = new MySqlParameter();
            UserId.ParameterName = "@UserId";
            UserId.Value = userId;
            DataTable result = db.ExecuteQuery(sql, UserId);

            if (result.Rows.Count != 0)
            {
                sql = "UPDATE LoggedDevices SET lastLogin = @LastLoginEPOC WHERE id = @LoggedDevicesId";
                MySqlParameter[] LoggedDevicesParamsAvailable = {
                    new MySqlParameter("@LastLoginEPOC", epochTime),
                    new MySqlParameter("@LoggedDevicesId", Convert.ToInt32(result.Rows[0]["id"])),
                };
                int FinalResult = db.ExecuteNonQuery(sql, LoggedDevicesParamsAvailable);
            }
            else
            {
                sql = "INSERT INTO LoggedDevices(lastLogin, MacAddress,  userId) VALUES(@LastLoginEPOC, @MACAddress, @UserId)";
                MySqlParameter[] LoggedDevicesParams =
                {
                    new MySqlParameter("@LastLoginEPOC", epochTime),
                    new MySqlParameter("@MACAddress", firstMacAddress),
                    new MySqlParameter("@UserId", userId)
                };
                int FinalResult = db.ExecuteNonQuery (sql, LoggedDevicesParams);
            }
        }

    }
}



