using Kirin_2.Pages;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Row1CustomToolTip.xaml
    /// </summary>
    public partial class UC_Row1CustomToolTip
    {
        private TooltipData _data;

        public UC_Row1CustomToolTip()
        {
            InitializeComponent();
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

     
    }

}
