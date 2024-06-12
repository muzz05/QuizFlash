using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Data;
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

            // Getting the Already made flashcards

            InitializeComponent();


            Database db = new Database();
            string sql = "SELECT * FROM Flashcards WHERE studentId = @StudentId";
            DataTable result = db.ExecuteQuery(sql, new MySqlParameter("@StudentId", StudentId));
            for (int i = 0; i < result.Rows.Count; i++)
            {
                AddFlashCard(result.Rows[i]["title"].ToString(), result.Rows[i]["data"].ToString(), Convert.ToInt32(result.Rows[i]["id"]));
            }


        }

        private void AddFlashCard_Click(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            string sql = "INSERT INTO Flashcards(studentId) VALUES(@StudentId)";
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