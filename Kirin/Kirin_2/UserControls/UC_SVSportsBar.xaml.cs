using Kirin_2.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_SVSportsBar.xaml
    /// </summary>
    public partial class UC_SVSportsBar : UserControl
    {
        public UC_SVSportsBar()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ViewAttendance(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            var x = (Button)sender;
            if (((StudentData)x.DataContext) != null)
            {
                string subjectId = App.Current.Properties["SubjectId"].ToString();
                StudentViewModel svm = new StudentViewModel();
                svm.GetStudentAttendanceData(((StudentData)x.DataContext).ID.ToString(), subjectId);

                if (svm.StudentAttendance != null)
                {
                    app.sportsAttendance.DataContext = svm;
                    app.sportsAttendance.DGStudentAttendance.ItemsSource = svm.StudentAttendance;
                    app.sportsAttendance.Visibility = Visibility.Visible;
                }
            }
            this.Visibility = Visibility.Collapsed;
        }

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
