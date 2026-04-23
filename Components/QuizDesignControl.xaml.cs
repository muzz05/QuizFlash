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
    /// Interaction logic for QuizDesignControl.xaml
    /// </summary>
    public partial class QuizDesignControl : UserControl
    {
        public int correct=0;
        public event EventHandler DeleteRequested;
        public QuizDesignControl(int questionN)
        {
            InitializeComponent();
            ques_Number.Text = questionN.ToString();
        }
        private void optionAcheck(object sender, RoutedEventArgs e)
        {
            correct = 0;
        }

        private void optionBcheck(object sender, RoutedEventArgs e)
        {
            correct = 1;
        }
        private void optionCcheck(object sender, RoutedEventArgs e)
        {
            correct = 2;
        }
        private void optionDcheck(object sender, RoutedEventArgs e)
        {
            correct = 3;
        }
        private void deleteQuestion(object sender, RoutedEventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
