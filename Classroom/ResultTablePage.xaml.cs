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
        Database database = new Database();

        int from = 0, to = 6, quizId=0;

        public ResultTablePage(int quizId, string quiz_name)
        {
            InitializeComponent();
            this.quizId = quizId;
            quizTitle.Text = " Results for " + quiz_name;
            
            NextTable(null, null);
            
        }

        public void NextTable(object sender, RoutedEventArgs e)
            {
            DataTable reader = database.ExecuteQuery("SELECT s.studentCode, q.marksPerQuestion, q.totalQuestions, u.name, d.name as departmentName, r.marksObtained FROM Students s JOIN Result r ON s.id = r.studentId JOIN Users u ON u.id = s.userId JOIN Quiz q ON q.id = r.quizId JOIN Department d ON u.departmentId = d.id where r.quizId=@quizId LIMIT @from,@to;", new MySqlParameter[] { new MySqlParameter("quizId", quizId), new MySqlParameter("@to", to), new MySqlParameter("@from", from) });

            if (reader.Rows.Count == 0)
            {
                CustomMessageBox msg = new CustomMessageBox("No more data", "No more results to show", "Error");
                msg.Show();
                return;
            }
            else
            {
                results.Clear();
                int totalMarks = Convert.ToInt32(reader.Rows[0]["marksPerQuestion"]) * Convert.ToInt32(reader.Rows[0]["totalQuestions"]);
                Console.WriteLine(totalMarks);
                for (int i = 0; i < reader.Rows.Count; i++)
                {
                    results.Add(new Result
                    {
                        Number = Convert.ToSByte(from+i+1),
                        Character = reader.Rows[i]["name"].ToString()[0],
                        Name = reader.Rows[i]["name"].ToString(),
                        Department = reader.Rows[i]["departmentName"].ToString(),
                        StudentCode = reader.Rows[i]["studentCode"].ToString(),
                        MarksObtained = Convert.ToInt32(reader.Rows[i]["marksObtained"]),
                        Grade = Utilities.GetGrade(Convert.ToDouble(reader.Rows[i]["marksObtained"]) / totalMarks * 100)
                    });                    
                }
                resultDataGrid.ItemsSource = results;
                from += 6;
                to += 6;
            }
        }

            public void PrevTable(object sender, RoutedEventArgs e)
            {
                to -= 6;
                from -= 6;
                if (to<0 || from < 0) 
                    {
                        from = 0;
                        to = 6;
                        CustomMessageBox msg = new CustomMessageBox("No more data", "No more results to show", "Error");
                        msg.Show();
                        return;
                    }
                else
                    {
                    results.Clear();
                    DataTable reader = database.ExecuteQuery("SELECT s.studentCode, q.marksPerQuestion, q.totalQuestions, u.name, d.name as departmentName, r.marksObtained FROM Students s JOIN Result r ON s.id = r.studentId JOIN Users u ON u.id = s.userId JOIN Quiz q ON q.id = r.quizId JOIN Department d ON u.departmentId = d.id where r.quizId=@quizId LIMIT @from,@to;", new MySqlParameter[] { new MySqlParameter("quizId", quizId), new MySqlParameter("@to", to), new MySqlParameter("@from", from) });

                    int totalMarks = Convert.ToInt32(reader.Rows[0]["marksPerQuestion"]) * Convert.ToInt32(reader.Rows[0]["totalQuestions"]);
                    for (int i = 0; i < reader.Rows.Count; i++)
                            {
                                results.Add(new Result
                                {
                                    Number = Convert.ToSByte(from+i+1),
                                    Character = reader.Rows[i]["name"].ToString()[0],
                                    Name = reader.Rows[i]["name"].ToString(),
                                    Department = reader.Rows[i]["departmentName"].ToString(),
                                    StudentCode = reader.Rows[i]["studentCode"].ToString(),
                                    MarksObtained = Convert.ToInt32(reader.Rows[i]["marksObtained"]),
                                    Grade = Utilities.GetGrade((Convert.ToDouble(reader.Rows[i]["marksObtained"])/totalMarks) * 100 )
                                });                   
                            }
                    resultDataGrid.ItemsSource = results;
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
