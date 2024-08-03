using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace QuizFlash
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string Title, string Message, string Type)
        {
            InitializeComponent();
            playSimpleSound();
            messageIcon.Kind = Type == "Error" ? MahApps.Metro.IconPacks.PackIconBootstrapIconsKind.ExclamationCircleFill : MahApps.Metro.IconPacks.PackIconBootstrapIconsKind.CheckCircleFill;
            if(Type == "Info")
            {
                messageIcon.Kind = MahApps.Metro.IconPacks.PackIconBootstrapIconsKind.ExclamationCircleFill;
            }
            messageBoxHeading.Text = Title;
            messageBoxText.Text = Message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard scaleDownStoryboard = (Storyboard)this.Resources["ScaleDownAnimation"];
            scaleDownStoryboard.Begin();
        }


        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"soundeffect.wav"));
            simpleSound.Play();
        }
        private void CloseMessageBox(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
