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
using MySql.Data.MySqlClient;

namespace QuizFlash
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : UserControl
    {
        public UserInfo()
        {
            Database db = new Database();

            int quizCountInt = 0;
            int classroomCountInt = 0;
            int flashcardCountInt = 0;
            int successInt = 0;

            if (!GlobalVariables.IsTeacher)
            {
                string sql = "SELECT COUNT(q.id) as QuizCount FROM Quiz q JOIN Classroom c ON c.id = q.classroomId JOIN ClassroomStudents cs ON cs.classroomId = c.id WHERE cs.studentId = @StudentID";
                object QuizCount = db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId));
                quizCountInt = Convert.ToInt32(QuizCount);

                sql = "SELECT COUNT(cs.id) as ClassroomCount FROM ClassroomStudents cs WHERE cs.studentId = @StudentID";
                object ClassroomCount = db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId));
                classroomCountInt = Convert.ToInt32(ClassroomCount);

                sql = "SELECT COUNT(id) as FlashCardCount FROM Flashcards WHERE studentId = @StudentID";
                object FlashcardCount = db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId));
                flashcardCountInt = Convert.ToInt32(FlashcardCount);

                sql = "SELECT AVG((r.marksObtained / q.totalMarks) * 100) AS SuccessRate FROM Result r JOIN Quiz q ON r.quizId = q.id WHERE r.studentId = @StudentId GROUP BY r.studentId";
                object success = db.ExecuteScalar(sql, new MySqlParameter("@StudentId", GlobalVariables.StudentId));
                successInt = Convert.ToInt32(success);

            }
            InitializeComponent();
            studentName.Text = GlobalVariables.Username;
            isStudentorTeacher.Text=GlobalVariables.IsTeacher ? "Teacher" : "Student";
            classroomNo.Text = classroomCountInt.ToString();
            flashcardNo.Text = flashcardCountInt.ToString();
            quizAttempted.Text = quizCountInt.ToString();
            quizProgress.Value= successInt;
            QuizPercentage.Text = successInt.ToString() + "%";

        }
    }
}
