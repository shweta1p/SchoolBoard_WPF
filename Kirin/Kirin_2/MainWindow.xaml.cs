using Kirin_2.Pages;
using Kirin_2.UserControls;
using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static Kirin_2.ViewModel.MainWindowVM;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable subsection_menuList;
        public List<string> TreeView_NonClickable_menuitems = new List<string>() {
            "Users", "Grade Center", "Attendance", "Predictiveanalysis", "Finicalmapping","Assessments,Administrator","Reports"
        };
        public DataSet ds;
        public App app;
        //string userName = App.Current.Properties["USERNAME"].ToString();
        string userName = string.Empty;
        MainWindowVM mvm;

        public MainWindow()
        {
            app = (App)Application.Current;
            app.Properties["USERNAME"] = "dduke@sabc.on.ca";
            userName = App.Current.Properties["USERNAME"].ToString(); //For Dev Perpose

            //var schoolData = App.Current.Properties["SchoolBoard"];

            mvm = new MainWindowVM(userName);
            this.DataContext = mvm; //new MainWindowVM(userName);
            InitializeComponent();

            //---------------Ended Insert data into slack-----------------//

            subsection_menuList = new DataTable("MenuList");
            subsection_menuList.Columns.Add("id", typeof(int));
            subsection_menuList.Columns.Add("ParentId", typeof(int));
            subsection_menuList.Columns.Add("Header");
            subsection_menuList.Columns.Add("Label");
            subsection_menuList.Columns.Add("Tag");

            subsection_menuList.Rows.Add(1, null, "Analytics", "Dashboard", "Dashboard");
            subsection_menuList.Rows.Add(2, null, "Analytics", "Sandbox", "Datasandbox");
            //subsection_menuList.Rows.Add(30, null, "Analytics", "Reference", "Datasandbox_Reference"); 
            subsection_menuList.Rows.Add(3, null, "Analytics", "Users", "Users");
            subsection_menuList.Rows.Add(4, 3, "Analytics", "SB Database", "SchoolDB");
            subsection_menuList.Rows.Add(25, 3, "Analytics", "School Directory", "StaffDirectory");
            subsection_menuList.Rows.Add(9, 3, "Analytics", "Class Directory", "Classes");
            //subsection_menuList.Rows.Add(5, null, "Analytics", "Sample", "Sample");
            subsection_menuList.Rows.Add(12, null, "Planner", "Register", "Gradebook");
            subsection_menuList.Rows.Add(7, 12, "Planner", "Attendance", "AttendancePage");
            subsection_menuList.Rows.Add(5, 12, "Planner", "Alerts", "Alerts");
            subsection_menuList.Rows.Add(6, null, "Planner", "Grade Center", "Gradebook");
            subsection_menuList.Rows.Add(8, 6, "Planner", "Class Calendar", "Classcalendar");
            subsection_menuList.Rows.Add(13, 12, "Planner", "StudentView", "StudentView");
            //subsection_menuList.Rows.Add(5, null, "Analytics", "Insight", "DashboardInsights");

            subsection_menuList.Rows.Add(10, 6, "Planner", "Scoresheet", "ScoreSheet");
            subsection_menuList.Rows.Add(11, 6, "Planner", "Assignment", "AssignmentMaker");
            //subsection_menuList.Rows.Add(12, 7, "Planner", "MultidayAttendance" , "ClassMultiDayAttendance_New");
            //subsection_menuList.Rows.Add(11, 7, "Planner", "Attendance 1" , "AttendancePage");
            //subsection_menuList.Rows.Add(12, 7, "Planner", "Attendance 2", "Attendance2");

            //subsection_menuList.Rows.Add(13, null, "Machinelearning", "Predictive Analysis", "Predictiveanalysis");
            //subsection_menuList.Rows.Add(14, null, "Machinelearning", "ML Dashboard", "MLDashboard");
            //subsection_menuList.Rows.Add(15, null, "Machinelearning", "ML Sandbox", "MLSandbox");
            //subsection_menuList.Rows.Add(16, 13, "Machinelearning", "Analysis 1", "Analysis1");
            //subsection_menuList.Rows.Add(17, 13, "Machinelearning", "Analysis 2", "Analysis2");
            subsection_menuList.Rows.Add(18, null, "Resourcemanagement", "Financial Planning", "MappingSV");
            subsection_menuList.Rows.Add(19, null, "Resourcemanagement", "Flow Charts", "Assessments");
            subsection_menuList.Rows.Add(20, 18, "Resourcemanagement", "Budgeting", "MappingSV");
            subsection_menuList.Rows.Add(27, 18, "Resourcemanagement", "Spreadsheet", "ExcelPage");
            //subsection_menuList.Rows.Add(21, 18, "Resourcemanagement", "Tabular View", "TabularFinancialView");
            //subsection_menuList.Rows.Add(21, 18, "Resourcemanagement", "Child View", "Mapping1_ChildView");

            //subsection_menuList.Rows.Add(21, 18, "Resourcemanagement", "Map 2", "Mapping2");
            //subsection_menuList.Rows.Add(22, 19, "Resourcemanagement", "Hierarchial Flow", "Assessment1");
            subsection_menuList.Rows.Add(22, 19, "Resourcemanagement", "Hierarchial Flow", "HierarchicalView");
            subsection_menuList.Rows.Add(23, 19, "Resourcemanagement", "Organizational Structure", "OrganizationChart");
            subsection_menuList.Rows.Add(26, 19, "Resourcemanagement", "Flowchart Sandbox", "FinancialMapSandbox");
            //subsection_menuList.Rows.Add(24, 19, "Resourcemanagement", "Sandbox", "ResourcesSandbox");

            //subsection_menuList.Rows.Add(28, null, "Report", "Reports", "StudentRoster");
            subsection_menuList.Rows.Add(28, null, "Planner", "Reports", "StudentRoster");
            subsection_menuList.Rows.Add(29, 28, "Planner", "StudentRoster", "StudentRoster");
            subsection_menuList.Rows.Add(30, 28, "Planner", "Class Score", "Scoreesheet_Report");
            subsection_menuList.Rows.Add(31, 28, "Planner", "Attendance Report", "AttendanceReport");

            subsection_menuList.Rows.Add(32, null, "Administrator", "Administrator", "Settings");
            subsection_menuList.Rows.Add(33, 32, "Administrator", "Settings", "Settings");

            //Use a DataSet to manage the data
            ds = new DataSet();
            ds.Tables.Add(subsection_menuList);

            //add a relationship
            ds.Relations.Add("rsParentChild", ds.Tables["MenuList"].Columns["id"], ds.Tables["MenuList"].Columns["ParentId"]);
            PopulateTreeRoot("Analytics");

            app.classSelector = new UC_ClassSelector();
            app.addAlerts = new UC_AddAlerts();
            app.staffProfileModal.Visibility = Visibility.Collapsed;
            app.attendanceComment.Visibility = Visibility.Collapsed;
            app.studentProfileModal.Visibility = Visibility.Collapsed;
            app.classSelector.Visibility = Visibility.Collapsed;
            app.viewBulletin.Visibility = Visibility.Collapsed;
            app.addColumn.Visibility = Visibility.Collapsed;
            app.contextmenu.Visibility = Visibility.Collapsed;
            app.addAssignment.Visibility = Visibility.Collapsed;
            app.attendanceComment.Visibility = Visibility.Collapsed;
            app.score_LearningSkills.Visibility = Visibility.Collapsed;
            app.studentViewforPieChart.Visibility = Visibility.Collapsed;
            app.studentViewforScatter.Visibility = Visibility.Collapsed;
            app.citizenshipView.Visibility = Visibility.Collapsed;
            app.attendaceCodeView.Visibility = Visibility.Collapsed;
            app.ethnicityView.Visibility = Visibility.Collapsed;
            app.addAlerts.Visibility = Visibility.Collapsed;
            app.citizenshipBar.Visibility = Visibility.Collapsed;
            app.transferStdBar.Visibility = Visibility.Collapsed;
            app.dobBar.Visibility = Visibility.Collapsed;
            app.dobPie.Visibility = Visibility.Collapsed;
            app.sportsPie.Visibility = Visibility.Collapsed;
            app.sportsBar.Visibility = Visibility.Collapsed;
            app.sportsAttendance.Visibility = Visibility.Collapsed;
            app.clubjoined.Visibility = Visibility.Collapsed;
            app.diversityBar.Visibility = Visibility.Collapsed;
            app.diversityPie.Visibility = Visibility.Collapsed;
            app.languageBar.Visibility = Visibility.Collapsed;
            app.deleteAssignment.Visibility = Visibility.Collapsed;
            app.tsStackedBar.Visibility = Visibility.Collapsed;
            app.fusionCalender.Visibility = Visibility.Collapsed;
            app.singleDay.Visibility = Visibility.Collapsed;
            app.svMapView.Visibility = Visibility.Collapsed;
            app.transportAbvr.Visibility = Visibility.Collapsed;
            app.subjectListView.Visibility = Visibility.Collapsed;
            app.insightFeedback.Visibility = Visibility.Collapsed;
            app.assignmentList.Visibility = Visibility.Collapsed;
            app.sportsList.Visibility = Visibility.Collapsed;
            app.languageList.Visibility = Visibility.Collapsed;
            app.scatterLegend.Visibility = Visibility.Collapsed;
            //app.imageMessage.Visibility = Visibility.Collapsed;
            app.accountAccess.Visibility = Visibility.Collapsed;
            app.iosStream.Visibility = Visibility.Collapsed;
            app.dataFilter.Visibility = Visibility.Collapsed;

            MainWindowGrid.Children.Add(app.staffProfileModal);
            MainWindowGrid.Children.Add(app.studentProfileModal);
            MainWindowGrid.Children.Add(app.classSelector);
            MainWindowGrid.Children.Add(app.viewBulletin);
            MainWindowGrid.Children.Add(app.addColumn);
            MainWindowGrid.Children.Add(app.contextmenu);
            MainWindowGrid.Children.Add(app.addAssignment);
            MainWindowGrid.Children.Add(app.attendanceComment);
            MainWindowGrid.Children.Add(app.score_LearningSkills);
            MainWindowGrid.Children.Add(app.studentViewforPieChart);
            MainWindowGrid.Children.Add(app.studentViewforScatter);
            MainWindowGrid.Children.Add(app.citizenshipView);
            MainWindowGrid.Children.Add(app.attendaceCodeView);
            MainWindowGrid.Children.Add(app.ethnicityView);
            MainWindowGrid.Children.Add(app.addAlerts);
            MainWindowGrid.Children.Add(app.citizenshipBar);
            MainWindowGrid.Children.Add(app.transferStdBar);
            MainWindowGrid.Children.Add(app.dobBar);
            MainWindowGrid.Children.Add(app.dobPie);
            MainWindowGrid.Children.Add(app.sportsPie);
            MainWindowGrid.Children.Add(app.sportsBar);
            MainWindowGrid.Children.Add(app.sportsAttendance);
            MainWindowGrid.Children.Add(app.clubjoined);
            MainWindowGrid.Children.Add(app.diversityBar);
            MainWindowGrid.Children.Add(app.diversityPie);
            MainWindowGrid.Children.Add(app.languageBar);
            MainWindowGrid.Children.Add(app.deleteAssignment);
            MainWindowGrid.Children.Add(app.tsStackedBar);
            MainWindowGrid.Children.Add(app.fusionCalender);
            MainWindowGrid.Children.Add(app.singleDay);
            MainWindowGrid.Children.Add(app.svMapView);
            MainWindowGrid.Children.Add(app.transportAbvr);
            MainWindowGrid.Children.Add(app.subjectListView);
            MainWindowGrid.Children.Add(app.insightFeedback);
            MainWindowGrid.Children.Add(app.assignmentList);
            MainWindowGrid.Children.Add(app.sportsList);
            MainWindowGrid.Children.Add(app.languageList);
            MainWindowGrid.Children.Add(app.scatterLegend);
            //MainWindowGrid.Children.Add(app.imageMessage);
            MainWindowGrid.Children.Add(app.accountAccess);
            MainWindowGrid.Children.Add(app.iosStream);
            MainWindowGrid.Children.Add(app.dataFilter);

            Storyboard sb = this.FindResource("OpenMenuSSPnl_nonFlick") as Storyboard;
            sb.Begin();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    //  NavigationService.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "ScoreSheet_StudentView.xaml?ID=" + (sender as Button).Tag), UriKind.RelativeOrAbsolute));
                    MainWindowFrame.Navigate(new Dashboard());
                    // (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", "ScoreSheet_StudentView?ID="+(sender as Button).Tag, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
            Globals.reset = 0;
        }

        public void PopulateTreeRoot(string selectedMenuItem)
        {
            string strFilter = "Header = '" + selectedMenuItem + "'";
            DataRow[] filteredSubmenuList;
            filteredSubmenuList = ds.Tables["MenuList"].Select(strFilter);

            subsection_tv.Items.Clear();
            foreach (DataRow dr in filteredSubmenuList)
            {
                if (dr["ParentId"] == DBNull.Value)
                {
                    TreeViewItem root = new TreeViewItem();
                    // root.MouseEnter += new MouseEventHandler(this.subsection_tv_MouseEnter); 
                    root.Header = dr["Label"].ToString();
                    root.Tag = dr["Tag"].ToString();
                    subsection_tv.Items.Add(root);

                    PopulateTree(dr, root);
                }
            }
            subsection_tv.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(TvItemSelect));
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintPreview.Print_WPF_Preview(MainWindowFrame);
        }

        public void PopulateTree(DataRow dr, TreeViewItem pNode)
        {
            foreach (DataRow row in dr.GetChildRows("rsParentChild"))
            {
                TreeViewItem cChild = new TreeViewItem();

                cChild.Header = row["Label"].ToString();
                cChild.Tag = row["Tag"].ToString();
                pNode.Items.Add(cChild);

                //Recursively build the tree
                PopulateTree(row, cChild);
            }
        }

        private void TvItemSelect(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            Storyboard sb = this.FindResource("OpenMenuSSPnl_nonFlick") as Storyboard;
            sb.Begin();
            if (!TreeView_NonClickable_menuitems.Contains(item.Tag))
            {
                this.SwitchScreen(item.Tag as String);
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void subsection_flyinbtn_Click(object sender, RoutedEventArgs e)
        {
            subsection_flyinbtn.Visibility = Visibility.Collapsed;
            subsection_flyoutbtn.Visibility = Visibility.Visible;
        }

        private void subsection_flyoutbtn_Click(object sender, RoutedEventArgs e)
        {
            subsection_flyinbtn.Visibility = Visibility.Visible;
            subsection_flyoutbtn.Visibility = Visibility.Collapsed;
        }

        internal void SwitchScreen(string page)
        {
            // Open Subsection menu panel
            Storyboard sb = this.FindResource("OpenMenuSSPnl_nonFlick") as Storyboard;
            sb.Begin();
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (page != null)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", page, ".xaml"), UriKind.RelativeOrAbsolute));
                        }
                    }
                    else
                    {
                        MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", "TBD", ".xaml"), UriKind.RelativeOrAbsolute));
                    }
                }
            }
            catch (Exception)
            {
                MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", "TBD", ".xaml"), UriKind.RelativeOrAbsolute));
            }
        }

        private void TreeViewItem_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            var sel = e.Source as TreeViewItem;
            if (sel != null)
            {
                String _tag = sel.Tag as String;
                this.SwitchScreen(_tag);
            }

            subsection_flyinbtn.Visibility = Visibility.Visible;
            subsection_flyoutbtn.Visibility = Visibility.Collapsed;
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            // img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 0.3;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            HelpPopup.Visibility = Visibility.Collapsed;
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HelpSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            String _tag;

            subsection_flyinbtn.Visibility = Visibility.Visible;
            subsection_flyoutbtn.Visibility = Visibility.Collapsed;

            if (item != null)
            {
                _tag = item.Tag as String;
                //navigate frame to selected Page

                switch (_tag)
                {
                    case "Analytics":
                        {
                            this.SwitchScreen("Dashboard");
                            break;
                        }
                    case "Planner":
                        {
                            this.SwitchScreen("AttendancePage");
                            break;
                        }
                    //case "Machinelearning":
                    //    {
                    //        this.SwitchScreen("MLSandbox");
                    //        break;
                    //    }
                    case "ResourceManagement":
                        {
                            this.SwitchScreen("MappingSV");
                            break;
                        }
                    //case "Report":
                    //    {
                    //        this.SwitchScreen("StudentRoster");
                    //        break;
                    //    }
                    case "Settings":
                        {
                            this.SwitchScreen("Settings");
                            break;
                        }
                    default:
                        break;

                }


                // this.SwitchScreen(_tag);

                //Repopulate subsection list according to selected Side Menu Item
                PopulateTreeRoot(_tag);

                if (_tag == "Settings")
                {
                    this.SwitchScreen(_tag);
                    Storyboard sbCloseMenuSSPnl = this.FindResource("CloseMenuSSPnl") as Storyboard;
                    sbCloseMenuSSPnl.Begin();
                }
                else
                {
                    // Open Subsection menu panel
                    Storyboard sb = this.FindResource("OpenMenuSSPnl") as Storyboard;
                    sb.Begin();

                    //close TPanel 
                    Storyboard sbCloseTPanel = this.FindResource("CloseMenu") as Storyboard;
                    sbCloseTPanel.Begin();
                }

                //  subsection_tv.ItemsSource = new DataView(subsection_menuList);
            }
        }

        private void ListViewItem_PreviewMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            String _tag;

            //subsection_flyinbtn.Visibility = Visibility.Visible;
            //subsection_flyoutbtn.Visibility = Visibility.Collapsed;

            if (item != null)
            {
                _tag = item.Tag as String;

                switch (_tag)
                {
                    case "Profile":
                        {
                            //this.SwitchScreen("Profile");
                            break;
                        }
                    case "Settings":
                        {
                            this.SwitchScreen("Settings");
                            break;
                        }
                    case "Docs":
                        {
                            //this.SwitchScreen("Docs");
                            break;
                        }
                    case "Logout":
                        {
                            Close();
                            //this.SwitchScreen("Settings");
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                mw.MainWindowFrame.Content = new Dashboard();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            //// Begin dragging the window
            //this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SwitchScreen("Dashboard");
            AnalyticsLVItem.IsSelected = true;
            PlannerLVItem.IsSelected = false;
            //MLLVItem.IsSelected = false;
            RMLVItem.IsSelected = false;
            SettingsListViewItem.IsSelected = false;
            PopulateTreeRoot("Analytics");

            if (subsection_tv.Items.Count > 0)
            {
                for (int i = 0; i < subsection_tv.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        TreeViewItem childNode = subsection_tv.ItemContainerGenerator
                            .ContainerFromItem(subsection_tv.Items[i]) as TreeViewItem;

                        childNode.IsSelected = true;

                    }
                }
            }
        }

        private void View_DailyBulletin(object sender, RoutedEventArgs e)
        {
            //DailyBulletin dbwin = new DailyBulletin();
            //dbwin.Show();

            ((App)Application.Current).viewBulletin.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindowFrame.NavigationService.CanGoBack)
            {
                MainWindowFrame.NavigationService.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindowFrame.NavigationService.CanGoForward)
            {
                MainWindowFrame.NavigationService.GoForward();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            SettingsListViewItem.Margin = new Thickness(5, ah / 2, 0, 0);

            MainWindowFrame.Height = ah - 100;
            MainWindowFrame.Width = Wrapper.ActualWidth - subsection_pnl.Width;
            subsection_pnl.Height = MainWindowGrid.ActualHeight - MainWindowHeader.ActualHeight;
            app.MainFrameWidth = MainWindowFrame.Width;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    {
                        var ah = ActualHeight;
                        var aw = ActualWidth;
                        var h = Height;
                        var w = Width;

                        SettingsListViewItem.Margin = new Thickness(0, ah - 320, 0, 0);

                        MainWindowFrame.Height = ah - 100;
                        MainWindowFrame.Width = Wrapper.ActualWidth - subsection_pnl.Width;
                        subsection_pnl.Height = MainWindowGrid.ActualHeight - MainWindowHeader.ActualHeight;
                        app.MainFrameWidth = MainWindowFrame.Width;
                        break;
                    }
            }
        }

        private void Storyboard_OpenMenuSSPnl_CurrentStateInvalidated(object sender, EventArgs e)
        {
            //Console.WriteLine(MainWindowFrame.ActualWidth);
            var ah = ActualHeight;
            var aw = ActualWidth;
            //  MainWindowFrame.Width = aw -225;
            MainWindowFrame.Width = Wrapper.ActualWidth - subsection_pnl.ActualWidth;
            subsection_pnl.Height = MainWindowGrid.ActualHeight - MainWindowHeader.ActualHeight;
            app.MainFrameWidth = MainWindowFrame.Width;
        }

        private void Storyboard_CloseMenuSSPnl_CurrentStateInvalidated(object sender, EventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            //MainWindowFrame.Width = Wrapper.ActualWidth;
            MainWindowFrame.Width = Wrapper.ActualWidth - subsection_pnl.ActualWidth;
            subsection_pnl.Height = MainWindowGrid.ActualHeight - MainWindowHeader.ActualHeight;
            // FrameCol.Width = aw - 200;
            // FrameCol.Width =  new GridLength(aw - 225, GridUnitType.Pixel);
            app.MainFrameWidth = MainWindowFrame.Width;
        }

        private void Classes_MainCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            app.chkBoxLst.Clear();
            GradeBookViewModel gbvm = new GradeBookViewModel();
            gbvm.cDisplayColumns.Execute(app.chkBoxLst);


            this.SwitchScreen("Dashboard");
            AnalyticsLVItem.IsSelected = true;
            PlannerLVItem.IsSelected = false;
            //MLLVItem.IsSelected = false;
            RMLVItem.IsSelected = false;
            SettingsListViewItem.IsSelected = false;

            this.Loaded += (s, args) =>
            {
                PopulateTreeRoot("Analytics");

            };

            if (subsection_tv.Items.Count > 0)
            {
                for (int i = 0; i < subsection_tv.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        TreeViewItem childNode = subsection_tv.ItemContainerGenerator
              .ContainerFromItem(subsection_tv.Items[i]) as TreeViewItem;

                        childNode.IsSelected = true;

                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            app.Properties["USERNAME"] = userName;
            app.classSelector.Visibility = Visibility.Visible;
            //ShowUserControl1().Begin();
        }

        private Storyboard ShowUserControl1()
        {
            app.classSelector.RenderTransform = new TranslateTransform();

            // Root is your root container. In this case - Grid
            // and you have to add it only in the case if it does not exists

            Storyboard sb = new Storyboard();

            DoubleAnimation slidey = new DoubleAnimation();
            slidey.To = 100;
            slidey.From = 0;
            slidey.Duration = new Duration(TimeSpan.FromMilliseconds(400));

            ScaleTransform scaleTransform = new ScaleTransform()
            {
                CenterX = 0,
                CenterY = 0,
                ScaleX = 400,
                ScaleY = 300
            };

            // Set the target of the animation
            Storyboard.SetTarget(slidey, app.classSelector);
            Storyboard.SetTargetProperty(slidey, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));


            // Kick the animation off
            sb.Children.Add(slidey);

            return sb;
        }

        private void ShowContextMenu(object sender, RoutedEventArgs e)
        {
            app.contextmenu.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Width = Wrapper.ActualWidth - subsection_pnl.ActualWidth;
            subsection_pnl.Height = MainWindowGrid.ActualHeight - MainWindowHeader.ActualHeight;
            app.MainFrameWidth = MainWindowFrame.Width;
        }

        private void subsection_tv_MouseEnter(object sender, MouseEventArgs e)
        {
            //    TreeViewItem t = (TreeViewItem)sender; 
            //    t.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("RED"));
            //    t.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("RED"));
            //e.Handled = false;
        }

        private void MouseWheelHandler(object sender, MouseWheelEventArgs e)
        {
            // If the mouse wheel delta is positive, move the box up.
            if (e.Delta > 0)
            {
                MainWindowHeader.Visibility = Visibility.Visible;
                rowToHide.Height = new GridLength(53, GridUnitType.Star);
            }
            // If the mouse wheel delta is negative, move the box down.
            if (e.Delta < 0)
            {
                MainWindowHeader.Visibility = Visibility.Collapsed;
                MainWindowHeader.Background = Brushes.Transparent;
                rowToHide.Height = new GridLength(0);
            }
        }

        private void DGNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewNotifications data = ((DataGrid)sender).SelectedItem as NewNotifications;

            if (data != null)
            {
                string type = data.Type.ToString();
                DateTime updatedDate = data.Originaldate;
                string classId = app.Properties["SubjectId"].ToString();

                switch (type)
                {
                    case "Attendance":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new ClassAttendance(classId, updatedDate));
                        break;
                    case "Comment":
                        break;
                    case "Insight":
                        break;
                    case "Download":
                        break;
                }
            }
            //this.DataContext = new MainWindowVM(userName);
        }

        private void DGEarlier_SelectionChanged(object sender, EventArgs e)
        {
            EarlierNotifications data = ((DataGrid)sender).SelectedItem as EarlierNotifications;

            if (data != null)
            {
                string type = data.Type.ToString();
                DateTime updatedDate = data.Originaldate;
                string classId = app.Properties["SubjectId"].ToString();

                switch (type)
                {
                    case "Attendance":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new ClassAttendance(classId, updatedDate));
                        break;
                    case "Comment":
                        break;
                    case "Insight":
                        break;
                    case "Download":
                        break;
                }
            }
            //this.DataContext = new MainWindowVM(userName);
        }

        private void NewClose_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            if (ID != null)
            {
                DataGridRow dataGridRow;
                int i = 0;
                ObservableCollection<NewNotifications> list = mvm.NewNotificationList;

                foreach (NewNotifications n in DGNew.ItemsSource)
                {
                    if (n.ID == ID)
                    {
                        dataGridRow = DGNew.ItemContainerGenerator.ContainerFromItem(list[i]) as DataGridRow;
                        dataGridRow.Visibility = Visibility.Collapsed;
                        return;
                    }
                    i++;
                }

                dataGridRow = DGNew.ItemContainerGenerator.ContainerFromItem(list[i]) as DataGridRow;
                dataGridRow.Visibility = Visibility.Visible;
            }
        }

        private void EarlierClose_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            if (ID != null)
            {
                DataGridRow dataGridRow;
                int i = 0;
                ObservableCollection<EarlierNotifications> list = mvm.EarlierNotificationList;

                foreach (EarlierNotifications n in DGEarlier.ItemsSource)
                {
                    if (n.ID == ID)
                    {
                        dataGridRow = DGEarlier.ItemContainerGenerator.ContainerFromItem(list[i]) as DataGridRow;
                        dataGridRow.Visibility = Visibility.Collapsed;
                        return;
                    }
                    i++;
                }

                dataGridRow = DGEarlier.ItemContainerGenerator.ContainerFromItem(list[i]) as DataGridRow;
                dataGridRow.Visibility = Visibility.Visible;
            }
        }

        private void btnNewPin_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            if (ID != null)
            {
                foreach (NewNotifications n in DGNew.ItemsSource)
                {
                    if (n.ID == ID)
                    {
                        mvm.SchoolAlertsList.Add(new SchoolAlerts
                        {
                            ID = n.ID.ToString(),
                            Message = n.DesignedMessage,
                            Type = n.Type,
                            Template = n.Template,
                            Student = n.Student,
                            Class = n.Class,
                            UpdatedBy = n.UpdatedBy,
                            Document = n.Document,
                            UpdatedDate = n.UpdatedDate,
                            //Icon = ByteArrayToImage(s.ICON),
                            Navigate = n.Navigate,
                            MovedType = "N",
                            Visibility = Visibility.Visible,
                            date = n.Originaldate
                        });

                        DGSchoolAlert.ItemsSource = mvm.SchoolAlertsList.OrderByDescending(a => a.date).ToList();

                        //listNew.Remove(n);
                        mvm.NewNotificationList.Remove(n);
                        DGNew.ItemsSource = mvm.NewNotificationList.OrderByDescending(a => a.Originaldate).ToList();

                        return;
                    }
                }
            }
        }

        private void btnEarlierPin_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            if (ID != null)
            {
                foreach (EarlierNotifications en in DGEarlier.ItemsSource)
                {
                    if (en.ID == ID)
                    {
                        mvm.SchoolAlertsList.Add(new SchoolAlerts
                        {
                            ID = en.ID.ToString(),
                            Message = en.DesignedMessage,
                            Type = en.Type,
                            Template = en.Template,
                            Student = en.Student,
                            Class = en.Class,
                            UpdatedBy = en.UpdatedBy,
                            Document = en.Document,
                            UpdatedDate = en.UpdatedDate,
                            //Icon = ByteArrayToImage(s.ICON),
                            Navigate = en.Navigate,
                            MovedType = "E",
                            Visibility = Visibility.Visible,
                            date = en.Originaldate
                        });

                        DGSchoolAlert.ItemsSource = mvm.SchoolAlertsList.OrderByDescending(a => a.date).ToList();

                        //listEarlier.Remove(en);
                        mvm.EarlierNotificationList.Remove(en);
                        DGEarlier.ItemsSource = mvm.EarlierNotificationList.OrderByDescending(a => a.Originaldate).ToList();

                        return;
                    }
                }
            }
        }

        private void UnPin_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            if (ID != null)
            {
                foreach (SchoolAlerts s in DGSchoolAlert.ItemsSource)
                {
                    if (s.ID == ID)
                    {
                        if (s.MovedType == "E")
                        {
                            //listEarlier.Add(new EarlierNotifications
                            //{
                            //    ID = s.ID.ToString(),
                            //    Message = s.Message,
                            //    Type = s.Type,
                            //    Template = s.Template,
                            //    Student = s.Student,
                            //    Class = s.Class,
                            //    UpdatedBy = s.UpdatedBy,
                            //    Document = s.Document,
                            //    UpdatedDate = s.UpdatedDate,
                            //    Navigate = s.Navigate
                            //});

                            mvm.EarlierNotificationList.Add(new EarlierNotifications
                            {
                                ID = s.ID.ToString(),
                                Message = s.Message,
                                Type = s.Type,
                                Template = s.Template,
                                Student = s.Student,
                                Class = s.Class,
                                UpdatedBy = s.UpdatedBy,
                                Document = s.Document,
                                UpdatedDate = s.UpdatedDate,
                                Navigate = s.Navigate,
                                DesignedMessage = s.Message,
                                Originaldate = s.date
                            });

                            DGEarlier.ItemsSource = mvm.EarlierNotificationList.OrderByDescending(a => a.Originaldate).ToList();

                            //listSchool.Remove(s);
                            mvm.SchoolAlertsList.Remove(s);
                            DGSchoolAlert.ItemsSource = mvm.SchoolAlertsList.OrderByDescending(a => a.date).ToList();

                            return;
                        }
                        else if (s.MovedType == "N")
                        {
                            //listNew.Add(new NewNotifications
                            //{
                            //    ID = s.ID.ToString(),
                            //    Message = s.Message,
                            //    Type = s.Type,
                            //    Template = s.Template,
                            //    Student = s.Student,
                            //    Class = s.Class,
                            //    UpdatedBy = s.UpdatedBy,
                            //    Document = s.Document,
                            //    UpdatedDate = s.UpdatedDate,
                            //    Navigate = s.Navigate
                            //});

                            mvm.NewNotificationList.Add(new NewNotifications
                            {
                                ID = s.ID.ToString(),
                                Message = s.Message,
                                Type = s.Type,
                                Template = s.Template,
                                Student = s.Student,
                                Class = s.Class,
                                UpdatedBy = s.UpdatedBy,
                                Document = s.Document,
                                UpdatedDate = s.UpdatedDate,
                                Navigate = s.Navigate,
                                DesignedMessage = s.Message,
                                Originaldate = s.date
                            });

                            DGNew.ItemsSource = mvm.NewNotificationList.OrderByDescending(a => a.Originaldate).ToList();

                            //listSchool.Remove(s);
                            mvm.SchoolAlertsList.Remove(s);
                            DGSchoolAlert.ItemsSource = mvm.SchoolAlertsList.OrderByDescending(a => a.date).ToList();

                            return;
                        }
                    }
                }
            }
        }

        private void feedbackBtnClicked(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            var subWindow = Window.GetWindow(new FeedbackWindow(bitmap));
            subWindow.Closing += subWindow_Closing;
            subWindow.Show();
            feedbackBtn.IsEnabled = false;
        }

        private void subWindow_Closing(object sender, CancelEventArgs e)
        {
            feedbackBtn.IsEnabled = true;
        }

        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            //SearchPopup.IsOpen = false;
            BitmapImage bitmap = new BitmapImage();
            var subWindow = Window.GetWindow(new FeedbackWindow(bitmap));
            subWindow.Closing += subWindow_Closing;
            subWindow.Show();

            feedbackBtn.IsEnabled = false;
        }

        private void Learn_RequestNavigate(object sender, RoutedEventArgs e)
        {

        }

        private void Students_Click(object sender, RoutedEventArgs e)
        {
            txtSearchTitle.Text = "Resent student searches";
            gdBasic.Visibility = Visibility.Collapsed;
            gdAdvanced.Visibility = Visibility.Visible;
            SearchPopup.StaysOpen = true;
            SearchBox.Focus();

            TextBlock txtfilter = (TextBlock)btntoggle.Template.FindName("txtfilter", btntoggle);
            txtfilter.Text = "Students";

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Visible;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Collapsed;

            DGSearchList.Visibility = Visibility.Visible;
            DGStaffList.Visibility = Visibility.Collapsed;
            DGAssignmentList.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
        }

        private void Staff_Click(object sender, RoutedEventArgs e)
        {
            txtSearchTitle.Text = "Resent staff searches";
            gdBasic.Visibility = Visibility.Collapsed;
            gdAdvanced.Visibility = Visibility.Visible;
            SearchPopup.StaysOpen = true;
            SearchBox.Focus();

            TextBlock txtfilter = (TextBlock)btntoggle.Template.FindName("txtfilter", btntoggle);
            txtfilter.Text = "Staff";

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Visible;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Collapsed;

            DGSearchList.Visibility = Visibility.Collapsed;
            DGStaffList.Visibility = Visibility.Visible;
            DGAssignmentList.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
        }

        private void Assignment_Click(object sender, RoutedEventArgs e)
        {
            txtSearchTitle.Text = "Resent assignment searches";
            gdBasic.Visibility = Visibility.Collapsed;
            gdAdvanced.Visibility = Visibility.Visible;
            SearchPopup.StaysOpen = true;
            SearchBox.Focus();

            TextBlock txtfilter = (TextBlock)btntoggle.Template.FindName("txtfilter", btntoggle);
            txtfilter.Text = "Assignment";

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Visible;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Collapsed;

            DGSearchList.Visibility = Visibility.Collapsed;
            DGStaffList.Visibility = Visibility.Collapsed;
            DGAssignmentList.Visibility = Visibility.Visible;
            gdGlobal.Visibility = Visibility.Collapsed;
        }

        private void btntoggle_Click(object sender, RoutedEventArgs e)
        {
            SearchPopup.IsOpen = true;
            gdBasic.Visibility = Visibility.Visible;
            gdGlobal.Visibility = Visibility.Collapsed;
            gdAdvanced.Visibility = Visibility.Collapsed;

            TextBox txtglobal = (TextBox)btntoggle.Template.FindName("GlobalSearchbox", btntoggle);
            txtglobal.Focus();
        }

        private void GlobalSearchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DGSearchList.Visibility == Visibility.Visible)
            {
                var AdvCollectionView = CollectionViewSource.GetDefaultView(DGSearchList.ItemsSource);
                AdvCollectionView.Filter = FilterRecords;
                AdvCollectionView.Refresh();
                SearchPopup.IsOpen = true;
            }
            else if (DGStaffList.Visibility == Visibility.Visible)
            {
                var AdvCollectionView = CollectionViewSource.GetDefaultView(DGStaffList.ItemsSource);
                AdvCollectionView.Filter = FilterRecords;
                AdvCollectionView.Refresh();
                SearchPopup.IsOpen = true;
            }
            else if (DGAssignmentList.Visibility == Visibility.Visible)
            {
                var AdvCollectionView = CollectionViewSource.GetDefaultView(DGAssignmentList.ItemsSource);
                AdvCollectionView.Filter = FilterRecords;
                AdvCollectionView.Refresh();
                SearchPopup.IsOpen = true;
            }
            else
            {
                gdGlobal.Visibility = Visibility.Visible;
                gdBasic.Visibility = Visibility.Collapsed;
                gdAdvanced.Visibility = Visibility.Collapsed;
                var AdvCollectionView = CollectionViewSource.GetDefaultView(DGGlobalSearch.ItemsSource);
                AdvCollectionView.Filter = FilterRecords;
                AdvCollectionView.Refresh();
                SearchPopup.IsOpen = true;
            }
        }

        public bool FilterRecords(object obj)
        {
            TextBox searchBox = (TextBox)btntoggle.Template.FindName("GlobalSearchbox", btntoggle);
            if (obj is AdvancedSearch adv)
            {
                if (searchBox != null)
                {
                    return adv.Name.ToUpper().StartsWith(searchBox.Text.ToUpper());
                }
            }
            else if (obj is AdvancedSearchStaff staff)
            {
                if (searchBox != null)
                {
                    return staff.Name.ToUpper().StartsWith(searchBox.Text.ToUpper());
                }
            }
            else if (obj is SearchAssignment assign)
            {
                if (searchBox != null)
                {
                    return assign.Name.ToUpper().StartsWith(searchBox.Text.ToUpper());
                }
            }
            else if (obj is GlobalSearch glob)
            {
                if (searchBox != null)
                {
                    return glob.Name.ToUpper().StartsWith(searchBox.Text.ToUpper());
                }
            }
            return false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            gdBasic.Visibility = Visibility.Visible;
            gdAdvanced.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
            DGSearchList.Visibility = Visibility.Collapsed;
            DGStaffList.Visibility = Visibility.Collapsed;
            DGAssignmentList.Visibility = Visibility.Collapsed;

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Collapsed;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Visible;
        }

        private void DGSearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdvancedSearch data = ((DataGrid)sender).SelectedItem as AdvancedSearch;

            if (data != null)
            {
                string type = data.Reference.ToString();

                switch (type)
                {
                    case ":in SB Database":
                        SchoolDB sb = new SchoolDB();
                        sb.rdStudents.IsChecked = true;
                        sb.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(sb);
                        break;
                    case ":in School Directory":
                        StaffDirectory sd = new StaffDirectory();
                        sd.rdStudents.IsChecked = true;
                        sd.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(sd);
                        break;
                    case ":in Class Directory":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new Classes());
                        break;
                    case ":in ScoreSheet":
                        ScoreSheet ss = new ScoreSheet();
                        ss.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(ss);
                        break;
                    case ":in Alerts":
                        Pages.Alerts alerts = new Pages.Alerts();
                        alerts.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(alerts);
                        break;
                    case ":in Assignment":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new AssignmentMaker());
                        break;
                }
            }

            gdBasic.Visibility = Visibility.Visible;
            gdAdvanced.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
            DGSearchList.Visibility = Visibility.Collapsed;

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Collapsed;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Visible;

            TextBox searchBox = (TextBox)btntoggle.Template.FindName("GlobalSearchbox", btntoggle);
            searchBox.Text = String.Empty;
            searchBox.Focus();

            SearchPopup.IsOpen = false;
        }

        private void DGStaffList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdvancedSearchStaff data = ((DataGrid)sender).SelectedItem as AdvancedSearchStaff;

            if (data != null)
            {
                string type = data.Reference.ToString();
                string role = data.Role.ToString();
                switch (type)
                {
                    case ":in SB Database":
                        SchoolDB sb = new SchoolDB();

                        if (role.ToLower() == "teacher")
                        {
                            sb.rdTeachers.IsChecked = true;
                        }
                        else if (role.ToLower() == "support")
                        {
                            sb.rdStaff.IsChecked = true;
                        }
                        else if (role.ToLower() == "admin")
                        {
                            sb.rdAdmin.IsChecked = true;
                        }
                        else
                        {
                            sb.rdAll.IsChecked = true;
                        }
                        sb.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(sb);
                        break;
                    case ":in School Directory":
                        StaffDirectory sd = new StaffDirectory();
                        if (role.ToLower() == "teacher")
                        {
                            sd.rdTeachers.IsChecked = true;
                        }
                        else if (role.ToLower() == "support")
                        {
                            sd.rdStaff.IsChecked = true;
                        }
                        else if (role.ToLower() == "lunchstaff")
                        {
                            sd.rdLStaff.IsChecked = true;
                        }
                        else
                        {
                            sd.rdAll.IsChecked = true;
                        }
                        sd.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(sd);
                        break;
                }
            }

            gdBasic.Visibility = Visibility.Visible;
            gdAdvanced.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
            DGStaffList.Visibility = Visibility.Collapsed;

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Collapsed;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Visible;

            TextBox searchBox = (TextBox)btntoggle.Template.FindName("GlobalSearchbox", btntoggle);
            searchBox.Text = String.Empty;
            searchBox.Focus();

            SearchPopup.IsOpen = false;
        }

        private void DGAssignmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchAssignment data = ((DataGrid)sender).SelectedItem as SearchAssignment;

            if (data != null)
            {
                string type = data.Reference.ToString();

                switch (type)
                {
                    case ":in Assignment":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new AssignmentMaker());
                        break;
                }
            }

            gdBasic.Visibility = Visibility.Visible;
            gdAdvanced.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
            DGAssignmentList.Visibility = Visibility.Collapsed;

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Collapsed;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Visible;

            TextBox searchBox = (TextBox)btntoggle.Template.FindName("GlobalSearchbox", btntoggle);
            searchBox.Text = String.Empty;
            searchBox.Focus();

            SearchPopup.IsOpen = false;
        }

        private void DGGlobalSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GlobalSearch data = ((DataGrid)sender).SelectedItem as GlobalSearch;

            if (data != null)
            {
                string type = data.Reference == ":in Pages" ? data.Name.ToString() : data.Reference.ToString();
                string role = data.Role.ToString();
                switch (type)
                {
                    case "SB Database":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new SchoolDB());
                        break;
                    case "School Directory":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new StaffDirectory());
                        break;
                    case "Class Directory":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new Classes());
                        break;
                    case "ScoreSheet":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new ScoreSheet());
                        break;
                    case "Alerts":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new Pages.Alerts());
                        break;
                    case "Assignment":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new AssignmentMaker());
                        break;
                    case ":in SB Database":
                        SchoolDB sb = new SchoolDB();
                        if (role.ToLower() == "teacher")
                        {
                            sb.rdTeachers.IsChecked = true;
                        }
                        else if (role.ToLower() == "student")
                        {
                            sb.rdStudents.IsChecked = true;
                        }
                        else if (role.ToLower() == "support")
                        {
                            sb.rdStaff.IsChecked = true;
                        }
                        else if (role.ToLower() == "admin")
                        {
                            sb.rdAdmin.IsChecked = true;
                        }
                        else
                        {
                            sb.rdAll.IsChecked = true;
                        }
                        sb.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(sb);
                        break;
                    case ":in School Directory":
                        StaffDirectory sd = new StaffDirectory();
                        if (role.ToLower() == "teacher")
                        {
                            sd.rdTeachers.IsChecked = true;
                        }
                        else if (role.ToLower() == "student")
                        {
                            sd.rdStudents.IsChecked = true;
                        }
                        else if (role.ToLower() == "support")
                        {
                            sd.rdStaff.IsChecked = true;
                        }
                        else if (role.ToLower() == "lunchstaff")
                        {
                            sd.rdLStaff.IsChecked = true;
                        }
                        else
                        {
                            sd.rdAll.IsChecked = true;
                        }
                        //sd.rdAll.IsChecked = true;
                        sd.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(sd);
                        break;
                    case ":in Class Directory":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new Classes());
                        break;
                    case ":in ScoreSheet":
                        ScoreSheet ss = new ScoreSheet();
                        ss.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(ss);
                        break;
                    case ":in Alerts":
                        Pages.Alerts alerts = new Pages.Alerts();
                        alerts.SearchBox.Text = data.LastName + ", " + data.FirstName;
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(alerts);
                        break;
                    case ":in Assignment":
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new AssignmentMaker());
                        break;
                }
            }

            gdBasic.Visibility = Visibility.Visible;
            gdAdvanced.Visibility = Visibility.Collapsed;
            gdGlobal.Visibility = Visibility.Collapsed;
            DGGlobalSearch.Visibility = Visibility.Collapsed;

            Border filterBorder = (Border)btntoggle.Template.FindName("spSearchFilter", btntoggle);
            filterBorder.Visibility = Visibility.Collapsed;

            Button Searchicon = (Button)btntoggle.Template.FindName("matSearch", btntoggle);
            Searchicon.Visibility = Visibility.Visible;

            TextBox searchBox = (TextBox)btntoggle.Template.FindName("GlobalSearchbox", btntoggle);
            searchBox.Text = String.Empty;
            searchBox.Focus();

            SearchPopup.IsOpen = false;
        }
    }
}
