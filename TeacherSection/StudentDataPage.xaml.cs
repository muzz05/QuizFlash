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
        static int minId = Convert.ToInt32(database.ExecuteScalar("Select MIN(id) from users where isTeacher=false"));
        int nextPageId = minId + 6;
        int prevPageId = minId;
        BrushConverter converter = new BrushConverter();
        ObservableCollection<Member> members = new ObservableCollection<Member>();
        public StudentDataPage()
        {
            InitializeComponent();  

            DataTable reader = database.ExecuteQuery("Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from Users u JOIN Students s On s.userId = u.id LEFT JOIN Department d ON u.departmentId=d.id");
            int rowCount = 1;
            for (int i = 0; i < reader.Rows.Count; i++)
            {
                members.Add(new Member { Number = reader.Rows[i]["id"].ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                rowCount++;
                if (rowCount == 7)
                {
                    break;
                }
            }
            studentsDataGrid.ItemsSource = members;

        }


        public void NextTable(object sender, RoutedEventArgs e)
        {           
            DataTable reader = database.ExecuteQuery("Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from users u JOIN Students s On s.userId = u.id left join department d on u.departmentId=d.id where u.id>=@pageCount;", new MySqlParameter("@pageCount", nextPageId));

            if (reader.Rows.Count == 0)
            {
                CustomMessageBox msg = new CustomMessageBox("No more data","No more students to show","Error");
                msg.Show();
            }
            else
            {
                members.Clear();
                int rowCount = 1;
                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    members.Add(new Member { Number = reader.Rows[i]["id"].ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                    rowCount++;
                    if (rowCount == 7)
                    {
                        break;
                    }
                }
                studentsDataGrid.ItemsSource = members;
                nextPageId += 6;
                prevPageId += 6;
            }
        }

                public void PrevTable(object sender, RoutedEventArgs e)
                {
                    if(prevPageId == minId)
                    {
                    CustomMessageBox msg = new CustomMessageBox("No more data", "No more students to show", "Error");
                    msg.Show();
                    return;
                    }
                    else
                    {
                        DataTable reader = database.ExecuteQuery("Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from users u JOIN Students s On s.userId = u.id left join department d on u.departmentId=d.id where u.id>=@pageCount;", new MySqlParameter("@pageCount", prevPageId - 6));
                        members.Clear();
                        int rowCount = 1;
                        for (int i = 0; i < reader.Rows.Count; i++)
                        {
                            members.Add(new Member { Number = reader.Rows[i]["id"].ToString(), Character = reader.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = reader.Rows[i]["name"].ToString(), Department = reader.Rows[i]["departmentName"].ToString(), Email = reader.Rows[i]["email"].ToString(), StudentCode = reader.Rows[i]["studentCode"].ToString() });
                            rowCount++;
                            if (rowCount == 7)
                            {
                                break;
                            }
                        }
                        studentsDataGrid.ItemsSource = members;
                        nextPageId -= 6;
                        prevPageId -= 6;
                    }   
        }

        public void SearchStudent(object sender, RoutedEventArgs e)
        {
            DataTable search = database.ExecuteQuery("Select s.studentCode, u.id, u.name, u.email,d.name as departmentName from users u JOIN Students s On s.userId = u.id left join department d on u.departmentId=d.id where u.name like @name;", new MySqlParameter("@name", "%" + textBoxFilter.Text + "%"));
            if(search.Rows.Count == 0)
            {
                CustomMessageBox msg = new CustomMessageBox("Invalid Search","No results found","Error");
                msg.Show();
            }
            else
            {
                members.Clear();
                for (int i = 0; i < search.Rows.Count; i++)
                {
                    members.Add(new Member { Number = search.Rows[i]["id"].ToString(), Character = search.Rows[i]["name"].ToString()[0].ToString(), BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = search.Rows[i]["name"].ToString(), Department = search.Rows[i]["departmentName"].ToString(), Email = search.Rows[i]["email"].ToString(), StudentCode = search.Rows[i]["studentCode"].ToString() });
                }
                studentsDataGrid.ItemsSource = members;
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
}

