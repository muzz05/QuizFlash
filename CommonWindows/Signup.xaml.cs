using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public int DepartmentId;
        public bool isTeacher;

        public static readonly RoutedCommand EnterCommand1 = new RoutedCommand();

        public Signup()
        {

            Database db = new Database();
            string sql = "SELECT * FROM Department";
            MySqlParameter parameters = new MySqlParameter();

            DataTable DepartmentsData = db.ExecuteQuery(sql, parameters);
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(EnterCommand1,HandleSignup ));
           

            // Adding Department Data to the Combo Box
            for (int i = 0; i < DepartmentsData.Rows.Count; i++)
            {
                ComboBoxItem DepartmentsDataItem = new ComboBoxItem();
                DepartmentsDataItem.Content = DepartmentsData.Rows[i]["name"].ToString();
                DepartmentsDataItem.Tag = Convert.ToInt32(DepartmentsData.Rows[i]["id"]);
                comboBoxDepartments.Items.Add(DepartmentsDataItem);
            }



        }


        // THIS IS FOR UI
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
            ComboBoxItem selectedItem = (ComboBoxItem)comboBoxDepartments.SelectedItem;

            string selectedDepartment = selectedItem.Content.ToString();
            DepartmentId = Convert.ToInt32(selectedItem.Tag);

            dropdowndept.Text = selectedDepartment;
        }


        private void Student_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            isTeacher = false;

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
            isTeacher = true;

            // Uncheck the Student checkbox
            Student_checkbox.IsChecked = false;
            

            // Update colors for Teacher
            teacher_ellipse.Fill = new SolidColorBrush(Color.FromRgb(196, 195, 207));
            teacher_check_tb.Foreground = new SolidColorBrush(Color.FromRgb(196, 195, 207));

            // Revert colors for Student
            student_ellipse.Fill = new SolidColorBrush(Color.FromRgb(89, 89, 92));
            student_check_tb.Foreground = new SolidColorBrush(Color.FromRgb(89, 89, 92));
        }

        private void OpenComboBox(object sender, MouseButtonEventArgs e)
        {
            comboBoxDepartments.IsDropDownOpen = true;
        }

        // THIS IS FOR LOGICAL PART
        private void HandleSignup(object sender, RoutedEventArgs e)
        {
            Database db = new Database();

            string email = txtEmail_signup.Text;
            string password = passwordBox_signup.Password;
            string name = txtUsername_signup.Text;
            string UserCode = UniqueCodeGenerator.GenerateUniqueCode();

            // Checking if a user with the same Email exists

            string sql = "SELECT email FROM Users WHERE email = @Email";
            object checkEmail = db.ExecuteScalar(sql, new MySqlParameter("@Email", email));
            if (checkEmail != null)
            {
                CustomMessageBox ErrorSignupEmail = new CustomMessageBox("Unsuccessfull Signup", "User with the same email already exists", "Error");
                ErrorSignupEmail.Show();
                return;
            }

            sql = "INSERT INTO Users(name, email, password, isTeacher, departmentId) VALUES(@Name, @Email, @Password, @isTeacher, @DeptId)";

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Name", name),
                new MySqlParameter("@Email", email),
                new MySqlParameter("@Password", password),
                new MySqlParameter("@IsTeacher", isTeacher),
                new MySqlParameter("@DeptId", DepartmentId)
            };

            int rowsAffected = db.ExecuteNonQuery(sql, parameters);

            // Adding the User to his respective table (Teachers/Students)

            if (rowsAffected > 0)
            {
                
                sql = "SELECT id FROM Users WHERE email = @Email";
                MySqlParameter EmailParam = new MySqlParameter("@Email", email);
                object lastInsertedUser = db.ExecuteScalar(sql, EmailParam);
                int userId = Convert.ToInt32(lastInsertedUser);
                MySqlParameter[] param = { new MySqlParameter("@UserId", userId), new MySqlParameter("@Code", UserCode) };

                if (isTeacher)
                {
                    sql = "INSERT INTO Teachers(userId, teacherCode) VALUES(@UserId, @Code)";
                }

                else
                {
                    sql = "INSERT INTO Students(userId, studentCode) VALUES(@UserId, @Code)";
                }

                int result = db.ExecuteNonQuery(sql, param);

                if (result > 0)
                {

                    Login loginWindow = new Login();
                    loginWindow.Show();
                    this.Close();
                }
            }
            else
            {
                CustomMessageBox ErrorSignup = new CustomMessageBox("Unsuccessfull Signup", "Some Error Occured in Signup", "Error");
                ErrorSignup.Show();
            }
        }

        private void minimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}

