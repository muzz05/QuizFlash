using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizFlash
{
    public partial class Flashcard : UserControl
    {
        public Flashcard(string Title, string Description)
        {
            InitializeComponent();
            FlashCardTitle.Text = Title;
            FlashCardDescription.Text = Description;
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
