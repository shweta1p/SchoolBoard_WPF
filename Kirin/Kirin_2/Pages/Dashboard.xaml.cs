﻿using Kirin_2.ViewModel;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Kirin_2.Models;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.Maps;
using static Kirin_2.ViewModel.DashboardViewModel;
using Syncfusion.Windows.PdfViewer;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public App app;
        DashboardViewModel dbvm;
        KIRINEntities1 kirinentities;
        string schoolId = string.Empty;
        string subjectId = string.Empty;
        //public static int reset = 0;
        public Dashboard()
        {
            app = (App)Application.Current;
            subjectId = App.Current.Properties["SubjectId"].ToString();
            schoolId = App.Current.Properties["SchoolId"].ToString();

            DateTime fromDate = Convert.ToDateTime(App.Current.Properties["FromDate"]);
            DateTime toDate = Convert.ToDateTime(App.Current.Properties["ToDate"]);

            dbvm = new DashboardViewModel();

            this.Loaded += (sender, args) =>
            {
                var scrollViewer = DashboardScrollViewer;
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }

                scrollViewer?.ScrollToVerticalOffset(_verticalOffset);
                //scrollViewer.UpdateLayout();
            };

            //if (fromDate.ToString("yyyy/MM/dd") != "0001-01-01") //|| (fromDate.ToString("yyyy/MM/dd") != "0001/01/01")
            //{
            //    dbvm.FromDate = fromDate;
            //    dbvm.ToDate = toDate;
            //    string dtFrom = fromDate.ToString("MM/dd/yyyy");
            //    string dtTo = toDate.ToString("MM/dd/yyyy");
            //    //dbvm.DateRangeFilter_lbl = dtFrom + " - " + dtTo;
            //    dbvm.StackedBarRow4(dtFrom, dtTo, subjectId, schoolId);
            //}

            this.DataContext = dbvm;
            InitializeComponent();
            FillStudentCombo(subjectId);
            FillParameterCombo();

            //doughnutSeries.ColorModel = dbvm.colorModel;
            //doughnutSeries.Palette = ChartColorPalette.Custom;

            ContentControl centerView = new ContentControl()
            {
                Content = new Ellipse()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Fill = new ImageBrush(dbvm.CenterImage),
                    Width = 160,
                    Height = 160
                }
            };

            doughnutSeries.CenterView = centerView;

            //-------------Row 5 Map View---------------//
            layer.Center = new Point(43.0042077, -81.2446123);
            layer.Radius = 15;
            layer.DistanceType = DistanceType.KiloMeter;
            layer.ResetOnDoubleTap = false;

            //-----------Logic for Demographic Height---------------//
            if (dbvm.custTooltip.Count < 4)
            {
                txtBirth.Visibility = Visibility.Hidden;
                BarChartSampleRow1.Visibility = Visibility.Hidden;
                BarChartSampleRow1.Height = 0;
                TopCountriesGrid.Height = 320;
                TopCountriesGrid.Margin = new Thickness(15, -55, 15, 50);
            }
            else
            {
                txtBirth.Visibility = Visibility.Visible;
                BarChartSampleRow1.Visibility = Visibility.Visible;
                BarChartSampleRow1.Height = 70; //70
                TopCountriesGrid.Height = 220;
                TopCountriesGrid.Margin = new Thickness(15, -35, 15, 20);
            }
            MultidayCal.MaxDate = DateTime.Now;

            Maps.ZoomedIn += Maps_ZoomedIn;
            Maps.ZoomedOut += Maps_ZoomedOut;
        }

        void Maps_ZoomedIn(object sender, Syncfusion.UI.Xaml.Maps.ZoomEventArgs args)
        {
            Globals.reset = 2;
        }

        void Maps_ZoomedOut(object sender, Syncfusion.UI.Xaml.Maps.ZoomEventArgs args)
        {
            Globals.reset = 2;
        }

        public static double _verticalOffset;
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var sv = (ScrollViewer)sender;

            if (Globals.reset == 2)
            {
                _verticalOffset = 1864.6133333333337;
            }
            else if (Globals.reset == 0)
            {
                _verticalOffset = sv.VerticalOffset;
            }
            else if (Globals.reset == 1 && sv.VerticalOffset != 0)
            {
                _verticalOffset = sv.VerticalOffset;
            }
            else
            {
                _verticalOffset = _verticalOffset + 0;
            }
        }

        private void feedback_Click(object sender, MouseEventArgs e)
        {
            PdfViewerControl pdfViewer = new PdfViewerControl();
            RootGrid.Children.Add(pdfViewer);
        }

        private bool TryFindVisualChildElement<TChild>(DependencyObject parent, out TChild resultElement)
        where TChild : DependencyObject
        {
            resultElement = null;
            for (var childIndex = 0; childIndex < VisualTreeHelper.GetChildrenCount(parent); childIndex++)
            {
                DependencyObject childElement = VisualTreeHelper.GetChild(parent, childIndex);

                if (childElement is Popup popup)
                {
                    childElement = popup.Child;
                }

                if (childElement is TChild)
                {
                    resultElement = childElement as TChild;
                    return true;
                }

                if (TryFindVisualChildElement(childElement, out resultElement))
                {
                    return true;
                }
            }

            return false;
        }

        private void dpicker_Click(object sender, RoutedEventArgs e)
        {
            app.fusionCalender.Visibility = Visibility.Visible;
        }

        public List<DateTime> SelectedDatesList { get; set; }
        private void Get_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesList = (MultidayCal.SelectedDates).ToList();

            if (SelectedDatesList.Count > 1)
            {
                string dtFrom = SelectedDatesList[0].ToString("MM/dd/yyyy");
                string dtTo = SelectedDatesList[SelectedDatesList.Count - 1].ToString("MM/dd/yyyy");
                dbvm.StackedBarRow4(dtFrom, dtTo, subjectId, schoolId);
            }
            else
            {
                MessageBox.Show("Please Select Date");
            }

            dpickerPopup.Visibility = Visibility.Hidden;
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            Pscore.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Paverage.Foreground = Brushes.Transparent;

            string studentId = cmbStudent.SelectedValue.ToString();
            string toggleType = "Assignment";

            if (studentId != null)
            {
                dbvm.LineChartSampleRow1(subjectId, studentId, toggleType);
            }
        }

        private void btnAverage_Click(object sender, RoutedEventArgs e)
        {
            Paverage.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
            Pscore.Foreground = Brushes.Transparent;

            string studentId = cmbStudent.SelectedValue.ToString();
            string toggleType = "Average";

            if (studentId != null)
            {
                dbvm.LineChartSampleRow1(subjectId, studentId, toggleType);
            }
        }

        private void Chart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            row1Line.ShowTooltip = false;
        }

        private void Row3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            row3Line.ShowTooltip = false;
        }

        //private void ToDate_DateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (FromDate.SelectedDate != null && FromDate.SelectedDate < ToDate.SelectedDate)
        //    {
        //        string dtFrom = FromDate.SelectedDate.Value.ToString("MM/dd/yyyy");
        //        string dtTo = ToDate.SelectedDate.Value.ToString("MM/dd/yyyy");

        //        dbvm.StackedBarRow4(dtFrom, dtTo, subjectId, schoolId);
        //    }
        //    else
        //    {
        //        MessageBox.Show("From Date should not be grater than To Date");
        //    }
        //}

        //public List<DateTime> SelectedDatesList { get; set; }
        //private void dtRange_DateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    SelectedDatesList = (dtRange.SelectedDates).ToList();
        //}

        private void GraphOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!GraphOption.Items.IsEmpty)
            {
                string selectedGraphType = GraphOption.SelectedItem.ToString();

                //ComboBoxItem selectedParameterOption = (ComboBoxItem)ParameterOption.SelectedItem;
                string selectedParameterOption = ParameterOption.SelectedItem.ToString();
                dbvm.populateSeriesCollectionsTransferStudents();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                if (selectedParameterOption == "Citizenship")
                {
                    if (selectedGraphType == "Pie")
                    {
                        CitizenshipPie.Series = dbvm.SeriesCollectionCitizenshipPie;
                        CitizenshipPie.Visibility = Visibility.Visible;
                        CitizenshipPie.UpdateLayout();
                        PieLegend.Visibility = Visibility.Visible;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        ///DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Bar")
                    {
                        CitizenshipBar.Series = dbvm.SeriesCollectionCitizenshipBar;
                        CitizenshipBar.UpdateLayout();
                        CitizenshipBar.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Geomap")
                    {
                        dbvm.CitizenshipGeomap(schoolId, subjectId);
                        GeoMapSample.Source = "Pages\\World.xml";
                        GeoMapSample.HeatMap = dbvm.CitizenshipGeomapValues;
                        GeoMapSample.LandStrokeThickness = 0.8;
                        GeoMapSample.LandStroke = Brushes.White;
                        GeoMapSample.GradientStopCollection = dbvm.citizenshipGradient;
                        GeoMapSample.Hoverable = true;
                        //GeoMapSample.EnableZoomingAndPanning = true;

                        GeoMapSample.Visibility = Visibility.Visible;
                        DGCitizenship.Visibility = Visibility.Visible;
                        //DGGradient.Visibility = Visibility.Visible;
                        GeoMapSample.UpdateLayout();
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (selectedParameterOption == "Transfer Student")
                {
                    if (selectedGraphType == "Pie")
                    {
                        PieChartTransferStudent.Series = dbvm.SeriesCollectionTransferStudentsPie;
                        PieChartTransferStudent.Visibility = Visibility.Visible;
                        PieChartTransferStudent.UpdateLayout();
                        DGTransferStd.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Bar")
                    {
                        BarchartSample.Series = dbvm.SeriesCollectionTransferStudentsBar;
                        BarchartSample.UpdateLayout();
                        BarchartSample.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Stacked bar")
                    {
                        StackedBarTransferredStd.Series = dbvm.TransferStudentStackedBar;
                        StackedBarTransferredStd.UpdateLayout();
                        StackedBarTransferredStd.Visibility = Visibility.Visible;
                        DGTransferStdStacked.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (selectedParameterOption == "Date of Birth")
                {
                    if (selectedGraphType == "Pie")
                    {
                        DOBPie.Series = dbvm.SeriesCollectionDOBPie;
                        DOBPie.UpdateLayout();
                        DOBPie.Visibility = Visibility.Visible;
                        DGDob.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Bar")
                    {
                        DOBBar.Series = dbvm.SeriesCollectionDOBBar;
                        DOBBar.UpdateLayout();
                        DOBBar.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Scatter")
                    {
                        dbvm.DateofBirthScatter();
                        DateofBirthScatter.UpdateLayout();
                        DateofBirthScatter.Visibility = Visibility.Visible;
                        DGDOBScatter.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (selectedParameterOption == "Sports/Cocurricular")
                {
                    if (selectedGraphType == "Bar")
                    {
                        SportsBar.Series = dbvm.SeriesCollectionSportsJoinedBar;
                        SportsBar.UpdateLayout();
                        SportsBar.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Pie")
                    {
                        PieChartSample.Series = dbvm.SeriesCollectionSportsJoinedPie;
                        PieChartSample.UpdateLayout();
                        PieChartSample.Visibility = Visibility.Visible;
                        DGSports.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (selectedParameterOption == "Clubs joined")
                {
                    if (selectedGraphType == "Bar")
                    {
                        ClubsBar.Series = dbvm.SeriesCollectionClubsJoinedBar;
                        ClubsBar.UpdateLayout();
                        ClubsBar.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Pie")
                    {
                        ClubsPieChart.Series = dbvm.SeriesCollectionClubsJoinedPie;
                        ClubsPieChart.UpdateLayout();
                        ClubsPieChart.Visibility = Visibility.Visible;
                        DGClubList.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (selectedParameterOption == "Diversity/Ethnicity")
                {
                    if (selectedGraphType == "Bar")
                    {
                        DiversityBar.Series = dbvm.SeriesCollectionDiversityBar;
                        DiversityBar.UpdateLayout();
                        DiversityBar.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Pie")
                    {
                        DiversityPie.Series = dbvm.DiversityEthnicityPie;
                        DiversityPie.UpdateLayout();
                        DiversityPie.Visibility = Visibility.Visible;
                        DGDiversity.Visibility = Visibility.Visible;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Geomap")
                    {
                        dbvm.EthnicityGeomap();
                        GeoMapDiversity.Source = "Pages\\World.xml";
                        GeoMapDiversity.HeatMap = dbvm.EthnicityGeomapValues;
                        GeoMapDiversity.LandStrokeThickness = 0.8;
                        GeoMapDiversity.LandStroke = Brushes.White;

                        GeoMapDiversity.GradientStopCollection = dbvm.EthnicityGradient;
                        GeoMapDiversity.Hoverable = true;
                        GeoMapDiversity.Visibility = Visibility.Visible;
                        GeoMapDiversity.UpdateLayout();
                        DGEthnicityGeomap.Visibility = Visibility.Visible;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Stacked bar")
                    {
                        StackedBarDiversity.Series = dbvm.SeriesCollectionDiversityStackedBar;
                        StackedBarDiversity.UpdateLayout();
                        StackedBarDiversity.Visibility = Visibility.Visible;
                        DGEthnicity.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (selectedParameterOption == "Languages")
                {
                    if (selectedGraphType == "Bar")
                    {
                        LanguagesBar.Series = dbvm.SeriesCollectionLanguagesBar;
                        LanguagesBar.UpdateLayout();
                        LanguagesBar.Visibility = Visibility.Visible;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        HeatmapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Heatmap")
                    {
                        dbvm.HeatMapLanguages();
                        HeatmapSample.Visibility = Visibility.Visible;
                        HeatmapSample.Cursor = Cursors.ScrollAll;
                        HeatmapSample.UpdateLayout();
                        DGLanguages.Visibility = Visibility.Visible;
                        PieChartSample.Visibility = Visibility.Collapsed;
                        BarchartSample.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DateofBirthScatter.Visibility = Visibility.Collapsed;
                        GeoMapLanguages.Visibility = Visibility.Collapsed;
                        GeoMapDiversity.Visibility = Visibility.Collapsed;
                        DiversityPie.Visibility = Visibility.Collapsed;
                        CitizenshipPie.Visibility = Visibility.Collapsed;
                        DiversityBar.Visibility = Visibility.Collapsed;
                        LanguagesBar.Visibility = Visibility.Collapsed;
                        DOBBar.Visibility = Visibility.Collapsed;
                        DOBPie.Visibility = Visibility.Collapsed;
                        LineChartSportsRow2.Visibility = Visibility.Collapsed;
                        SportsBar.Visibility = Visibility.Collapsed;
                        PieChartTransferStudent.Visibility = Visibility.Collapsed;
                        ClubsBar.Visibility = Visibility.Collapsed;
                        ClubsPieChart.Visibility = Visibility.Collapsed;
                        LineChartClubs.Visibility = Visibility.Collapsed;
                        CitizenshipBar.Visibility = Visibility.Collapsed;
                        GeoMapSample.Visibility = Visibility.Collapsed;
                        DGCitizenship.Visibility = Visibility.Collapsed;
                        PieLegend.Visibility = Visibility.Collapsed;
                        DGTransferStd.Visibility = Visibility.Collapsed;
                        DGDob.Visibility = Visibility.Collapsed;
                        DGSports.Visibility = Visibility.Collapsed;
                        DGClubList.Visibility = Visibility.Collapsed;
                        DGDiversity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicityGeomap.Visibility = Visibility.Collapsed;
                        StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                        //DGGradient.Visibility = Visibility.Collapsed;
                        StackedBarDiversity.Visibility = Visibility.Collapsed;
                        DGTransferStdStacked.Visibility = Visibility.Collapsed;
                        DGDOBScatter.Visibility = Visibility.Collapsed;
                    }
                    //else if (selectedGraphType == "Geomap")
                    //{
                    //    dbvm.LanguagesGeomap();
                    //    GeoMapLanguages.Source = "Pages\\World.xml";
                    //    GeoMapLanguages.HeatMap = dbvm.LanguagesGeomapValues;
                    //    GradientStopCollection c = new GradientStopCollection();

                    //    // Light Purple
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#E9EAFE"), 0.0));
                    //    // Purple
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#8CA6F5"), 0.2));
                    //    //Aqua Blue
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#76F8FB"), 0.3));
                    //    // Lime Green
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#85F950"), 0.4));
                    //    // Yellow
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#E1EE43"), 0.5));
                    //    // Yellow - Orange
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFCD3C"), 0.6));
                    //    // Orange
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FBBE3C"), 0.7));
                    //    // Dark Orange
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FB9A2F"), 0.8));
                    //    // Orange - Red
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#F7651E"), 0.9));
                    //    // Red 
                    //    c.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#E35307"), 1.0));

                    //    GeoMapLanguages.GradientStopCollection = c;
                    //    GeoMapLanguages.Hoverable = true;
                    //    // GeoMapSample.EnableZoomingAndPanning = true;
                    //    GeoMapLanguages.Visibility = Visibility.Visible;
                    //    GeoMapLanguages.UpdateLayout();
                    //    GeoMapDiversity.Visibility = Visibility.Collapsed;
                    //    PieChartSample.Visibility = Visibility.Collapsed;
                    //    BarchartSample.Visibility = Visibility.Collapsed;
                    //    HeatmapSample.Visibility = Visibility.Collapsed;
                    //    DateofBirthScatter.Visibility = Visibility.Collapsed;
                    //    GeoMapDiversity.Visibility = Visibility.Collapsed;
                    //    StackedBarDiversity.Visibility = Visibility.Collapsed;
                    //    DiversityPie.Visibility = Visibility.Collapsed;
                    //    CitizenshipPie.Visibility = Visibility.Collapsed;
                    //    DiversityBar.Visibility = Visibility.Collapsed;
                    //    LanguagesBar.Visibility = Visibility.Collapsed;
                    //    DOBBar.Visibility = Visibility.Collapsed;
                    //    DOBPie.Visibility = Visibility.Collapsed;
                    //    LineChartSportsRow2.Visibility = Visibility.Collapsed;
                    //    SportsBar.Visibility = Visibility.Collapsed;
                    //    PieChartTransferStudent.Visibility = Visibility.Collapsed;
                    //    ClubsBar.Visibility = Visibility.Collapsed;
                    //    ClubsPieChart.Visibility = Visibility.Collapsed;
                    //    LineChartClubs.Visibility = Visibility.Collapsed;
                    //    CitizenshipBar.Visibility = Visibility.Collapsed;
                    //    DGCitizenship.Visibility = Visibility.Collapsed;
                    //    PieLegend.Visibility = Visibility.Collapsed;
                    //    DGTransferStd.Visibility = Visibility.Collapsed;
                    //    DGDob.Visibility = Visibility.Collapsed;
                    //    DGSports.Visibility = Visibility.Collapsed;
                    //    DGClubList.Visibility = Visibility.Collapsed;
                    //    DGDiversity.Visibility = Visibility.Collapsed;
                    //    DGLanguages.Visibility = Visibility.Collapsed;
                    //    StackedBarTransferredStd.Visibility = Visibility.Collapsed;
                    //    DGGradient.Visibility = Visibility.Collapsed;
                    //}
                }
                else
                {

                }
            }
        }

        private void DoubleBackButton_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption.Items.Count;
            int selectedIndex1 = ParameterOption.SelectedIndex;

            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            //------------------Paging-------------------//
            if (selectedIndex2 + 1 - 1 > 0)
            {
                txtPaging.Text = "1 | " + totalCount2;
            }

            ////-----------Logic for 2nd Combobox Selection------------//
            //if (selectedIndex2 < totalCount2 && selectedIndex2 != 0)
            //{
            //    GraphOption.SelectedItem = GraphOption.Items[selectedIndex2 - 1].ToString();
            //}

            //-----------Logic for 1nd Combobox Selection------------//
            //if (selectedIndex2 == 0 && selectedIndex1 != totalCount1)
            //{
            if (selectedIndex1 < totalCount1 && selectedIndex1 != 0)
            {
                ParameterOption.SelectedItem = ParameterOption.Items[selectedIndex1 - 1];
                //btnDBackward.Visibility = Visibility.Hidden;
            }
            //}
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption.Items.Count;
            int selectedIndex1 = ParameterOption.SelectedIndex;

            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            //------------------Paging-------------------//
            if (selectedIndex2 + 1 - 1 > 0)
            {
                txtPaging.Text = (selectedIndex2 + 1 - 1).ToString() + " | " + totalCount2;
            }

            ////---------logic for double Backward visibility---------//
            //if ((selectedIndex2 == 0 || selectedIndex2 -1 == 0) && selectedIndex1 != 0)
            //{
            //    btnDBackward.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btnDBackward.Visibility = Visibility.Hidden;
            //}

            //-----------Logic for 2nd Combobox Selection------------//
            if (selectedIndex2 < totalCount2 && selectedIndex2 != 0)
            {
                GraphOption.SelectedItem = GraphOption.Items[selectedIndex2 - 1].ToString();
            }

            //    //-----------Logic for 1nd Combobox Selection------------//
            //    else if (selectedIndex2 == 0 && selectedIndex1 != totalCount1)
            //    {
            //        if (selectedIndex1 < totalCount1 && selectedIndex1 != 0)
            //        {
            //            ParameterOption.SelectedItem = ParameterOption.Items[selectedIndex1 - 1];
            //        }
            //    }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption.Items.Count;
            int selectedIndex1 = ParameterOption.SelectedIndex;

            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            //------------------Paging-------------------//
            if (selectedIndex2 + 2 <= totalCount2)
            {
                txtPaging.Text = (selectedIndex2 + 2).ToString() + " | " + totalCount2;
            }

            ////---------logic for double Forward visibility---------//
            //if ((selectedIndex2 + 1 == totalCount2 || selectedIndex2 + 2 == totalCount2) && selectedIndex1 != totalCount1 -1)
            //{
            //    btnDForward.Visibility = Visibility.Visible;
            //    btnDBackward.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btnDForward.Visibility = Visibility.Hidden;
            //    btnDBackward.Visibility = Visibility.Hidden;
            //}


            //-----------Logic for 2nd Combobox Selection------------//
            if (selectedIndex2 < totalCount2 - 1)
            {
                GraphOption.SelectedItem = GraphOption.Items[selectedIndex2 + 1].ToString();
            }

            ////-----------Logic for 1nd Combobox Selection------------//
            //else if (selectedIndex2 == totalCount2 - 1 && selectedIndex1 != totalCount1 - 1)
            //{
            //    if (selectedIndex1 < totalCount1 - 1)
            //    {
            //        ParameterOption.SelectedItem = ParameterOption.Items[selectedIndex1 + 1];
            //    }
            //}
        }

        private void DoubleForwardButton_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption.Items.Count;
            int selectedIndex1 = ParameterOption.SelectedIndex;

            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            //------------------Paging-------------------//
            if (selectedIndex2 + 2 <= totalCount2)
            {
                txtPaging.Text = "1 | " + totalCount2;
            }

            ////-----------Logic for 2nd Combobox Selection------------//
            //if (selectedIndex2 < totalCount2 - 1)
            //{
            //    GraphOption.SelectedItem = GraphOption.Items[selectedIndex2 + 1].ToString();
            //}

            //-----------Logic for 1nd Combobox Selection------------//
            //if (selectedIndex2 == totalCount2 - 1 && selectedIndex1 != totalCount1 - 1)
            //{
            if (selectedIndex1 < totalCount1 - 1)
            {
                ParameterOption.SelectedItem = ParameterOption.Items[selectedIndex1 + 1];
                //btnDForward.Visibility = Visibility.Hidden;
            }
            //}
        }

        private void btnArrowLeftRow3_Click(object sender, RoutedEventArgs e)
        {
            lineChartRow3.Visibility = Visibility.Hidden;
            stackedDoughnut.Visibility = Visibility.Visible;
            btnInfo.Visibility = Visibility.Hidden;
            btnCourses.Visibility = Visibility.Hidden;
            DGStudentScore.Visibility = Visibility.Visible;
        }

        private void btnArrowRightRow3_Click(object sender, RoutedEventArgs e)
        {
            lineChartRow3.Visibility = Visibility.Visible;
            stackedDoughnut.Visibility = Visibility.Hidden;
            btnInfo.Visibility = Visibility.Visible;
            btnCourses.Visibility = Visibility.Visible;
            DGStudentScore.Visibility = Visibility.Hidden;
        }

        private void GeoMapSample_Click(object sender, LiveCharts.Maps.MapData data)
        {
            //MessageBox.Show("OnClick");

            var citizenship = data.Id;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentCitizenshipList(schoolId, subjectId, citizenship);

            if (svm.StudentList != null)
            {
                app.citizenshipView.DataContext = svm;
                app.citizenshipView.DGStudentData.ItemsSource = svm.StudentList;
                app.citizenshipView.Visibility = Visibility.Visible;
            }
        }

        private void GeoMapDiversity_LandClick(object sender, LiveCharts.Maps.MapData data)
        {
            var ethnicity = data.Id;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentEthnicityList(schoolId, subjectId, ethnicity);

            if (svm.StudentList != null)
            {
                app.ethnicityView.DataContext = svm;
                app.ethnicityView.DGStudentData.ItemsSource = svm.StudentList;
                app.ethnicityView.Visibility = Visibility.Visible;
            }
        }

        private void StudentOption_SelectionChanged_Row3(object sender, SelectionChangedEventArgs e)
        {
            if (StudentParameterOption_Row3.SelectedValue != null)
            {
                //-----Bind Doughnut Chart Data for Selected Student----------//
                string studentId = StudentParameterOption_Row3.SelectedValue.ToString();

                dbvm.DoughnutChartRow3(studentId);
                ContentControl centerView = new ContentControl()
                {
                    Content = new Ellipse()
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Fill = new ImageBrush(dbvm.CenterImage),
                        Width = 160,
                        Height = 160
                    }
                };

                doughnutSeries.CenterView = centerView;

                stackedDoughnutChart.Visibility = Visibility.Visible;

                //-------------Line chart based on Subject------------------//
                dbvm.GetCourseList(studentId);
                dbvm.LineChartRow3(subjectId, studentId);

                //---------------Multiple Line series Code-----------//
                ////string toggleType = "Average";
                //kirinentities = new KIRINEntities1();
                //List<ObservableCollection<double>> subScore = new List<ObservableCollection<double>> { };
                ////List<ObservableCollection<LineChartRow1>> subScore = new List<ObservableCollection<LineChartRow1>> { };
                //List<string> subjectName = new List<string> { };
                //List<string> xlist = new List<string>();
                //var stdSubjectId = kirinentities.GetSubjectIdforStudent(studentId).ToList();
                ////var xAxisData = kirinentities.GetAverageGradesbySubject(subjectId, studentId).ToList();


                //if (stdSubjectId.Count() > 0)
                //{
                //    //foreach (var item in xAxisData)
                //    //{
                //    //    xlist.Add(item.LabelDate);
                //    //}

                //    foreach (GetSubjectIdforStudent_Result item in stdSubjectId)
                //    {
                //        string subId = item.ID.ToString();
                //        //var studentGrades = kirinentities.GetGradesbySubjectId(subId, studentId, toggleType).ToList();
                //        var studentGrades = kirinentities.GetAverageGradesbySubject(subId, studentId).ToList();

                //        if (studentGrades.Count() > 0)
                //        {
                //            subjectName.Add(item.COURSE_NAME);
                //            //ObservableCollection<LineChartRow1> stdGrades = new ObservableCollection<LineChartRow1>();
                //            ObservableCollection<double> stdGrades = new ObservableCollection<double>();

                //            int xVar = 0;

                //            //------Add Start Date of Semester------------//
                //            //stdGrades.Add(new LineChartRow1()
                //            //{
                //            //    POINTS = "0",
                //            //    POINTS_POSSIBLE = "0",
                //            //    LabelDate = item.LabelDate,
                //            //    Average = "0",
                //            //    Xlabel = item.LabelDate,
                //            //});

                //            xVar = Convert.ToInt32(item.Month);
                //            int yValue = 0;
                //            foreach (var result in studentGrades)
                //            {
                //                if (result.Percentage != 0)
                //                {
                //                    yValue = Convert.ToInt32(result.Percentage);
                //                }

                //                stdGrades.Add(Convert.ToDouble(result.Percentage) != 0 ? Convert.ToDouble(result.Percentage) : yValue);
                //                xlist.Add(result.LabelDate);

                //                if (xVar == result.Month)
                //                {
                //                    xlist.Add(result.Day);
                //                }
                //                else
                //                {
                //                    xlist.Add(result.LabelDate);
                //                }

                //                //if (!xlist.Contains(result.LabelDate))
                //                //{
                //                //    xlist.Add(result.LabelDate);
                //                //}
                //                //else
                //                //{
                //                //    xlist.Add("");
                //                //}


                //                //stdGrades.Add(new LineChartRow1()
                //                //{
                //                //    Assignment = result.Assignment,
                //                //    POINTS = result.POINTS.ToString(),
                //                //    POINTS_POSSIBLE = result.POINTS_POSSIBLE.ToString(),
                //                //    LabelDate = result.LabelDate,
                //                //    Average = result.Average.ToString(),
                //                //    Percentage = result.Percentage != 0 ? result.Percentage.ToString() : yValue.ToString(),
                //                //    Xlabel = xVar == result.Month ? result.Day : result.LabelDate,
                //                //});

                //                xVar = Convert.ToInt32(result.Month);
                //            }

                //            if (studentGrades.Count() > 0)
                //            {
                //                subScore.Add(stdGrades);
                //            }
                //        }
                //    }
                //    dbvm.LineChartSampleRow3(subScore, subjectName, xlist);
                //}
                //else
                //{
                //    dbvm.LineChartSampleRow3(subScore, subjectName, xlist);
                //}
            }
            else
            {
                FillStudentCombo(subjectId);
            }
        }

        private void StudentOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudent.SelectedValue != null)
            {
                string studentId = cmbStudent.SelectedValue.ToString();
                string toggleType = "ASSIGNMENT";

                var data = kirinentities.GetStudentScoreandAverage(subjectId, studentId).ToList();
                GetStudentListbySubjectId_Result obj = (GetStudentListbySubjectId_Result)cmbStudent.SelectedItem;
                //txtStudent.Text = obj.NAME;

                if (data.Count > 0)
                {
                    txtScore.Text = " " + data.Where(x => x.Type == "Score").Select(x => x.Percentage).FirstOrDefault().ToString();
                    txtAverage.Text = data.Where(x => x.Type == "Average").Select(x => x.Percentage).FirstOrDefault().ToString();
                }

                SolidColorBrush solidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#247BC0");
                if (Pscore.Foreground.ToString() == solidColorBrush.Color.ToString())
                {
                    toggleType = "ASSIGNMENT";
                }
                else
                {
                    toggleType = "AVERAGE";
                }
                dbvm.LineChartSampleRow1(subjectId, studentId, toggleType);
            }
            else
            {
                FillStudentCombo(subjectId);
            }
        }

        private void ParameterOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox param = (ComboBox)sender;
            //ComboBoxItem sel = (ComboBoxItem)param.SelectedItem;
            string selectedValue = param.SelectedItem.ToString();  //sel.Content.ToString();

            if (GraphOption != null)
            {
                GraphOption.Items.Clear();
            }

            switch (selectedValue)
            {
                case "Citizenship":
                    {
                        txtRow2.Text = "WHERE ARE YOUR STUDENTS FROM?";
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        GraphOption.Items.Add("Geomap");
                        break;
                    }
                case "Transfer Student":
                    {
                        txtRow2.Text = "WHEN WERE YOUR STUDENTS TRANSFERRED?";
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        GraphOption.Items.Add("Stacked bar");
                        break;
                    }
                case "Date of Birth":
                    {
                        txtRow2.Text = "WHEN WERE YOUR STUDENTS BORN?";
                        GraphOption.Items.Add("Scatter");
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        break;
                    }
                case "Sports/Cocurricular":
                    {
                        txtRow2.Text = "WHAT DO YOUR STUDENTS ENJOY?";
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Pie");
                        break;
                    }
                //case "Clubs joined":
                //    {
                //        GraphOption.Items.Add("Bar");
                //        GraphOption.Items.Add("Pie");
                //        break;
                //    }
                case "Diversity/Ethnicity":
                    {
                        txtRow2.Text = "WHAT ARE YOUR STUDENTS ETHNICITIES?";
                        GraphOption.Items.Add("Bar");
                        GraphOption.Items.Add("Geomap");
                        GraphOption.Items.Add("Stacked bar");
                        GraphOption.Items.Add("Pie");
                        break;
                    }
                case "Languages":
                    {
                        txtRow2.Text = "WHAT LANGUAGES DO YOUR STUDENTS SPEAK?";
                        GraphOption.Items.Add("Heatmap");
                        GraphOption.Items.Add("Bar");
                        break;
                    }
                default:
                    {
                        GraphOption.Items.Add("");
                        break;
                    }
            }
            GraphOption.SelectedItem = GraphOption.Items[0].ToString();

            //------------------Paging-------------------//
            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            txtPaging.Text = (selectedIndex2 + 1).ToString() + " | " + totalCount2;
        }

        //private void btnToggle_Click(object sender, RoutedEventArgs e)
        //{
        //    //var item = sender as ToggleButton;

        //    //if (item.Content.ToString().ToUpper() == "AVERAGE")
        //    //{
        //    //    item.Content = "Assignment";
        //    string studentId = cmbStudent.SelectedValue.ToString();
        //    string toggleType = "Assignment"; //btnToggle.IsChecked == true ? "Assignment" : "Average"; //item.Content.ToString().ToUpper();
        //    //txtRow1.Text = toggleType + " Score";

        //    if (studentId != null)
        //    {
        //        dbvm.LineChartSampleRow1(subjectId, studentId, toggleType);
        //        //GetStudentGrades(studentId, toggleType);
        //    }
        //    //}
        //    //else if (item.Content.ToString().ToUpper() == "ASSIGNMENT")
        //    //{
        //    //    item.Content = "Average";
        //    //    string studentId = cmbStudent.SelectedValue.ToString();
        //    //    string toggleType = item.Content.ToString().ToUpper();

        //    //    if (studentId != null)
        //    //    {
        //    //        dbvm.LineChartSampleRow1(subjectId, studentId, toggleType);
        //    //        //GetStudentGrades(studentId, toggleType);
        //    //    }
        //    //}
        //}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            //Demographic.Width = aw - AveGrades.Width-50; //285 349
            //TopCountriesGrid.Width = Demographic.Width - 34; //240 309
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // vbdashboard.Width = app.MainFrameWidth;
        }

        private void FillStudentCombo(string subjectId)
        {
            kirinentities = new KIRINEntities1();
            List<GetStudentListbySubjectId_Result> lstStudent = kirinentities.GetStudentListbySubjectId(subjectId).ToList();

            cmbStudent.ItemsSource = lstStudent;
            StudentParameterOption_Row3.ItemsSource = lstStudent;

            cmbStudent.SelectedValue = "2"; //lstStudent[0].ID;
            StudentParameterOption_Row3.SelectedValue = "2"; //lstStudent[0].ID;
        }

        private void FillParameterCombo()
        {
            ParameterOption.Items.Add("Citizenship");
            ParameterOption.Items.Add("Transfer Student");
            ParameterOption.Items.Add("Date of Birth");
            ParameterOption.Items.Add("Sports/Cocurricular");
            ParameterOption.Items.Add("Diversity/Ethnicity");
            ParameterOption.Items.Add("Languages");

            ParameterOption.SelectedItem = ParameterOption.Items[0].ToString();
        }

        private void PieChartTransferStudent_OnDataClick(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var label = chartpoint.SeriesView.Title;
            //string split = selectedPoint.Split(':')[0];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentList(schoolId, subjectId, label);

            if (svm.StudentList != null)
            {
                app.studentViewforPieChart.DataContext = svm;
                app.studentViewforPieChart.DGStudentData.ItemsSource = svm.StudentList;
                app.studentViewforPieChart.Visibility = Visibility.Visible;
            }
        }

        private void CitizenshipPie_OnDataClick(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var citizenship = chartpoint.SeriesView.Title;
            //string split = selectedPoint.Split(':')[0];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentCitizenshipList(schoolId, subjectId, citizenship);

            if (svm.StudentList != null)
            {
                app.citizenshipView.DataContext = svm;
                app.citizenshipView.DGStudentData.ItemsSource = svm.StudentList;
                app.citizenshipView.Visibility = Visibility.Visible;
            }
        }

        private void CitizenshipBar_OnClick(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var citizenship = dbvm.LabelsBarCitizenship[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentCitizenshipList(schoolId, subjectId, citizenship);

            if (svm.StudentList != null)
            {
                app.citizenshipBar.DataContext = svm;
                app.citizenshipBar.DGStudentData.ItemsSource = svm.StudentList;
                app.citizenshipBar.Visibility = Visibility.Visible;
            }
        }

        private void TransferStdBar_OnClick(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var month = dbvm.LabelsBarTransferStudents[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetTransferStudentData(schoolId, subjectId, month);

            if (svm.StudentList != null)
            {
                app.transferStdBar.DataContext = svm;
                app.transferStdBar.DGStudentData.ItemsSource = svm.StudentList;
                app.transferStdBar.Visibility = Visibility.Visible;
            }
        }

        private void StackedBarTransferredStd_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var transferType = chartpoint.SeriesView.Title;
            int xPoint = Convert.ToInt32(chartpoint.X);
            var month = dbvm.LabelsTransferStacked[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetTransferStudentDataforStackBar(schoolId, subjectId, transferType, month);

            if (svm.TransferredList != null)
            {
                app.tsStackedBar.DataContext = svm;
                app.tsStackedBar.DGStudentData.ItemsSource = svm.TransferredList;
                app.tsStackedBar.Visibility = Visibility.Visible;
            }
        }

        private void DOBBar_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var month = dbvm.LabelsBarDOB[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetDOBDatabyBirthMonth(schoolId, subjectId, month);

            if (svm.StudentList != null)
            {
                app.dobBar.DataContext = svm;
                app.dobBar.DGStudentData.ItemsSource = svm.StudentList;
                app.dobBar.Visibility = Visibility.Visible;
            }
        }

        private void DOBPie_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var month = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetDOBDatabyBirthMonth(schoolId, subjectId, month);

            if (svm.StudentList != null)
            {
                app.dobPie.DataContext = svm;
                app.dobPie.DGStudentData.ItemsSource = svm.StudentList;
                app.dobPie.Visibility = Visibility.Visible;
            }
        }

        private void SportsPie_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var sports = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentSportsData(schoolId, subjectId, sports);

            if (svm.StudentList != null)
            {
                app.sportsPie.DataContext = svm;
                app.sportsPie.DGStudentData.ItemsSource = svm.StudentList;
                app.sportsPie.Visibility = Visibility.Visible;
            }
        }

        private void SportsBar_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var sport = dbvm.LabelsSportsBar[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentSportsData(schoolId, subjectId, sport);

            if (svm.StudentList != null)
            {
                app.sportsBar.DataContext = svm;
                app.sportsBar.DGStudentData.ItemsSource = svm.StudentList;
                app.sportsBar.Visibility = Visibility.Visible;
            }
        }

        private void ClubsBar_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var club = dbvm.LabelsClubsBar[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyClub(schoolId, subjectId, club);

            if (svm.StudentList != null)
            {
                app.clubjoined.DataContext = svm;
                app.clubjoined.DGStudentData.ItemsSource = svm.StudentList;
                app.clubjoined.Visibility = Visibility.Visible;
            }
        }

        private void ClubsPie_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var club = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyClub(schoolId, subjectId, club);

            if (svm.StudentList != null)
            {
                app.clubjoined.DataContext = svm;
                app.clubjoined.DGStudentData.ItemsSource = svm.StudentList;
                app.clubjoined.Visibility = Visibility.Visible;
            }
        }

        private void DiversityBar_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var diversity = dbvm.LabelsDiversityBar[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyDiversity(schoolId, subjectId, diversity);

            if (svm.StudentList != null)
            {
                app.diversityBar.DataContext = svm;
                app.diversityBar.DGStudentData.ItemsSource = svm.StudentList;
                app.diversityBar.Visibility = Visibility.Visible;
            }
        }

        private void DiversityPie_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var diversity = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyDiversity(schoolId, subjectId, diversity);

            if (svm.StudentList != null)
            {
                app.diversityPie.DataContext = svm;
                app.diversityPie.DGStudentData.ItemsSource = svm.StudentList;
                app.diversityPie.Visibility = Visibility.Visible;
            }
        }

        private void StackedBarDiversity_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var diversity = dbvm.LabelsStackedBarDiversity[xPoint];
            var ethnicity = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyEthnicityandDivesity(schoolId, subjectId, ethnicity, diversity);

            if (svm.StudentList != null)
            {
                app.ethnicityView.DataContext = svm;
                app.ethnicityView.DGStudentData.ItemsSource = svm.StudentList;
                app.ethnicityView.Visibility = Visibility.Visible;
            }
        }

        private void LanguagesBar_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var language = dbvm.LabelsBarLanguages[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyLanguage(schoolId, subjectId, language);

            if (svm.StudentList != null)
            {
                app.languageBar.DataContext = svm;
                app.languageBar.DGStudentData.ItemsSource = svm.StudentList;
                app.languageBar.Visibility = Visibility.Visible;
            }
        }

        private void Row4Scatter_OnDataClick(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var selectedPoint = chartpoint.SeriesView.Title;
            var grade = chartpoint.X;
            var attendance = chartpoint.Y;
            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentAssignmentData(schoolId, subjectId, selectedPoint);

            if (svm.StudentScatterList != null || svm.StudentAttendance != null)
            {
                app.studentViewforScatter.DataContext = svm;
                app.studentViewforScatter.DGStudentData.ItemsSource = svm.StudentScatterList;
                app.studentViewforScatter.DGStudentAttendance.ItemsSource = svm.StudentAttendance;
                app.studentViewforScatter.Visibility = Visibility.Visible;
                app.studentViewforScatter.lblGrade.Content = "Current Grade: " + grade + "%";
                app.studentViewforScatter.lblAttendance.Content = "Attendance Grade: " + attendance + "%";
                app.studentViewforScatter.DGAttendanceWeight.ItemsSource = svm.AttendanceCodeList;
            }
            else
            {
                app.studentViewforScatter.Visibility = Visibility.Collapsed;
            }
        }

        private void StackedBarRow4_OnDataClick(object sender, LiveCharts.ChartPoint chartpoint)
        {
            var code = chartpoint.SeriesView.Title;
            int xPoint = Convert.ToInt32(chartpoint.X);
            var date = dbvm.LabelsStackedDate[xPoint];

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentAttendanceData(schoolId, subjectId, date, code);

            app.attendaceCodeView.DataContext = svm;
            app.attendaceCodeView.DGStudentData.ItemsSource = svm.StudentAttendanceList;
            app.attendaceCodeView.Visibility = Visibility.Visible;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = System.Windows.Visibility.Visible;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = System.Windows.Visibility.Collapsed;
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

        private void DGEthnicity_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {

            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            DiversityStackedList diversity = (MainDataGrid.CurrentItem as DiversityStackedList);
            if (diversity == null)
            {
                return;
            }

            List<EthnicityList> ethnicityList = kirinentities.GetEthnicityfromDiversity(schoolId, subjectId, diversity.Diversity).Select(x => new EthnicityList
            {
                Ethnicity = x.ETHNICITY,
                Color = (Color)ColorConverter.ConvertFromString(x.Color),
                EthnicityCount = x.EthnicityCount.ToString(),
                Progress = (int)x.EthnicityCount
            }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = ethnicityList;
        }

        private void btnArrowRight_Click(object sender, RoutedEventArgs e)
        {
            GenderWheel.Visibility = Visibility.Visible;
            //PieGender.Visibility = Visibility.Visible;
            DemographicView.Visibility = Visibility.Hidden;
        }

        private void btnArrowLeft_Click(object sender, RoutedEventArgs e)
        {
            GenderWheel.Visibility = Visibility.Hidden;
            //PieGender.Visibility = Visibility.Hidden;
            DemographicView.Visibility = Visibility.Visible;
        }

        private void btnEye_Click(object sender, RoutedEventArgs e)
        {
            StudentViewModel svm = new StudentViewModel();
            svm.GetTransportationData(schoolId);

            if (svm.ModeList != null)
            {
                app.transportAbvr.DataContext = svm;
                app.transportAbvr.DGAbvrData.ItemsSource = svm.ModeList;
                app.transportAbvr.Visibility = Visibility.Visible;
            }
            else
            {
                app.transportAbvr.Visibility = Visibility.Collapsed;
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (dbvm.Row3DataPoints != null)
            {
                app.assignmentList.DataContext = dbvm.Row3DataPoints;
                app.assignmentList.DGStudentData.ItemsSource = dbvm.Row3DataPoints;
                app.assignmentList.Visibility = Visibility.Visible;
            }
            else
            {
                app.assignmentList.Visibility = Visibility.Collapsed;
            }
        }

        //private void btnCourses_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dbvm.SubjectList != null)
        //    {
        //        app.subjectListView.DataContext = dbvm.SubjectList;
        //        app.subjectListView.DGSubject.ItemsSource = dbvm.SubjectList;
        //        app.subjectListView.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        app.subjectListView.Visibility = Visibility.Collapsed;
        //    }
        //}

        private void btnInsight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnviewInsight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        if (this.NavigationService == null)
                        {
                            ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new DashboardInsights());
                        }
                        else
                        {
                            this.NavigationService.Navigate(new DashboardInsights());
                        }

                    }
                    Globals.reset = 1;
                }
            }
            catch (Exception ee)
            {

            }
        }


        private void Maps_MapToolTipOpening(object sender, ToolTipOpeningEventArgs e)
        {
            StudentModel data = e.Data as StudentModel;

            if (data != null)
            {
                string stdId = data.ID.ToString();

                StudentViewModel svm = new StudentViewModel();
                svm.GetStudentMapData(stdId);

                if (svm.StudentMview != null)
                {
                    app.svMapView.name.Content = svm.StudentMview[0].Name;
                    app.svMapView.parents.Text = svm.StudentMview[0].ParentCount;
                    app.svMapView.siblings.Text = svm.StudentMview[0].SiblingCount;
                    app.svMapView.income.Text = svm.StudentMview[0].HouseholdIncome + " $";
                    app.svMapView.involvement.Text = svm.StudentMview[0].ParentalInvolvement;
                    app.svMapView.distance.Text = svm.StudentMview[0].Distance;
                    app.svMapView.transportation.Text = svm.StudentMview[0].Transportation;

                    app.svMapView.Visibility = Visibility.Visible;
                }
            }
        }

        private void MapLegend_SelectionChanged(object sender, EventArgs e)
        {
            StudentMapData data = ((DataGrid)sender).SelectedItem as StudentMapData;

            if (data != null)
            {
                string stdId = data.ID.ToString();

                StudentViewModel svm = new StudentViewModel();
                svm.GetStudentMapData(stdId);

                if (svm.StudentMview != null)
                {
                    app.svMapView.DataContext = svm.StudentMview[0];
                    //app.svMapView.DGStudentData.ItemsSource = svm.StudentList;

                    app.svMapView.name.Content = svm.StudentMview[0].Name;
                    app.svMapView.parents.Text = svm.StudentMview[0].ParentCount;
                    app.svMapView.siblings.Text = svm.StudentMview[0].SiblingCount;
                    app.svMapView.income.Text = svm.StudentMview[0].HouseholdIncome + " $";
                    app.svMapView.involvement.Text = svm.StudentMview[0].ParentalInvolvement;
                    app.svMapView.distance.Text = svm.StudentMview[0].Distance;
                    app.svMapView.transportation.Text = svm.StudentMview[0].Transportation;

                    app.svMapView.Visibility = Visibility.Visible;
                }
            }
        }

        private void DGStudentScore_SelectionChanged(object sender, EventArgs e)
        {
            StudentScore data = ((DataGrid)sender).SelectedItem as StudentScore;

            if (data != null)
            {
                string stdId = data.StudentId.ToString();
                string subjectId = data.SubjectId.ToString();

                //---------Line graph for Row 3----------------------//
                dbvm.LineChartRow3(subjectId, stdId);

                lineChartRow3.Visibility = Visibility.Visible;
                stackedDoughnut.Visibility = Visibility.Hidden;
                DGStudentScore.Visibility = Visibility.Hidden;
                btnInfo.Visibility = Visibility.Visible;
                btnCourses.Visibility = Visibility.Visible;
            }
        }

        private void btnInfoRow4_Click(object sender, RoutedEventArgs e)
        {
            if (dbvm.Row3DataPoints != null)
            {
                app.scatterLegend.DataContext = dbvm.Row4LegendList;
                app.scatterLegend.DGStudentData.ItemsSource = dbvm.Row4LegendList;
                app.scatterLegend.Visibility = Visibility.Visible;
            }
            else
            {
                app.scatterLegend.Visibility = Visibility.Collapsed;
            }
        }

    }

    static class Globals
    {
        // global int
        public static int reset = 0;
    }
}
