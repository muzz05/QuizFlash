using QuizFlash.Components;
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
            AddClassroomUser("Jane Smith", true);
            AddClassroomUser("John Doe", false);
            AddClassroomUser("Emily Jones", false);
            AddClassroomUser("Michael Brown", false);
            AddClassroomUser("Sarah Davis", false);
            AddClassroomUser("David Miller", false);
            AddClassroomUser("Linda Wilson", false);
            AddClassroomUser("James Moore", false);
            AddClassroomUser("Patricia Taylor", false);
            AddClassroomUser("Robert Anderson", false);
            AddClassroomUser("Barbara Thomas", false);
            AddClassroomUser("William Jackson", false);
            AddClassroomUser("Elizabeth White", false);
            AddClassroomUser("Charles Harris", false);
            AddClassroomUser("Mary Martin", false);
            AddClassroomUser("Joseph Thompson", false);
            AddClassroomUser("Nancy Garcia", false);
            AddClassroomUser("Richard Martinez", false);
            AddClassroomUser("Margaret Robinson", false);
            AddClassroomUser("Thomas Clark", false);
            AddClassroomUser("Dorothy Rodriguez", false);
            AddClassroomUser("Christopher Lewis", false);
            AddClassroomUser("Susan Lee", false);
            AddClassroomUser("Daniel Walker", false);
            AddClassroomUser("Jessica Hall", false);

        }

        public void AddClassroomUser(string username, bool isTeacher)
        {
            ClassroomStudents newStudent = new ClassroomStudents(username, isTeacher);
            studentListPanel.Children.Add(newStudent);
        }
    }
}
