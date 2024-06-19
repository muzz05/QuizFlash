using System;
using System.Windows;
using System.Windows.Controls;

namespace QuizFlash
{
    public partial class QuizDetail_Input : Window
    {
        public QuizDetail_Input()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string quizName = quizname.Text;
            string questions = no_ques.Text;
            string totalMarks = totalmarks.Text;
            DateTime? dueDate = duedate.SelectedDate;

            if (string.IsNullOrWhiteSpace(quizName) || string.IsNullOrWhiteSpace(questions) ||
                string.IsNullOrWhiteSpace(totalMarks) || dueDate == null)
            {
                CustomMessageBox msg= new CustomMessageBox("Input Error", "Please fill in all fields.","Error");
                msg.Show();
            }

            CustomMessageBox info= new CustomMessageBox("Quiz Details Saved", $"Quiz Name: {quizName}\nQuestions: {questions}\nTotal Marks: {totalMarks}\nDue Date: {dueDate.Value.ToShortDateString()}"
                            ,"Done");
            info.Show();

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
