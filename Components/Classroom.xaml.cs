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
    /// Interaction logic for classroomcontrol.xaml
    /// </summary>
    public partial class Classroom : UserControl
    {

        private int ClassroomId;
        private bool isTeacher;

        private static readonly Random random = new Random();
        private static readonly Brush[] suttleColors = new Brush[]
        {
            new SolidColorBrush(Color.FromRgb(74, 72, 87)),   // #4A4857 - a lighter shade of the background
            new SolidColorBrush(Color.FromRgb(88, 80, 109)),  // #58506D - soft lavender
            new SolidColorBrush(Color.FromRgb(100, 103, 120)),// #646778 - muted blue-gray
            new SolidColorBrush(Color.FromRgb(115, 112, 115)),// #737073 - neutral gray
            new SolidColorBrush(Color.FromRgb(140, 136, 163)),// #8C88A3 - soft violet
            new SolidColorBrush(Color.FromRgb(164, 157, 191)),// #A49DBF - pastel purple
            new SolidColorBrush(Color.FromRgb(176, 176, 195)),// #B0B0C3 - light grayish blue
            new SolidColorBrush(Color.FromRgb(210, 212, 224)),// #D2D4E0 - light silver
            new SolidColorBrush(Color.FromRgb(227, 228, 237)),// #E3E4ED - light pastel blue

            // Added more subtle dark colors
            new SolidColorBrush(Color.FromRgb(54, 69, 79)),   // #36454F - dark slate gray
            new SolidColorBrush(Color.FromRgb(79, 93, 117)),  // #4F5D75 - dark blue-gray
            new SolidColorBrush(Color.FromRgb(58, 58, 58)),   // #3A3A3A - dark charcoal gray
            new SolidColorBrush(Color.FromRgb(102, 102, 102)),// #666666 - medium dark gray
            new SolidColorBrush(Color.FromRgb(81, 92, 107)),  // #515C6B - deep muted blue
            new SolidColorBrush(Color.FromRgb(110, 123, 139)),// #6E7B8B - cool grayish blue
            new SolidColorBrush(Color.FromRgb(93, 107, 153)), // #5D6B99 - dark desaturated blue
            new SolidColorBrush(Color.FromRgb(70, 82, 91))   // #46525B - dark muted blue-gray
        };



        public Classroom(string course, string code, string teacher,int class_no,string class_gc_code, bool is_teacher, int classroomId)
        {
            isTeacher = is_teacher;
            ClassroomId = classroomId;

            InitializeComponent();
            class_name.Text = course;
            course_name.Text = code;
            teacher_name.Text = teacher;
            classmembers_data.Text = class_no.ToString();
            classcode_data.Text = class_gc_code;
            string[] parts = code.Split('-');
            string result = parts[0].ToUpper();
            ellipse_tb.Text=result;
            SetRandomBackgroundColor();

        }


        private void SetRandomBackgroundColor()
        {
            int index = random.Next(suttleColors.Length);
            ellipse.Fill= suttleColors[index];

        }

        private void RedirectToClassroom(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ActiveClassroomId = ClassroomId;

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Teacher teacher)
                    {
                        teacher.TeacherViewFrame.Content = new TeacherClassroomMainPage();
                    }
                    else if (window is Student student) {
                        student.StudentViewFrame.Content = new TeacherClassroomMainPage();
                    }
                }
        }
    }
}





