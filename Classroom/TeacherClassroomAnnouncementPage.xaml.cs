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
    /// Interaction logic for TeacherClassroomAnnouncementPage.xaml
    /// </summary>
    public partial class TeacherClassroomAnnouncementPage : Page
    {
        public TeacherClassroomAnnouncementPage()
        {
            InitializeComponent();

            Database db = new Database();

            // Getting the Classroom Info
            string sql = "SELECT c.name AS classroomName, c.courseCode, u.name FROM Classroom c JOIN Teachers t ON c.teacherId = t.id JOIN Users u ON u.id = t.userId WHERE c.id = @ClassroomId";
            DataTable result = db.ExecuteQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));

            ClassroomNameAnnouncement.Text = result.Rows[0]["classroomName"].ToString();
            TeacherNameAnnouncement.Text = result.Rows[0]["name"].ToString();
            CourseCodeAnnouncement.Text = result.Rows[0]["courseCode"].ToString();
            OwnUsernameInitials.Text = GetInitials(GlobalVariables.Username);

            // Getting the Announcement Info
            sql = "SELECT cs.*, u.name FROM ClassroomStream cs JOIN Users u ON u.id = cs.userId WHERE classroomId = @ClassroomID ORDER BY cs.id DESC";
            DataTable announcementsResult = db.ExecuteQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));

            for (int i = 0; i < announcementsResult.Rows.Count; i++)
            {
                AddAnnouncements(announcementsResult.Rows[i]["name"].ToString(), Convert.ToBoolean(announcementsResult.Rows[i]["isTeacher"]), Convert.ToInt32(announcementsResult.Rows[i]["createTime"]), announcementsResult.Rows[i]["message"].ToString());
            }
        }


        // UI Related
        private void AnnouncmentTextBoxChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AnnouncementTextBox.Text) && AnnouncementTextBox.Text.Length > 0)
                AnnouncementPlaceHolderTextBlock.Visibility = Visibility.Collapsed;
            else
                AnnouncementPlaceHolderTextBlock.Visibility = Visibility.Visible;
        }

        private void AnnouncementTextMouseDown(object sender, MouseButtonEventArgs e)
        {
            AnnouncementTextBox.Focus();
        }

        public void AddAnnouncements(string username, bool isTeacher, long epochDate, string message)
        {
            Announcements newAnnouncement = new Announcements(username, isTeacher, epochDate, message);
            newAnnouncement.Margin = new Thickness(0, 5,0,0);
            AnnoucementsPanel.Children.Add(newAnnouncement);
        }

        private string GetInitials(string username)
        {
            var names = username.Split(' ');
            if (names.Length >= 2)
            {
                return $"{names[0][0]}{names[1][0]}";
            }
            if (names.Length == 1)
            {
                return $"{names[0][0]}";
            }
            return "NN";
        }

        private void AnnouncementTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && AnnouncementTextBox.Text.Length > 0)
            {
                Database db = new Database();
                string sql = "INSERT INTO ClassroomStream(userId, classroomId, message, createTime, isTeacher) VALUES(@UserId, @ClassroomId, @Message, @CreateTime, @isTeacher)";

                // Getting the current epoch time
                DateTime currentTime = DateTime.UtcNow;
                DateTimeOffset dateTimeOffset = new DateTimeOffset(currentTime);
                long epochTime = dateTimeOffset.ToUnixTimeSeconds();


                MySqlParameter[] insertAnnouncementParams =
                {
                    new MySqlParameter("@UserId", GlobalVariables.UserId),
                    new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId),
                    new MySqlParameter("@Message", AnnouncementTextBox.Text.ToString()),
                    new MySqlParameter("@CreateTime", epochTime),
                    new MySqlParameter("@isTeacher", GlobalVariables.IsTeacher)
                };

                int result = db.ExecuteNonQuery(sql, insertAnnouncementParams);

                if (result != 0)
                {
                    Announcements newAnnouncement = new Announcements(GlobalVariables.Username, GlobalVariables.IsTeacher, epochTime, AnnouncementTextBox.Text.ToString());
                    newAnnouncement.Margin = new Thickness(0, 5, 0, 0);
                    AnnoucementsPanel.Children.Insert(2,newAnnouncement);
                    AnnouncementTextBox.Text = "";
                }
                else
                {
                    MessageBox.Show("Some error occured");
                }


            }
        }        
    }
}
