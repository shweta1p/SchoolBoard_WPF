using Kirin_2.ViewModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for PieCustomToolTip.xaml
    /// </summary>
    public partial class PieCustomToolTip : IChartTooltip
    {
        private TooltipData _piedata;
        //DashboardViewModel dbvm;
        public PieCustomToolTip()
        {
            InitializeComponent();
            //dbvm = new DashboardViewModel();
            //DataContext = dbvm.pieTooltip;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TooltipData Data
        {
            get { return _piedata; }
            set
            {
                _piedata = value;
                OnPropertyChanged("Data");
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PieVM
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
        public BitmapImage PHOTO { get; set; }
    }
}
