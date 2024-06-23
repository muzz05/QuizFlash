using MySql.Data.MySqlClient;
using QuizFlash.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for TeacherClassroomStudentList.xaml
    /// </summary>
    public partial class TeacherClassroomStudentList : Page
    {
        public TeacherClassroomStudentList()
        {
            InitializeComponent();

            AddStudentStackPanel.Visibility = GlobalVariables.IsTeacher ? Visibility.Visible : Visibility.Collapsed;

            Database db = new Database();

            string sql = "SELECT COUNT(cs.studentId) AS StudentCount FROM ClassroomStudents cs JOIN Students s ON cs.studentId = s.id JOIN Users u ON u.id = s.userId WHERE cs.classroomId = @ClassroomId";
            object StudentCount = db.ExecuteScalar(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));
            TotalStudentCount.Text = StudentCount.ToString();


            sql = "SELECT t.id, t.teacherCode, u.name FROM Classroom c JOIN Teachers t ON c.teacherId = t.id JOIN Users u ON t.userId = u.id WHERE c.id = @ClassroomId";
            DataTable TeacherInfo = db.ExecuteQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));

            AddClassroomTeacher(Convert.ToInt32(TeacherInfo.Rows[0]["id"]), TeacherInfo.Rows[0]["name"].ToString(), TeacherInfo.Rows[0]["teacherCode"].ToString());
            
            sql = "SELECT cs.studentId, s.studentCode, u.name FROM ClassroomStudents cs JOIN Students s ON cs.studentId = s.id JOIN Users u ON u.id = s.userId WHERE cs.classroomId = @ClassroomId ORDER BY u.name DESC";
            DataTable StudentInfo = db.ExecuteQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));

            if (StudentInfo != null)
            {
                for (int i = 0; i < StudentInfo.Rows.Count; i++)
                {
                    AddClassroomStudent(Convert.ToInt32(StudentInfo.Rows[i]["studentId"]), StudentInfo.Rows[i]["name"].ToString(), StudentInfo.Rows[i]["studentCode"].ToString());
                }
            }

        }

        private void StudentCode_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StudentCode_ClassroomTextBox.Text) && StudentCode_ClassroomTextBox.Text.Length > 0)
                StudentCode_ClassroomTextBlock.Visibility = Visibility.Collapsed;
            else
                StudentCode_ClassroomTextBlock.Visibility = Visibility.Visible;
        }

        private void StudentCode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StudentCode_ClassroomTextBox.Focus();
        }

        public void AddClassroomTeacher(int id, string username, string code)
        {
            ClassroomStudents newStudent = new ClassroomStudents(id, username, true, code);
            studentListPanel.Children.Add(newStudent);
        }



        public void AddClassroomStudent(int id, string username, string code)
        {
            ClassroomStudents newStudent = new ClassroomStudents(id, username, false, code);
            studentListPanel.Children.Insert(1,newStudent);
        }


        // Add Button Functionality
        private void AddStudentToClassroom(object sender, RoutedEventArgs e)
        {
            if(StudentCode_ClassroomTextBox.Text.Length > 0)
            {
                Database db = new Database();
                string sql = "SELECT s.id, s.studentCode, u.name FROM Students s JOIN Users u ON u.id = s.userId WHERE studentCode = @StudentCode";
                DataTable StudentInfo = db.ExecuteQuery(sql, new MySqlParameter("@StudentCode", StudentCode_ClassroomTextBox.Text));
                if (StudentInfo != null) {
                    sql = "INSERT INTO ClassroomStudents(classroomId, studentId) VALUES(@ClassroomId, @StudentId)";
                    MySqlParameter[] insertparams =
                    {
                        new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId),
                        new MySqlParameter("@StudentId", Convert.ToInt32(StudentInfo.Rows[0]["id"]))
                    };
                    int result = db.ExecuteNonQuery(sql, insertparams);
                    if (result > 0) {
                        AddClassroomStudent(Convert.ToInt32(StudentInfo.Rows[0]["id"]), StudentInfo.Rows[0]["name"].ToString(), StudentInfo.Rows[0]["studentCode"].ToString());
                        TotalStudentCount.Text = (Convert.ToInt32(TotalStudentCount.Text) + 1).ToString();
                    }
                    else
                    {
                        CustomMessageBox errorOccured = new CustomMessageBox("Error Occured", "Some Error Occured in adding Student to Classroom", "Error");
                        errorOccured.ShowDialog();
                    }
                }
                else
                {
                    CustomMessageBox incorrectCode = new CustomMessageBox("Incorrect Code", "Enter a correct student code", "Error");
                    incorrectCode.ShowDialog();
                }
            }
            else
            {
                CustomMessageBox noCode = new CustomMessageBox("Empty Feild", "You cannot leave this entry empty", "Error");
                noCode.ShowDialog();
            }
        }

        // Leave Classroom
        private void LeaveClassroom(object sender, RoutedEventArgs e)
        {
            Database db = new Database();

            if(GlobalVariables.IsTeacher){
                string sql = "DELETE FROM Classroom WHERE id = @ClassroomId";
                int removeClassroom = db.ExecuteNonQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));
                sql = "DELETE FROM ClassroomStudents WHERE classroomId = @ClassroomId";
                int removeStudents = db.ExecuteNonQuery(sql, new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId));
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Teacher teacher)
                    {
                        teacher.TeacherViewFrame.Content = new TeacherClassroomPage(GlobalVariables.TeacherId, GlobalVariables.UserId);
                    }
                }
            }
            else
            {
                string sql = "DELETE FROM ClassroomStudents WHERE studentId = @StudentId AND classroomId = @ClassroomId";
                MySqlParameter[] deleteparams =
                    {
                        new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId),
                        new MySqlParameter("@StudentId", GlobalVariables.StudentId)
                    };
                int removeStudent = db.ExecuteNonQuery(sql, deleteparams);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Student student)
                    {
                        student.StudentViewFrame.Content = new StudentClassroomPage(GlobalVariables.StudentId, GlobalVariables.UserId);
                    }
                }
            }
        }
    }
}
