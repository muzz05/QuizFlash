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
    /// Interaction logic for Announcements.xaml
    /// </summary>
    public partial class Announcements : UserControl
    {
        public Announcements(string username, bool isTeacher, long epochDate, string message)
        {
            InitializeComponent();
            UserNameText.Text = username;
            UserBadgeText.Text = GetInitials(username);
            AnnouncementTimeText.Text = ConvertEpochToRelativeTime(epochDate);
            AnnouncementText.Text = message;
            isTeacherBadge.Visibility = isTeacher ? Visibility.Visible : Visibility.Collapsed;
        }

        private string ConvertEpochToRelativeTime(long epochTimestamp)
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

        private string GetInitials(string username)
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

    }
}
