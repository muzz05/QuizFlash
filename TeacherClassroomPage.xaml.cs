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
    /// Interaction logic for TeacherClassroomPage.xaml
    /// </summary>
    public partial class TeacherClassroomPage : Page
    {
        public TeacherClassroomPage()
        {
            InitializeComponent();
            AddClassroom("Applied Physics", "PH-122", "Sir Tahir Jamal");
            AddClassroom("Software Engineering", "SE-205", "Miss Sidra");
            AddClassroom("Mathematics", "MTH-101", "Dr. Smith");
            AddClassroom("Computer Science", "CS-301", "Prof. Johnson");
            AddClassroom("Chemistry", "CH-202", "Dr. Lee");
            AddClassroom("History", "HI-110", "Prof. Adams");
            AddClassroom("Literature", "LI-220", "Dr. Brown");


        }

        private void AddClassroom(string coursename, string code, string teacher)
        {

            classroomcontrol newClassroom = new classroomcontrol(coursename, code, teacher);
            // Add the new classroomcontrol instance to the container
            newClassroom.Margin = new Thickness(15, 15, 15, 15);
            classroomContainer.Children.Add(newClassroom);


        }

    }
}


