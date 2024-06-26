using MySql.Data.MySqlClient;
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
    /// Interaction logic for QuizDesignPage.xaml
    /// </summary>
    public partial class QuizDesignPage : Page
    {
        string correct;
        int quizId;
        public QuizDesignPage(int quizId)
        {
            InitializeComponent();
            this.quizId = quizId;
        }

        private void optionAcheck(object sender, RoutedEventArgs e)
        {
            correct = "A";
        }

        private void optionBcheck(object sender, RoutedEventArgs e)
        {
            correct = "B";
        }
        private void optionCcheck(object sender, RoutedEventArgs e)
        {
            correct = "C";
        }
        private void optionDcheck(object sender, RoutedEventArgs e)
        {
            correct = "D";
        }
        private void save_question(object sender, RoutedEventArgs e)
        {
            Database database = new Database();
            string question = questionTextBox.Text;
            string option1 = optionATextBox.Text;
            string option2 = optionBTextBox.Text;
            string option3 = optionCTextBox.Text;
            string option4 = optionDTextBox.Text;

            string sql = "INSERT INTO Questions (quizId, question, option1, option2, option3, option4, correct) VALUES (@quizId,@question, @option1, @option2, @option3, @option4, @correct)";
            MySqlParameter[] parameters = {
                                           new MySqlParameter("@quizId", quizId),    
                                           new MySqlParameter("@question",question), 
                                           new MySqlParameter("@option1", question), 
                                           new MySqlParameter("@option2", question), 
                                           new MySqlParameter("@option3", question),
                                           new MySqlParameter("@option4", question),
                                           new MySqlParameter("@correct", Convert.ToChar(correct)) 
                                           };
            database.ExecuteNonQuery(sql, parameters);
            this.NavigationService.Navigate(new QuizDesignPage(quizId));
        }
    }
}
