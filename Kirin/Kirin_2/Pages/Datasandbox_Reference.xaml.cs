using Kirin_2.Models;
using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Datasandbox_Reference.xaml
    /// </summary>
    public partial class Datasandbox_Reference : Page
    {
        public App app;
        SandboxViewModel sbvm;
        KIRINEntities1 kirinentities;
        string schoolId = string.Empty;
        string subjectId = string.Empty;
        public Datasandbox_Reference()
        {
            app = (App)Application.Current;
            subjectId = App.Current.Properties["SubjectId"].ToString();
            schoolId = App.Current.Properties["SchoolId"].ToString();

            sbvm = new SandboxViewModel();

            this.DataContext = sbvm;
            InitializeComponent();
            
            RadarSeries series = new RadarSeries()
            {
                ItemsSource = sbvm.PlantDetails,
                XBindingPath = "Direction",
                YBindingPath = "Tree",
                Interior = new SolidColorBrush(Color.FromRgb(36, 123, 192))
            };

            BoxAndWhiskerSeries boxAndWhisker = new BoxAndWhiskerSeries();
            boxAndWhisker.ItemsSource = sbvm.BoxWhiskerData;
            boxAndWhisker.XBindingPath = "Department";
            boxAndWhisker.YBindingPath = "Age";

            BubbleSeries bubbleChart = new BubbleSeries()
            {
                ItemsSource = sbvm.BubbleData,
                XBindingPath = "Label",
                YBindingPath = "Value",
                Size = "Size",
                MinimumRadius = 5,
                Interior = new SolidColorBrush(Color.FromRgb(36, 123, 192))
            };

            AreaSeries AreaSeries = new AreaSeries()
            {

                ItemsSource = sbvm.Performance,

                XBindingPath = "Load",

                YBindingPath = "Server1",

                Interior = new SolidColorBrush(Color.FromRgb(36, 123, 192))

            };
        }

    }
}
