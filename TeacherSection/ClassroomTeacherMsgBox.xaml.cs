using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for ClassroomTeacherMsgBox.xaml
    /// </summary>
    public partial class ClassroomTeacherMsgBox : Window
    {
        public ClassroomTeacherMsgBox()
        {
            InitializeComponent();
            playSimpleSound();
            

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard scaleDownStoryboard = (Storyboard)this.Resources["ScaleDownAnimation"];
            scaleDownStoryboard.Begin();
        }


        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "soundeffect.wav"));
            simpleSound.Play();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddClassroomToDB(object sender, RoutedEventArgs e)
        {
            if(classnametb.Text == "" || coursecodetb.Text == "")
            {
                CustomMessageBox error = new CustomMessageBox("Empty Input", "Classname and Course name is mandatory", "Error");
                error.ShowDialog();
            }

            Database db = new Database();

            string sql = "INSERT INTO Classroom (name, teacherId, studentCount, courseCode, classCode) VALUES (@Name, @TeacherId, 0, @CourseCode, @ClassCode)";

            MySqlParameter[] insertParams =
            {
                new MySqlParameter("@Name", classnametb.Text.ToString()),
                new MySqlParameter("@TeacherId", GlobalVariables.TeacherId),
                new MySqlParameter("@CourseCode", coursecodetb.Text.ToString()),
                new MySqlParameter("@ClassCode", UniqueCodeGenerator.GenerateUniqueCode()),
            };

            int insertResult = db.ExecuteNonQuery(sql, insertParams);

            if (insertResult != 0) 
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Teacher teacher)
                    {
                        teacher.TeacherViewFrame.Content = new TeacherClassroomPage(GlobalVariables.TeacherId, GlobalVariables.UserId);
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Some Error occured");
            }
        }
    }
}
