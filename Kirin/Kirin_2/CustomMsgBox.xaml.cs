using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for CustomMsgBox.xaml
    /// </summary>
    public partial class CustomMsgBox : Window
    {
        public CustomMsgBox()
        {
            InitializeComponent();
            //FromDate.SelectedDate = DateTime.Now;
            //ToDate.SelectedDate = DateTime.Now.AddDays(7);

            //string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            MultidayCal.MaxDate = DateTime.Now;
        }

        public List<DateTime> SelectedDatesList { get; set; }
        private void UpdateDateRange(object sender, RoutedEventArgs e)
        {
            SelectedDatesList = (MultidayCal.SelectedDates).ToList();

            if (SelectedDatesList.Count > 1)
            {
                ClassAttendanceViewModel dc = (ClassAttendanceViewModel)this.DataContext;
                dc.DateRangeFilter_From = SelectedDatesList[0];
                dc.DateRangeFilter_To = SelectedDatesList[SelectedDatesList.Count - 1];
                dc.DateRangeFilter_lbl = dc.DateRangeFilter_From.ToString("MM/dd/yyyy") + " - " + dc.DateRangeFilter_To.ToString("MM/dd/yyyy");

                dc.getMultiDayClassAttendance();
            }

            //ClassAttendanceViewModel dc = (ClassAttendanceViewModel)this.DataContext;
            //dc.DateRangeFilter_From = FromDate.SelectedDate.Value;
            //dc.DateRangeFilter_To = ToDate.SelectedDate.Value;
            //dc.DateRangeFilter_lbl = dc.DateRangeFilter_From.ToString("MM/dd/yyyy") + " - " + dc.DateRangeFilter_To.ToString("MM/dd/yyyy");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
