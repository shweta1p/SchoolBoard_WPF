using Kirin_2.Models;
using Kirin_2.ViewModel.Command;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.UI.Xaml.Diagram.Theming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;


namespace Kirin_2.ViewModel
{
    public class Flowchart : DiagramViewModel
    {
        private ICommand _ExpandCollapseCommand;
        KIRINEntities1 kirinentities;
               
        public Flowchart()
        {
            SnapSettings = new SnapSettings()
            {
                SnapToObject = SnapToObject.All,
                SnapConstraints = SnapConstraints.None,

            };

            // Initialize Diagram properties
            
            Menu = null;
            Tool = Tool.ZoomPan;
            DefaultConnectorType = ConnectorType.CubicBezier;
            
            //Initialize Commands
            ExpandCollapseCommand = new CommandVM(OnExpandCollaseCommand, canExecuteMethod); 
            ItemAddedCommand = new CommandVM(OnItemAddedCommand, canExecuteMethod);
           
            // Initialize DataSourceSettings for SfDiagram
            DataSourceSettings = new FlowchartDataSourceSettings()
            {
                ParentId = "ParentId",
                Id = "Id",
                DataSource = Getdata(),
                ConnectorTextMapping = "Label",
                
                //ShapeMapping = "_Shape",
                //WidthMapping = "_Width",
                //HeightMapping = "_Height"
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
                //Layout = new DirectedTreeLayout()
                //{
                //    Orientation = TreeOrientation.TopToBottom,
                //    Type = LayoutType.Organization,

                //    HorizontalSpacing = 250,
                //    VerticalSpacing = 250,
                //},
            };
        }

        private void OnItemAddedCommand(object obj)
        {
            var args = obj as ItemAddedEventArgs;
            
            if (args.Item is ConnectorViewModel)
            {
                var connectorannotations = (args.Item as ConnectorViewModel).Annotations as AnnotationCollection;
                foreach (AnnotationEditorViewModel anno in connectorannotations)
                {
                    anno.Alignment = ConnectorAnnotationAlignment.Center;
                    anno.Length = 0.6;
                    //anno.Displacement = 60;
                }
            }
            else if (args.Item is NodeViewModel)
            {
                NodeViewModel node = args.Item as NodeViewModel;
                BudgetData info = node.Content as BudgetData;

                //if (info.Level == 4)
                //{
                //    DefaultConnectorType = ConnectorType.CubicBezier;
                //}
                //else {
                //    DefaultConnectorType = ConnectorType.Line;
                //}

                var nodeannotations = (args.Item as NodeViewModel).Annotations as AnnotationCollection;
                foreach (AnnotationEditorViewModel anno in nodeannotations)
                {
                    anno.TextHorizontalAlignment = TextAlignment.Center;
                }
            }

        }


        #region Commands

        public ICommand ExpandCollapseCommand
        {
            get { return _ExpandCollapseCommand; }
            set
            {
                if (_ExpandCollapseCommand != value)
                {
                    _ExpandCollapseCommand = value;
                }
            }
        }

        public bool canExecuteMethod(object param)
        {
            return true;
        }

        #endregion
        
        #region Helper Methods
        /// <summary>
        /// The method to execute the expand and collapse operation.
        /// </summary>
        /// <param name="args">The parent node to be expanded or collapsed</param>
        private void OnExpandCollaseCommand(object args)
        {
            if (args is Node && (args as Node).DataContext is NodeViewModel)
            {
                ExpandCollapseParameter obj = new ExpandCollapseParameter();
                obj.node = (args as Node).DataContext as NodeViewModel;
                obj.IsUpdateLayout = true;

                IGraphInfo graphinfo = Info as IGraphInfo;
                if (((args as Node).DataContext as NodeViewModel).IsExpanded)
                {
                    graphinfo.Commands.ExpandCollapse.Execute(obj);
                    ((args as Node).DataContext as NodeViewModel).IsExpanded = false;
                    ((args as Node).Content as BudgetData).IsExpand = State.Collapse;
                }
                else
                {
                    graphinfo.Commands.ExpandCollapse.Execute(obj);
                    ((args as Node).DataContext as NodeViewModel).IsExpanded = true;
                    ((args as Node).Content as BudgetData).IsExpand = State.Expand;
                }
            }
        }

        /// <summary>
        /// Method to Get the data for DataSource
        /// </summary>
        /// <param name="data"></param>
        private BudgetDataList Getdata()
        {
            kirinentities = new KIRINEntities1();
            var budgetData = kirinentities.GetShortBudgetData("2019/2020").ToList();
            //var budgetData = kirinentities.GetBudgetData().ToList();

            BudgetDataList budgets = new BudgetDataList();

            foreach (GetShortBudgetData_Result item in budgetData)
            {
                budgets.Add(new BudgetData()
                {
                    Id = item.ID.ToString(),
                    Label = item.BUDGET + "$",
                    Name = item.BUDGET_LABEL,
                    ParentId = item.PARENTID.ToString(),
                    _Shape = item.LEVEL_NUMBER == 4 ? App.Current.Resources["OnPageReference"] as string : "",
                    _Width = item.LEVEL_NUMBER == 4 ? 110 : 250,
                    _Height = item.LEVEL_NUMBER == 4 ? 110 : 50,
                    RatingColor = item.COLOR,
                    IsRoot = item.LEVEL_NUMBER != 4 ? Convert.ToBoolean(item.ISHEADER) : false,
                    Level = Convert.ToInt32(item.LEVEL_NUMBER),
                    LblVisibility = item.LEVEL_NUMBER != 4 ? Visibility.Visible : Visibility.Hidden,
                    RlblVisibility = item.LEVEL_NUMBER == 4 ? Visibility.Visible : Visibility.Hidden,
                });
            }
            return budgets;
        }

        #endregion

        
    }
}
