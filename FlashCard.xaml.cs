using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuizFlash
{
    public partial class Flashcard : UserControl
    {
        private int FlashCardId;
        private string Title;
        private string Description;

        private static readonly Random random = new Random();
        private static readonly Brush[] lightColors = new Brush[]
        {
            new SolidColorBrush(Color.FromRgb(255, 239, 213)),
            new SolidColorBrush(Color.FromRgb(255, 228, 225)), // Light pink
            new SolidColorBrush(Color.FromRgb(240, 255, 240)), // Light green
            new SolidColorBrush(Color.FromRgb(224, 255, 255)), // Light cyan
            new SolidColorBrush(Color.FromRgb(255, 250, 240)), // Light cream
            new SolidColorBrush(Color.FromRgb(255, 240, 245)), // Light lavender
            new SolidColorBrush(Color.FromRgb(255, 222, 173)), // Navajo white
            new SolidColorBrush(Color.FromRgb(230, 230, 250)), // Lavender
            new SolidColorBrush(Color.FromRgb(255, 182, 193)), // Light coral
            new SolidColorBrush(Color.FromRgb(250, 235, 215)), // Antique white
            new SolidColorBrush(Color.FromRgb(240, 230, 140)), // Khaki
            new SolidColorBrush(Color.FromRgb(240, 248, 255)), // Alice blue
            new SolidColorBrush(Color.FromRgb(175, 238, 238)), // Pale turquoise
            new SolidColorBrush(Color.FromRgb(221, 160, 221)), // Plum
            new SolidColorBrush(Color.FromRgb(176, 196, 222)), // Light steel blue
            new SolidColorBrush(Color.FromRgb(173, 216, 230)), // Light blue
            new SolidColorBrush(Color.FromRgb(240, 128, 128)), // Light salmon
            new SolidColorBrush(Color.FromRgb(216, 191, 216)), // Thistle
            new SolidColorBrush(Color.FromRgb(144, 238, 144)), // Light green
            new SolidColorBrush(Color.FromRgb(255, 182, 193)), // Light pink
            new SolidColorBrush(Color.FromRgb(255, 228, 181)), // Moccasin
            new SolidColorBrush(Color.FromRgb(255, 218, 185)), // Peach puff
            new SolidColorBrush(Color.FromRgb(221, 160, 221)), // Plum
            new SolidColorBrush(Color.FromRgb(210, 180, 140)), // Tan
            new SolidColorBrush(Color.FromRgb(188, 143, 143)), // Rosy brown
            new SolidColorBrush(Color.FromRgb(255, 240, 245)), // Lavender blush
            new SolidColorBrush(Color.FromRgb(245, 245, 220))  // Light lavender
        };

        public Flashcard(string _Title, string _Description, int Id)
        {
            Title = _Title;
            Description = _Description;
            InitializeComponent();
            SetRandomBackgroundColor();
            FlashCardTitleBox.Text = _Title;
            FlashCardDescriptionBox.Text = _Description;
            FlashCardId = Id;
        }


        private void SetRandomBackgroundColor()
        {
            int index = random.Next(lightColors.Length);
            border_of_flashcard.Background = lightColors[index];
            
        }

        private void FlashCardTitleTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FlashCardTitle.Text = "";
            FlashCardTitleBox.Focus();
        }

        private void FlashCardTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UnsavedBadge.Visibility = FlashCardTitleBox.Text.ToString() != Title ? Visibility.Visible : Visibility.Hidden;
            if (!string.IsNullOrEmpty(FlashCardTitleBox.Text) && FlashCardTitleBox.Text.Length > 0)
            {
                FlashCardTitle.Visibility = Visibility.Collapsed;         
            }
            else
                FlashCardTitle.Visibility = Visibility.Visible;
        }

        private void FlashCardTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FlashCardDescription.Text = "";
            FlashCardDescriptionBox.Focus();
        }

        private void FlashCardTextChanged(object sender, TextChangedEventArgs e)
        {
            UnsavedBadge.Visibility = FlashCardDescriptionBox.Text.ToString() != Description ? Visibility.Visible : Visibility.Hidden;
            if (!string.IsNullOrEmpty(FlashCardDescriptionBox.Text) && FlashCardDescriptionBox.Text.Length > 0)
            {
                FlashCardDescription.Visibility = Visibility.Collapsed;
            }
            else
                FlashCardDescription.Visibility = Visibility.Visible;
        }

        private void SaveFlashCard(object sender, RoutedEventArgs e)
        {
            Title = FlashCardTitleBox.Text.ToString();
            Description = FlashCardDescriptionBox.Text.ToString();
            Database db = new Database();
            string sql = "UPDATE Flashcards SET Title = @Title, Data = @Description WHERE id = @FlashCardId";
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Title",Title ),
                new MySqlParameter("@Description",Description ),
                new MySqlParameter("@FlashCardId",FlashCardId ),
            };
            int result = db.ExecuteNonQuery(sql, parameters);
            UnsavedBadge.Visibility = Visibility.Collapsed;
        }

        private void DeleteFlashCard(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            string sql = "DELETE FROM Flashcards WHERE id = @FlashCardId";
            int deletingFlashCard = db.ExecuteNonQuery(sql, new MySqlParameter("@FlashCardId", FlashCardId));
            if(Parent is Panel panelWrapPanel)
            {
                panelWrapPanel.Children.Remove(this);
            }
        }

        private void ShareFlashCard(object sender, RoutedEventArgs e)
        {

        }
    }
}
