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
    /// Interaction logic for StudentHomepageInfoCard.xaml
    /// </summary>
    public partial class StudentHomepageInfoCard : UserControl
    {

        public int AssociatedClassroomId;

        public StudentHomepageInfoCard(int classroomId, string className, string announcement, long epoch)
        {
            AssociatedClassroomId = classroomId;
            InitializeComponent();
            RecentQuizDateBadge.Text = "Due " + Utilities.ConvertEpochToRelativeTimeFuture(epoch);
            classname.Text = className;
            this.announcement.Text = announcement;
        }

        private void RedirectToQuiz(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ActiveClassroomId = AssociatedClassroomId;

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Teacher teacher)
                {
                    teacher.classroomNavigatorButton.IsChecked = true;
                    teacher.TeacherViewFrame.Content = new TeacherClassroomMainPage();
                }
                else if (window is Student student)
                {
                    student.classroomNavigatorButton.IsChecked = true;
                    student.StudentViewFrame.Content = new TeacherClassroomMainPage();
                }
            }
        }
    }
}


