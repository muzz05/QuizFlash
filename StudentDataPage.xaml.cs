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
        private int pageStart = 10;
        public StudentDataPage()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();
            
            Database database = new Database();
            DataTable reader = database.ExecuteQuery("Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from Users u JOIN Students s On s.userId = u.id LEFT JOIN Department d ON u.departmentId=d.id");
            int rowCount = 0;
            for(int i = 0; i < reader.Rows.Count; i++)
            {
                members.Add(new Member { Number = reader.Rows[i]["id"].ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                rowCount++;
                if (rowCount == 10)
                {
                    break;
                }
            }
            studentsDataGrid.ItemsSource = members;

        }


        public void PagingTable(object sender, RoutedEventArgs e) {

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            Database database = new Database();
            DataTable reader = database.ExecuteQuery("Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from users u JOIN Students s On s.userId = u.id left join department d on u.departmentId=d.id where u.id>@pageCount;", new MySqlParameter("@pageCount",pageStart));

            if (reader.Rows.Count == 0)
            {
                MessageBox.Show("No more data to show");
            }
            else
            {
                int rowCount = 0;
                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    members.Add(new Member { Number = reader.Rows[i]["id"].ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                    rowCount++;
                    if (rowCount == 10)
                    {
                        break;
                    }
                }
                studentsDataGrid.ItemsSource = members;
                pageStart += 10;
            }
        }
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
}

