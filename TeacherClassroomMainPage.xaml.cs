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
    }
}
