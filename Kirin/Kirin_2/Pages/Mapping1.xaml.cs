﻿using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Mapping1.xaml
    /// </summary>
    public partial class Mapping1 : Page
    {
        public Mapping1()
        {
            InitializeComponent();
            //sfdiagram.Constraints = sfdiagram.Constraints.Remove(GraphConstraints.PageEditing, GraphConstraints.PanRails);
     
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            //Button btn = sender as Button;
            //if (btn.Content.ToString() == "Collepsed View")
            //{
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "MappingSV.xaml"), UriKind.RelativeOrAbsolute));
                    }
                }
            //}
        }
    }
}
