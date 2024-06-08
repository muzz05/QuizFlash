using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizFlash
{
    public partial class FlashCardPage : Page
    {
        public ObservableCollection<FlashCard> FlashCards { get; set; }

        public FlashCardPage()
        {
            InitializeComponent();
            FlashCards = new ObservableCollection<FlashCard>();
            FlashCardItemsControl.ItemsSource = FlashCards;
        }

        private void AddFlashCard_Click(object sender, RoutedEventArgs e)
        {
            FlashCards.Add(new FlashCard());
        }

        private void flash_card_textblock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.Tag is TextBox textBox)
            {
                textBox.Focus();
            }
        }

        private void flash_card_text_changed(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Tag is TextBlock textBlock)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                    textBlock.Visibility = Visibility.Collapsed;
                else
                    textBlock.Visibility = Visibility.Visible;
            }
        }

        private void flash_card_title_textblock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.Tag is TextBox textBox)
            {
                textBox.Focus();
            }
        }

        private void flash_card_title_text_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Tag is TextBlock textBlock)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                    textBlock.Visibility = Visibility.Collapsed;
                else
                    textBlock.Visibility = Visibility.Visible;
            }
        }
    }

    public class FlashCard
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public FlashCard()
        {
            Title = "Enter Title";
            Description = "Enter description for your Flash-Card";
        }
    }
}