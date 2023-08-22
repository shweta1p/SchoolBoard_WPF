using Kirin_2.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class AttendancePage : Page 
    {
        string userName = App.Current.Properties["USERNAME"].ToString();
        public AttendancePage()
        {
            InitializeComponent();
            CombinedViewModel cvm = new CombinedViewModel();
            this.DataContext = cvm;

            txtSchool.Text = cvm.SubjectVM._schooldata.FirstOrDefault().SchoolName;
            txtSemester.Text = cvm.SubjectVM._schooldata.FirstOrDefault().Semester;
        }

        private void ItemContainerTemplate_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            SubjectDetails subjDetailswin = new SubjectDetails();
            SubjectViewModel subjVM = new SubjectViewModel();

            subjVM.getSubject(int.Parse(item.Tag.ToString()));
            subjVM.SelectedSubject = subjVM.getSubject(int.Parse(item.Tag.ToString()));
            subjVM.SelectedSubjectSchedule = subjVM.getSubjectSchedule(int.Parse(item.Tag.ToString()));
            
            subjDetailswin.DataContext =  subjVM;
            subjDetailswin.Show();
        }

        private void Button_ClassAttendance(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;

            //(window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", page, ".xaml"), UriKind.RelativeOrAbsolute));
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                //SINGLE DAY VIEW
                mw.MainWindowFrame.Content = new ClassAttendance(item.Tag.ToString(),DateTime.Now);
                //MULTIDAYVIEW
                //mw.MainWindowFrame.Content = new ClassMultiDayAttendance(item.Tag.ToString());
            }
        }

        private void Button_ClassMultidayAttendance(object sender, RoutedEventArgs e)
        {
            var item = sender as Button;
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {                
                //MULTIDAYVIEW
                mw.MainWindowFrame.Content = new ClassMultiDayAttendance(item.Tag.ToString());
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            DGClasses.Height = (ah /2 );
            
            //DGClasses.Width= aw-10;
          //  BorderClasses.Height = DGClasses.Height+10;
        }

    }

    public class CombinedViewModel
    {
        //string userName = App.Current.Properties["USERNAME"].ToString();
        public SchoolViewModel SchoolVM { get; set; }
        public SubjectViewModel SubjectVM { get; set; }

        public CombinedViewModel()
        {
            this.SchoolVM = new SchoolViewModel();
            this.SubjectVM = new SubjectViewModel();

            this.SubjectVM.SelectedSchool = this.SchoolVM.LstSchool[0];
        }
    }

}
