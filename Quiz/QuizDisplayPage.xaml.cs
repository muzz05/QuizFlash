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
    /// Interaction logic for QuizDisplayPage.xaml
    /// </summary>
    public partial class QuizDisplayPage : Page
    {
        char response, correct;
        Database database = new Database();
        public QuizDisplayPage()
        {
            InitializeComponent();
        }

        public void DisplayQuestion(string id,string question, string option1, string option2, string option3, string option4, char answer)
        {
            quizTitle.Text = $"Quiz {id}";
            question_text.Text = question;
            option_1.Content = option1;
            option_2.Content = option2;
            option_3.Content = option3;
            option_4.Content = option4;
            correct = answer;
        }
        private void option_1_Checked(object sender, RoutedEventArgs e)
        {
            response = 'A';
        }

        private void option_2_Checked(object sender, RoutedEventArgs e)
        {
            response = 'B';
        }
        private void option_3_Checked(object sender, RoutedEventArgs e)
        {
            response ='C';
        }
        private void option_4_Checked(object sender, RoutedEventArgs e)
        {
            response = 'D';
        }

        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            string query = $"Insert into studentresponse (quizId, questionId, studentId, isCorrect, checked) values({Convert.ToInt32(quizTitle.Text.Substring(5))},{Convert.ToInt32(question_text.Text[0])},{GlobalVariables.StudentId},{response==correct},{true})";
            database.ExecuteNonQuery(query);
        }
    }
}
