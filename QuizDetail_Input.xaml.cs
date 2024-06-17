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
            // Retrieve values from the form
            string quizName = quizname.Text;
            string questions = no_ques.Text;
            string totalMarks = totalmarks.Text;
            DateTime? dueDate = duedate.SelectedDate;

            // Validate inputs (you can add more comprehensive validation)
            if (string.IsNullOrWhiteSpace(quizName) || string.IsNullOrWhiteSpace(questions) ||
                string.IsNullOrWhiteSpace(totalMarks) || dueDate == null)
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Process the data (e.g., save to database, file, etc.)
            // For demonstration, we'll just show a message box
            MessageBox.Show($"Quiz Name: {quizName}\nQuestions: {questions}\nTotal Marks: {totalMarks}\nDue Date: {dueDate.Value.ToShortDateString()}",
                            "Quiz Details Saved", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close the window after saving
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving
            this.Close();
        }
    }
}
