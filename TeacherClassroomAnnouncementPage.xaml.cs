using System;
using System.Collections.Generic;
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
            long currentEpochTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            AddAnnouncements("Sophia Brown", false, currentEpochTime - 300, "Can we get extra credit for the group project? Many of us are looking for ways to improve our grades and demonstrate our understanding of the course material. Additional opportunities for extra credit would be greatly appreciated."); // 5 minutes ago
            AddAnnouncements("Emily Davis", false, currentEpochTime - 7200, "There will be a pop quiz on Friday. Make sure to review your notes and the key concepts discussed in the last few lectures. This will help you stay on track and identify any areas that need further clarification."); // 2 hours ago
            AddAnnouncements("Mike Johnson", false, currentEpochTime - 14400, "I have a question about the homework due next week. Could we possibly go over some of the more challenging problems in the next class? I think it would benefit everyone."); // 4 hours ago
            AddAnnouncements("Jane Smith", false, currentEpochTime - 86400, "Please remember to complete your homework before Monday. The assignments are designed to help you better understand the material covered in class. If you have any questions, don't hesitate to reach out."); // 1 day ago
            AddAnnouncements("John Doe", false, currentEpochTime - 172800, "I wanted to remind everyone about the upcoming project deadline. It's crucial that we all contribute equally to ensure a successful outcome. Let's aim to have our drafts ready for peer review by next week."); // 2 days ago
            AddAnnouncements("Dr. Tahir Jamal", true, currentEpochTime - 259200, "Welcome back to school! We have a lot of exciting things planned for this semester. Our focus will be on advancing our understanding of complex topics and ensuring that each student reaches their full potential. Please make sure to review the syllabus and be prepared for our first class discussion."); // 3 days ago
        }

        public void AddAnnouncements(string username, bool isTeacher, long epochDate, string message)
        {
            Announcements newAnnouncement = new Announcements(username, isTeacher, epochDate, message);
            newAnnouncement.Margin = new Thickness(0, 5,0,0);
            AnnoucementsPanel.Children.Add(newAnnouncement);
        }
    }
}
