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
    /// Interaction logic for SchoolDB_SBAdminView.xaml
    /// </summary>
    public partial class SchoolDB_SBAdminView : Page
    {
        public SchoolDB_SBAdminView(string ID)
        {
            UserViewModel uvm = new UserViewModel();
            uvm.ViewAdminProfile(Int32.Parse(ID));
            DataContext = uvm;
            InitializeComponent();
        }

    }
}
