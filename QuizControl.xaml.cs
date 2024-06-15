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
    /// <summary>
    /// Interaction logic for QuizControl.xaml
    /// </summary>
    public partial class QuizControl : UserControl
    {
        public QuizControl(string quizname, int totalmarks, int questions, long validUntilEpoch)
        {
            InitializeComponent();
            QuizName.Text = quizname;
            QuizMarks.Text = totalmarks.ToString();
            QuesCount.Text = questions.ToString();

            // Convert epoch timestamp to ISO 8601 string (assuming validUntilEpoch is in seconds)
            string validUntilIsoString = ConvertEpochToIsoString(validUntilEpoch);
            string validUntilDate = ConvertIsoStringToDate(validUntilIsoString);  // New line for date conversion
            ValidUntil.Text= validUntilDate;
        }

        private string ConvertEpochToIsoString(long epochTimestamp)
        {
            // Assuming epoch time is in seconds (multiply by 1000 for milliseconds)
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(epochTimestamp * 1000);
            return dateTime.UtcDateTime.ToString("o"); // Use 'o' format for ISO 8601 with UTC time
        }
        private string ConvertIsoStringToDate(string isoString)
        {
            // Parse the ISO 8601 string
            DateTime parsedDateTime = DateTime.Parse(isoString);

            // Use 'd' format specifier for day/month/year
            return parsedDateTime.ToString("d");  
        }
    }
}