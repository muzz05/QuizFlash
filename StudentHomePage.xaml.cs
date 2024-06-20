using QuizFlash;
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
    /// Interaction logic for StudentHomePage.xaml
    /// </summary>
    public partial class StudentHomePage : Page
    {
        public StudentHomePage()
        { 
            InitializeComponent();
            var cards = new[]
            {
                    new StudentHomepageInfoCard("Software Engineering", "Grand Quiz on 7/9/24"),
                    new StudentHomepageInfoCard("Applied Physics", "Chapter 1 Test on 9/6/24"),
                    new StudentHomepageInfoCard("Mathematics", "Midterm Exam on 8/15/24"),
                    new StudentHomepageInfoCard("History", "Presentation on 9/1/24"),
                    new StudentHomepageInfoCard("Software Engineering", "Final Exam on 9/1/24")

            };

            foreach (var card in cards)
            {
                card.Margin = new Thickness(8);
                infocards.Children.Add(card);
            }

        }
    }
}


//private void AddClassroom(string coursename, string code, string teacher, int count, string gcr_code, int classroomId)
//{

//    Classroom newClassroom = new Classroom(coursename, code, teacher, count, gcr_code, false, classroomId);
//    newClassroom.Margin = new Thickness(0, 0, 15, 15);
//    WrapPanelClassroom.Children.Add(newClassroom);

//}