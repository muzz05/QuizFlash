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
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_signup_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox_signup.Password) && passwordBox_signup.Password.Length > 0)
                textPassword_signup.Visibility = Visibility.Collapsed;
            else
                textPassword_signup.Visibility = Visibility.Visible;
        }

        private void textPassword_signup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox_signup.Focus();
        }

        private void Signup_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername_signup.Text) && !string.IsNullOrEmpty(passwordBox_signup.Password))
            {
                MessageBox.Show("Successfully Signed In");
            }
        }

        private void txtUsername_signup_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername_signup.Text) && txtUsername_signup.Text.Length > 0)
                textUsername_signup.Visibility = Visibility.Collapsed;
            else
                textUsername_signup.Visibility = Visibility.Visible;
        }

        private void txtEmail_signup_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail_signup.Text) && txtEmail_signup.Text.Length > 0)
                textEmail_signup.Visibility = Visibility.Collapsed;
            else
                textEmail_signup.Visibility = Visibility.Visible;
        }




        private void textUsername_signup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUsername_signup.Focus();
        }





        private void textEmail_signup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail_signup.Focus();
        }


        private void CmbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)cmbDepartments.SelectedItem;

            string selectedDepartment = selectedItem.Content.ToString();

            dropdowndept.Text = selectedDepartment;
        }





        private void Student_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            // Uncheck the Teacher checkbox
            Teacher_checkbox.IsChecked = false;

            // Update colors for Student
            student_ellipse.Fill = new SolidColorBrush(Color.FromRgb(196, 195, 207));
            student_check_tb.Foreground = new SolidColorBrush(Color.FromRgb(196, 195, 207));

            // Revert colors for Teacher
            teacher_ellipse.Fill = new SolidColorBrush(Color.FromRgb(89, 89, 92));
            teacher_check_tb.Foreground = new SolidColorBrush(Color.FromRgb(89, 89, 92));


        }

        private void Teacher_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            // Uncheck the Student checkbox
            Student_checkbox.IsChecked = false;

            // Update colors for Teacher
            teacher_ellipse.Fill = new SolidColorBrush(Color.FromRgb(196, 195, 207));
            teacher_check_tb.Foreground = new SolidColorBrush(Color.FromRgb(196, 195, 207));

            // Revert colors for Student
            student_ellipse.Fill = new SolidColorBrush(Color.FromRgb(89, 89, 92));
            student_check_tb.Foreground = new SolidColorBrush(Color.FromRgb(89, 89, 92));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
    }
}

