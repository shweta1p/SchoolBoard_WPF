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

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_AttendanceComment.xaml
    /// </summary>
    public partial class UC_AttendanceComment : UserControl
    {
        public UC_AttendanceComment()
        {
            InitializeComponent();
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            commentbox.Clear();
            this.Visibility = Visibility.Collapsed;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string classAttendaceId = ClassAttendanceId.Content.ToString();
            string comment = commentbox.Text;
            string subjectId = SubjectId.Content.ToString();
            string stdId = StudentId.Content.ToString();
            string schoolId = SchoolId.Content.ToString();
            string userName = App.Current.Properties["USERNAME"].ToString();

            if (classAttendaceId != "0" && !string.IsNullOrWhiteSpace(classAttendaceId))
            {
                KIRINEntities1 kirinEntities = new KIRINEntities1();
                kirinEntities.AddComments(classAttendaceId, comment, schoolId, stdId, subjectId, userName);

                this.Visibility = Visibility.Collapsed;

                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                {
                    //SINGLE DAY VIEW
                    mw.MainWindowFrame.Content = new ClassAttendance(subjectId, DateTime.Now);

                    MainWindowVM mvm = new MainWindowVM(userName);
                    mw.DGNew.DataContext = mvm.NewNotificationList;
                }
            }
        }

        private void CommentSave_Click(object sender, RoutedEventArgs e)
        {
            string classAttendaceId = ClassAttendanceId.Content.ToString();
            string comment = commentbox2.Text;
            string subjectId = SubjectId.Content.ToString();
            string stdId = StudentId.Content.ToString();
            string schoolId = SchoolId.Content.ToString();
            string userName = App.Current.Properties["USERNAME"].ToString();

            if (classAttendaceId != "0" && !string.IsNullOrWhiteSpace(classAttendaceId))
            {
                KIRINEntities1 kirinEntities = new KIRINEntities1();
                kirinEntities.AddComments(classAttendaceId, comment, schoolId, stdId, subjectId, userName);

                this.Visibility = Visibility.Collapsed;

                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                {
                    MainWindowVM mvm = new MainWindowVM(userName);

                    mw.MainWindowFrame.Content = new ClassAttendance(subjectId, DateTime.Now);
                    mw.DGNew.ItemsSource = mvm.NewNotificationList.ToList();
                }
            }
        }
        
    }
}
