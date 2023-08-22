using Kirin_2.ViewModel;
using Syncfusion.Windows.Controls.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Classes.xaml
    /// </summary>
    public partial class Classes : Page
    {
        public Classes()
        {
            InitializeComponent();
            GradeBookViewModel gradebookvm = new GradeBookViewModel();

            //busyIndicator.AnimationType = AnimationTypes.Gear;
            //busyIndicator.Foreground = Brushes.Blue;
            //busyIndicator.Background = Brushes.Transparent;
            //busyIndicator.ViewboxHeight = 100;
            //busyIndicator.ViewboxWidth = 100;
            //busyIndicator.IsBusy = true;
            //busyIndicator.Visibility = Visibility.Visible;
            
            //BackgroundWorker worker = new BackgroundWorker();

            //cmbSemesterList.SelectedValue = "27";
            string subjectId = App.Current.Properties["SubjectId"].ToString();
            string schoolId = App.Current.Properties["SchoolId"].ToString();
            string userName = App.Current.Properties["USERNAME"].ToString();
            //string semesterId = cmbSemesterList.SelectedValue.ToString();

            ////this is where the long running process should go
            //worker.DoWork += (o, ea) =>
            //{
            //    //no direct interaction with the UI is allowed from this method
            //    gradebookvm.getSchoolList();
            //    gradebookvm.getSemesterList();
            //    gradebookvm.getSubjectList();
            //    gradebookvm.getStudentList(subjectId, schoolId, userName); //semesterId
            //};

            //worker.RunWorkerCompleted += async (o, ea) =>
            //{
            //    await Task.Delay(1000);
            //    //work has completed. you can now interact with the UI
            //    busyIndicator.IsBusy = false;
            //    busyIndicator.Visibility = Visibility.Collapsed;
            //    //_busyIndicator.IsBusy = false;
            //    //displayAllEmail();
            //};
            //worker.RunWorkerAsync();

            gradebookvm.getSchoolList();
            gradebookvm.getSemesterList();
            gradebookvm.getSubjectList();
            gradebookvm.getStudentList(subjectId, schoolId, userName);
            this.DataContext = gradebookvm;
            gradebookvm.cDisplayColumns.Execute(app.chkBoxLst);

            Globals.reset = 0;
        }

        public App app = (App)Application.Current;
        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            //DisplayNewColumns newcolumnwin = new DisplayNewColumns(this.DataContext);

            //newcolumnwin.Show();
            // app.addColumn = new UC_AddColumn();
            app.addColumn.DataContext = this.DataContext;
            app.addColumn.Visibility = Visibility.Visible;
        }

        private void UpdateColumns()
        {
            var viewModel = (GradeBookViewModel)this.DataContext;
            if (viewModel.cDisplayColumns.CanExecute(app.chkBoxLst))
                viewModel.cDisplayColumns.Execute(app.chkBoxLst);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;
        }

        private void DGClasses_Loaded(object sender, RoutedEventArgs e)
        {
            int colCount = DGClasses.Columns.Count;


            BindingOperations.SetBinding(LanguageColumn, DataGridColumn.DisplayIndexProperty,
        new Binding("LanguageDisplayIndex") { Source = this,  FallbackValue = 6 });

            BindingOperations.SetBinding(GenderColumn, DataGridColumn.DisplayIndexProperty,
        new Binding("GenderDisplayIndex") { Source = this, FallbackValue = 6 });

            BindingOperations.SetBinding(EthnicityColumn, DataGridColumn.DisplayIndexProperty,
        new Binding("EthnicityDisplayIndex") { Source = this, FallbackValue = 6 });

            BindingOperations.SetBinding(CitizenshipColumn, DataGridColumn.DisplayIndexProperty,
        new Binding("CitizenshipDisplayIndex") { Source = this, FallbackValue = 6 });
        }
    }
}
