using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using static QuizFlash.StudentDataPage;
using System.Data;
using System.Web.Configuration;
using System.Collections.ObjectModel;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for ResultTablePage.xaml
    /// </summary>
    public partial class ResultTablePage : Page
    {
        ObservableCollection<Result> results = new ObservableCollection<Result>();
        static Database database = new Database();
        static int minId = Convert.ToInt32(database.ExecuteScalar("Select MIN(studentId) from result where quizId=1"));
        int nextPageId = minId + 9;
        int prevPageId = minId;
        public ResultTablePage(string quiz_name)
        {
            InitializeComponent();
            string query = "SELECT r.studentId, r.quizId, u.name, d.name as departmentName, r.marksObtained FROM users u JOIN result r ON u.Id = r.studentId JOIN department d ON u.departmentId = d.id where r.quizId=1;";
            var reader = database.ExecuteQuery(query);
            quizTitle.Text = $"{quiz_name} Quiz {reader.Rows[0]["quizId"]}";
            for (int i = 0; i < reader.Rows.Count; i++)
            {
                results.Add(new Result
                {
                    Number = Convert.ToSByte(Convert.ToInt32(reader.Rows[i]["studentId"]) - 20),
                    Character = reader.Rows[i]["name"].ToString()[0],
                    Name = reader.Rows[i]["name"].ToString(),
                    Department = reader.Rows[i]["departmentName"].ToString(),
                    StudentCode = reader.Rows[i]["studentId"].ToString(),
                    MarksObtained = Convert.ToInt32(reader.Rows[i]["marksObtained"]),
                    Grade = Utilities.GetGrade(Convert.ToInt32(reader.Rows[i]["marksObtained"]))
                });
                if (i == 8) break;
            }
            resultDataGrid.ItemsSource = results;
        }

        public void NextTable(object sender, RoutedEventArgs e)
        {
            DataTable reader = database.ExecuteQuery("SELECT r.studentId, u.name, d.name as departmentName, r.marksObtained FROM users u JOIN result r ON u.Id = r.studentId JOIN department d ON u.departmentId = d.id where r.quizId=1 and r.studentId>=@pageCount;", new MySqlParameter("@pageCount", nextPageId));

            if (reader.Rows.Count == 0)
            {
                CustomMessageBox msg = new CustomMessageBox("No more data", "No more results to show", "Error");
                msg.Show();
                return;
            }
            else
            {
                results.Clear();
                int rowCount = 0;
                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    results.Add(new Result
                    {
                        Number = Convert.ToSByte(Convert.ToInt32(reader.Rows[i]["studentId"]) - 20),
                        Character = reader.Rows[i]["name"].ToString()[0],
                        Name = reader.Rows[i]["name"].ToString(),
                        Department = reader.Rows[i]["departmentName"].ToString(),
                        StudentCode = reader.Rows[i]["studentId"].ToString(),
                        MarksObtained = Convert.ToInt32(reader.Rows[i]["marksObtained"]),
                        Grade = Utilities.GetGrade(Convert.ToInt32(reader.Rows[i]["marksObtained"]))
                    });
                    rowCount++;
                    if (rowCount == 9)
                    {
                        break;
                    }
                }
                resultDataGrid.ItemsSource = results;
                nextPageId += 9;
                prevPageId += 9;
            }
        }

        public void PrevTable(object sender, RoutedEventArgs e)
        {
            if (prevPageId == minId)
            {
                CustomMessageBox msg = new CustomMessageBox("No more data", "No more results to show", "Error");
                msg.Show();
                return;
            }
            else
            {
                results.Clear();
                DataTable reader = database.ExecuteQuery("SELECT r.studentId, u.name, d.name as departmentName, r.marksObtained FROM users u JOIN result r ON u.Id = r.studentId JOIN department d ON u.departmentId = d.id where r.quizId=1 and r.studentId>=@pageCount;", new MySqlParameter("@pageCount", prevPageId-9));
                int rowCount = 0;
                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    results.Add(new Result
                    {
                        Number = Convert.ToSByte(Convert.ToInt32(reader.Rows[i]["studentId"]) - 20),
                        Character = reader.Rows[i]["name"].ToString()[0],
                        Name = reader.Rows[i]["name"].ToString(),
                        Department = reader.Rows[i]["departmentName"].ToString(),
                        StudentCode = reader.Rows[i]["studentId"].ToString(),
                        MarksObtained = Convert.ToInt32(reader.Rows[i]["marksObtained"]),
                        Grade = Utilities.GetGrade(Convert.ToInt32(reader.Rows[i]["marksObtained"]))
                    });
                    rowCount++;
                    if (rowCount == 9)
                    {
                        break;
                    }
                }
                resultDataGrid.ItemsSource = results;
                nextPageId -= 9;
                prevPageId -= 9;
            }
        }        
        public class Result
        {
            public sbyte Number { get; set; }
            public char Character { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public string StudentCode { get; set; }
            public int MarksObtained { get; set; }
            public string Grade { get; set; }
        }
    }
}
