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
    /// Interaction logic for student_graph.xaml
    /// </summary>
    public partial class loginData : UserControl
    {

        private int LoggedDeviceId;
        public loginData(int id, string devicename,int lastlogin, int deviceType)
        {
            LoggedDeviceId = id;
            InitializeComponent();
            if (deviceType == 0) {
                DeviceIcon.Kind = MahApps.Metro.IconPacks.PackIconFontAwesomeKind.WindowsBrands;
            } else if (deviceType == 2) {
                DeviceIcon.Kind = MahApps.Metro.IconPacks.PackIconFontAwesomeKind.AppleBrands;
            }

            devName.Text = devicename;
            LastDate.Text = Utilities.ConvertEpochToRelativeTime(lastlogin);

        }

        private void RemoveLoggedDevice(object sender, MouseButtonEventArgs e)
        {
            Database db = new Database();
            string sql = "DELETE FROM LoggedDevices WHERE id = @LoggedDeviceId";
            int result = db.ExecuteNonQuery(sql, new MySqlParameter("@LoggedDeviceId", LoggedDeviceId));
            if (result > 0) {
                if (Parent is Panel panelWrapPanel)
                {
                    panelWrapPanel.Children.Remove(this);
                }
            }
        }
    }
}
