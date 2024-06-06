using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for StudentDataPage.xaml
    /// </summary>
    public partial class StudentDataPage : Page
    {
        public StudentDataPage()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "John Doe", Department = "SE", Email = "john.doe@gmail.com", RollNumber = "ND-10001" });
            members.Add(new Member { Number = "2", Character = "R", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Reza Alavi", Department = "IM", Email = "reza110@hotmail.com", RollNumber = "ND-10002" });
            members.Add(new Member { Number = "3", Character = "D", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Dennis Castillo", Department = "ME", Email = "deny.cast@gmail.com", RollNumber = "ND-10003" });
            members.Add(new Member { Number = "4", Character = "G", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Gabriel Cox", Department = "CSIT", Email = "coxcox@gmail.com", RollNumber = "ND-10004" });
            members.Add(new Member { Number = "5", Character = "L", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Lena Jones", Department = "CIS", Email = "lena.offi@hotmail.com", RollNumber = "ND-10005" });
            members.Add(new Member { Number = "6", Character = "B", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Benjamin Caliword", Department = "EE", Email = "beni12@hotmail.com", RollNumber = "ND-10006" });
            members.Add(new Member { Number = "7", Character = "S", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Sophia Muris", Department = "SE", Email = "sophi.muri@gmail.com", RollNumber = "ND-10007" });
            members.Add(new Member { Number = "8", Character = "A", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Ali Pormand", Department = "IM", Email = "alipor@yahoo.com", RollNumber = "ND-10008" });
            members.Add(new Member { Number = "9", Character = "F", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Frank Underwood", Department = "ME", Email = "frank@yahoo.com", RollNumber = "ND-10009" });
            members.Add(new Member { Number = "10", Character = "S", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Saeed Dasman", Department = "CSIT", Email = "saeed.dasi@hotmail.com", RollNumber = "ND-10010" });

            members.Add(new Member { Number = "11", Character = "J", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "John Doe", Department = "CIS", Email = "john.doe@gmail.com", RollNumber = "ND-10011" });
            members.Add(new Member { Number = "12", Character = "R", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Reza Alavi", Department = "EE", Email = "reza110@hotmail.com", RollNumber = "ND-10012" });
            members.Add(new Member { Number = "13", Character = "D", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Dennis Castillo", Department = "SE", Email = "deny.cast@gmail.com", RollNumber = "ND-10013" });
            members.Add(new Member { Number = "14", Character = "G", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Gabriel Cox", Department = "IM", Email = "coxcox@gmail.com", RollNumber = "ND-10014" });
            members.Add(new Member { Number = "15", Character = "L", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Lena Jones", Department = "ME", Email = "lena.offi@hotmail.com", RollNumber = "ND-10015" });
            members.Add(new Member { Number = "16", Character = "B", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Benjamin Caliword", Department = "CSIT", Email = "beni12@hotmail.com", RollNumber = "ND-10016" });
            members.Add(new Member { Number = "17", Character = "S", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Sophia Muris", Department = "CIS", Email = "sophi.muri@gmail.com", RollNumber = "ND-10017" });
            members.Add(new Member { Number = "18", Character = "A", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Ali Pormand", Department = "EE", Email = "alipor@yahoo.com", RollNumber = "ND-10018" });
            members.Add(new Member { Number = "19", Character = "F", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Frank Underwood", Department = "SE", Email = "frank@yahoo.com", RollNumber = "ND-10019" });
            members.Add(new Member { Number = "20", Character = "S", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Saeed Dasman", Department = "IM", Email = "saeed.dasi@hotmail.com", RollNumber = "ND-10020" });

            members.Add(new Member { Number = "21", Character = "J", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "John Doe", Department = "ME", Email = "john.doe@gmail.com", RollNumber = "ND-10021" });
            members.Add(new Member { Number = "22", Character = "R", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Reza Alavi", Department = "CSIT", Email = "reza110@hotmail.com", RollNumber = "ND-10022" });
            members.Add(new Member { Number = "23", Character = "D", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Dennis Castillo", Department = "CIS", Email = "deny.cast@gmail.com", RollNumber = "ND-10023" });
            members.Add(new Member { Number = "24", Character = "G", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Gabriel Cox", Department = "EE", Email = "coxcox@gmail.com", RollNumber = "ND-10024" });
            members.Add(new Member { Number = "25", Character = "L", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Lena Jones", Department = "SE", Email = "lena.offi@hotmail.com", RollNumber = "ND-10025" });
            members.Add(new Member { Number = "26", Character = "B", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Benjamin Caliword", Department = "IM", Email = "beni12@hotmail.com", RollNumber = "ND-10026" });
            members.Add(new Member { Number = "27", Character = "S", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Sophia Muris", Department = "ME", Email = "sophi.muri@gmail.com", RollNumber = "ND-10027" });
            members.Add(new Member { Number = "28", Character = "A", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Ali Pormand", Department = "CSIT", Email = "alipor@yahoo.com", RollNumber = "ND-10028" });
            members.Add(new Member { Number = "29", Character = "F", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Frank Underwood", Department = "CIS", Email = "frank@yahoo.com", RollNumber = "ND-10029" });
            members.Add(new Member { Number = "30", Character = "S", BgColor = (Brush)converter.ConvertFromString("#22202f"), Name = "Saeed Dasman", Department = "EE", Email = "saeed.dasi@hotmail.com", RollNumber = "ND-10030" });

            membersDataGrid.ItemsSource = members;
        }

       
        public class Member
        {
            public string Character { get; set; }
            public Brush BgColor { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public string Email { get; set; }
            public string RollNumber { get; set; }
        }
    }
}
