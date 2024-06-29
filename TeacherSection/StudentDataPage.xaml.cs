using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for StudentDataPage.xaml
    /// </summary>
    public partial class StudentDataPage : Page
    {
        static Database database = new Database();

        int threshold = 6;
        int totalStudents = 0;
        int currentStudent = 0;

        BrushConverter converter = new BrushConverter();
        ObservableCollection<Member> members = new ObservableCollection<Member>();
        public StudentDataPage()
        {
            InitializeComponent();

            GetPaginatedDataFromDb(0, "",0);
        }

        private void TxtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxFilter.Text) && textBoxFilter.Text.Length > 0)
                SearchTextBlock.Visibility = Visibility.Collapsed;
            else
                SearchTextBlock.Visibility = Visibility.Visible;
        }

        private void TextSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchTextBlock.Visibility = Visibility.Hidden;
            textBoxFilter.Focus();
        }


        public void NextTable(object sender, RoutedEventArgs e)
        {
            if(textBoxFilter.Text == "")
            {
                GetPaginatedDataFromDb(totalStudents, "",0);
            }
            else
            {
                GetPaginatedDataFromDb(totalStudents, textBoxFilter.Text,0);
            }
        }

        public void PrevTable(object sender, RoutedEventArgs e)
        {
            if(totalStudents < currentStudent + threshold)
            {
                CustomMessageBox msg = new CustomMessageBox("No more data", "No more students to show", "Error");
                msg.ShowDialog();
            }
            else
            {
                if (totalStudents >= currentStudent + threshold) totalStudents -= currentStudent + threshold;
                if (textBoxFilter.Text == "")
                {
                    GetPaginatedDataFromDb(totalStudents, "",0);
                }
                else
                {
                    GetPaginatedDataFromDb(totalStudents, textBoxFilter.Text,0);
                }
            }
            
        }

        public void GetPaginatedDataFromDb(int from, string query, int operation)
        {
            string sql;
            DataTable reader;
            

            if (query == "")
            {
                sql = "Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from users u JOIN Students s On s.userId = u.id left join department d on u.departmentId=d.id LIMIT @From, @Threshold";
                MySqlParameter[] getParams =
                {
                    new MySqlParameter("@From", from),
                    new MySqlParameter("@Threshold", threshold),
                };
                reader = database.ExecuteQuery(sql, getParams);

                if (reader.Rows.Count <= 0)
                {
                    CustomMessageBox msg = new CustomMessageBox("No more data", "No more students to show", "Error");
                    msg.ShowDialog();
                    return;
                }
                else
                {
                    members.Clear();

                }

                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    members.Add(new Member { Number = (i + totalStudents + 1).ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                }
                if (operation == 0)
                {
                    totalStudents += reader.Rows.Count;
                }else if( operation == 1)
                {
                    totalStudents -= reader.Rows.Count;
                }
                else if (operation == 2)
                {
                    totalStudents = reader.Rows.Count;
                    currentStudent = reader.Rows.Count;
                }

            }
            else
            {
                sql = "Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from users u JOIN Students s On s.userId = u.id left join department d on u.departmentId=d.id WHERE u.name like @name LIMIT @From, @Threshold";
                MySqlParameter[] getParams =
                {
                    new MySqlParameter("@From", from),
                    new MySqlParameter("@Threshold", threshold),
                    new MySqlParameter("@name", "%" + query + "%"),
                };
                reader = database.ExecuteQuery(sql, getParams);

                if(reader.Rows.Count <= 0)
                {
                    CustomMessageBox msg = new CustomMessageBox("No more data", "No more students to show for this search", "Error");
                    msg.ShowDialog();
                    return;
                }
                else
                {
                    members.Clear();

                }



                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    members.Add(new Member { Number = (i + totalStudents + 1).ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                }
                if (operation == 0)
                {
                    totalStudents += reader.Rows.Count;
                }
                else if (operation == 1)
                {
                    totalStudents -= reader.Rows.Count;
                }
                else if (operation == 2)
                {
                    totalStudents = reader.Rows.Count;
                    currentStudent = reader.Rows.Count;
                }
            }

            studentsDataGrid.ItemsSource = members;
            currentStudent = reader.Rows.Count;
        }

        public void SearchStudent(object sender, RoutedEventArgs e)
        {
            totalStudents = 0;
            GetPaginatedDataFromDb(0, textBoxFilter.Text, 3);
        }
        public class Member
        {
            public string Character { get; set; }
            public Brush BgColor { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public string Email { get; set; }
            public string StudentCode { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBoxFilter.Text = "";
            StudentSearcherButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}

