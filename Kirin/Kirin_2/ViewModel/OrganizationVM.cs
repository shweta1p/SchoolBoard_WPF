﻿using Kirin_2.Models;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.ViewModel
{
    public class OrganizationVM : DiagramViewModel
    {
        KIRINEntities1 kirinentities;
        private ICommand _orgCompactLeft_Command; 
        private ICommand _ExpandCollapseCommand;
        private string compact;
        private ICommand _GetLayoutInfoCommand;
        private ICommand _ItemSelectedCommand;
        public Button prevbutton = null;
        
        public OrganizationVM()
        {
            // Initialize Diagram properties
            Constraints = Constraints.Remove(GraphConstraints.PageEditing, GraphConstraints.PanRails);
            Menu = null;
            Tool = Tool.ZoomPan;
            DefaultConnectorType = ConnectorType.Orthogonal;
            
            //Initialize Commands
            //orgCompactLeft_Command = new CommandVM(OnorgCompactLeft_Command, canExecuteMethod);
            GetLayoutInfoCommand = new CommandVM(OnGetLayoutInfoCommand, canExecuteMethod);
            ExpandCollapseCommand = new CommandVM(OnExpandCollaseCommand, canExecuteMethod);
            //ItemAddedCommand = new CommandVM(OnItemAddedCommand, canExecuteMethod);

            // Initialize DataSourceSettings for SfDiagram
            DataSourceSettings = new FlowchartDataSourceSettings()
            {
                ParentId = "ReportingPerson",
                Id = "Name",
                //ParentId = "ParentId",
                //Id = "Id",
                DataSource = Getdata(),
                ShapeMapping = "_Shape",
                WidthMapping = "_Width",
                HeightMapping = "_Height"
            };

            // Initialize LayoutSettings for SfDiagram
            LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Orientation = TreeOrientation.TopToBottom,
                    Type = LayoutType.Organization,
                    HorizontalSpacing = 50,
                    VerticalSpacing = 90
                },
            };
        }

        #region Commands

        public ICommand orgCompactLeft_Command
        {
            get { return _orgCompactLeft_Command; }
            set
            {
                if (_orgCompactLeft_Command != value)
                {
                    _orgCompactLeft_Command = value;
                    onPropertyChanged("orgCompactLeft_Command");
                }
            }
        }

        public new ICommand GetLayoutInfoCommand
        {
            get { return _GetLayoutInfoCommand; }
            set
            {
                if (_GetLayoutInfoCommand != value)
                {
                    _GetLayoutInfoCommand = value;
                    onPropertyChanged("orgCompactLeft_Command");
                }
            }
        }

        public new ICommand ItemSelectedCommand
        {
            get { return _ItemSelectedCommand; }
            set
            {
                if (_ItemSelectedCommand != value)
                {
                    _ItemSelectedCommand = value;
                    onPropertyChanged("ItemSelectedCommand");
                }
            }
        }

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

        #endregion

        public bool canExecuteMethod(object param)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        #region Helper Methods

        /// <summary>
        /// Method for GetLayoutInfo Command 
        /// Method to change the Orientation and Type of Layout
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetLayoutInfoCommand(object obj)
        {
            var args = obj as LayoutInfoArgs;
            if (LayoutManager.Layout is DirectedTreeLayout)
            {
                if ((LayoutManager.Layout as DirectedTreeLayout).Type == LayoutType.Organization)
                {
                    //if (args.Item is INode)
                    //{
                    //    if (((args.Item as INode).Content as StaffData).Designation.ToString() == "DIRECTOR")
                    //    {
                    //        args.Assistants.Add(args.Children[0]);
                    //        args.Children.Remove(args.Children[0]);
                    //    }
                    //}

                    switch (compact)
                    {
                        case "left":
                            if (!args.HasSubTree)
                            {
                                args.Type = ChartType.Left;
                                args.Orientation = Orientation.Vertical;
                            }
                            break;
                        case "right":
                            if (!args.HasSubTree)
                            {
                                args.Type = ChartType.Right;
                                args.Orientation = Orientation.Vertical;
                            }
                            break;
                        case "alternate":
                            if (!args.HasSubTree)
                            {
                                args.Type = ChartType.Alternate;
                                args.Orientation = Orientation.Vertical;
                            }
                            break;
                        case "horizontal_center":
                            if (!args.HasSubTree)
                            {
                                args.Type = ChartType.Center;
                                args.Orientation = Orientation.Horizontal;
                            }
                            break;
                        case "horizontal_right":
                            if (!args.HasSubTree)
                            {
                                args.Type = ChartType.Right;
                                args.Orientation = Orientation.Horizontal;
                            }
                            break;
                        case "horizontal_left":
                            if (!args.HasSubTree)
                            {
                                args.Type = ChartType.Left;
                                args.Orientation = Orientation.Horizontal;
                            }
                            break;
                    }
                }
            }
        }

        private void OnorgCompactLeft_Command(object obj)
        {
            if (obj != null && obj is Button)
            {
                Button button = obj as Button;
                if (prevbutton != null)
                {
                    prevbutton.Style = App.Current.MainWindow.Resources["ButtonStyle"] as Style;
                }
                button.Style = App.Current.MainWindow.Resources["SelectedButtonStyle"] as Style;

                if (button.Name.Equals("orgCompactLeft"))
                {
                    compact = "left";
                }
                else if (button.Name.Equals("orgCompactRight"))
                {
                    compact = "right";
                }
                else if (button.Name.Equals("orgCompactAlternate"))
                {
                    compact = "alternate";
                }
                else if (button.Name.Equals("orgCompactCenter"))
                {
                    compact = "horizontal_center";
                }
                else if (button.Name.Equals("orgCompactHorizontalRight"))
                {
                    compact = "horizontal_right";
                }
                else if (button.Name.Equals("orgCompactHorizontalLeft"))
                {
                    compact = "horizontal_left";
                }
                (LayoutManager.Layout as DirectedTreeLayout).UpdateLayout();

                prevbutton = obj as Button;
            }
        }

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
                    ((args as Node).Content as StaffData).IsExpand = State.Collapse;
                }
                else
                {
                    graphinfo.Commands.ExpandCollapse.Execute(obj);
                    ((args as Node).DataContext as NodeViewModel).IsExpanded = true;
                    ((args as Node).Content as StaffData).IsExpand = State.Expand;
                }
            }
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
                    anno.Length = 0.9;
                    //anno.Displacement = 60;
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

        private StaffDataList Getdata()
        {
            kirinentities = new KIRINEntities1();
            kirinentities.Database.CommandTimeout = 300;
            var staffData = kirinentities.GetOrganizationalData().ToList();

            StaffDataList staff = new StaffDataList();

            foreach (GetOrganizationalData_Result item in staffData)
            {
                staff.Add(new StaffData()
                {
                    Id = Convert.ToInt32(item.RNo),
                    Name = item.Name,
                    Designation = item.Level == 6 ? item.ADDRESS : item.Designation,
                    TbDesignation = item.Designation,
                    ImageUrl = "pack://application:,,,/Images/Logo.png",
                    RatingColor = item.RatingColor,
                    ReportingPerson = item.Level != 1 ? item.ReportingPerson : null,
                    Level = item.Level,
                    PhoneNo = item.PHONE_NUMBER,
                    Email = item.EMAIL,
                    Visibility = item.Level == 3 ? Visibility.Hidden : item.Level == 10 && item.RoleID == 0 ? Visibility.Hidden : item.Level == 5 && item.RoleID == 0 ? Visibility.Hidden : Visibility.Visible,
                    HomeRoom = item.HomeRoom,
                    LblDesignation = item.Level == 6 ? "Address" : item.Level == 10 && item.RoleID != 0 ? "Department" : "Designation",
                    Roomvisibility = item.Designation == "TEACHER" ? Visibility.Visible : Visibility.Hidden,
                    Imgvisibility = item.Level == 3 || item.Level == 5 || item.Level == 6 || item.Level == 10 && item.RoleID == 0 ? Visibility.Hidden : Visibility.Visible,
                    _Shape = item.Level == 3 || item.Level == 5 ? App.Current.Resources["PaperTap"] as string : App.Current.Resources["RoundedRectangle"] as string,
                    _Width = item.Level == 6 ? 380 : item.Level == 3 || item.Level == 5 ? 270 : 200,
                    _Height = item.Level == 3 || item.Level == 5 ? 70 : 50,
                    IsRoot = item.Level == 11 || item.Level == 2 || item.Level == 8 ? false : true,
                    FontSize = item.Level == 3 || item.Level == 5 || item.Level == 6 ? 14 : 12,
                    ParentId = item.parentId.ToString(),
                    //Alignment = item.Level == 6 ? HorizontalAlignment.Center : HorizontalAlignment.Left
                }); ;
            }

            return staff;
        }


    }

}
