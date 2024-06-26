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
    /// Interaction logic for StudentHomepageInfoCard.xaml
    /// </summary>
    public partial class StudentHomepageInfoCard : UserControl
    {
        public StudentHomepageInfoCard(string className, string announcement, long epoch)
        {
            InitializeComponent();
            RecentQuizDateBadge.Text = "Due " + Utilities.ConvertEpochToRelativeTimeFuture(epoch);
            classname.Text = className;
            this.announcement.Text = announcement;
        }
    }
}


