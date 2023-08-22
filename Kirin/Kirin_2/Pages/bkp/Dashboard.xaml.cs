using Kirin_2.ViewModel;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using Kirin_2.Models;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        DashboardViewModel dbvm;
        public Dashboard()
        {
            dbvm = new DashboardViewModel();
            this.DataContext = dbvm;
            InitializeComponent();
            HideAllOtherClasses();
            //BindStudentComboBox();
        }

        public List<STUDENT> students { get; set; }
        public void BindStudentComboBox()
        {
            KIRINEntities1 KirinEntities = new KIRINEntities1();
            var item = KirinEntities.STUDENTs.ToList();
            students = item;
            DataContext = students;
        }


        private void HideAllOtherClasses()
        {
            //dbvm.toggleSeriesDash.Visibility = System.Windows.Visibility.Hidden;
            dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
            dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
            dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        }


        private void GraphOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!GraphOption.Items.IsEmpty)
            {
                string selectedGraphType = GraphOption.SelectedItem.ToString();

                ComboBoxItem selectedParameterOption = (ComboBoxItem)ParameterOption.SelectedItem;

               // dbvm.populateSeriesCollectionsTransferStudents();

                if (selectedGraphType == "Pie" && selectedParameterOption.Content.ToString() == "Transfer Student")
                {


                    PieChartSample.Series = dbvm.SeriesCollectionTransferStudentsPie;
                    PieChartSample.UpdateLayout();
                    PieChartSample.Visibility = System.Windows.Visibility.Visible;
                    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                    BarchartSample.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (selectedGraphType == "Bar" && selectedParameterOption.Content.ToString() == "Transfer Student")
                {


                    BarchartSample.Series = dbvm.SeriesCollectionTransferStudentsBar;
                    BarchartSample.UpdateLayout();
                    BarchartSample.Visibility = System.Windows.Visibility.Visible;
                    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                    PieChartSample.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (selectedGraphType == "Bar" && selectedParameterOption.Content.ToString() == "Sports/Cocurricular")
                {

                    BarchartSample.Series = dbvm.SeriesCollectionSportsJoinedBar;
                    BarchartSample.UpdateLayout();
                    BarchartSample.Visibility = System.Windows.Visibility.Visible;
                    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                    PieChartSample.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (selectedGraphType == "Bar" && selectedParameterOption.Content.ToString() == "Clubs joined")
                {

                    BarchartSample.Series = dbvm.SeriesCollectionClubsJoinedBar;
                    BarchartSample.UpdateLayout();
                    BarchartSample.Visibility = System.Windows.Visibility.Visible;
                    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                    PieChartSample.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (selectedGraphType == "Pie" && selectedParameterOption.Content.ToString() == "Sports/Cocurricular")
                {


                    PieChartSample.Series = dbvm.SeriesCollectionSportsJoinedPie;
                    PieChartSample.UpdateLayout();
                    PieChartSample.Visibility = System.Windows.Visibility.Visible;
                    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                    BarchartSample.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (selectedGraphType == "Pie" && selectedParameterOption.Content.ToString() == "Clubs joined")
                {


                    PieChartSample.Series = dbvm.SeriesCollectionClubsJoinedPie;
                    PieChartSample.UpdateLayout();
                    PieChartSample.Visibility = System.Windows.Visibility.Visible;
                    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                    BarchartSample.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {

                }


                //if (selectedGraphType.Content.ToString() == "Line")
                //{
                //    dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double>() { 10,2,5,6};
                //    BarchartSample.Visibility = System.Windows.Visibility.Collapsed;
                //    LineChartSample.Visibility = System.Windows.Visibility.Visible;
                //    PieChartSample.Visibility = System.Windows.Visibility.Collapsed;


                //}
                //if (selectedGraphType.Content.ToString() == "Bar")
                //{
                //    BarchartSample.Visibility = System.Windows.Visibility.Visible;
                //    LineChartSample.Visibility = System.Windows.Visibility.Collapsed;
                //    PieChartSample.Visibility = System.Windows.Visibility.Collapsed;
                //}
            }

        }

        //---Dynemic Combobox Change-----------------//
        //private void StudentOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    var selectedStudent = StudentParameterOption.SelectedItem as STUDENT;

        //    if (selectedStudent.FIRST_NAME == null)
        //    {
        //        dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 10, 20, 15, 18, 19, 20, 21, 18, 25, 19, 21, 22 };
        //        dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 35, 33, 38, 40, 42, 39, 38, 37, 31, 32, 34, 35 };
        //        dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
        //        dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 45, 46, 48, 44, 50, 49, 48, 44, 47, 45, 46, 49 };
        //        dbvm.LineChartSampleRow1();
        //    }
        //    else
        //    {
        //        if (selectedStudent.FIRST_NAME.ToString() == "Karen")
        //        {
        //            dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 10, 20, 15, 18, 19, 20, 21, 18, 25, 19, 21, 22 };
        //            dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 35, 33, 38, 40, 42, 39, 38, 37, 31, 32, 34, 35 };
        //            dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
        //            dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 45, 46, 48, 44, 50, 49, 48, 44, 47, 45, 46, 49 };
        //            dbvm.LineChartSampleRow1();
        //            dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

        //        }

        //        if (selectedStudent.FIRST_NAME.ToString() == "Jolene")
        //        {
        //            dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 34.23, 36.83, 30.63, 34.19, 36.74, 29.54, 36.38, 33.49, 29.31, 35.35, 39.55, 37.8 };
        //            dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 58.1, 50.71, 54.41, 55.7, 58.01, 53.68, 60.05, 59.46, 51.45, 55.95, 58.14, 59.8 };
        //            dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
        //            dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 70.2, 71.21, 66.93, 68.85, 67.58, 62.56, 68.95, 71.19, 68.71, 66.4, 67.26, 71.27 };
        //            dbvm.LineChartSampleRow1();
        //            dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

        //        }

        //        if (selectedStudent.FIRST_NAME.ToString() == "Curran")
        //        {
        //            dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 97.93, 91.62, 97.15, 95.82, 94.48, 92.05, 93.87, 94.18, 96.93, 99.51, 94.58, 99.6 };
        //            dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 90.25, 88.71, 91.95, 90.31, 90.46, 91.89, 90.06, 88.86, 87.18, 89.87, 90.54, 88.13 };
        //            dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 85, 88, 85, 83, 90, 90.5, 89, 95, 96, 95, 96.5, 97 };
        //            dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 92, 93, 94, 95, 95, 94, 94, 95, 98, 93, 93, 94 };
        //            dbvm.LineChartSampleRow1();
        //            dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

        //        }
        //        if (selectedStudent.FIRST_NAME.ToString() == "Parth")
        //        {
        //            dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 78.48, 74.69, 75.18, 73.84, 74.84, 76.62, 79.81, 65.07, 78.43, 83.01, 70.32, 84.26, };
        //            dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 76.16, 79.1, 75.98, 77.03, 85.8, 87.93, 88.86, 76.06, 79.9, 92.43, 78.17, 86.73 };
        //            dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
        //            dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
        //            dbvm.LineChartSampleRow1();
        //            dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

        //        }
        //        if (selectedStudent.FIRST_NAME.ToString() == "Anh")
        //        {
        //            dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 85, 88, 85, 83, 90, 90.5, 89, 95, 96, 95, 96.5, 97 };
        //            dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 78.48, 74.69, 75.18, 73.84, 74.84, 76.62, 79.81, 65.07, 78.43, 83.01, 70.32, 84.26 };
        //            dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
        //            dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 90.25, 88.71, 91.95, 90.31, 90.46, 91.89, 90.06, 88.86, 87.18, 89.87, 90.54, 88.13 };
        //            dbvm.LineChartSampleRow1();
        //            dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

        //        }
        //    }
        //}


        private void StudentOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem selectedStudent = (ComboBoxItem)StudentParameterOption.SelectedItem;

            if (selectedStudent.Content == null)
            {
                dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 10, 20, 15, 18, 19, 20, 21, 18, 25, 19, 21, 22 };
                dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 35, 33, 38, 40, 42, 39, 38, 37, 31, 32, 34, 35 };
                dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
                dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 45, 46, 48, 44, 50, 49, 48, 44, 47, 45, 46, 49 };
                dbvm.LineChartSampleRow1();
                //  dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
                //  dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
                //   dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (selectedStudent.Content.ToString() == "Student 1")
                {
                    dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 10, 20, 15, 18, 19, 20, 21, 18, 25, 19, 21, 22 };
                    dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 35, 33, 38, 40, 42, 39, 38, 37, 31, 32, 34, 35 };
                    dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
                    dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 45, 46, 48, 44, 50, 49, 48, 44, 47, 45, 46, 49 };
                    dbvm.LineChartSampleRow1();
                    dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

                }

                if (selectedStudent.Content.ToString() == "Student 2")
                {
                    dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 34.23, 36.83, 30.63, 34.19, 36.74, 29.54, 36.38, 33.49, 29.31, 35.35, 39.55, 37.8 };
                    dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 58.1, 50.71, 54.41, 55.7, 58.01, 53.68, 60.05, 59.46, 51.45, 55.95, 58.14, 59.8 };
                    dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
                    dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 70.2, 71.21, 66.93, 68.85, 67.58, 62.56, 68.95, 71.19, 68.71, 66.4, 67.26, 71.27 };
                    dbvm.LineChartSampleRow1();
                    dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

                }

                if (selectedStudent.Content.ToString() == "Student 3")
                {
                    dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 97.93, 91.62, 97.15, 95.82, 94.48, 92.05, 93.87, 94.18, 96.93, 99.51, 94.58, 99.6 };
                    dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 90.25, 88.71, 91.95, 90.31, 90.46, 91.89, 90.06, 88.86, 87.18, 89.87, 90.54, 88.13 };
                    dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 85, 88, 85, 83, 90, 90.5, 89, 95, 96, 95, 96.5, 97 };
                    dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 92, 93, 94, 95, 95, 94, 94, 95, 98, 93, 93, 94 };
                    dbvm.LineChartSampleRow1();
                    dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

                }
                if (selectedStudent.Content.ToString() == "Student 4")
                {
                    dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 78.48, 74.69, 75.18, 73.84, 74.84, 76.62, 79.81, 65.07, 78.43, 83.01, 70.32, 84.26, };
                    dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 76.16, 79.1, 75.98, 77.03, 85.8, 87.93, 88.86, 76.06, 79.9, 92.43, 78.17, 86.73 };
                    dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
                    dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
                    dbvm.LineChartSampleRow1();
                    dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

                }
                if (selectedStudent.Content.ToString() == "Student 5")
                {
                    dbvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 85, 88, 85, 83, 90, 90.5, 89, 95, 96, 95, 96.5, 97 };
                    dbvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 78.48, 74.69, 75.18, 73.84, 74.84, 76.62, 79.81, 65.07, 78.43, 83.01, 70.32, 84.26 };
                    dbvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
                    dbvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 90.25, 88.71, 91.95, 90.31, 90.46, 91.89, 90.06, 88.86, 87.18, 89.87, 90.54, 88.13 };
                    dbvm.LineChartSampleRow1();
                    dbvm.subject2.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject3.Visibility = System.Windows.Visibility.Hidden;
                    dbvm.subject4.Visibility = System.Windows.Visibility.Hidden;

                }
            }
        }

        public void ToggleSeries(object sender, System.EventArgs e)
        {
            var x = (CheckBox)sender;
            //if (x.IsChecked == true)
            //{
            //    dbvm.toggleSeriesDash.Visibility = System.Windows.Visibility.Visible;
            //}
            //if (x.IsChecked == false)
            //{
            //    dbvm.toggleSeriesDash.Visibility = System.Windows.Visibility.Hidden;
            //}
            //dbvm.toggleSeriesDash.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            dbvm.subject2.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            dbvm.subject3.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            dbvm.subject4.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        private void ParameterOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox param = (ComboBox)sender;
            ComboBoxItem sel = (ComboBoxItem)param.SelectedItem;
            string selectedValue = sel.Content.ToString();
            GraphOption.Items.Clear();
            switch (selectedValue)
            {
                case "Transfer Student":
                    {

                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        break;
                    }
                case "Sports/Cocurricular":
                    {
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        GraphOption.Items.Add("Bubble");
                        break;

                    }
                case "Clubs joined":
                    {
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        GraphOption.Items.Add("Bubble");
                        break;
                    }
                case "Diversity/Ethnicity":
                    {
                        GraphOption.Items.Add("Distribution");
                        GraphOption.Items.Add("Column");
                        break;
                    }
                default:
                    {
                        GraphOption.Items.Add("");

                        break;
                    }
            }


        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            Demographic.Width = aw - AveGrades.Width - 70;
            TopCountriesGrid.Width = Demographic.Width - 40;


        }


        }
}
