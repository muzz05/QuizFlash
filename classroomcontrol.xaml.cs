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
    public partial class classroomcontrol : UserControl
    {

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
            new SolidColorBrush(Color.FromRgb(193, 195, 209)),// #C1C3D1 - pale lavender
            new SolidColorBrush(Color.FromRgb(210, 212, 224)),// #D2D4E0 - light silver
            new SolidColorBrush(Color.FromRgb(227, 228, 237))

        };



        public classroomcontrol(string course, string code, string teacher)
        {
            InitializeComponent();
            class_name.Text = course;
            course_name.Text = code;
            teacher_name.Text = teacher;
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


    }
}





