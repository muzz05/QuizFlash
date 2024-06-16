using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for TeacherClassroomPage.xaml
    /// </summary>
    public partial class TeacherClassroomPage : Page
    {

        private int TeacherId;
        private int UserId;

        public TeacherClassroomPage(int teacherId, int userId)
        {
            TeacherId = teacherId;
            UserId = userId;

            InitializeComponent();

            //AddClassroom("Applied Physics", "PH-122", "Sir Tahir Jamal",49,"XA-9118-PF-09");
            //AddClassroom("Software Engineering", "SE-205", "Miss Sidra",23, "XA-8174-PF-69");
            //AddClassroom("Mathematics", "MTH-101", "Dr. Smith", 89, "LA-PO18-PF-09");
            //AddClassroom("Computer Science", "CS-301", "Prof. Johnson", 69, "NA-9118-PF-06");
            //AddClassroom("Chemistry", "CH-202", "Dr. Lee",34, "PA-1293-PF-08");
            //AddClassroom("History", "HI-110", "Prof. Adams", 43, "LO-9118-LF-00");
            //AddClassroom("Literature", "LI-220", "Dr. Brown", 39, "XA-9118-PF-09");

            Database db = new Database();

            MySqlParameter[] classroomFetchParams =
            {
                new MySqlParameter("@UserId", UserId),
                new MySqlParameter("@TeacherId", TeacherId)
            };

            DataTable ClassroomsData = db.ExecuteQuery("SELECT c.*, u.name as TeacherName FROM Classroom c JOIN Users u ON u.id = @UserId WHERE c.teacherId = @TeacherId", classroomFetchParams);
            if (ClassroomsData.Rows.Count > 0)
            {
                for (int i = 0; i < ClassroomsData.Rows.Count; i++)
                {
                    AddClassroom(ClassroomsData.Rows[i]["name"].ToString(), ClassroomsData.Rows[i]["courseCode"].ToString(), ClassroomsData.Rows[i]["teacherName"].ToString(), Convert.ToInt32(ClassroomsData.Rows[i]["studentCount"]), ClassroomsData.Rows[i]["classCode"].ToString());
                }
            }
        }
        private void classroom_add(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("coming");
        }

        private void AddClassroom(string coursename, string code, string teacher ,int count,string gcr_code)
        {

            Classroom newClassroom = new Classroom(coursename, code, teacher, count,gcr_code);
            // Add the new classroomcontrol instance to the container
            int index = WrapPanelClassroom.Children.Count - 1;
            newClassroom.Margin = new Thickness(0, 0, 15, 15);
            WrapPanelClassroom.Children.Insert(index,newClassroom);

        }

    }
}


