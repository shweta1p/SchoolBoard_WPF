using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_CustomToolTip.xaml
    /// </summary>
    public partial class UC_CustomToolTip : IChartTooltip
    {
        private TooltipData _data;

        public UC_CustomToolTip()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TooltipData Data
        {
            get { return _data; }
            set
            {
                _data = value;
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

    public class CustomVM
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
