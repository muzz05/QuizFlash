using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
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
        private bool isGeneratingAI = false;

        private static readonly Random random = new Random();
        private static readonly Brush[] lightColors = new Brush[]
        {
            new SolidColorBrush(Color.FromRgb(255, 239, 213)),
            new SolidColorBrush(Color.FromRgb(255, 228, 225)),
            new SolidColorBrush(Color.FromRgb(240, 255, 240)),
            new SolidColorBrush(Color.FromRgb(224, 255, 255)),
            new SolidColorBrush(Color.FromRgb(255, 250, 240)),
            new SolidColorBrush(Color.FromRgb(255, 240, 245)),
            new SolidColorBrush(Color.FromRgb(255, 222, 173)),
            new SolidColorBrush(Color.FromRgb(230, 230, 250)),
            new SolidColorBrush(Color.FromRgb(255, 182, 193)),
            new SolidColorBrush(Color.FromRgb(250, 235, 215)),
            new SolidColorBrush(Color.FromRgb(240, 230, 140)),
            new SolidColorBrush(Color.FromRgb(240, 248, 255)),
            new SolidColorBrush(Color.FromRgb(175, 238, 238)),
            new SolidColorBrush(Color.FromRgb(221, 160, 221)),
            new SolidColorBrush(Color.FromRgb(176, 196, 222)),
            new SolidColorBrush(Color.FromRgb(173, 216, 230)),
            new SolidColorBrush(Color.FromRgb(240, 128, 128)),
            new SolidColorBrush(Color.FromRgb(216, 191, 216)),
            new SolidColorBrush(Color.FromRgb(144, 238, 144)),
            new SolidColorBrush(Color.FromRgb(255, 182, 193)),
            new SolidColorBrush(Color.FromRgb(255, 228, 181)),
            new SolidColorBrush(Color.FromRgb(255, 218, 185)),
            new SolidColorBrush(Color.FromRgb(221, 160, 221)),
            new SolidColorBrush(Color.FromRgb(210, 180, 140)),
            new SolidColorBrush(Color.FromRgb(188, 143, 143)),
            new SolidColorBrush(Color.FromRgb(255, 240, 245)),
            new SolidColorBrush(Color.FromRgb(245, 245, 220))
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
            if (!isGeneratingAI)
            {
                UnsavedBadge.Visibility = FlashCardDescriptionBox.Text.ToString() != Description ? Visibility.Visible : Visibility.Hidden;
            }

            if (!string.IsNullOrEmpty(FlashCardDescriptionBox.Text) && FlashCardDescriptionBox.Text.Length > 0)
            {
                FlashCardDescription.Visibility = Visibility.Collapsed;
            }
            else
                FlashCardDescription.Visibility = Visibility.Visible;
        }

        private async void GenerateAIDescription(object sender, RoutedEventArgs e)
        {
            // Check if title is empty
            if (string.IsNullOrWhiteSpace(FlashCardTitleBox.Text))
            {
                //MessageBox.Show("Please enter a title first to generate description.", "Title Required",
                //    MessageBoxButton.OK, MessageBoxImage.Information);
                ShowErrorMessage("Title Required", "Please enter the title first to generate description");
                return;
            }

            // Prevent multiple simultaneous generations
            if (isGeneratingAI)
                return;

            try
            {
                isGeneratingAI = true;

                // Show loader and hide description content
                aiLoader.Visibility = Visibility.Visible;
                DescriptionContent.Visibility = Visibility.Collapsed;
                loadingDescription.Visibility = Visibility.Visible;
                GenerateAIButton.IsEnabled = false;

                // Call your API here - Replace this with your actual API call
                string generatedDescription = await CallAIGenerationAPI(FlashCardTitleBox.Text);

                // Update the description
                FlashCardDescriptionBox.Text = generatedDescription;
                FlashCardDescription.Visibility = Visibility.Collapsed;

                // Show unsaved badge
                UnsavedBadge.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error Occured", "Failed to generate description");
            }
            finally
            {
                // Hide loader and show description content
                aiLoader.Visibility = Visibility.Collapsed;
                DescriptionContent.Visibility = Visibility.Visible;
                loadingDescription.Visibility = Visibility.Collapsed;
                GenerateAIButton.IsEnabled = true;
                isGeneratingAI = false;
            }
        }

        private async Task<string> CallAIGenerationAPI(string title)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                try
                {

                    // Call the API
                    var response = await client.PostAsync($"http://localhost:5001/flashcards/from-title?title={Uri.EscapeDataString(title)}", null);
                    response.EnsureSuccessStatusCode();

                    // Parse the response
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(responseString);

                    // Extract the content from the nested structure
                    var flashcardContent = jsonResponse["flashcard"]["content"].ToString();

                    return flashcardContent ?? string.Empty;
                }
                catch (System.Net.Http.HttpRequestException ex)
                {
                    throw new Exception($"API request failed: {ex.Message}", ex);
                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    throw new Exception($"Failed to parse API response: {ex.Message}", ex);
                }
            }
        }

        private void SaveFlashCard(object sender, RoutedEventArgs e)
        {
            Title = FlashCardTitleBox.Text.ToString();
            Description = FlashCardDescriptionBox.Text.ToString();
            Database db = new Database();
            string sql = "UPDATE Flashcards SET Title = @Title, Data = @Description WHERE id = @FlashCardId";
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Title", Title),
                new MySqlParameter("@Description", Description),
                new MySqlParameter("@FlashCardId", FlashCardId),
            };
            int result = db.ExecuteNonQuery(sql, parameters);
            UnsavedBadge.Visibility = Visibility.Collapsed;
        }

        private void DeleteFlashCard(object sender, RoutedEventArgs e)
        {
            Database db = new Database();
            string sql = "DELETE FROM Flashcards WHERE id = @FlashCardId";
            int deletingFlashCard = db.ExecuteNonQuery(sql, new MySqlParameter("@FlashCardId", FlashCardId));
            if (Parent is Panel panelWrapPanel)
            {
                panelWrapPanel.Children.Remove(this);
            }
        }

        private void ShareFlashCard(object sender, RoutedEventArgs e)
        {
            FlashCardSharePopup Share = new FlashCardSharePopup(FlashCardId);
            Share.Show();
        }

        private void ShowErrorMessage(string title, string message)
        {
            CustomMessageBox errorMsg = new CustomMessageBox(title, message, "Error");
            errorMsg.Show();
        }
    }
}