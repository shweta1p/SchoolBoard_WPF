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
    /// Interaction logic for UC_AddAssignment.xaml
    /// </summary>
    public partial class UC_AddAssignment : UserControl
    {
        GradeBookViewModel gradebookvm = new GradeBookViewModel();
        public UC_AddAssignment()
        {
            InitializeComponent();
            FillSemesterCombo();
            gradebookvm.getAssignmentCategory();
            this.DataContext = gradebookvm;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void AddAssignmentModal_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void Refresh()
        {
            cmbSemester.SelectedValue = 0;
            txtname.Text = string.Empty;
            txtabvr.Text = string.Empty;
            cmbcategory.SelectedValue = 0;
            txtpoints.Text = string.Empty;
            txtweight.Text = string.Empty;
            datepicker.SelectedDate = null;
            cmbscoreType.SelectedValue = 0;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string subjectId = App.Current.Properties["SubjectId"].ToString();
            string reportingTerm = cmbSemester.SelectedValue.ToString();
            string name = txtname.Text;
            string abvr = txtabvr.Text;
            string category = cmbcategory.SelectedValue.ToString();
            string point = txtpoints.Text;
            string weight = txtweight.Text;
            DateTime date = (DateTime)datepicker.SelectedDate;
            string duedate = date.ToString("dd-MM-yyyy");
            string scoreType = cmbscoreType.SelectedValue.ToString();
            bool isLoaded = (bool)cbYes.IsChecked;

            KIRINEntities1 kirinEntities = new KIRINEntities1();
            kirinEntities.AddAssignment(reportingTerm, name, abvr, category, point, weight, duedate, subjectId, scoreType, isLoaded);

            Refresh();
            this.Visibility = Visibility.Collapsed;
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainWindowFrame.Content = new AssignmentMaker();
            }

            //foreach (Window window in Application.Current.Windows)
            //{
            //    if (window.GetType() == typeof(MainWindow))
            //    {
            //        (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "AssignmentMaker.xaml"), UriKind.RelativeOrAbsolute));
            //    }
            //}            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            this.Visibility = Visibility.Collapsed;
        }

        private void FillSemesterCombo()
        {
            gradebookvm.getSemesterList();
            List<GetSemesterList_Result> lstSemesterList = gradebookvm.Semester_List.ToList();

            cmbSemester.ItemsSource = lstSemesterList;
        }
    }
}
