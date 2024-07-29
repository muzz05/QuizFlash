using System;
using System.Windows;
using System.Windows.Controls;

namespace QuizFlash
{
    public partial class CustomTimePicker : UserControl
    {
        public CustomTimePicker()
        {
            InitializeComponent();
            DataContext = this; // Set the DataContext to the control itself for binding

            // Initialize time values
            Hour = 0;
            Minute = 0;
            Second = 0;
        }

        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            PART_Popup.IsOpen = !PART_Popup.IsOpen;
        }

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                int value;
                if (int.TryParse(textBox.Text, out value))
                {
                    if (textBox == PART_HourTextBox)
                    {
                        Hour = Math.Max(0, Math.Min(23, value));
                    }
                    else if (textBox == PART_MinuteTextBox)
                    {
                        Minute = Math.Max(0, Math.Min(59, value));
                    }
                    else if (textBox == PART_SecondTextBox)
                    {
                        Second = Math.Max(0, Math.Min(59, value));
                    }
                }
                else
                {
                    // Handle invalid input
                    if (textBox == PART_HourTextBox) Hour = 0;
                    else if (textBox == PART_MinuteTextBox) Minute = 0;
                    else if (textBox == PART_SecondTextBox) Second = 0;
                }
                // Update the text in the TextBox
                textBox.Text = value.ToString("D2"); // Ensure the text is always two characters long
            }
            UpdateTime();
        }

        private void UpdateTime()
        {
            // Update button content
            PART_Button.Content = $"{Hour:D2}:{Minute:D2}:{Second:D2}";

            // Update time in milliseconds
            SelectedTimeInMilliseconds = (Hour * 3600 + Minute * 60 + Second) * 1000;
        }

        public int Hour
        {
            get => _hour;
            set
            {
                if (_hour != value)
                {
                    _hour = Math.Max(0, Math.Min(23, value));
                    UpdateTime();
                }
            }
        }
        private int _hour;

        public int Minute
        {
            get => _minute;
            set
            {
                if (_minute != value)
                {
                    _minute = Math.Max(0, Math.Min(59, value));
                    UpdateTime();
                }
            }
        }
        private int _minute;

        public int Second
        {
            get => _second;
            set
            {
                if (_second != value)
                {
                    _second = Math.Max(0, Math.Min(59, value));
                    UpdateTime();
                }
            }
        }
        private int _second;

        public long SelectedTimeInMilliseconds { get; private set; }
    }
}
