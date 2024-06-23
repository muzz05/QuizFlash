using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizFlash.Components
{
    /// <summary>
    /// Interaction logic for ClassroomStudents.xaml
    /// </summary>
    public partial class ClassroomStudents : UserControl
    {
        public int ClassroomStudentId;
        public ClassroomStudents(int id, string username, bool isUserTeacher, string code)
        {
            ClassroomStudentId = id;
            InitializeComponent();
            StudentNameOrTeacher.Text = code;
            UserBadgeText.Text = Utilities.GetInitials(username);
            UserNameText.Text = username;
            isTeacherBadge.Visibility = isUserTeacher ? Visibility.Visible : Visibility.Collapsed;
            if(isUserTeacher || !GlobalVariables.IsTeacher)
            {
                removeStudent.Visibility = Visibility.Collapsed;
            }
            else
            {
                removeStudent.Visibility = Visibility.Visible;
            }
        }

        private void RemoveStudentFromClassroom(object sender, MouseButtonEventArgs e)
        {
            Database db = new Database();
            string sql = "DELETE FROM ClassroomStudents WHERE studentId = @StudentId AND classroomId = @ClassroomId";
            MySqlParameter[] deleteparams =
            {
                new MySqlParameter("@ClassroomId", GlobalVariables.ActiveClassroomId),
                new MySqlParameter("@StudentId", ClassroomStudentId)
            };
            int result = db.ExecuteNonQuery(sql, deleteparams);
            if (result > 0) {
                if (Parent is Panel panelStackPanel)
                {
                    panelStackPanel.Children.Remove(this);
                }
            }
            else
            {
                CustomMessageBox errorOccured = new CustomMessageBox("Error Occured", "Some Error Occured in removing Student from Classroom", "Error");
                errorOccured.ShowDialog();
            }
        }
    }
}
