using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizFlash
{
    public partial class FlashCardPage : Page
    {

        private int StudentId;
        private int UserId;
        private bool isLoading;

        public FlashCardPage(int _studentId, int _userId )
        {
            this.StudentId = _studentId;
            this.UserId = _userId;

            InitializeComponent();

            Loaded += FlashCardPage_Loaded;

        }

        private async void FlashCardPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetLoadingState(true);
            await LoadAsyncData();
            SetLoadingState(false);
        }

        private void SetLoadingState(bool loading)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            FlashcardGrid.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
        }

        private async Task LoadAsyncData()
        {
            Database db = new Database();
            string sql = "SELECT * FROM Flashcards WHERE studentId = @StudentId";
            DataTable result = await Task.Run(() => db.ExecuteQuery(sql, new MySqlParameter("@StudentId", StudentId)));
            for (int i = 0; i < result.Rows.Count; i++)
            {
                AddFlashCard(result.Rows[i]["title"].ToString(), result.Rows[i]["data"].ToString(), Convert.ToInt32(result.Rows[i]["id"]));
            }
        }

        private void AddFlashCard_Click(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            string sql = "INSERT INTO Flashcards(studentId, data, title) VALUES(@StudentId, '', '')";
            int result = db.ExecuteNonQuery(sql, new MySqlParameter("@StudentId", StudentId));
            sql = "SELECT MAX(id) AS MaxId FROM Flashcards WHERE studentId = @StudentId";
            object id = db.ExecuteScalar(sql, new MySqlParameter("@StudentId", StudentId));
            AddFlashCard("", "", Convert.ToInt32(id));
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