using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizFlash
{
    public partial class FlashCardPage : Page
    {

        private int StudentId;
        private int UserId;

        public FlashCardPage(int _studentId, int _userId )
        {
            this.StudentId = _studentId;
            this.UserId = _userId;

            InitializeComponent();
        }

        private void AddFlashCard_Click(object sender, RoutedEventArgs e)
        {
            AddFlashCard("", "", 1);
        }

        private void AddFlashCard(string title, string description, int id)
        {
            int index = WrapPanelFlashCards.Children.Count - 1;
            Flashcard newFlashCard = new Flashcard(title, description, id);
            newFlashCard.Margin = new Thickness(0, 0, 15, 15);
            WrapPanelFlashCards.Children.Insert(index, newFlashCard);
        }
    }
}