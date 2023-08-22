using Kirin_2.Pages;
using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for SingleDayCalender.xaml
    /// </summary>
    public partial class SingleDayCalender : UserControl
    {
        public App app;
        public SingleDayCalender()
        {
            InitializeComponent();

            SingleDayCal.MaxDate = DateTime.Now;
        }

        private void SingleDayCal_DateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTime datepicker = SingleDayCal.SelectedDates[0];
            string classID = App.Current.Properties["SubjectId"].ToString();
        
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainWindowFrame.Content = new ClassAttendance(classID, datepicker);
            }

            this.Visibility = Visibility.Collapsed;
        }

    }
}
