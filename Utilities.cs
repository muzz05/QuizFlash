using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizFlash
{
    public class Utilities
    {

        // To Get the Initials of a name
        public static string GetInitials(string username)
        {
            var names = username.Split(' ');
            if (names.Length >= 2)
            {
                return $"{names[0][0]}{names[1][0]}";
            }
            if (names.Length == 1)
            {
                return $"{names[0][0]}";
            }
            return "NN";
        }

        // To Get the current time in Epoch
        public static long GetCurrentTimeInEpoch() {
            DateTime currentTime = DateTime.UtcNow;
            DateTimeOffset dateTimeOffset = new DateTimeOffset(currentTime);
            return dateTimeOffset.ToUnixTimeSeconds();
        }

        //Determining the grade of a student based on marks
        public static string GetGrade(int marks)
        {
            switch (marks)
            {
                case int n when (n >= 90):
                    return "A+";
                case int n when (n >= 80):
                    return "A";
                case int n when (n >= 70):
                    return "B";
                case int n when (n >= 60):
                    return "C";
                case int n when (n >= 50):
                    return "D";
                default:
                    return "F";
            }
        }

        // To Convert Epoch to Relative time (just now, 2 hours ago etc.)
        private static string ConvertEpochToRelativeTime(long epochTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(epochTimestamp);
            var timeSpan = DateTime.UtcNow - dateTime.UtcDateTime;

            if (timeSpan.TotalDays >= 1)
            {
                return $"{(int)timeSpan.TotalDays} days ago";
            }
            if (timeSpan.TotalHours >= 1)
            {
                return $"{(int)timeSpan.TotalHours} hours ago";
            }
            if (timeSpan.TotalMinutes >= 1)
            {
                return $"{(int)timeSpan.TotalMinutes} minutes ago";
            }
            return "just now";
        }

        // To Convert Epoch to ISO String
        private static string ConvertEpochToIsoString(long epochTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(epochTimestamp * 1000);
            return dateTime.UtcDateTime.ToString("o");
        }

        // To Convert ISO String to Date
        private static string ConvertIsoStringToDate(string isoString)
        {
            DateTime parsedDateTime = DateTime.Parse(isoString);
            return parsedDateTime.ToString("d");
        }
    }

    
}
