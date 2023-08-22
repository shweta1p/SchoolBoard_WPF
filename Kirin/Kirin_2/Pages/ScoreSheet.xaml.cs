using Kirin_2.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for ScoreSheet.xaml
    /// </summary>
    public partial class ScoreSheet : Page
    {
        GradeBookViewModel gradebookvm = new GradeBookViewModel();
        string semesterId = string.Empty;

        public ScoreSheet()
        {
            InitializeComponent();
            //FillSemesterCombo();
            SubjectViewModel svm = new SubjectViewModel();
            string subjectId = App.Current.Properties["SubjectId"].ToString();
            string schoolId = App.Current.Properties["SchoolId"].ToString();
            string userName = App.Current.Properties["USERNAME"].ToString();
            semesterId = svm._schooldata.FirstOrDefault().SemesterId;

            gradebookvm.getStudentList(subjectId, schoolId, userName); //semesterId
            this.DataContext = gradebookvm;
            Globals.reset = 0;
            //SearchBox.TextChanged += SearchBox_TextChanged;
        }

        private void ViewScoreSheetPagePerStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        // NavigationService.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "ScoreSheet_StudentView.xaml?ID=" + (sender as Button).Tag), UriKind.RelativeOrAbsolute));
                        this.NavigationService.Navigate(new ScoreSheet_StudentView((sender as Button).Tag.ToString(), semesterId));
                        // (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", "ScoreSheet_StudentView?ID="+(sender as Button).Tag, ".xaml"), UriKind.RelativeOrAbsolute));
                    }
                }
            }
            catch (Exception ee)
            {

            }
        }

        //private void cmbSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var semester = cmbSemesterList.SelectedValue;

        //    if (semester != null)
        //    {
        //        gradebookvm.getStudentList(subjectId, schoolId, semester.ToString(), userName);
        //    }
        //    else
        //    {
        //        FillSemesterCombo();
        //    }
        //}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            DGScoresheet.Height = (ah - 100);
        }

        public System.ComponentModel.ICollectionView SDCollectionView;
        public void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (DGScoresheet.ItemsSource != null && SearchBox.Text != ", ")
            if (gradebookvm.STUDENTLIST != null && SearchBox.Text != ", ") 
            {
                var Searchbox = sender as TextBox;
                SDCollectionView = CollectionViewSource.GetDefaultView(gradebookvm.STUDENTLIST); //DGScoresheet.ItemsSource
                SDCollectionView.Filter = FilterStaffDirectory;
                
                refreshGrid();
            }
        }

        public void refreshGrid()
        {
            SDCollectionView.Refresh();
        }

        private bool FilterStaffDirectory(object obj)
        {
            if (obj is STUDENTDESC sd)
            {
                return sd.FULLNAME.ToUpper().StartsWith(SearchBox.Text.ToUpper());
            }
            return false;
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
