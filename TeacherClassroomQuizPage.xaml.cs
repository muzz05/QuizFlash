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
    /// Interaction logic for TeacherClassroomQuizPage.xaml
    /// </summary>
    public partial class TeacherClassroomQuizPage : Page
    {
        public TeacherClassroomQuizPage()
        {
            InitializeComponent();
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
            AddQuiz("Applied Physics", 20, 10, 1718256809);
        }

        public void AddQuiz(string quizname, int totalmarks, int questions, long validUntilEpoch)
        {
            QuizControl newQuiz = new QuizControl(quizname, totalmarks, questions, validUntilEpoch);
            newQuiz.Margin = new Thickness(0, 15, 15, 0);
            TeacherClassroomPanel.Children.Add(newQuiz);

        }

    }
}
