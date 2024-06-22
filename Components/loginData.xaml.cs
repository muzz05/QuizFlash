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
    /// Interaction logic for student_graph.xaml
    /// </summary>
    public partial class loginData : UserControl
    {
        public loginData(string devicename,string lastlogin)
        {

            InitializeComponent();
            devName.Text = devicename;
            LastDate.Text = lastlogin;

        }
    }
}
