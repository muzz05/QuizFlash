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
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : UserControl
    {
        public UserInfo(string name,string isStudent,int classNo,int flashcards,int totalQuiz,int successRate)
        {
            InitializeComponent();
            studentName.Text = name;
            isStudentorTeacher.Text=isStudent;
            classroomNo.Text = classNo.ToString();
            flashcardNo.Text = flashcards.ToString();
            quizAttempted.Text = totalQuiz.ToString();
            quizProgress.Value= successRate;

        }
    }
}
