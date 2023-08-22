using Kirin_2.Models;
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
    /// Interaction logic for Alerts.xaml
    /// </summary>
    public partial class Alerts : Page
    {
        AlertsViewModel avm = new AlertsViewModel();
        string semesterId = string.Empty;
        string subjectId = string.Empty;
        public App app;
        public Alerts()
        {
            InitializeComponent();
            //FillSemesterCombo();
            subjectId = App.Current.Properties["SubjectId"].ToString();
            app = (App)Application.Current;
            avm.GetStudentAlertsData(); 
            this.DataContext = avm;
            Globals.reset = 0;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            DGStudentAlerts.Height = (ah - 100);
        }

        public System.ComponentModel.ICollectionView SDCollectionView;
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var Searchbox = sender as TextBox;
            SDCollectionView = CollectionViewSource.GetDefaultView(avm.StudentAlertList);
            SDCollectionView.Filter = FilterStaffDirectory;
            refreshGrid();
        }

        public void refreshGrid()
        {
            SDCollectionView.Refresh();
        }

        private bool FilterStaffDirectory(object obj)
        {
            if (obj is StudentAlertData sd)
            {
                return sd.Name.ToUpper().StartsWith(SearchBox.Text.ToUpper());
            }
            return false;
        }

        private void Alters_Click(object sender, RoutedEventArgs e)
        {
            app.addAlerts.Visibility = Visibility.Visible;
            app.addAlerts.SubjectId.Content = subjectId;
        }

        //private void FillSemesterCombo()
        //{
        //    gradebookvm.getSemesterList();
        //    List<GetSemesterList_Result> lstSemesterList = gradebookvm.Semester_List.ToList();

        //    cmbSemesterList.ItemsSource = lstSemesterList;

        //    cmbSemesterList.SelectedValue = lstSemesterList[lstSemesterList.Count() - 1].ID;
        //}
    }
}
