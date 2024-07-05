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
        int quizId;
        public QuizDesignPage(int quizId)
        {
            InitializeComponent();
            this.quizId = quizId;
        }
        
        private void save_question(object sender, RoutedEventArgs e)
        {
            Database database = new Database();

            foreach(var control in quizDesignPanel.Children)
            {
                if(control is QuizDesignControl quizDesignControl)
                {
                    string question = quizDesignControl.questionTextBox.Text;
                    string option1 = quizDesignControl.optionATextBox.Text;
                    string option2 = quizDesignControl.optionBTextBox.Text;
                    string option3 = quizDesignControl.optionCTextBox.Text;
                    string option4 = quizDesignControl.optionDTextBox.Text;

                    string sql = "INSERT INTO QuestionAnswers (quizId, question, optionA, optionB, optionC, optionD, correct) VALUES (@quizId,@question, @option1, @option2, @option3, @option4, @correct)";
                    MySqlParameter[] parameters = {
                                                   new MySqlParameter("@quizId", quizId),    
                                                   new MySqlParameter("@question",question), 
                                                   new MySqlParameter("@option1", option1), 
                                                   new MySqlParameter("@option2", option2), 
                                                   new MySqlParameter("@option3", option3),
                                                   new MySqlParameter("@option4", option4),
                                                   new MySqlParameter("@correct", quizDesignControl.correct) 
                                                   };

                        database.ExecuteNonQuery(sql, parameters);
                }
            }

            foreach(Window window in Application.Current.Windows)
            {
                if(window is Teacher teacher)
                {
                    teacher.TeacherViewFrame.Content= new TeacherClassroomQuizPage();
                }
            }
        }
    }
}
