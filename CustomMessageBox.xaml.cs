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
using System.Windows.Shapes;

namespace QuizFlash
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string Title, string Message, string Type)
        {
            InitializeComponent();
            messageIcon.Kind = Type == "Error" ? MahApps.Metro.IconPacks.PackIconBootstrapIconsKind.ExclamationCircleFill : MahApps.Metro.IconPacks.PackIconBootstrapIconsKind.CheckCircleFill;
            messageBoxHeading.Text = Title;
            messageBoxText.Text = Message;
        }

        private void CloseMessageBox(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
