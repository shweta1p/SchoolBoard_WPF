using Kirin_2.Models;
using Kirin_2.ViewModel.Command;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.ViewModel
{
    public class OrganizationChildVM : DiagramViewModel
    {
        KIRINEntities1 kirinentities;
        string parentId = App.Current.Properties["TeacherId"].ToString();

        private ICommand _orgCompactLeft_Command;
        private string compact;
        private ICommand _GetLayoutInfoCommand;
        public Button prevbutton = null;

        public OrganizationChildVM()
        {
            // Initialize Diagram properties
            Constraints = Constraints.Remove(GraphConstraints.PageEditing, GraphConstraints.PanRails);
            Menu = null;
            Tool = Tool.ZoomPan;
            DefaultConnectorType = ConnectorType.Orthogonal;
            
            //Initialize Commands
            orgCompactLeft_Command = new CommandVM(OnorgCompactLeft_Command, canExecuteMethod);
            GetLayoutInfoCommand = new CommandVM(OnGetLayoutInfoCommand, canExecuteMethod);
        

            // Initialize DataSourceSettings for SfDiagram
            DataSourceSettings = new FlowchartDataSourceSettings()
            {
                ParentId = "ReportingPerson",
                Id = "Name",
                DataSource = Getdata(parentId),  
                ShapeMapping = "_Shape",
                WidthMapping = "_Width",
                HeightMapping = "_Height"
            };

            // Initialize LayoutSettings for SfDiagram
            LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Organization,
                    HorizontalSpacing = 40,
                    VerticalSpacing = 50
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

        #endregion

        private StaffDataList Getdata(string parentId)
        {
            kirinentities = new KIRINEntities1();
            var studentData = kirinentities.GetStudentDatafromTeacherID(Convert.ToInt32(parentId)).ToList();

            StaffDataList staff = new StaffDataList();

            foreach (var item in studentData)
            {
                staff.Add(new StaffData()
                {
                    Id = item.ID,
                    Name = item.Name,
                    Designation = item.Designation,
                    ImageUrl = "pack://application:,,,/Images/Logo.png",
                    RatingColor = item.RatingColor,
                    ReportingPerson = item.Level != 1 ? item.ReportingPerson : null,
                    Level = item.Level,
                    PhoneNo = item.PHONE_NUMBER,
                    Email = item.EMAIL,
                    Imgvisibility = item.Level == 2 ? Visibility.Hidden : Visibility.Visible,
                    LblDesignation = item.Level == 2 ? "Subject" : "Designation",
                    Roomvisibility = item.Level != 2 ? Visibility.Visible : Visibility.Hidden,
                    Visibility = item.Level == 2 ? Visibility.Hidden : Visibility.Visible,
                    DesiVisibility = item.Level == 3 ? Visibility.Hidden : Visibility.Visible,
                    HomeRoom = item.HomeRoom,
                    _Shape = item.Level == 2 ? App.Current.Resources["PaperTap"] as string : App.Current.Resources["RoundedRectangle"] as string,
                    _Width = item.Level == 2 ? (item.Designation.Length * 15) : 200,
                    _Height = item.Level == 2 ? 70 : 50
                });

            }

            return staff;

        }


    }

}
