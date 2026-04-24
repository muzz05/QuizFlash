using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
            playSimpleSound();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard scaleDownStoryboard = (Storyboard)this.Resources["ScaleDownAnimation"];
            scaleDownStoryboard.Begin();
        }


        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "soundeffect.wav"));
            simpleSound.Play();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShareFlashCard(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            string sql = "SELECT * FROM Students WHERE studentCode = @StudentCode";

            DataTable CheckingStudent = db.ExecuteQuery(sql, new MySqlParameter("@StudentCode", studentcodeflashcard.Text.ToString().ToUpper()));
            if(CheckingStudent.Rows.Count == 0)
            {
                CustomMessageBox error = new CustomMessageBox("Incorrect Code", "Please enter the correct student code", "Error");
                error.Show();
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
                if(result != null)
                {
                    this.Close();
                    CustomMessageBox success = new CustomMessageBox("Share Success", "Flashcard has been shared successfully", "Success");
                    success.Show();
                }
            }

        }
    }
}
