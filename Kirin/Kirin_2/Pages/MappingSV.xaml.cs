using Kirin_2.Models;
using Kirin_2.UserControls;
using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Mapping1.xaml
    /// </summary>
    public partial class MappingSV : Page
    {
        KIRINEntities1 kirinentities;
        FlowChartVM chartVM;

        public MappingSV()
        {
            InitializeComponent();
            //sfdiagram.Constraints = sfdiagram.Constraints.Remove(GraphConstraints.PageEditing, GraphConstraints.PanRails);

            (sfdiagram.Info as IGraphInfo).ItemTappedEvent += Mapping1_ChildView_ItemTappedEvent;
            FillFinancialYearCombo();

            chartVM = new FlowChartVM();
            this.DataContext = chartVM;
            Globals.reset = 0;
        }

        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}", "Pages/", "Mapping1.xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            int totalCount = cbFinancial.Items.Count;
            int selectedIndex = cbFinancial.SelectedIndex;

            if (selectedIndex < totalCount && selectedIndex != 0)
            {
                cbFinancial.SelectedItem = cbFinancial.Items[selectedIndex - 1].ToString();

                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            this.NavigationService.Navigate(new MappingSV());
                        }
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int totalCount = cbFinancial.Items.Count;
            int selectedIndex = cbFinancial.SelectedIndex;

            if (selectedIndex < totalCount - 1)
            {
                cbFinancial.SelectedItem = cbFinancial.Items[selectedIndex + 1].ToString();

                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            this.NavigationService.Navigate(new MappingSV());
                        }
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            string yearFolder = chartVM.Year;
            string excelFilePath = startupPath + ConfigurationManager.AppSettings.Get("budgetPath") + yearFolder + "\\";

            //--------Open File in Spreadsheet---------------//
            string fileName = string.Empty;
            DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
            List<FileInfo> files = directoryInfo.GetFiles("*.xlsx").ToList();

            if (files.Count > 0)
            {
                fileName = files[0].FullName;
            }

            string pathString = Path.Combine(excelFilePath, fileName);

            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new UC_ExcelView(pathString));
                    }
                }
            }
            catch (Exception ee)
            {
            }

            ////-------------------Download File on button click---------------------//
            //string fileName = string.Empty;
            //DirectoryInfo directoryInfo = new DirectoryInfo(excelFilePath);
            //List<FileInfo> files = directoryInfo.GetFiles("*.xlsx").ToList();

            //for (int i = 0; i < files.Count; i++)
            //{
            //    fileName = files[i].Name;
            //}

            //string pathString = Path.Combine(excelFilePath, fileName);
            //var uri = new Uri(pathString);

            //if (!File.Exists(excelFilePath))
            //{
            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            //    webClient.DownloadFileAsync(uri, "Budget_" + yearFolder + ".xlsx");
            //}
            //else
            //{
            //    Console.WriteLine("File \"{0}\" already exists.", fileName);
            //    return;
            //}
        }

        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            string yearFolder = chartVM.Year;
            string pdfFilePath = startupPath + ConfigurationManager.AppSettings.Get("budgetPath") + yearFolder + "\\";

            //--------Open File in Spreadsheet---------------//
            string fileName = string.Empty;
            DirectoryInfo directoryInfo = new DirectoryInfo(pdfFilePath);
            List<FileInfo> files = directoryInfo.GetFiles("*.pdf").ToList();

            if (files.Count > 0)
            {
                fileName = files[0].FullName;
            }

            string pathString = Path.Combine(pdfFilePath, fileName);

            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new UC_PdfView(pathString));
                    }
                }
            }
            catch (Exception ee)
            {
            }
        }
        

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            System.Windows.MessageBox.Show("Download completed!");
        }

        private void Mapping1_ChildView_ItemTappedEvent(object sender, DiagramEventArgs args)
        {
            if (args.Item is INode)
            {
                if ((args.Item as INode).Content != null && (args.Item as INode).Content is ItemInfo)
                {
                    INode node = args.Item as INode;
                    ItemInfo info = node.Content as ItemInfo;
                    string id = info.Id.ToString();

                    if (info.IsRoot && info.Level == 4)
                    {
                        try
                        {
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window.GetType() == typeof(MainWindow))
                                {
                                    this.NavigationService.Navigate(new Mapping1_ChildView(id, cbFinancial.SelectedValue.ToString()));
                                }
                            }
                        }
                        catch (Exception ee)
                        {

                        }
                    }
                }
            }
        }

        private void DGChildView_SelectionChanged(object sender, EventArgs e)
        {
            SubChildView data = ((DataGrid)sender).SelectedItem as SubChildView;

            if (data != null)
            {
                string parentId = data.Id.ToString();

                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            this.NavigationService.Navigate(new Mapping1_ChildView(parentId, cbFinancial.SelectedValue.ToString()));
                        }
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        private void DGBudgetView_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {

            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            BudgetView budgetView = (MainDataGrid.CurrentItem as BudgetView);
            if (budgetView == null)
            {
                return;
            }

            List<ChildView> childView = kirinentities.GetBudgetLegendData(chartVM.Year, budgetView.Id.ToString()).
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
                                                                        }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = childView;
        }

        private void DGChildView_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            ChildView childView = (MainDataGrid.CurrentItem as ChildView);
            if (childView == null)
            {
                return;
            }

            List<SubChildView> subChildView = kirinentities.GetBudgetLegendChildData(childView.Id.ToString()).
                                                                            Select(b => new SubChildView
                                                                            {
                                                                                Id = b.ID,
                                                                                label = b.BUDGET_LABEL,
                                                                                budget = b.BUDGET + "$",
                                                                                year = b.YEAR,
                                                                            }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = subChildView;
        }
        

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Visible;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Collapsed;
        }

        private void ChildExpander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Visible;
        }

        private void ChildExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Collapsed;
        }

        public T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

        private void FillFinancialYearCombo()
        {
            kirinentities = new KIRINEntities1();
            var lstYear = kirinentities.GetFinancialYearforBudget().ToList();

            cbFinancial.ItemsSource = lstYear;

            cbFinancial.SelectedValue = lstYear[0];
        }
    }
}
