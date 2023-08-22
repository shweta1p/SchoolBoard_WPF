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

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for UC_ModalBox.xaml
    /// </summary>
    public partial class UC_StaffProfileModal : UserControl
    {        
        public UC_StaffProfileModal(string id,string role)
        {          
            InitializeComponent();
            UserViewModel uvm = new UserViewModel();
            uvm.GetStaffProfile(id, role);
            DataContext = uvm;

            //string[] emailcomponents = uvm.StaffProfView_emailaddress.Split('@');
            //uvm.StaffProfView_username = emailcomponents[0];
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
