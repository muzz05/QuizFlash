using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace QuizFlash
{
    public partial class QuizDetail_Input : Window
    {

        public QuizDetail_Input()
        {
            InitializeComponent(); 
        }

        private void PART_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Popup popup = button?.Template?.FindName("PART_Popup", button) as Popup;
            if (popup != null)
            {
                popup.IsOpen = !popup.IsOpen;
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string quizName = quizname.Text;
            string perQmarks = permarks.Text;
            DateTime? dueDate = duedate.SelectedDate;
            int marksPerQuestion=Int32.Parse(perQmarks);
            long time = Utilities.GetCurrentTimeInEpoch();
            int tid=GlobalVariables.TeacherId;
            int cid = GlobalVariables.ActiveClassroomId;



            if (string.IsNullOrWhiteSpace(quizName) ||
                string.IsNullOrWhiteSpace(perQmarks) || dueDate == null)
            {
                CustomMessageBox msg = new CustomMessageBox("Input Error", "Please fill in all fields.", "Error");
                msg.Show();
                return;
            }

            long epochTimestamp = ConvertToEpoch(dueDate.Value);

            MessageBox.Show(epochTimestamp.ToString());

            Database db = new Database();

            string sql = "INSERT INTO Quiz(name,totalQuestions,totalMarks,marksPerQuestion,teacherId,classroomId,createTime,dueDate) VALUES(@name,@totalQues,@totalmark,@marksperQ,@teacherid,@classid,@createtime,@duedate)";
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@name",quizName),
                new MySqlParameter("@totalques",1),
                new MySqlParameter("@totalmark",1),
                new MySqlParameter("@marksperQ",perQmarks),
                new MySqlParameter("@teacherid",GlobalVariables.TeacherId),
                new MySqlParameter("@classid",GlobalVariables.ActiveClassroomId),
                new MySqlParameter("@createtime",time),
                new MySqlParameter("@duedate",epochTimestamp)

            };

            db.ExecuteNonQuery(sql, parameters);
            DataTable quizId = db.ExecuteQuery("SELECT id FROM Quiz WHERE createTime=@createtime", new MySqlParameter("@createtime", time));
            this.Close();

            QuizDesignPage addQuestion = new QuizDesignPage(Convert.ToInt32(quizId.Rows[0]["id"]),marksPerQuestion);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Teacher teacher)
                {
                    teacher.TeacherViewFrame.Content = addQuestion;
                }
            }                
                addQuestion.quizDesignPanel.Children.Add(new QuizDesignControl());            

        }

        private long ConvertToEpoch(DateTime date)
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(date);
            long epochTimestamp = dateTimeOffset.ToUnixTimeSeconds();
            return epochTimestamp;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


