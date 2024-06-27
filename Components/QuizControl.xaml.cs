using QuizFlash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizFlash
{

    public partial class QuizControl : UserControl
    {
        int quizId;
        public QuizControl(int quizId,string quizname, int totalmarks, int questions, long validUntilEpoch, bool IsAttempted)
        {
            InitializeComponent();
            this.quizId = quizId;
            QuizName.Text = quizname;
            QuizMarks.Text = totalmarks.ToString();
            QuesCount.Text = questions.ToString();

            AttemptedBadge.Visibility =  GlobalVariables.IsTeacher || !IsAttempted ? Visibility.Collapsed: Visibility.Visible;

            // Convert epoch timestamp to ISO 8601 string (assuming validUntilEpoch is in seconds)
            string validUntilIsoString = ConvertEpochToIsoString(validUntilEpoch);
            string validUntilDate = ConvertIsoStringToDate(validUntilIsoString); 
            ValidUntil.Text= validUntilDate;
        }

        private string ConvertEpochToIsoString(long epochTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(epochTimestamp * 1000);
            return dateTime.UtcDateTime.ToString("o");
        }
        private string ConvertIsoStringToDate(string isoString)
        {
            DateTime parsedDateTime = DateTime.Parse(isoString);
            return parsedDateTime.ToString("d");  
        }        

        private void result_page_redirect(object sender, RoutedEventArgs e)
        {            
        }

        private void quizRedirect(object sender, RoutedEventArgs e)
        {
            foreach(Window window in Application.Current.Windows)
            {
                if(window is Student student)
                {
                    student.StudentViewFrame.Content = new QuizDisplayPage(quizId, QuizName.Text);                    
                }
            }
        }
    }
}