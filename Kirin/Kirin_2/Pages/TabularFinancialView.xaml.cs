using Kirin_2.Models;
using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for TabularFinancialView.xaml
    /// </summary>
    public partial class TabularFinancialView : Page
    {
        public TabularFinancialView()
        {
            FinancialViewModel fvm = new FinancialViewModel();
            InitializeComponent();

            DGBudgetView.ItemsSource = fvm.ReadRecordFromEXCELAsync().Result;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            DGBudgetView.Height = (ah - 120);
        }
    }
}
