using Kirin_2.Models;
using Kirin_2.ViewModel;
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
    public partial class Mapping1_ChildView : Page
    {
        KIRINEntities1 kirinentities;
        ChildNodeView chartVM;
        string pId = string.Empty;
        public Mapping1_ChildView(string parentId, string year)
        {
            App.Current.Properties["ParentId"] = parentId;
            App.Current.Properties["Year"] = year;
            pId = parentId;
            InitializeComponent();
            FillFinancialYearCombo();
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
                            this.NavigationService.Navigate(new Mapping1_ChildView(pId, cbFinancial.SelectedValue.ToString()));
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
                            this.NavigationService.Navigate(new Mapping1_ChildView(pId, cbFinancial.SelectedValue.ToString()));
                        }
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
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

        private void DGChildView_SelectionChanged(object sender, EventArgs e)
        {
            Child data = ((DataGrid)sender).SelectedItem as Child;

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

            Budget budgetView = (MainDataGrid.CurrentItem as Budget);
            if (budgetView == null)
            {
                return;
            }

            List<Child> childView = kirinentities.GetBudgetLegendChildData(budgetView.Id.ToString()).
                                                                        Select(x => new Child
                                                                        {
                                                                            Id = x.ID,
                                                                            label = x.BUDGET_LABEL,
                                                                            budget = x.BUDGET + "$",
                                                                            year = x.YEAR,
                                                                        }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = childView;
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
