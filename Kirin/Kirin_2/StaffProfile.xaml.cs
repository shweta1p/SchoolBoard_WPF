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
using System.Windows.Shapes;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for StaffProfile.xaml
    /// </summary>
    public partial class StaffProfile : Window
    {
        public StaffProfile(STAFF_DIRECTORY_DESC staff)
        {
            InitializeComponent();
            UserViewModel uvm = new UserViewModel();
            //uvm.GetStaffProfile(staff.ID);

            ProfPic.ImageSource = uvm.STAFF_PHOTO;
            this.Title = staff.NAME;
            Name.Content = staff.NAME;
            EmailAddress.Text = staff.EMAIL;
            EmpID.Content = staff.STAFF_ID;

            string[] emailcomponents = staff.EMAIL.Split('@');
            Username.Content = emailcomponents[0];
        }
    }
}
