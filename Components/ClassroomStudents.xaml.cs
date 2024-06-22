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

namespace QuizFlash.Components
{
    /// <summary>
    /// Interaction logic for ClassroomStudents.xaml
    /// </summary>
    public partial class ClassroomStudents : UserControl
    {
        public ClassroomStudents(string username, bool isUserTeacher)
        {
            InitializeComponent();
            UserBadgeText.Text = Utilities.GetInitials(username);
            UserNameText.Text = username;
            isTeacherBadge.Visibility = isUserTeacher ? Visibility.Visible : Visibility.Collapsed;
            if(isUserTeacher || !GlobalVariables.IsTeacher)
            {
                removeStudent.Visibility = Visibility.Collapsed;
            }
            else
            {
                removeStudent.Visibility = Visibility.Visible;
            }
        }
    }
}
