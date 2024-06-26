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
    /// Interaction logic for QuizDisplayControl.xaml
    /// </summary>
    public partial class QuizDisplayControl : UserControl
    {
        public int response, correct;
        public QuizDisplayControl(string question, string option1, string option2, string option3, string option4, int answer)
        {
            InitializeComponent();
            question_text.Text = question;
            option_1.Content = option1;
            option_2.Content = option2;
            option_3.Content = option3;
            option_4.Content = option4;
            correct = answer;
        }        
        private void option_1_Checked(object sender, RoutedEventArgs e)
        {
            response = 1;
        }

        private void option_2_Checked(object sender, RoutedEventArgs e)
        {
            response = 2;
        }
        private void option_3_Checked(object sender, RoutedEventArgs e)
        {
            response = 3;
        }
        private void option_4_Checked(object sender, RoutedEventArgs e)
        {
            response = 4;
        }       
    }
}
