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
using System.Windows.Shapes;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {


        public Teacher()
        {
            InitializeComponent();
        }

        private void StudentTabCheck(object sender, RoutedEventArgs e)
        {
            // This is for Student page Navigation
            TeacherViewFrame.Content = new StudentDataPage();
        }

        private void ClassroomTagCheck(object sender, RoutedEventArgs e)
        {
            // This is for Classroom Navigation
            TeacherViewFrame.Content = new TeacherClassroomPage();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
