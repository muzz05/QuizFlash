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
    public partial class QuizDesignPage : Page
    {
        int quizId, marks, questionCount;
        public QuizDesignPage(int quizId, int marksPerQuestion)
        {
            InitializeComponent();
            this.quizId = quizId;
            marks = marksPerQuestion;
            questionCount = 1;
        }
        
        private void save_question(object sender, RoutedEventArgs e)
        {
            Database database = new Database();           

            foreach (var control in quizDesignPanel.Children)
            {
                if(control is QuizDesignControl quizDesignControl)
                {
                    string question = quizDesignControl.questionTextBox.Text;
                    string option1 = quizDesignControl.optionATextBox.Text;
                    string option2 = quizDesignControl.optionBTextBox.Text;
                    string option3 = quizDesignControl.optionCTextBox.Text;
                    string option4 = quizDesignControl.optionDTextBox.Text;


                    if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(option1) ||
                        string.IsNullOrWhiteSpace(option2) || string.IsNullOrWhiteSpace(option3) || string.IsNullOrWhiteSpace(option4))
                    {
                            CustomMessageBox error = new CustomMessageBox("Empty Field", "You cannot leave any question or options empty", "Error");
                            error.ShowDialog();
                            return;
                    }

                        string sql = "INSERT INTO QuestionAnswers (quizId, question, optionA, optionB, optionC, optionD, correct) " +
                        "VALUES (@quizId,@question, @option1, @option2, @option3, @option4, @correct)";
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

            int questions = quizDesignPanel.Children.Count;
            database.ExecuteNonQuery("Update Quiz Set totalQuestions=@questions, totalMarks=@marks where id=@quizId;", 
                                      new MySqlParameter[] {new MySqlParameter("@questions",questions), 
                                      new MySqlParameter("@quizId",quizId), new MySqlParameter("@marks",marks*questions)});

            foreach(Window window in Application.Current.Windows)
            {
                if(window is Teacher teacher)
                {
                    teacher.TeacherViewFrame.Content= new TeacherClassroomMainPage();
                }
            }
        }
        

        private void add_question(object sender, RoutedEventArgs e)
        {
            questionCount++;
            quizDesignPanel.Children.Add(new QuizDesignControl(questionCount));
        }
    }
}

