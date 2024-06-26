using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static string ConvertEpochToRelativeTime(long epochTimestamp)
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

        // This is to get the relative time of the future
        public static string ConvertEpochToRelativeTimeFuture(long epochTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(epochTimestamp).UtcDateTime;
            var now = DateTime.UtcNow;
            var timeSpan = dateTime - now;

            if (timeSpan.TotalDays >= 1)
            {
                return $"in {(int)timeSpan.TotalDays} days";
            }
            if (timeSpan.TotalHours >= 1)
            {
                return $"in {(int)timeSpan.TotalHours} hours";
            }
            if (timeSpan.TotalMinutes >= 1)
            {
                return $"in {(int)timeSpan.TotalMinutes} minutes";
            }
            return "just now";
        }

        // To Convert Eoch to ISO String
        public static string ConvertEpochToIsoString(long epochTimestamp)
        {
            var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(epochTimestamp * 1000);
            return dateTime.UtcDateTime.ToString("o");
        }

        // To Convert ISO String to Date
        public static string ConvertIsoStringToDate(string isoString)
        {
            DateTime parsedDateTime = DateTime.Parse(isoString);
            return parsedDateTime.ToString("d");
        }

        // To get the Device name
        public static string GetDeviceName()
        {
            try
            {
                string hostName = Dns.GetHostName();
                return hostName;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        // To get the OS Type
        public static int GetOperatingSystemType()
        {
            try
            {
                int osType;

                PlatformID platform = Environment.OSVersion.Platform;

                switch (platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                        osType = 0;
                        break;

                    case PlatformID.Unix:
                        osType = 1;
                        break;

                    case PlatformID.MacOSX:
                        osType = 2;
                        break;

                    default:
                        osType = 3;
                        break;
                }

                return osType;
            }
            catch (Exception)
            {
                return 3;
            }
        }
    }

    
}
