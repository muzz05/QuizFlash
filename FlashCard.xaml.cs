using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuizFlash
{
    public partial class Flashcard : UserControl
    {
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

        public Flashcard(string Title, string Description)
        {
            InitializeComponent();
            SetRandomBackgroundColor();
            FlashCardTitle.Text = Title;
            FlashCardDescription.Text = Description;
        }


        private void SetRandomBackgroundColor()
        {
            int index = random.Next(lightColors.Length);
            border_of_flashcard.Background = lightColors[index];
            
        }

        private void FlashCardTitleTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var textBox = textBlock?.Tag as TextBox;
            if (textBox != null)
            {
                textBox.Visibility = Visibility.Visible;
                textBox.Focus();
            }
        }

        private void FlashCardTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var textBlock = textBox?.Tag as TextBlock;
            if (textBlock != null)
            {
                textBlock.Text = textBox.Text;
                textBox.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void FlashCardTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var textBox = textBlock?.Tag as TextBox;
            if (textBox != null)
            {
                textBox.Visibility = Visibility.Visible;
                textBox.Focus();
            }
        }

        private void FlashCardTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var textBlock = textBox?.Tag as TextBlock;
            if (textBlock != null)
            {
                textBlock.Text = textBox.Text;
                textBox.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
