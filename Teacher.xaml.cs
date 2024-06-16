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
    /// <summary>
    /// Interaction logic for Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {
        public Frame TeacherFrame => TeacherViewFrame;

        private int TeacherId;
        private int UserId;
        private string UserName;

        public Teacher(int _TeacherId, int _UserId, string _TeacherName)
        {
            TeacherId = _TeacherId;
            UserId = _UserId;
            UserName = _TeacherName;
            InitializeComponent();
            TeacherNameTextBox.Text = UserName;
        }

        private void StudentTabCheck(object sender, RoutedEventArgs e)
        {
            // This is for Student page Navigation
            TeacherViewFrame.Content = new StudentDataPage();
        }

        private void TeacherHomePageCheck(object sender, RoutedEventArgs e)
        {
            // This is for Student page Navigation
            TeacherViewFrame.Content = new TeacherHomePage();
        }

        private void ClassroomTagCheck(object sender, RoutedEventArgs e)
        {
            // This is for Classroom Navigation
            TeacherViewFrame.Content = new TeacherClassroomPage(TeacherId, UserId);
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
