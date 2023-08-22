using Kirin_2.ViewModel;
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

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for SchoolDB_TeacherView.xaml
    /// </summary>
    public partial class SchoolDB_TeacherView : Page
    {
        public SchoolDB_TeacherView(string ID)
        {
            UserViewModel uvm = new UserViewModel();
            uvm.ViewTeacherProfile(Int32.Parse(ID));
            DataContext = uvm;
            InitializeComponent();
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var brdr = (Border)sender;
            string tag = brdr.Tag.ToString();
            ClassInfo classInfoWin = new ClassInfo(brdr.Tag.ToString());
            classInfoWin.Show();
        }
    }
}
