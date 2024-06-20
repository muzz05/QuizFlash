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
    /// Interaction logic for TeacherClassroomMainPage.xaml
    /// </summary>
    public partial class TeacherClassroomMainPage : Page
    {

        public TeacherClassroomMainPage()
        {
            InitializeComponent();
            TeacherClassroomFrame.Content = new TeacherClassroomQuizPage();
            DefaultNavigation.IsChecked = true;
        }

        private void NavigateToQuizzes(object sender, RoutedEventArgs e)
        {
            TeacherClassroomFrame.Content = new TeacherClassroomQuizPage();
        }

        private void NavigateToAnnouncements(object sender, RoutedEventArgs e)
        {
            TeacherClassroomFrame.Content = new TeacherClassroomAnnouncementPage();
        }

        private void NavigateToStudentList(object sender, RoutedEventArgs e)
        {
            TeacherClassroomFrame.Content = new TeacherClassroomStudentList();
        }

        private void GoBackToClassroomPage(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Teacher teacher)
                {
                    teacher.TeacherViewFrame.Content = new TeacherClassroomPage(GlobalVariables.TeacherId, GlobalVariables.UserId);
                }
                else if (window is Student student)
                {
                    student.StudentViewFrame.Content = new StudentClassroomPage(GlobalVariables.StudentId, GlobalVariables.UserId);
                }
            }
        }
    }
}
