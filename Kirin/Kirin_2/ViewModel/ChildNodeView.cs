using Kirin_2.Models;
using Kirin_2.ViewModel.Command;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.ViewModel
{
    public class ChildNodeView : DiagramViewModel
    {
        KIRINEntities1 kirinentities;

        string parentId = App.Current.Properties["ParentId"].ToString();
        string year = App.Current.Properties["Year"].ToString();

        // SfDiagram Configuration for Round Graph
        public ChildNodeView()
        {
            SnapSettings = new SnapSettings()
            {
                SnapToObject = SnapToObject.All,
                SnapConstraints = SnapConstraints.None,
            };

            ItemAddedCommand = new CommandFlow(args => OnItemAdded((ItemAddedEventArgs)args));

            //Initialize Context menu for diagram.
            Menu = null;

            // Initialize DataSourceSettings for SfDiagram
            DataSourceSettings = new FlowchartDataSourceSettings()
            {
                ParentId = "ParentId",
                Id = "Id",
                DataSource = GetData(parentId),
                ConnectorTextMapping = "Label",

                ShapeMapping = "_Shape",
                WidthMapping = "_Width",
                HeightMapping = "_Height"
            };

            // Initialize LayoutSettings for SfDiagram
            LayoutManager = new LayoutManager()
            {
                Layout = new MindMapTreeLayout()
                {
                    Orientation = Orientation.Horizontal,

                    HorizontalSpacing = 150,
                    VerticalSpacing = 70,
                },
            };

            FinancialLegendView();
        }

        public string Title { get; set; }

        private void OnItemAdded(ItemAddedEventArgs args)
        {
            if (args.Item is ConnectorViewModel)
            {
                var connectorannotations = (args.Item as ConnectorViewModel).Annotations as AnnotationCollection;
                foreach (AnnotationEditorViewModel anno in connectorannotations)
                {
                    anno.Alignment = ConnectorAnnotationAlignment.Center;
                    anno.Length = 0.6;
                    //anno.Displacement = 0;
                }
            }
            else if (args.Item is NodeViewModel)
            {
                var nodeannotations = (args.Item as NodeViewModel).Annotations as AnnotationCollection;
                foreach (AnnotationEditorViewModel anno in nodeannotations)
                {
                    anno.TextHorizontalAlignment = TextAlignment.Center;
                }
            }
        }

        private DataItems GetData(string parentId)
        {
            kirinentities = new KIRINEntities1();
            var budgetData = kirinentities.GetChildNodeofBudgetData(Convert.ToInt32(parentId), year).ToList();

            DataItems ItemsInfo = new DataItems();

            foreach (GetChildNodeofBudgetData_Result item in budgetData)
            {
                ItemsInfo.Add(new ItemInfo()
                {
                    Id = item.ID.ToString(),
                    Label = new List<string> { item.BUDGET + "$" },
                    Name = item.BUDGET_LABEL,
                    ParentId = new List<string> { item.PARENTID.ToString() },
                    _Shape = App.Current.Resources["OnPageReference"] as string,
                    _Width = Convert.ToDouble(item.WIDTH),
                    _Height = Convert.ToDouble(item.HEIGHT),
                    _Color = item.COLOR,
                    IsRoot = Convert.ToBoolean(item.ISHEADER),
                    Level = Convert.ToInt32(item.LEVEL_NUMBER)
                });
            }

            if (budgetData.Count != 0)
            {
                Title = budgetData[0].BUDGET_LABEL + " [" + budgetData[0].YEAR + "]";
                Year = budgetData[0].YEAR;
            }

            return ItemsInfo;
        }

        private ObservableCollection<Budget> _ChildViewLegend;
        public ObservableCollection<Budget> ChildViewLegend
        {
            get { return _ChildViewLegend; }
            set
            {
                _ChildViewLegend = value;
                OnPropertyChanged(nameof(ChildViewLegend));
            }
        }

        private string _Parent;
        public string Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
                OnPropertyChanged("Parent");
            }
        }
        

        public void FinancialLegendView()
        {
            var parentData = kirinentities.GetParentID(parentId).ToList();

            if (parentData.Count > 0) 
            {
                Parent = parentData.Select(x => x.BUDGET_LABEL).FirstOrDefault().ToString();
                string parentId = parentData.Select(x => x.ID).FirstOrDefault().ToString();

                var data = kirinentities.GetBudgetLegendData(Year, parentId).ToList();

                ChildViewLegend = new ObservableCollection<Budget>(from c in data
                                                                       select new Budget
                                                                       {
                                                                           Id = c.ID,
                                                                           label = c.BUDGET_LABEL,
                                                                           budget = c.BUDGET + "$",
                                                                           year = c.YEAR,
                                                                           childView = kirinentities.GetBudgetLegendChildData(c.ID.ToString()).
                                                                            Select(x => new Child
                                                                            {
                                                                                Id = x.ID,
                                                                                label = x.BUDGET_LABEL,
                                                                                budget = x.BUDGET + "$",
                                                                                year = x.YEAR,
                                                                            }).ToList()
                                                                       });
            }            
        }


        public string _Year = "2019-2020";
        public string Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                OnPropertyChanged("Year");
            }
        }

        ////SfDiagram Configuration for MindMap Tree


        //public ChildNodeView()
        //{
        //    SnapSettings = new SnapSettings()
        //    {
        //        SnapToObject = SnapToObject.All,
        //        SnapConstraints = SnapConstraints.None,
        //    };

        //    //Initialize Context menu for diagram.
        //    Menu = null;

        //    DataSourceSettings = new DataSourceSettings()
        //    {
        //        DataSource = this.Getdata(parentId),
        //        ParentId = "ParentId",
        //        Id = "Id",               
        //    };

        //    LayoutManager = new LayoutManager()
        //    {
        //        Layout = new MindMapTreeLayout()
        //        {
        //            HorizontalSpacing = 500,
        //            VerticalSpacing = 300,
        //            Orientation = Orientation.Horizontal,
        //            SplitMode = MindMapTreeMode.Custom
        //        },
        //        RefreshFrequency = RefreshFrequency.ArrangeParsing
        //    };
        //}

        //private MindmapDataItems Getdata(string parentId)
        //{
        //    kirinentities = new KIRINEntities1();
        //    var budgetData = kirinentities.GetChildNodeDataforMindTree(Convert.ToInt32(parentId)).ToList();

        //    MindmapDataItems budgets = new MindmapDataItems();

        //    foreach (GetChildNodeDataforMindTree_Result item in budgetData)
        //    {
        //        budgets.Add(new MindmapDataItem()
        //        {
        //            Label = item.BUDGET_LABEL,
        //            ParentId = item.PARENTID.ToString(),
        //        });
        //    }
        //    return budgets;
        //}


    }

    public class Budget
    {
        public int Id { get; set; }
        public string label { get; set; }
        public string budget { get; set; }
        public string year { get; set; }
        public List<Child> childView { get; set; }
    }

    public class Child
    {
        public int Id { get; set; }
        public string label { get; set; }
        public string budget { get; set; }
        public string year { get; set; }
    }
}
