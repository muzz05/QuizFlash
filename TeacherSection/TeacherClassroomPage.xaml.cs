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
        private bool isLoading;

        public TeacherClassroomPage(int teacherId, int userId)
        {
            TeacherId = teacherId;
            UserId = userId;

            InitializeComponent();

            Loaded += TeacherClassroomPage_Loaded;

            
        }

        private async void TeacherClassroomPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetLoadingState(true);
            await LoadAsyncData();
            SetLoadingState(false);
        }

        private void SetLoadingState(bool loading)
        {
            isLoading = loading;
            loadingOverlay.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
            TeacherClassroomGrid.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
        }

        private async Task LoadAsyncData()
        {
            Database db = new Database();

            MySqlParameter[] classroomFetchParams =
            {
                new MySqlParameter("@UserId", UserId),
                new MySqlParameter("@TeacherId", TeacherId)
            };

            DataTable ClassroomsData = await Task.Run(() => db.ExecuteQuery("SELECT c.*, u.name as TeacherName FROM Classroom c JOIN Users u ON u.id = @UserId WHERE c.teacherId = @TeacherId", classroomFetchParams));
            
            if (ClassroomsData.Rows.Count > 0)
            {
                for (int i = 0; i < ClassroomsData.Rows.Count; i++)
                {
                    AddClassroom(ClassroomsData.Rows[i]["name"].ToString(), ClassroomsData.Rows[i]["courseCode"].ToString(), ClassroomsData.Rows[i]["teacherName"].ToString(), Convert.ToInt32(ClassroomsData.Rows[i]["studentCount"]), ClassroomsData.Rows[i]["classCode"].ToString(), Convert.ToInt32(ClassroomsData.Rows[i]["id"]));
                }
            }
        }

        private void classroom_add(object sender, RoutedEventArgs e)
        {
            ClassroomTeacherMsgBox classroomTeacherMsgBox = new ClassroomTeacherMsgBox();
            classroomTeacherMsgBox.ShowDialog();
        }

        private void AddClassroom(string coursename, string code, string teacher ,int count,string gcr_code, int classroomId)
        {

            Classroom newClassroom = new Classroom(coursename, code, teacher, count,gcr_code, true, classroomId);
            int index = WrapPanelClassroom.Children.Count - 1;
            newClassroom.Margin = new Thickness(0, 0, 15, 15);
            WrapPanelClassroom.Children.Insert(index,newClassroom);

        }

    }
}


