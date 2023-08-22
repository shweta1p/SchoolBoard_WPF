using Kirin_2.Pages;
using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for SyncFusionCalender.xaml
    /// </summary>
    public partial class SyncFusionCalender : UserControl
    {
        public App app;
        public SyncFusionCalender()
        {
            InitializeComponent();
            
            MultidayCal.MaxDate = DateTime.Now;
        }

        public List<DateTime> SelectedDatesList { get; set; }
        private void Get_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesList = (MultidayCal.SelectedDates).ToList();

            if (SelectedDatesList.Count > 1)
            {
                //DashboardViewModel dbvm = new DashboardViewModel();
                //string dtFrom = SelectedDatesList[0].ToString("MM/dd/yyyy");
                //string dtTo = SelectedDatesList[SelectedDatesList.Count - 1].ToString("MM/dd/yyyy");

                //string subjectId = App.Current.Properties["SubjectId"].ToString();
                //string schoolId = App.Current.Properties["SchoolId"].ToString();

                //dbvm.FromDate = SelectedDatesList[0];
                //dbvm.ToDate = SelectedDatesList[SelectedDatesList.Count - 1];
                //dbvm.DateRangeFilter_lbl = dtFrom + " - " + dtTo;

                //dbvm.StackedBarRow4(dtFrom, dtTo, subjectId, schoolId);

                app = (App)Application.Current;
                app.Properties["FromDate"] = SelectedDatesList[0];
                app.Properties["ToDate"] = SelectedDatesList[SelectedDatesList.Count - 1];

                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                {
                    mw.MainWindowFrame.Content = new Dashboard();
                }

            }
            else
            {
                MessageBox.Show("Please Select Date");
            }

            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        //private void MultidayCal_DateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    SelectedDatesList = (MultidayCal.SelectedDates).ToList();

        //    if (SelectedDatesList.Count > 1)
        //    {
        //        app = (App)Application.Current;
        //        app.Properties["FromDate"] = SelectedDatesList[0];
        //        app.Properties["ToDate"] = SelectedDatesList[SelectedDatesList.Count - 1];

        //        MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        //        if (mw != null)
        //        {
        //            mw.MainWindowFrame.Content = new Dashboard();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please Select Date");
        //    }

        //    this.Visibility = Visibility.Collapsed;
        //}

    }
}
