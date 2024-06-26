using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace QuizFlash
{
    public partial class QuizDetail_Input : Window
    {

        public QuizDetail_Input()
        {
            InitializeComponent(); 
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string quizName = quizname.Text;
            string questions = no_ques.Text;
            string perQmarks = permarks.Text;
            DateTime? dueDate = duedate.SelectedDate;
            int a=Int32.Parse(questions);
            int b=Int32.Parse(perQmarks);
            int totalmarks = a * b;
            long time = Utilities.GetCurrentTimeInEpoch();
            int tid=GlobalVariables.TeacherId;
            int cid = GlobalVariables.ActiveClassroomId;



            if (string.IsNullOrWhiteSpace(quizName) || string.IsNullOrWhiteSpace(questions) ||
                string.IsNullOrWhiteSpace(perQmarks) || dueDate == null)
            {
                CustomMessageBox msg = new CustomMessageBox("Input Error", "Please fill in all fields.", "Error");
                msg.Show();
                return;
            }

            long epochTimestamp = ConvertToEpoch(dueDate.Value);

            Database db = new Database();

            string sql="INSERT INTO quiz(name,totalQuestions,totalMarks,marksPerQuestion,teacherId,classroomId,createTime,dueDate) VALUES(@name,@totalQues,@totalmark,@marksperQ,@teacherid,@classid,@createtime,@duedate)";
            MySqlParameter[] parameters =
            {
                new MySqlParameter("@name",quizName),
                new MySqlParameter("@totalques",questions),
                new MySqlParameter("@totalmark",totalmarks),
                new MySqlParameter("@marksperQ",perQmarks),
                new MySqlParameter("@teacherid",GlobalVariables.TeacherId),
                new MySqlParameter("@classid",GlobalVariables.ActiveClassroomId),
                new MySqlParameter("@createtime",time),
                new MySqlParameter("@duedate",epochTimestamp)

            };

            db.ExecuteNonQuery(sql, parameters);
            DataTable quizId=db.ExecuteQuery("SELECT id FROM quiz WHERE createTime=@createtime", new MySqlParameter("@createtime", time));
            this.Close();

            QuizDesignPage addQuestion = new QuizDesignPage(Convert.ToInt32(quizId.Rows[0]["Id"]));

            for (int i=0; i<Convert.ToInt32(questions); i++)
            {
                QuizDesignControl quizDesignControl = new QuizDesignControl();
                addQuestion.quizDesignPanel.Children.Add(quizDesignControl);
            }

            //CustomMessageBox info = new CustomMessageBox("Quiz Details Saved",
            //    $"Quiz Name: {quizName}\nQuestions: {questions}\nTotal Marks: {perQmarks}\nDue Date: {dueDate.Value.ToShortDateString()}",
            //    "Done");
            //info.Width = 450;
            //info.Height = 250;
            //info.Show();

            //this.Close();
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


