using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Syncfusion.UI.Xaml.Grid;
using System.Collections;
using Syncfusion.Data;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Media;
using Syncfusion.Windows.Controls.Notification;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for StaffDirectory.xaml
    /// </summary>
    public partial class StaffDirectory : Page
    {
        public string schoolId = "1";
        UserViewModel uvm = new UserViewModel();

        public StaffDirectory()
        {
            InitializeComponent();
            Globals.reset = 0;
            //busyIndicator.AnimationType = AnimationTypes.Gear;
            //busyIndicator.Foreground = Brushes.Blue;
            //busyIndicator.Background = Brushes.Transparent;
            //busyIndicator.ViewboxHeight = 100;
            //busyIndicator.ViewboxWidth = 100;
            //busyIndicator.IsBusy = true;
            //busyIndicator.Visibility = Visibility.Visible;
            ////DGStaff.Items.Add(busyIndicator);
            //BackgroundWorker worker = new BackgroundWorker();

            ////this is where the long running process should go
            //worker.DoWork += (o, ea) =>
            //{
            //    //no direct interaction with the UI is allowed from this method
            //    //uvm.LoadStaffDirectory(schoolId, "All");
            //};

            //worker.RunWorkerCompleted += async (o, ea) =>
            //{
            //    await Task.Delay(1000);
            //    //work has completed. you can now interact with the UI
            //    busyIndicator.IsBusy = false;
            //    busyIndicator.Visibility = Visibility.Collapsed;
            //    //_busyIndicator.IsBusy = false;
            //    displayAllEmail();
            //};
            //worker.RunWorkerAsync();

            //uvm.LoadStaffDirectory(schoolId, "All");
            //DataContext = uvm;
            dataPager.OnDemandLoading += dataPager_OnDemandLoading;
            //displayAllEmail();
        }

        private async void dataPager_OnDemandLoading(object sender, Syncfusion.UI.Xaml.Controls.DataPager.OnDemandLoadingEventArgs args)
        {
            var source = await GetStaffData();

            //Data's loaded to SfDataPager dynamically     
            dataPager.LoadDynamicItems(args.StartIndex, source.Take(args.PageSize));

            if (DGStaff.ItemsSource != null)
            {
                DGStaff.View.Filter = FilterRecords;
                DGStaff.View.RefreshFilter();
            }
            displayAllEmail();
        }

        public async Task<ObservableCollection<STAFF_DIRECTORY_DESC>> GetStaffData()
        {
            uvm.LoadStaffDirectory(schoolId, "All");
            return uvm.STAFFDIRECTORY;
        }

        private void displayAllEmail()
        {
            //if (DGStaff != null)
            //{
            //    foreach (STAFF_DIRECTORY_DESC c in DGStaff.ItemsSource)
            //    {
            //        tbMultiLine.AppendText(c.EMAIL + ", ");
            //    }

            //    if (tbMultiLine.Text.Length > 1)
            //    {
            //        tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
            //    }
            //}

            if (DGStaff != null)
            {
                //var viewmodel = (this.DGStaff.ItemsSource as UserViewModel);

                ObservableCollection<STAFF_DIRECTORY_DESC> viewmodel = this.dataPager.Source as ObservableCollection<STAFF_DIRECTORY_DESC>;
              
                foreach (var c in viewmodel)
                {
                    tbMultiLine.AppendText(c.EMAIL + ", ");
                }

                if (tbMultiLine.Text.Length > 1)
                {
                    tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
                }
            }
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            tbMultiLine.Clear();
            var checkbox = (sender as CheckBox).IsChecked;
            //if (checkbox == false)
            //{
            var viewmodel = (this.DGStaff.DataContext as UserViewModel);
            this.DGStaff.ClearSelections(false);
            foreach (var collection in viewmodel.STAFFDIRECTORY)
            {
                collection.IsSelected = true;
            }

            displayAllEmail();

            //}
            //else if (checkbox == true)
            //{
            //    var viewmodel = (this.DGStaff.DataContext as UserViewModel);
            //    this.DGStaff.SelectAll();
            //    foreach (var collection in viewmodel.STAFFDIRECTORY)
            //    {
            //        if (collection.IsSelected == false)
            //            collection.IsSelected = true;
            //    }
            //}
        }

        //void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        //{
        //    if (DGStaff.ItemContainerGenerator.Status
        //    == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
        //    {
        //        DGStaff.ItemContainerGenerator.StatusChanged
        //            -= ItemContainerGenerator_StatusChanged;
        //    }
        //}

        private void ChckClicked(object sender, RoutedEventArgs e)
        {
            // if (DGStaff.ItemContainerGenerator.Status
            //== System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            // {
            //     DGStaff.ItemContainerGenerator.StatusChanged
            //         -= ItemContainerGenerator_StatusChanged;

            //tbMultiLine.Clear();

            //foreach (STAFF_DIRECTORY_DESC c in DGStaff.ItemsSource)
            //{
            //    if ((CheckBox)Checkboxcolumn.GetCellContent(c) != null)
            //    {
            //        if (((CheckBox)Checkboxcolumn.GetCellContent(c)).IsChecked == true)
            //        {
            //            tbMultiLine.AppendText(c.EMAIL + ", ");
            //        }
            //    }
            //}

            //if (tbMultiLine.Text.Length > 1)
            //{
            //    tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
            //}
            //else
            //{
            //    displayAllEmail();
            //}

            tbMultiLine.Clear();
            var viewmodel = (this.DGStaff.DataContext as UserViewModel);

            foreach (var c in viewmodel.STAFFDIRECTORY)
            {
                if (c.IsSelected == true)
                {
                    tbMultiLine.AppendText(c.EMAIL + ", ");
                }
            }

            if (tbMultiLine.Text.Length > 1)
            {
                tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
            }
            else
            {
                displayAllEmail();
            }
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            //foreach (STAFF_DIRECTORY_DESC c in DGStaff.ItemsSource)
            //{
            //    c.IsSelected = false;
            //}
            //tbMultiLine.Clear();

            //foreach (STAFF_DIRECTORY_DESC c in chkSelectAll_Unchecked)
            //{
            //    tbMultiLine.AppendText(c.EMAIL + ", ");
            //}

            //if (tbMultiLine.Text.Length > 1)
            //{
            //    tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
            //}

            var viewmodel = (this.DGStaff.DataContext as UserViewModel);

            foreach (var c in viewmodel.STAFFDIRECTORY)
            {
                c.IsSelected = false;
            }
            tbMultiLine.Clear();

            foreach (var c in viewmodel.STAFFDIRECTORY)
            {
                tbMultiLine.AppendText(c.EMAIL + ", ");
            }

            if (tbMultiLine.Text.Length > 1)
            {
                tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            DGStaff.Height = (ah / 2);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DGStaff.ItemsSource != null)
            {
                DGStaff.View.Filter = FilterRecords;
                DGStaff.View.RefreshFilter();
            }
        }

        public bool FilterRecords(object obj)
        {
            if (obj is STAFF_DIRECTORY_DESC sd)
            {
                return sd.NAME.ToUpper().StartsWith(SearchBox.Text.ToUpper())
                    || sd.EMAIL.ToUpper().StartsWith(SearchBox.Text.ToUpper()) ||
                    sd.PHONE_NUMBER.StartsWith(SearchBox.Text) ||
                    sd.ROLE.ToUpper().StartsWith(SearchBox.Text.ToUpper());
            }
            return false;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            //Customize the Print settings in SfDataGrid 
            //DGStaff.PrintSettings.PrintManagerBase = new CustomManagerBase(DGStaff);
            //DGStaff.PrintSettings.AllowPrintByDrawing = true;

            DGStaff.PrintSettings = new PrintSettings();
            DGStaff.PrintSettings.AllowRepeatHeaders = false;

            //provides option to display print preview of SfDataGrid rows 
            DGStaff.ShowPrintPreview();

            //ViewModel.PrintPreview.Print_WPF_Preview(DGStaff);

            //var pd = new PrintDialog();
            //var result = pd.ShowDialog();
            //if (result.HasValue && result.Value)
            //    pd.PrintVisual(DGStaff, "My WPF printing a DataGrid");
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            var textBox = tbMultiLine as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
                Clipboard.SetDataObject(textBox.Text);
                MessageBox.Show("Copied to Clipboard successfully!");
            }
            else
            {
                MessageBox.Show("Email chain is empty!");
            }
            // Retrieves data  
            IDataObject iData = Clipboard.GetDataObject();
            // Is Data Text?  
        }

        private void ViewProfile(object sender, RoutedEventArgs e)
        {
            var x = (Button)sender;
            if (((STAFF_DIRECTORY_DESC)x.DataContext).ID != null)
            {
                if (((STAFF_DIRECTORY_DESC)x.DataContext).ROLE == "STUDENT")
                {
                    try
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                if (this.NavigationService == null)
                                {
                                    ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new SchoolDB_StudentView(((SCHOOLDB_DESC)x.DataContext).ID.ToString()));
                                }
                                else
                                {
                                    this.NavigationService.Navigate(new SchoolDB_StudentView(((STAFF_DIRECTORY_DESC)x.DataContext).ID.ToString()));
                                }
                            }

                        }
                    }
                    catch (Exception ee)
                    {

                    }
                }
            }
            if (((STAFF_DIRECTORY_DESC)x.DataContext).ROLE == "TEACHER")
            {
                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            this.NavigationService.Navigate(new SchoolDB_TeacherView(((STAFF_DIRECTORY_DESC)x.DataContext).ID.ToString()));
                        }
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        public FixedDocumentSequence Document { get; set; }

        public void rdAll_Checked(object sender, RoutedEventArgs e)
        {
            uvm.LoadStaffDirectory(schoolId, "All");
            //dataPager.Source = uvm.STAFFDIRECTORY;
            //if (uvm.STAFFDIRECTORY == null)
            //{
            //    uvm.LoadStaffDirectory(schoolId, "All");
            //}

            //if (dataPager != null)
            //{
            //    dataPager.Source = uvm.STAFFDIRECTORY;
            //}
            DataContext = uvm;
            GetEmailData();
        }

        private void rdStudents_Checked(object sender, RoutedEventArgs e)
        {
            uvm.LoadStaffDirectory(schoolId, "Student");
            dataPager.Source = uvm.STAFFDIRECTORY;
            //DataContext = uvm;
            GetEmailData();
        }

        private void rdTeachers_Checked(object sender, RoutedEventArgs e)
        {
            uvm.LoadStaffDirectory(schoolId, "Teacher");
            //dataPager.Source = uvm.STAFFDIRECTORY;
            DataContext = uvm;
            GetEmailData();
        }

        private void rdStaff_Checked(object sender, RoutedEventArgs e)
        {
            uvm.LoadStaffDirectory(schoolId, "Staff");
            //dataPager.Source = uvm.STAFFDIRECTORY;
            DataContext = uvm;
            GetEmailData();
        }

        private void rdLStaff_Checked(object sender, RoutedEventArgs e)
        {
            uvm.LoadStaffDirectory(schoolId, "LunchStaff");
            //dataPager.Source = uvm.STAFFDIRECTORY;
            DataContext = uvm;
            GetEmailData();
        }

        private void rdSubstitutes_Checked(object sender, RoutedEventArgs e)
        {
            //uvm.LoadStaffDirectory(schoolId, "Substitute");
            //DataContext = uvm;
            //GetEmailData();
        }


        //    private void btnAll_Click(object sender, RoutedEventArgs e)
        //{
        //    uvm.LoadStaffDirectory(schoolId, "All");
        //    DataContext = uvm;
        //    GetEmailData();
        //}

        //private void btnStudents_Click(object sender, RoutedEventArgs e)
        //{
        //    uvm.LoadStaffDirectory(schoolId, "Student");
        //    DataContext = uvm;
        //    GetEmailData();
        //}

        //private void btnTeacher_Click(object sender, RoutedEventArgs e)
        //{
        //    uvm.LoadStaffDirectory(schoolId, "Teacher");
        //    DataContext = uvm;
        //    GetEmailData();
        //}

        //private void btnStaff_Click(object sender, RoutedEventArgs e)
        //{
        //    uvm.LoadStaffDirectory(schoolId, "Staff");
        //    DataContext = uvm;
        //    GetEmailData();
        //}

        //private void btnLunchStaff_Click(object sender, RoutedEventArgs e)
        //{
        //    uvm.LoadStaffDirectory(schoolId, "LunchStaff");
        //    DataContext = uvm;
        //    GetEmailData();
        //}

        //private void btnSubstitutes_Click(object sender, RoutedEventArgs e)
        //{
        //    //uvm.LoadStaffDirectory(schoolId, "Substitute");
        //    //DataContext = uvm;
        //    //GetEmailData();
        //}

        private void GetEmailData()
        {
            if (dataPager != null)
            {
                //var viewmodel = (this.dataPager.DataContext as UserViewModel);

                //uvm.LoadStaffDirectory(schoolId, "All");
                var viewmodel = uvm.STAFFDIRECTORY;

                foreach (var c in viewmodel)
                {
                    c.IsSelected = false;
                }
                tbMultiLine.Clear();

                foreach (var c in viewmodel)
                {
                    tbMultiLine.AppendText(c.EMAIL + ", ");
                }

                if (tbMultiLine.Text.Length > 1)
                {
                    tbMultiLine.Text = tbMultiLine.Text.Substring(0, tbMultiLine.Text.Length - 2);
                }
            }
        }

    }

    public class CustomManagerBase : GridPrintManager
    {
        public SfDataGrid DataGrid;

        internal IList source;

        public CustomManagerBase(SfDataGrid grid) : base(grid)
        {
            DataGrid = grid;
        }

        private int GetUnBoundRowCount(UnBoundRowsPosition position, bool isbelowsummary)
        {
            int count = 0;

            if (position == UnBoundRowsPosition.Top && !isbelowsummary)
                count = dataGrid.GetUnBoundRowsCount(UnBoundRowsPosition.Top, false);
            else if (position == UnBoundRowsPosition.Top && isbelowsummary)
                count = dataGrid.GetUnBoundRowsCount(UnBoundRowsPosition.Top, true);
            else if (position == UnBoundRowsPosition.Bottom && isbelowsummary)
                count = dataGrid.GetUnBoundRowsCount(UnBoundRowsPosition.Bottom, true);
            else if (position == UnBoundRowsPosition.Bottom && !isbelowsummary)
                count = dataGrid.GetUnBoundRowsCount(UnBoundRowsPosition.Bottom, false);

            return count;
        }


        protected override IList GetSourceListForPrinting()
        {
            var source = (View as PagedCollectionView).GetInternalList();
            List<object> OrderedSource = new List<object>();

            if (GetUnBoundRowCount(UnBoundRowsPosition.Top, false) > 0)
            {
                foreach (var row in dataGrid.UnBoundRows.Where(r => r.Position == UnBoundRowsPosition.Top && !r.ShowBelowSummary))
                    OrderedSource.Add(row);
            }

            if (GetTableSummaryList(TableSummaryRowPosition.Top).Count > 0)
            {
                foreach (var item in GetTableSummaryList(TableSummaryRowPosition.Top))
                    OrderedSource.Add(item);
            }

            if (GetUnBoundRowCount(UnBoundRowsPosition.Top, true) > 0)
            {
                foreach (var row in dataGrid.UnBoundRows.Where(r => r.Position == UnBoundRowsPosition.Top && r.ShowBelowSummary))
                    OrderedSource.Add(row);
            }

            foreach (var item in source)
                OrderedSource.Add(item);

            if (GetUnBoundRowCount(UnBoundRowsPosition.Bottom, false) > 0)
            {
                foreach (var row in dataGrid.UnBoundRows.Where(r => r.Position == UnBoundRowsPosition.Bottom && !r.ShowBelowSummary))
                    OrderedSource.Add(row);
            }

            if (GetTableSummaryList(TableSummaryRowPosition.Bottom).Count > 0)
            {
                foreach (var item in GetTableSummaryList(TableSummaryRowPosition.Bottom))
                    OrderedSource.Add(item);
            }

            if (GetUnBoundRowCount(UnBoundRowsPosition.Bottom, true) > 0)
            {
                foreach (var row in dataGrid.UnBoundRows.Where(r => r.Position == UnBoundRowsPosition.Bottom && r.ShowBelowSummary))
                    OrderedSource.Add(row);
            }

            return OrderedSource;
        }
    }
}
