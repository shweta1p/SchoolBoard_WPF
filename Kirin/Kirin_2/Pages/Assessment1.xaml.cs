﻿using System;
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
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Assessment1.xaml
    /// </summary>
    public partial class Assessment1 : Page
    {
       
            public Assessment1()
        {
            InitializeComponent();
            Grid RootGrid = new Grid();
            SfDiagram diagram = new SfDiagram();
            //Initialize NodeCollection to SfDiagram
            diagram.Nodes = new NodeCollection();
            
            //Initialize ConnectorCollection to SfDiagram
            diagram.Connectors = new ConnectorCollection();
          
            //Initialize DataSourceSettings to SfDiagram
            diagram.DataSourceSettings = new DataSourceSettings()
            {
                DataSource = GetData(),
                ParentId = "ParentId",
                Id = "EmpId"
            };

            //Initialize LayoutManager to SfDiagram
            diagram.LayoutManager = new LayoutManager()
            {
                //Initialize Layout for LayoutManager  
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Organization,
                    Orientation = TreeOrientation.TopToBottom,
                    HorizontalSpacing = double.PositiveInfinity,
                    VerticalSpacing = 2000
                }
            };
            diagram.OutlineSettings = new OutlineSettings()
            {
                //Specifies the outline style
                
                //Specifies the outline rendering interval
                RenderInterval = new TimeSpan(0, 0, 0, 10),
            };
            //RootGrid is the instance of the MainWindow Grid
            RootGrid.Children.Add(diagram);

        }
        private Employees GetData()
        {
            Employees data = new Employees();



            return data;
        }



    }



    public class Employee
    {
        public Employee()
        {

        }
        public string EmpId { get; set; }
        public string ParentId { get; set; }
        public string Designation { get; set; }
        public string _Color { get; set; }

    }

    public class Employees : ObservableCollection<Employee>
    {
    }

}
