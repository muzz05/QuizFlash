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
    public partial class StudentClassroomPage : Page
    {

        private int StudentId;
        private int UserId;

        public StudentClassroomPage(int studentId, int userId)
        {
            StudentId = studentId;
            UserId = userId;

            InitializeComponent();

            Database db = new Database();

            DataTable ClassroomsData = db.ExecuteQuery("SELECT c.*, u.name as TeacherName FROM Classroom c JOIN Teachers t ON t.id = c.teacherId JOIN Users u ON u.id = t.userId JOIN ClassroomStudents cs ON cs.classroomId = c.id WHERE cs.studentId = @StudentId", new MySqlParameter("@StudentId", StudentId));
            if (ClassroomsData.Rows.Count > 0)
            {
                for (int i = 0; i < ClassroomsData.Rows.Count; i++)
                {
                    AddClassroom(ClassroomsData.Rows[i]["name"].ToString(), ClassroomsData.Rows[i]["courseCode"].ToString(), ClassroomsData.Rows[i]["teacherName"].ToString(), Convert.ToInt32(ClassroomsData.Rows[i]["studentCount"]), ClassroomsData.Rows[i]["classCode"].ToString(),Convert.ToInt32(ClassroomsData.Rows[i]["id"]));
                }
            }
        }

        private void AddClassroom(string coursename, string code, string teacher, int count, string gcr_code, int classroomId)
        {

            Classroom newClassroom = new Classroom(coursename, code, teacher, count, gcr_code, false, classroomId);
            newClassroom.Margin = new Thickness(0, 0, 15, 15);
            WrapPanelClassroom.Children.Add(newClassroom);

        }

    }
}


