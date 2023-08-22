using Kirin_2.Models;
using Kirin_2.Pages;
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
    /// Interaction logic for UC_StudentProfileModal.xaml
    /// </summary>
    public partial class UC_StudentProfileModal : UserControl
    {
        public UC_StudentProfileModal()
        {
            InitializeComponent();
            GradeBookViewModel gbvm = new GradeBookViewModel();
            DataContext = gbvm;
            lblAlert.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            string comment = txtComment.Text;
            string stdId = StudentId.Content.ToString();
            string userName = App.Current.Properties["USERNAME"].ToString();

            if (stdId != "0" && !string.IsNullOrWhiteSpace(stdId) && !string.IsNullOrWhiteSpace(comment))
            {
                KIRINEntities1 kirinEntities = new KIRINEntities1();
                kirinEntities.AddStudentComments(stdId, comment, userName);

                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                {
                    //Page refresh
                    mw.MainWindowFrame.Content = new Classes();
                }

                lblAlert.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblAlert.Visibility = Visibility.Visible;
            }
        }
    }

}
