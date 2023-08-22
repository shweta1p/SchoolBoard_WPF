using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Windows.Controls;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Classcalendar.xaml
    /// </summary>
    public partial class Classcalendar: Page
    {

        public Classcalendar()
        {
            this.DataContext = new CalendarViewModel();
            InitializeComponent();
            this.schedulerViewTypeComboBox.ItemsSource = Enum.GetValues(typeof(SchedulerViewType));
            Globals.reset = 0;
        }
    }
}
