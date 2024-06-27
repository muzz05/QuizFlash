using QuizFlash;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for StudentHomePage.xaml
    /// </summary>
    public partial class StudentHomePage : Page
    {


        private string[] quotes = new string[]
            {
            "Education is the most powerful weapon which you can use to change the world. - Nelson Mandela",
            "Success is not final, failure is not fatal: It is the courage to continue that counts. - Winston Churchill",
            "The roots of education are bitter, but the fruit is sweet. - Aristotle",
            "Education is not the filling of a pail, but the lighting of a fire. - William Butler Yeats",
            "Success is walking from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "Education is the passport to the future, for tomorrow belongs to those who prepare for it today. - Malcolm X",
            "Success usually comes to those who are too busy to be looking for it. - Henry David Thoreau",
            "An investment in knowledge pays the best interest. - Benjamin Franklin",
            "The journey of a thousand miles begins with one step. - Lao Tzu",
            "Education is not preparation for life; education is life itself. - John Dewey",
            "Success is not how high you have climbed, but how you make a positive difference to the world. - Roy T. Bennett",
            "Learning is not attained by chance, it must be sought for with ardor and attended to with diligence. - Abigail Adams",
            "Success is not in what you have, but who you are. - Bo Bennett",
            "The capacity to learn is a gift; the ability to learn is a skill; the willingness to learn is a choice. - Brian Herbert",
            "Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful. - Albert Schweitzer",
            "Education is the key to unlocking the world, a passport to freedom. - Oprah Winfrey",
            "Success is liking yourself, liking what you do, and liking how you do it. - Maya Angelou",
            "The beautiful thing about learning is that no one can take it away from you. - B.B. King",
            "Success is not measured by what you accomplish, but by the opposition you have encountered, and the courage with which you have maintained the struggle against overwhelming odds. - Orison Swett Marden",
            "Education breeds confidence. Confidence breeds hope. Hope breeds peace. - Confucius",
            "Success is stumbling from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "The goal of education is the advancement of knowledge and the dissemination of truth. - John F. Kennedy",
            "Success is to be measured not so much by the position that one has reached in life as by the obstacles which he has overcome. - Booker T. Washington",
            "The roots of education are bitter, but the fruit is sweet. - Aristotle",
            "Success is not how high you have climbed, but how you make a positive difference to the world. - Roy T. Bennett",
            "Education is the most powerful weapon which you can use to change the world. - Nelson Mandela",
            "Success is not in what you have, but who you are. - Bo Bennett",
            "The capacity to learn is a gift; the ability to learn is a skill; the willingness to learn is a choice. - Brian Herbert",
            "Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful. - Albert Schweitzer",
            "Education is the key to unlocking the world, a passport to freedom. - Oprah Winfrey",
            "Success is liking yourself, liking what you do, and liking how you do it. - Maya Angelou",
            "The beautiful thing about learning is that no one can take it away from you. - B.B. King",
            "Success is not measured by what you accomplish, but by the opposition you have encountered, and the courage with which you have maintained the struggle against overwhelming odds. - Orison Swett Marden",
            "Education breeds confidence. Confidence breeds hope. Hope breeds peace. - Confucius",
            "Success is stumbling from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "The goal of education is the advancement of knowledge and the dissemination of truth. - John F. Kennedy",
            "Success is to be measured not so much by the position that one has reached in life as by the obstacles which he has overcome. - Booker T. Washington",
            "The roots of education are bitter, but the fruit is sweet. - Aristotle",
            "Success is not how high you have climbed, but how you make a positive difference to the world. - Roy T. Bennett",
            "Education is the most powerful weapon which you can use to change the world. - Nelson Mandela",
            "Success is not in what you have, but who you are. - Bo Bennett",
            "The capacity to learn is a gift; the ability to learn is a skill; the willingness to learn is a choice. - Brian Herbert",
            "Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful. - Albert Schweitzer",
            "Education is the key to unlocking the world, a passport to freedom. - Oprah Winfrey",
            "Success is liking yourself, liking what you do, and liking how you do it. - Maya Angelou",
            "The beautiful thing about learning is that no one can take it away from you. - B.B. King",
            "Success is not measured by what you accomplish, but by the opposition you have encountered, and the courage with which you have maintained the struggle against overwhelming odds. - Orison Swett Marden",
            "Education breeds confidence. Confidence breeds hope. Hope breeds peace. - Confucius",
            "Success is stumbling from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "The goal of education is the advancement of knowledge and the dissemination of truth. - John F. Kennedy",
            "Success is to be measured not so much by the position that one has reached in life as by the obstacles which he has overcome. - Booker T. Washington",
            "The roots of education are bitter, but the fruit is sweet. - Aristotle",
            "Success is not how high you have climbed, but how you make a positive difference to the world. - Roy T. Bennett",
            "Education is the most powerful weapon which you can use to change the world. - Nelson Mandela",
            "Success is not in what you have, but who you are. - Bo Bennett",
            "The capacity to learn is a gift; the ability to learn is a skill; the willingness to learn is a choice. - Brian Herbert",
            "Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful. - Albert Schweitzer",
            "Education is the key to unlocking the world, a passport to freedom. - Oprah Winfrey",
            "Success is liking yourself, liking what you do, and liking how you do it. - Maya Angelou",
            "The beautiful thing about learning is that no one can take it away from you. - B.B. King",
            "Success is not measured by what you accomplish, but by the opposition you have encountered, and the courage with which you have maintained the struggle against overwhelming odds. - Orison Swett Marden",
            "Education breeds confidence. Confidence breeds hope. Hope breeds peace. - Confucius",
            "Success is stumbling from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "The goal of education is the advancement of knowledge and the dissemination of truth. - John F. Kennedy",
            "Success is to be measured not so much by the position that one has reached in life as by the obstacles which he has overcome. - Booker T. Washington",
            "The roots of education are bitter, but the fruit is sweet. - Aristotle",
            "Success is not how high you have climbed, but how you make a positive difference to the world. - Roy T. Bennett",
            "Education is the most powerful weapon which you can use to change the world. - Nelson Mandela",
            "Success is not in what you have, but who you are. - Bo Bennett",
            "The capacity to learn is a gift; the ability to learn is a skill; the willingness to learn is a choice. - Brian Herbert",
            "Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful. - Albert Schweitzer",
            "Education is the key to unlocking the world, a passport to freedom. - Oprah Winfrey",
            "Success is liking yourself, liking what you do, and liking how you do it. - Maya Angelou",
            "The beautiful thing about learning is that no one can take it away from you. - B.B. King",
            "Success is not measured by what you accomplish, but by the opposition you have encountered, and the courage with which you have maintained the struggle against overwhelming odds. - Orison Swett Marden",
            "Education breeds confidence. Confidence breeds hope. Hope breeds peace. - Confucius",
            "Success is stumbling from failure to failure with no loss of enthusiasm. - Winston Churchill",
            "The goal of education is the advancement of knowledge and the dissemination of truth. - John F. Kennedy",
            "Success is to be measured not so much by the position that one has reached in life as by the obstacles which he has overcome. - Booker T. Washington"
        };


        public StudentHomePage()
        {
            Database db = new Database();


            string sql = "SELECT * FROM LoggedDevices WHERE userId = @UserId";
            DataTable AllDevices = db.ExecuteQuery(sql, new MySqlParameter("@UserId", GlobalVariables.UserId));

            sql = "SELECT q.name as quizName, q.dueDate ,c.name as classroomName FROM ClassroomStudents cs JOIN Classroom c ON cs.classroomId = c.id JOIN Quiz q ON q.classroomId = c.id WHERE cs.studentId = @StudentId AND q.dueDate > @CurrentDate";
            MySqlParameter[] resultParams =
            {
                new MySqlParameter("@StudentId", GlobalVariables.StudentId),
                new MySqlParameter("@CurrentDate", Utilities.GetCurrentTimeInEpoch()),
            };
            DataTable QuizesResult = db.ExecuteQuery(sql, resultParams);

            InitializeComponent();
            AddUserInfo(GlobalVariables.Username,GlobalVariables.IsTeacher,30 ,12, 7, 80);

            for (int i = 0; i < AllDevices.Rows.Count; i++)
            {
                AddLoggedDevices(Convert.ToInt32(AllDevices.Rows[i]["id"]), AllDevices.Rows[i]["deviceName"].ToString(), Convert.ToInt32(AllDevices.Rows[i]["lastLogin"]), Convert.ToInt32(AllDevices.Rows[i]["deviceType"]));
            }

            for (int i = 0; i < QuizesResult.Rows.Count; i++)
            {
                AddRecentQuiz(QuizesResult.Rows[i]["classroomName"].ToString(), QuizesResult.Rows[i]["quizName"].ToString(), Convert.ToInt64(QuizesResult.Rows[i]["dueDate"]));
            }


            //var cards = new[]
            //{
            //    new StudentHomepageInfoCard("Software Engineering", "Grand Quiz on 7/9/24"),
            //    new StudentHomepageInfoCard("Applied Physics", "Chapter 1 Test on 9/6/24"),
            //    new StudentHomepageInfoCard("Mathematics", "Midterm Exam on 8/15/24"),
            //    new StudentHomepageInfoCard("History", "Presentation on 9/1/24"),
            //    new StudentHomepageInfoCard("Software Engineering", "Final Exam on 9/1/24"),
            //    new StudentHomepageInfoCard("CIS", "Final Exam on 9/1/24"),
            //    new StudentHomepageInfoCard("Islamiat", "Final Exam on 11/1/24")



            //};

            //foreach (var card in cards)
            //{
            //    card.Margin = new Thickness(8);
            //    infocards.Children.Add(card);
            //}



            //var device_detail = new[]
            //{
            //    new loginData("iPhone 12", 1719163903, 0),
            //    new loginData("Samsung Galaxy S21", 1719168903,2),
            //    new loginData("Google Pixel 6", 1719168103,0),
            //    new loginData("OnePlus 9", 1719368103,1)
            //};

            //foreach (var dev in device_detail) { 

            //    dev.Margin = new Thickness(6);
            //    devices.Children.Add(dev);

            //}


            AddQuote();
        }

        private void AddQuote()
        {
            Random random = new Random();
            int index = random.Next(quotes.Length);
            string randomQuote = quotes[index];

            studentQuote quo = new studentQuote(randomQuote);

            quote_panel.Children.Add(quo);
        }

        private void AddLoggedDevices(int id, string deviceName, int lastLogin, int deviceType)
        {
            loginData newDevice = new loginData(id, deviceName, lastLogin, deviceType);
            newDevice.Margin = new Thickness(6);
            devices.Children.Add(newDevice);
        }

        private void AddRecentQuiz(string className, string announcement, long epoch)
        {
            StudentHomepageInfoCard newCard = new StudentHomepageInfoCard(className, announcement, epoch);
            newCard.Margin = new Thickness(8);
            infocards.Children.Add(newCard);
        }





        private void AddUserInfo(string name, bool isTeacher, int classNo,int flashcardNum, int totalQuiz, int successRate)
        {
            string studentOrTeacher = isTeacher ? "Teacher" : "Student";
            UserInfo newInfo = new UserInfo(name, studentOrTeacher, classNo,flashcardNum, totalQuiz, successRate);
            newInfo.Margin = new Thickness(5,5,25,5);
            userInfo.Children.Add(newInfo);


        }


    }


}

