using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizFlash
{
    public partial class FlashCardPage : Page
    {

        public FlashCardPage()
        {
            InitializeComponent();
        }

        private void AddFlashCard_Click(object sender, RoutedEventArgs e)
        {
            int index = WrapPanelFlashCards.Children.Count - 1;
            Flashcard newFlashCard = new Flashcard("Enter The Title", "Enter the Description");
            newFlashCard.Margin = new Thickness(0,0,15,15);
            WrapPanelFlashCards.Children.Insert(index, newFlashCard);
        }
    }
}