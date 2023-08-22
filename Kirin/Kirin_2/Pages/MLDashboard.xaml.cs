using Kirin_2.ViewModel;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Kirin_2.Models;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Controls.Primitives;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for MLDashboard.xaml
    /// </summary>
    public partial class MLDashboard : Page
    {
        public MLDashboard()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // vbdashboard.Width = app.MainFrameWidth;
        }

    }
}
