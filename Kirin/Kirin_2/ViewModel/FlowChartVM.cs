using Kirin_2.Models;
using Kirin_2.ViewModel.Command;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.ViewModel
{
    public class FlowChartVM : DiagramViewModel
    {
        KIRINEntities1 kirinentities;

        #region Constructor
        public FlowChartVM()
        {
            //This command will get invoked when an item is added to the diagram.
            ItemAddedCommand = new CommandFlow(args => OnItemAdded((ItemAddedEventArgs)args));

            // Initialize Gridlines for SfDiagram
            SnapSettings = new SnapSettings()
            {
                SnapToObject = SnapToObject.All,
                SnapConstraints = SnapConstraints.None,

            };

            //Initialize Context menu for diagram.
            Menu = null;
            DefaultConnectorType = ConnectorType.CubicBezier;

            // Initialize DataSourceSettings for SfDiagram
            DataSourceSettings = new FlowchartDataSourceSettings()
            {
                ParentId = "ParentId",
                Id = "Id",
                DataSource = GetData(),
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

                    HorizontalSpacing = 200,
                    VerticalSpacing = 70,
                },

                //Layout = new FlowchartLayout()
                //{
                //    Orientation = FlowchartOrientation.TopToBottom,

                //    HorizontalSpacing = 70,
                //    VerticalSpacing = 90,
                //},
            };

            FinancialLegendView();
        }

        private void OnItemAdded(ItemAddedEventArgs args)
        {
            if (args.Item is ConnectorViewModel)
            {
                var connectorannotations = (args.Item as ConnectorViewModel).Annotations as AnnotationCollection;
                foreach (AnnotationEditorViewModel anno in connectorannotations)
                {
                    //anno.Alignment = ConnectorAnnotationAlignment.Center;
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
        #endregion

        /// <summary>
        /// Method to Get Data for DataSource
        /// </summary>
        /// <param name="data"></param>
        private DataItems GetData()
        {
            kirinentities = new KIRINEntities1();
            var staffData = kirinentities.GetShortBudgetData(Year).ToList();

            DataItems ItemsInfo = new DataItems();

            foreach (GetShortBudgetData_Result item in staffData)
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

                FinancialYear = "Approved Budget [" + item.YEAR + "]";

                Year = item.YEAR;
            }

            return ItemsInfo;
        }

        private ObservableCollection<BudgetView> _ChildViewLegend;
        public ObservableCollection<BudgetView> ChildViewLegend
        {
            get { return _ChildViewLegend; }
            set
            {
                _ChildViewLegend = value;
                OnPropertyChanged(nameof(ChildViewLegend));
            }
        }


        public void FinancialLegendView()
        {
            var data = kirinentities.GetParentBudgetData(Year);

            ChildViewLegend = new ObservableCollection<BudgetView>(from c in data
                                                                   select new BudgetView
                                                                   {
                                                                       Id = c.ID,
                                                                       label = c.BUDGET_LABEL,
                                                                       budget = c.BUDGET + "$",
                                                                       year = c.YEAR,
                                                                       childView = kirinentities.GetBudgetLegendData(Year, c.ID.ToString()).
                                                                        Select(x => new ChildView
                                                                        {
                                                                            Id = x.ID,
                                                                            label = x.BUDGET_LABEL,
                                                                            budget = x.BUDGET + "$",
                                                                            year = x.YEAR,
                                                                            subChildView = kirinentities.GetBudgetLegendChildData(x.ID.ToString()).
                                                                            Select(b => new SubChildView
                                                                            {
                                                                                Id = b.ID,
                                                                                label = b.BUDGET_LABEL,
                                                                                budget = b.BUDGET + "$",
                                                                                year = b.YEAR,
                                                                            }).ToList()
                                                                        }).ToList()
                                                                   });
        }


        public string _FinancialYear;
        public string FinancialYear
        {
            get { return _FinancialYear; }
            set
            {
                _FinancialYear = value;
                OnPropertyChanged("FinancialYear");
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
    }

    public class BudgetView
    {
        public int Id { get; set; }
        public string label { get; set; }
        public string budget { get; set; }
        public string year { get; set; }
        public List<ChildView> childView { get; set; }
    }

    public class ChildView
    {
        public int Id { get; set; }
        public string label { get; set; }
        public string budget { get; set; }
        public string year { get; set; }
        public List<SubChildView> subChildView { get; set; }
    }

    public class SubChildView
    {
        public int Id { get; set; }
        public string label { get; set; }
        public string budget { get; set; }
        public string year { get; set; }
    }
}
