using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace QuizFlash
{
    public partial class FlashCardPage : Page
    {
        private readonly int StudentId;
        private readonly int UserId;
        private bool isLoading;
        private CancellationTokenSource cancellationTokenSource;
        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
        private const string API_BASE_URL = "http://localhost:5001";

        public FlashCardPage(int _studentId, int _userId)
        {
            StudentId = _studentId;
            UserId = _userId;
            InitializeComponent();
            Loaded += FlashCardPage_Loaded;
        }

        private async void FlashCardPage_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeFlashcardsAsync();
        }

        private async Task InitializeFlashcardsAsync()
        {
            SetLoadingState(true, "Loading flashcards...", false);
            try
            {
                await LoadFlashcardsFromDatabaseAsync();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error", "Failed to load flashcards");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool loading, string message = "Loading...", bool isAiGeneration = false)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            FlashcardGrid.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
            loadingText.Text = message;

            if (loading)
            {
                regularLoader.Visibility = isAiGeneration ? Visibility.Collapsed : Visibility.Visible;
                aiLoader.Visibility = isAiGeneration ? Visibility.Visible : Visibility.Collapsed;
                cancelButton.Visibility = isAiGeneration ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private async Task LoadFlashcardsFromDatabaseAsync()
        {
            Database db = new Database();
            const string sql = "SELECT id, title, data FROM Flashcards WHERE studentId = @StudentId ORDER BY id DESC";

            DataTable result = await Task.Run(() =>
                db.ExecuteQuery(sql, new MySqlParameter("@StudentId", StudentId))
            );

            if (result?.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    AddFlashCard(
                        row["title"]?.ToString() ?? string.Empty,
                        row["data"]?.ToString() ?? string.Empty,
                        Convert.ToInt32(row["id"])
                    );
                }
            }
        }

        private async void AddFlashCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int newCardId = await CreateEmptyFlashcardAsync();
                AddFlashCardNew(string.Empty, string.Empty, newCardId);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error", "Failed to create flashcard");
            }
        }

        private async Task<int> CreateEmptyFlashcardAsync()
        {
            Database db = new Database();
            const string insertSql = "INSERT INTO Flashcards(studentId, data, title) VALUES(@StudentId, '', '')";

            await Task.Run(() =>
                db.ExecuteNonQuery(insertSql, new MySqlParameter("@StudentId", StudentId))
            );

            const string selectSql = "SELECT MAX(id) AS MaxId FROM Flashcards WHERE studentId = @StudentId";
            object id = await Task.Run(() =>
                db.ExecuteScalar(selectSql, new MySqlParameter("@StudentId", StudentId))
            );

            return Convert.ToInt32(id);
        }

        private async void UploadPdf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Documents|*.pdf;*.doc;*.docx|PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx",
                Title = "Select a document to convert to flashcards"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                await ProcessDocumentWithAIAsync(openFileDialog.FileName);
            }
        }

        private async Task ProcessDocumentWithAIAsync(string filePath)
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                SetLoadingState(true, "AI is generating flashcards from your document...", true);

                byte[] fileBytes = await Task.Run(() => File.ReadAllBytes(filePath), cancellationTokenSource.Token);
                string fileName = Path.GetFileName(filePath);

                var flashcards = await SendDocumentToAPIAsync(fileBytes, fileName, cancellationTokenSource.Token);

                if (flashcards.Count > 0)
                {
                    await SaveGeneratedFlashcardsAsync(flashcards, cancellationTokenSource.Token);
                }
                else
                {
                    ShowErrorMessage("Warning", "No flashcards were generated from the document");
                }
            }
            catch (OperationCanceledException)
            {
                ShowInfoMessage("Cancelled", "Flashcard generation was cancelled");
            }
            catch (HttpRequestException)
            {
                ShowErrorMessage("Connection Error", "Failed to connect to AI service. Please ensure the server is running.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error", "An error occurred while processing the document");
            }
            finally
            {
                SetLoadingState(false);
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private async Task<List<FlashcardData>> SendDocumentToAPIAsync(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
        {
            using (var formData = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                formData.Add(fileContent, "file", fileName);
                formData.Add(new StringContent(StudentId.ToString()), "student_id");

                var response = await httpClient.PostAsync(
                    $"{API_BASE_URL}/flashcards/from-file",
                    formData,
                    cancellationToken
                );

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return ParseFlashcardsFromJson(jsonResponse);
                }
                else
                {
                    throw new HttpRequestException($"API returned status code: {response.StatusCode}");
                }
            }
        }

        private void CancelGeneration_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource?.Cancel();
            cancelButton.IsEnabled = false;
        }

        private List<FlashcardData> ParseFlashcardsFromJson(string json)
        {
            var flashcards = new List<FlashcardData>();

            try
            {
                int flashcardsStart = json.IndexOf("\"flashcards\"", StringComparison.Ordinal);
                if (flashcardsStart == -1) return flashcards;

                int arrayStart = json.IndexOf("[", flashcardsStart, StringComparison.Ordinal);
                int arrayEnd = json.LastIndexOf("]", StringComparison.Ordinal);

                if (arrayStart == -1 || arrayEnd == -1 || arrayEnd <= arrayStart)
                    return flashcards;

                string arrayContent = json.Substring(arrayStart + 1, arrayEnd - arrayStart - 1);
                var matches = Regex.Matches(arrayContent, @"\{[^\}]+\}");

                foreach (Match match in matches)
                {
                    var flashcard = ParseSingleFlashcard(match.Value);
                    if (flashcard != null)
                    {
                        flashcards.Add(flashcard);
                    }
                }
            }
            catch (Exception)
            {
                ShowErrorMessage("Parse Error", "Failed to parse AI response");
            }

            return flashcards;
        }

        private FlashcardData ParseSingleFlashcard(string jsonObject)
        {
            var titleMatch = Regex.Match(jsonObject, @"""title""\s*:\s*""([^""]*)""");
            var contentMatch = Regex.Match(jsonObject, @"""content""\s*:\s*""([^""]*)""");

            if (!titleMatch.Success && !contentMatch.Success)
                return null;

            return new FlashcardData
            {
                title = UnescapeJsonString(titleMatch.Success ? titleMatch.Groups[1].Value : string.Empty),
                description = UnescapeJsonString(contentMatch.Success ? contentMatch.Groups[1].Value : string.Empty)
            };
        }

        private string UnescapeJsonString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.Replace("\\n", "\n")
                     .Replace("\\r", "\r")
                     .Replace("\\t", "\t")
                     .Replace("\\\"", "\"")
                     .Replace("\\\\", "\\");
        }

        //private async Task SaveGeneratedFlashcardsAsync(List<FlashcardData> flashcards, CancellationToken cancellationToken)
        //{
        //    Database db = new Database();
        //    const string insertSql = "INSERT INTO Flashcards(studentId, data, title) VALUES(@StudentId, @Data, @Title)";
        //    const string selectSql = "SELECT MAX(id) AS MaxId FROM Flashcards WHERE studentId = @StudentId";

        //    foreach (var flashcard in flashcards)
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();

        //        await Task.Run(() => db.ExecuteNonQuery(insertSql,
        //            new MySqlParameter("@StudentId", StudentId),
        //            new MySqlParameter("@Data", flashcard.description ?? string.Empty),
        //            new MySqlParameter("@Title", flashcard.title ?? string.Empty)
        //        ), cancellationToken);

        //        object id = await Task.Run(() =>
        //            db.ExecuteScalar(selectSql, new MySqlParameter("@StudentId", StudentId)),
        //            cancellationToken
        //        );

        //        await Application.Current.Dispatcher.InvokeAsync(() =>
        //        {
        //            AddAIFlashCard(flashcard.title, flashcard.description, Convert.ToInt32(id));
        //        });
        //    }
        //}

        private async Task SaveGeneratedFlashcardsAsync(List<FlashcardData> flashcards, CancellationToken cancellationToken)
        {
            Database db = new Database();
            const string insertSqlTemplate = "INSERT INTO Flashcards(studentId, data, title) VALUES ";
            const string selectSql = "SELECT id, title, data FROM Flashcards WHERE studentId = @StudentId ORDER BY id DESC LIMIT @Count";

            if (flashcards == null || flashcards.Count == 0)
                return;

            var parameters = new List<MySqlParameter>();
            var valuesList = new List<string>();

            for (int i = 0; i < flashcards.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string dataParam = $"@Data{i}";
                string titleParam = $"@Title{i}";
                string studentParam = $"@StudentId";

                valuesList.Add($"({studentParam}, {dataParam}, {titleParam})");

                parameters.Add(new MySqlParameter(dataParam, flashcards[i].description ?? string.Empty));
                parameters.Add(new MySqlParameter(titleParam, flashcards[i].title ?? string.Empty));
            }

            parameters.Add(new MySqlParameter("@StudentId", StudentId));

            string insertSql = insertSqlTemplate + string.Join(", ", valuesList);

            await Task.Run(() => db.ExecuteNonQuery(insertSql, parameters.ToArray()), cancellationToken);

            object result = await Task.Run(() =>
                db.ExecuteQuery(selectSql,
                    new MySqlParameter("@StudentId", StudentId),
                    new MySqlParameter("@Count", flashcards.Count)
                ),
                cancellationToken
            );

            if (result is DataTable dt)
            {
                var rows = dt.AsEnumerable();

                foreach (var row in rows)
                {
                    var id = Convert.ToInt32(row["id"]);
                    var title = row["title"].ToString();
                    var data = row["data"].ToString();

                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        AddAIFlashCard(title, data, id);
                    });
                }
            }
        }


        private void AddFlashCard(string title, string description, int id)
        {
            int index = Math.Max(0, WrapPanelFlashCards.Children.Count);
            Flashcard newFlashCard = new Flashcard(title, description, id)
            {
                Margin = new Thickness(0, 0, 15, 15)
            };
            WrapPanelFlashCards.Children.Insert(index, newFlashCard);
        }

        private void AddFlashCardNew(string title, string description, int id)
        {
            Flashcard newFlashCard = new Flashcard(title, description, id)
            {
                Margin = new Thickness(0, 0, 15, 15)
            };
            WrapPanelFlashCards.Children.Insert(2, newFlashCard);
        }

        private void AddAIFlashCard(string title, string description, int id)
        {
            Flashcard newFlashCard = new Flashcard(title, description, id)
            {
                Margin = new Thickness(0, 0, 15, 15)
            };
            WrapPanelFlashCards.Children.Insert(2, newFlashCard);
        }

        private void ShowErrorMessage(string title, string message)
        {
            CustomMessageBox errorMsg = new CustomMessageBox(title, message, "Error");
            errorMsg.Show();
        }


        private void ShowInfoMessage(string title, string message)
        {
            CustomMessageBox infoMsg = new CustomMessageBox(title, message, "Info");
            infoMsg.Show();
        }
    }

    public class FlashcardData
    {
        public string title { get; set; }
        public string description { get; set; }
    }
}