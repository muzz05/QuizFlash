using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Media;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Input;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace QuizFlash
{
    public partial class QuizDetail_Input : Window
    {
        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
        private CancellationTokenSource cancellationTokenSource;
        private bool isLoading;
        private const string API_BASE_URL = "http://localhost:5001";
        public QuizDetail_Input()
        {
            InitializeComponent();
            playSimpleSound();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard scaleDownStoryboard = (Storyboard)this.Resources["ScaleDownAnimation"];
            scaleDownStoryboard.Begin();
        }

        


        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "soundeffect.wav"));
            simpleSound.Play();
        }

        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Popup popup = button?.Template?.FindName("PART_Popup", button) as Popup;
            if (popup != null)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string quizName = quizname.Text;
            string perQmarks = permarks.Text;
            DateTime? startdate = startDate.SelectedDate;
            int marksPerQuestion=Int32.Parse(perQmarks);
            long time = Utilities.GetCurrentTimeInEpoch();
            int tid=GlobalVariables.TeacherId;
            int cid = GlobalVariables.ActiveClassroomId;



            if (string.IsNullOrWhiteSpace(quizName) ||
                string.IsNullOrWhiteSpace(perQmarks) || startdate == null || string.IsNullOrWhiteSpace(durationOfQuiz.Text))
            {
                CustomMessageBox msg = new CustomMessageBox("Input Error", "Please fill in all fields.", "Error");
                msg.Show();
                return;
            }

            long epochTimestamp = ConvertToEpoch(startdate.Value);
            epochTimestamp += timePicker.SelectedTimeInSeconds;

            Database db = new Database();

            string sql = "INSERT INTO Quiz(name,totalQuestions,totalMarks,marksPerQuestion,teacherId,classroomId,createTime,startTime, duration) VALUES(@name,@totalQues,@totalmark,@marksperQ,@teacherid,@classid,@createtime,@startTime, @duration)";
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@name",quizName),
                new MySqlParameter("@totalques",1),
                new MySqlParameter("@totalmark",1),
                new MySqlParameter("@marksperQ",perQmarks),
                new MySqlParameter("@teacherid",GlobalVariables.TeacherId),
                new MySqlParameter("@classid",GlobalVariables.ActiveClassroomId),
                new MySqlParameter("@createtime",time),
                new MySqlParameter("@startTime",epochTimestamp),
                new MySqlParameter("@duration",Convert.ToInt32(durationOfQuiz.Text))

            };

            db.ExecuteNonQuery(sql, parameters);
            DataTable quizId = db.ExecuteQuery("SELECT id FROM Quiz WHERE createTime=@createtime", new MySqlParameter("@createtime", time));
            this.Close();

            QuizDesignPage addQuestion = new QuizDesignPage(Convert.ToInt32(quizId.Rows[0]["id"]),marksPerQuestion);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Teacher teacher)
                {
                    teacher.TeacherViewFrame.Content = addQuestion;
                }
            }

            QuizDesignControl quizDesignControl = new QuizDesignControl(1);
            quizDesignControl.DeleteRequested += addQuestion.DeleteQuestion;
            addQuestion.quizDesignPanel.Children.Add(quizDesignControl);            

        }

        private long ConvertToEpoch(DateTime date)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(date);
            long epochTimestamp = dateTimeOffset.ToUnixTimeSeconds();
            return epochTimestamp;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void duration_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        // Checking if the input is only number
        private static readonly Regex rgx = new Regex("^[1-9][0-9]*$");

        private bool IsTextAllowed(string text)
        {
            return rgx.IsMatch(text);
        }

        private async void UploadPdf_Click(object sender, RoutedEventArgs e)
        {
            string quizName = quizname.Text;
            string perQmarks = permarks.Text;
            DateTime? startdate = startDate.SelectedDate;
            int marksPerQuestion = Int32.Parse(perQmarks);
            long time = Utilities.GetCurrentTimeInEpoch();
            int tid = GlobalVariables.TeacherId;
            int cid = GlobalVariables.ActiveClassroomId;



            if (string.IsNullOrWhiteSpace(quizName) ||
                string.IsNullOrWhiteSpace(perQmarks) || startdate == null || string.IsNullOrWhiteSpace(durationOfQuiz.Text))
            {
                CustomMessageBox msg = new CustomMessageBox("Input Error", "Please fill in all fields.", "Error");
                msg.Show();
                return;
            }

            long epochTimestamp = ConvertToEpoch(startdate.Value);
            epochTimestamp += timePicker.SelectedTimeInSeconds;

            Database db = new Database();

            string sql = "INSERT INTO Quiz(name,totalQuestions,totalMarks,marksPerQuestion,teacherId,classroomId,createTime,startTime, duration) VALUES(@name,@totalQues,@totalmark,@marksperQ,@teacherid,@classid,@createtime,@startTime, @duration)";
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@name",quizName),
                new MySqlParameter("@totalques",1),
                new MySqlParameter("@totalmark",1),
                new MySqlParameter("@marksperQ",perQmarks),
                new MySqlParameter("@teacherid",GlobalVariables.TeacherId),
                new MySqlParameter("@classid",GlobalVariables.ActiveClassroomId),
                new MySqlParameter("@createtime",time),
                new MySqlParameter("@startTime",epochTimestamp),
                new MySqlParameter("@duration",Convert.ToInt32(durationOfQuiz.Text))

            };

            db.ExecuteNonQuery(sql, parameters);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Documents|*.pdf;*.doc;*.docx|PDF files (*.pdf)|*.pdf|Word files (*.doc;*.docx)|*.doc;*.docx",
                Title = "Select a document to generate a quiz from."
            };

            if (openFileDialog.ShowDialog() == true)
            {
                await ProcessDocumentWithAIAsync(openFileDialog.FileName, time);
            }
        }
        private void SetLoadingState(bool loading, string message = "Loading...", bool isAiGeneration = false)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            loadingText.Text = message;

            if (loading)
            {
                regularLoader.Visibility = isAiGeneration ? Visibility.Collapsed : Visibility.Visible;
                aiLoader.Visibility = isAiGeneration ? Visibility.Visible : Visibility.Collapsed;
                cancelButton.Visibility = isAiGeneration ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private async Task ProcessDocumentWithAIAsync(string filePath, long time)
        {
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                SetLoadingState(true, "AI is generating quiz from your document...", true);

                byte[] fileBytes = await Task.Run(() => File.ReadAllBytes(filePath), cancellationTokenSource.Token);
                string fileName = Path.GetFileName(filePath);

                var quiz = await SendDocumentToAPIAsync(fileBytes, fileName, cancellationTokenSource.Token);
                Debug.WriteLine($"Recieved quiz with questions: {quiz.Count}. Quiz: {quiz}");

                if (quiz.Count > 0)
                {
                    DisplayGeneratedQuiz(quiz, cancellationTokenSource.Token, time);
                }
                else
                {
                    ShowErrorMessage("Warning", "No quiz could be generated from the document");
                }

            }
            catch (OperationCanceledException)
            {
                ShowInfoMessage("Cancelled", "Quiz generation was cancelled");
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

        private void DisplayGeneratedQuiz(List<QuizData> quiz, CancellationToken cancellationToken, long time)
        {
            Database db = new Database();
            DataTable quizData = db.ExecuteQuery("SELECT * FROM Quiz WHERE createTime=@createtime", new MySqlParameter("@createtime", time));
            this.Close();

            QuizDesignPage addQuestion = new QuizDesignPage(Convert.ToInt32(quizData.Rows[0]["id"]), Convert.ToInt32(quizData.Rows[0]["marksPerQuestion"]));

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Teacher teacher)
                {
                    teacher.TeacherViewFrame.Content = addQuestion;
                }
            }

            for (int i = 0; i < quiz.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var questionData = quiz[i];
                var control = new QuizDesignControl(i + 1)
                {
                    questionTextBox = { Text = questionData.Question },
                    optionATextBox = { Text = questionData.Options.ContainsKey("a") ? questionData.Options["a"] : string.Empty },
                    optionBTextBox = { Text = questionData.Options.ContainsKey("b") ? questionData.Options["b"] : string.Empty },
                    optionCTextBox = { Text = questionData.Options.ContainsKey("c") ? questionData.Options["c"] : string.Empty },
                    optionDTextBox = { Text = questionData.Options.ContainsKey("d") ? questionData.Options["d"] : string.Empty }
                };

                switch (questionData.Answer)
                {
                    case "a":
                        control.optionARadioButton.IsChecked = true;
                        break;
                    case "b":
                        control.optionBRadioButton.IsChecked = true;
                        break;
                    case "c":
                        control.optionCRadioButton.IsChecked = true;
                        break;
                    case "d":
                        control.optionDRadioButton.IsChecked = true;
                        break;
                }

                control.DeleteRequested += addQuestion.DeleteQuestion;
                addQuestion.quizDesignPanel.Children.Add(control);
            }
        }
        private async Task<List<QuizData>> SendDocumentToAPIAsync(byte[] fileBytes, string fileName, CancellationToken cancellationToken)
        {
            using (var formData = new MultipartFormDataContent())
            {
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                formData.Add(fileContent, "file", fileName);

                var response = await httpClient.PostAsync(
                    $"{API_BASE_URL}/quiz/from-file",
                    formData,
                    cancellationToken
                );

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return ParseQuizFromJson(jsonResponse);
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
            //cancelButton.IsEnabled = false;
        }

        private List<QuizData> ParseQuizFromJson(string json)
        {
            try
            {
                // Define options to ignore case and tolerate extra fields
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                };

                // Try deserializing directly into QuizResponse
                var result = JsonSerializer.Deserialize<QuizResponse>(json, options);

                if (result?.Quiz != null)
                    return result.Quiz;
            }
            catch (JsonException ex)
            {
                ShowErrorMessage("Parse Error", $"Invalid JSON format: {ex.Message}");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Parse Error", $"Unexpected error: {ex.Message}");
            }

            return new List<QuizData>();
        }

        private void ShowErrorMessage(string title, string message)
        {
            CustomMessageBox errorMsg = new CustomMessageBox(title, message, "Error");
            errorMsg.ShowDialog();
        }


        private void ShowInfoMessage(string title, string message)
        {
            CustomMessageBox infoMsg = new CustomMessageBox(title, message, "Info");
            infoMsg.ShowDialog();
        }


        //private List<QuizData> ParseQuizFromJson(string json)
        //{
        //    var quiz = new List<QuizData>();

        //    try
        //    {
        //        int quizStart = json.IndexOf("\"quiz\"", StringComparison.Ordinal);
        //        if (quizStart == -1) return quiz;

        //        int arrayStart = json.IndexOf("[", quizStart, StringComparison.Ordinal);
        //        int arrayEnd = json.LastIndexOf("]", StringComparison.Ordinal);

        //        if (arrayStart == -1 || arrayEnd == -1 || arrayEnd <= arrayStart)
        //            return quiz;

        //        string arrayContent = json.Substring(arrayStart + 1, arrayEnd - arrayStart - 1);
        //        var matches = Regex.Matches(arrayContent, @"\{[^\}]+\}");

        //        foreach (Match match in matches)
        //        {
        //            var question = ParseSingleQuestion(match.Value);
        //            if (question != null)
        //            {
        //                quiz.Add(flashcard);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ShowErrorMessage("Parse Error", "Failed to parse AI response");
        //    }

        //    return quiz;
        //}

        //private QuizData ParseSingleQuestion(string jsonObject)
        //{
        //    var titleMatch = Regex.Match(jsonObject, @"""title""\s*:\s*""([^""]*)""");
        //    var contentMatch = Regex.Match(jsonObject, @"""content""\s*:\s*""([^""]*)""");

        //    if (!titleMatch.Success && !contentMatch.Success)
        //        return null;

        //    return new QuizData
        //    {
        //        title = UnescapeJsonString(titleMatch.Success ? titleMatch.Groups[1].Value : string.Empty),
        //        description = UnescapeJsonString(contentMatch.Success ? contentMatch.Groups[1].Value : string.Empty)
        //    };
        //}

        //private string UnescapeJsonString(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //        return str;

        //    return str.Replace("\\n", "\n")
        //             .Replace("\\r", "\r")
        //             .Replace("\\t", "\t")
        //             .Replace("\\\"", "\"")
        //             .Replace("\\\\", "\\");
        //}

    }

    public class QuizData
    {
        public string Question { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public string Answer { get; set; }
    }

    public class QuizResponse
    {
        public List<QuizData> Quiz { get; set; }
    }
}


