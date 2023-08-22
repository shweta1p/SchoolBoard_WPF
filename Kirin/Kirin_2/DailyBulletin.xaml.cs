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
using System.Windows.Shapes;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for DailyBulletin.xaml
    /// </summary>
    public partial class DailyBulletin : Window
    {
        public DailyBulletin()
        {
            InitializeComponent();
            AnnouncementHeader.Text = "Saint Andre Bessette Catholic Secondary School Bulletin for " + DateTime.UtcNow;
            AnnouncementContent.Text = "  Schools are closed. Buses are cancelled.";
        }
    }
}
