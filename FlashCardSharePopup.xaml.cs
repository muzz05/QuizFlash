using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for FlashCardSharePopup.xaml
    /// </summary>
    public partial class FlashCardSharePopup : Window
    {
        public int FlashCardId;

        public FlashCardSharePopup(int flashCardId)
        {
            FlashCardId = flashCardId;
            InitializeComponent();
        }

        private void StudentCodeChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StudentCode_flashCardTextBox.Text) && StudentCode_flashCardTextBox.Text.Length > 0)
                StudentCode_flashCardTextBlock.Visibility = Visibility.Collapsed;
            else
                StudentCode_flashCardTextBlock.Visibility = Visibility.Visible;
        }

        private void MouseDownTextBoxShare(object sender, MouseButtonEventArgs e)
        {
            StudentCode_flashCardTextBox.Focus();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void  ShareFlashCard(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            string sql = "SELECT * FROM Students WHERE studentCode = @StudentCode";

            DataTable CheckingStudent = db.ExecuteQuery(sql, new MySqlParameter("@StudentCode", StudentCode_flashCardTextBox.Text.ToString()));
            if(CheckingStudent.Rows.Count == 0)
            {
                alertMessage.Text = "Please Enter the correct student code";
                await Task.Delay(4000);
                alertMessage.Text = "";
            }
            else
            {
                sql = "INSERT INTO Flashcards (title, data, studentId) SELECT title, data, @StudentId FROM Flashcards WHERE id = @FLashCardId";
                MySqlParameter[] AddingFlashcardParameter =
                {
                    new MySqlParameter("@StudentId", Convert.ToInt32(CheckingStudent.Rows[0]["id"])),
                    new MySqlParameter("@FlashCardId", FlashCardId)
                };
                object result = db.ExecuteNonQuery(sql, AddingFlashcardParameter);
                this.Close();
            }

        }
    }
}
