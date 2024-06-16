using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    public partial class Student : Window
    {

        public Frame StudentFrame => StudentViewFrame;

        private int StudentId;
        private int UserId;
        private string UserName;

        public Student(int _StudentId, int _UserId, string _StudentName)
        {
            StudentId = _StudentId;
            UserId = _UserId;
            UserName = _StudentName;
            InitializeComponent();
            StudentNameTextBox.Text = UserName;
        }

        private void StudentClassroomCheck(object sender, RoutedEventArgs e)
        {
            StudentViewFrame.Content = new StudentClassroomPage(StudentId,UserId);
        }

        private void StudentFlashCardCheck(object sender, RoutedEventArgs e)
        {
            StudentViewFrame.Content = new FlashCardPage(StudentId, UserId);
        }

        private void StudentHomePageCheck(object sender, RoutedEventArgs e)
        {
            StudentViewFrame.Content = new StudentHomePage();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            // Getting the MAC Address

            string firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();


            Database db = new Database();
            string sql = "DELETE FROM LoggedDevices WHERE MacAddress = @MACAddress AND userId = @UserId";
            int result = db.ExecuteNonQuery(sql, new MySqlParameter("@MACAddress", firstMacAddress), new MySqlParameter("@UserId", UserId));

            this.Close();
        }
    }
}
