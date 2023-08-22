﻿using Kirin_2.Models;
using Kirin_2.UserControls;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kirin_2.ViewModel
{
    public class DashboardViewModel : UserControl, INotifyPropertyChanged
    {
        KIRINEntities1 KirinEntities;
        string subjectId = App.Current.Properties["SubjectId"].ToString();
        string schoolId = App.Current.Properties["SchoolId"].ToString();

        public ICommand EditDateRangeCommand { get; set; }
        public DashboardViewModel()
        {
            KirinEntities = new KIRINEntities1();

            string dtFrom = FromDate.ToString("MM/dd/yyyy");
            string dtTo = ToDate.ToString("MM/dd/yyyy");

            DateRangeFilter_lbl = dtFrom + " - " + dtTo;
            EthnicityVSLanguageGraph();
            getBarChartDS(subjectId);
            StackedBarRow4(dtFrom, dtTo, subjectId, schoolId);
            BarChartSampleRow1(subjectId);
            ScatterPlotSample();
            DateofBirthScatter();
            CitizenshipGeomap(schoolId, subjectId);
            EthnicityGeomap();
            HeatMapLanguages();
            setMonthColor();
            setSportsColor();
            setDiversityColor();
            setSubjectColor();
            StudentGenderCount();
            BindStudentData();
            setSubjectScore();

            Models = new ObservableCollection<StudentModel>();
            Models = GridStudentLatLong();
        }

        public void BindStudentData()
        {
            var data = KirinEntities.GetStudentsList(subjectId, schoolId);

            StudentListData = new ObservableCollection<StudentList>(from s in data
                                                                    select new StudentList
                                                                    {
                                                                        Column1 = s.Column1,
                                                                        Column2 = s.Column2,
                                                                        ID1 = s.ID1.ToString(),
                                                                        ID2 = s.ID2.ToString()
                                                                    });
        }

        private DateTime _fromDate = Convert.ToDateTime("2022-09-01");
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                _fromDate = value;
                OnPropertyChanged("FromDate");

            }
        }

        private DateTime _toDate = Convert.ToDateTime("2022-09-10");
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                _toDate = value;
                OnPropertyChanged("ToDate");

            }
        }

        private string _DateRangeFilter_lbl;

        public string DateRangeFilter_lbl
        {
            get { return _DateRangeFilter_lbl; }
            set
            {
                _DateRangeFilter_lbl = value;
                OnPropertyChanged("DateRangeFilter_lbl");
            }
        }


        private SeriesCollection _SeriesCollectionDOBBar;
        public SeriesCollection SeriesCollectionDOBBar
        {
            get { return _SeriesCollectionDOBBar; }
            set { _SeriesCollectionDOBBar = value; }
        }

        private SeriesCollection _SeriesCollectionClubsJoinedBar;
        public SeriesCollection SeriesCollectionClubsJoinedBar
        {
            get { return _SeriesCollectionClubsJoinedBar; }
            set { _SeriesCollectionClubsJoinedBar = value; }
        }

        private SeriesCollection _SeriesCollectionSportsJoinedBar;
        public SeriesCollection SeriesCollectionSportsJoinedBar
        {
            get { return _SeriesCollectionSportsJoinedBar; }
            set { _SeriesCollectionSportsJoinedBar = value; }
        }

        private SeriesCollection _SeriesCollectionSportsJoinedPie;
        public SeriesCollection SeriesCollectionSportsJoinedPie
        {
            get { return _SeriesCollectionSportsJoinedPie; }
            set { _SeriesCollectionSportsJoinedPie = value; }
        }

        private SeriesCollection _SeriesCollectionGenderPie;
        public SeriesCollection SeriesCollectionGenderPie
        {
            get { return _SeriesCollectionGenderPie; }
            set { _SeriesCollectionGenderPie = value; }
        }

        private SeriesCollection _SeriesCollectionClubsJoinedPie;
        public SeriesCollection SeriesCollectionClubsJoinedPie
        {
            get { return _SeriesCollectionClubsJoinedPie; }
            set { _SeriesCollectionClubsJoinedPie = value; }
        }

        private SeriesCollection _SeriesCollectionTransferStudentsBar;
        public SeriesCollection SeriesCollectionTransferStudentsBar
        {
            get { return _SeriesCollectionTransferStudentsBar; }
            set { _SeriesCollectionTransferStudentsBar = value; }
        }

        private SeriesCollection _SeriesCollectionCitizenshipBar;
        public SeriesCollection SeriesCollectionCitizenshipBar
        {
            get { return _SeriesCollectionCitizenshipBar; }
            set { _SeriesCollectionCitizenshipBar = value; }
        }

        private SeriesCollection _SeriesCollectionLanguagesBar;
        public SeriesCollection SeriesCollectionLanguagesBar
        {
            get { return _SeriesCollectionLanguagesBar; }
            set { _SeriesCollectionLanguagesBar = value; }
        }


        private SeriesCollection _SeriesCollectionTransferStudentsPie;
        public SeriesCollection SeriesCollectionTransferStudentsPie
        {
            get { return _SeriesCollectionTransferStudentsPie; }
            set
            {
                _SeriesCollectionTransferStudentsPie = value;
                OnPropertyChanged("SeriesCollectionTransferStudentsPie");
            }
        }

        private SeriesCollection _StudentScoreScatterSeriesCollection;
        public SeriesCollection StudentScoreScatterSeriesCollection
        {
            get { return _StudentScoreScatterSeriesCollection; }
            set
            {
                _StudentScoreScatterSeriesCollection = value;
                OnPropertyChanged("StudentScoreScatterSeriesCollection");
            }
        }

        private SeriesCollection _DOBScatterSeriesCollection;
        public SeriesCollection DOBScatterSeriesCollection
        {
            get { return _DOBScatterSeriesCollection; }
            set
            {
                _DOBScatterSeriesCollection = value;
                OnPropertyChanged("DOBScatterSeriesCollection");
            }
        }

        public string[] _labelsBarTransferStudents { get; set; }
        public string[] LabelsBarTransferStudents
        {
            get { return _labelsBarTransferStudents; }
            set
            {
                _labelsBarTransferStudents = value;
                OnPropertyChanged("LabelsBarTransferStudents");
            }
        }

        public string[] _labelsBarCitizenship { get; set; }
        public string[] LabelsBarCitizenship
        {
            get { return _labelsBarCitizenship; }
            set
            {
                _labelsBarCitizenship = value;
                OnPropertyChanged("LabelsBarCitizenship");
            }
        }

        public string[] _labelsBarLanguages { get; set; }
        public string[] LabelsBarLanguages
        {
            get { return _labelsBarLanguages; }
            set
            {
                _labelsBarLanguages = value;
                OnPropertyChanged("LabelsBarLanguages");
            }
        }

        private Dictionary<string, string> _MonthColor;

        public Dictionary<string, string> MonthColor
        {
            get { return _MonthColor; }
            set
            {
                _MonthColor = value;
                OnPropertyChanged("MonthColor");
            }
        }

        private Dictionary<int, string> _SubjectColor;

        public Dictionary<int, string> SubjectColor
        {
            get { return _SubjectColor; }
            set
            {
                _SubjectColor = value;
                OnPropertyChanged("SubjectColor");
            }
        }

        private Dictionary<int, Color> _ScoreColor;

        public Dictionary<int, Color> ScoreColor
        {
            get { return _ScoreColor; }
            set
            {
                _ScoreColor = value;
                OnPropertyChanged("ScoreColor");
            }
        }
        private Dictionary<string, string> _SportsColor;

        public Dictionary<string, string> SportsColor
        {
            get { return _SportsColor; }
            set
            {
                _SportsColor = value;
                OnPropertyChanged("SportsColor");
            }
        }

        private Dictionary<string, string> _DiversityColor;

        public Dictionary<string, string> DiversityColor
        {
            get { return _DiversityColor; }
            set
            {
                _DiversityColor = value;
                OnPropertyChanged("DiversityColor");
            }
        }
        private Dictionary<string, string> _DiversityAbvr;

        public Dictionary<string, string> DiversityAbvr
        {
            get { return _DiversityAbvr; }
            set
            {
                _DiversityAbvr = value;
                OnPropertyChanged("DiversityAbvr");
            }
        }

        public Func<double, string> FormatterBarTransferStudents { get; set; }
        public Func<double, string> FormatterBarCitizenship { get; set; }
        public Func<double, string> FormatterBarLanguages { get; set; }


        private ObservableCollection<double> _subject1 = new ObservableCollection<double>() { 10, 20, 12, 30, 50, 60 };
        public ObservableCollection<double> Subject1
        {
            get { return _subject1; }
            set
            {
                _subject1 = value;
                OnPropertyChanged("Subject1");
            }
        }

        private ObservableCollection<double> _predictionsubject1 = new ObservableCollection<double>() { 10, 20, 12, 30, 50 };
        public ObservableCollection<double> PredictionSubject1
        {
            get { return _predictionsubject1; }
            set
            {
                _predictionsubject1 = value;
                OnPropertyChanged("PredictionSubject1");
            }
        }

        private ObservableCollection<double> _subject2 = new ObservableCollection<double>() { 25, 22, 24, 25, 21, 22 };
        public ObservableCollection<double> Subject2
        {
            get { return _subject2; }
            set
            {
                _subject2 = value;
                OnPropertyChanged("Subject2");
            }
        }

        private ObservableCollection<double> _predictionsubject2 = new ObservableCollection<double>() { 10, 20, 12, 30, 50 };
        public ObservableCollection<double> PredictionSubject2
        {
            get { return _predictionsubject2; }
            set
            {
                _predictionsubject2 = value;
                OnPropertyChanged("PredictionSubject2");
            }
        }

        private ObservableCollection<double> _subject3 = new ObservableCollection<double>() { 25, 22, 24, 25, 21, 22 };
        public ObservableCollection<double> Subject3
        {
            get { return _subject3; }
            set
            {
                _subject3 = value;
                OnPropertyChanged("Subject3");
            }
        }

        private ObservableCollection<double> _predictionsubject3 = new ObservableCollection<double>() { 10, 20, 12, 30, 50 };
        public ObservableCollection<double> PredictionSubject3
        {
            get { return _predictionsubject3; }
            set
            {
                _predictionsubject3 = value;
                OnPropertyChanged("PredictionSubject3");
            }
        }

        private ObservableCollection<double> _subject4 = new ObservableCollection<double>() { 25, 22, 24, 25, 21, 22 };
        public ObservableCollection<double> Subject4
        {
            get { return _subject4; }
            set
            {
                _subject4 = value;
                OnPropertyChanged("Subject4");
            }
        }

        private ObservableCollection<double> _predictionsubject4 = new ObservableCollection<double>() { 10, 20, 12, 30, 50 };
        public ObservableCollection<double> PredictionSubject4
        {
            get { return _predictionsubject4; }
            set
            {
                _predictionsubject4 = value;
                OnPropertyChanged("PredictionSubject4");
            }
        }

        private ObservableCollection<BarchartValue> _BarchartDS;

        public ObservableCollection<BarchartValue> BarchartDS
        {
            get { return _BarchartDS; }
            set
            {
                _BarchartDS = value;
                OnPropertyChanged("BarchartDS");
            }
        }

        private ObservableCollection<BarchartValue> _BarchartDSTop10;

        public ObservableCollection<BarchartValue> BarchartDSTop10
        {
            get { return _BarchartDSTop10; }
            set
            {
                _BarchartDSTop10 = value;
                OnPropertyChanged("BarchartDSTop10");
            }
        }

        private ObservableCollection<CitizenshipValue> _CitizenshipGeoMap;

        public ObservableCollection<CitizenshipValue> CitizenshipGeoMap
        {
            get { return _CitizenshipGeoMap; }
            set
            {
                _CitizenshipGeoMap = value;
                OnPropertyChanged("CitizenshipGeoMap");
            }
        }

        private ObservableCollection<StudentMapData> _StudentMapDataList;

        public ObservableCollection<StudentMapData> StudentMapDataList
        {
            get { return _StudentMapDataList; }
            set
            {
                _StudentMapDataList = value;
                OnPropertyChanged("StudentMapDataList");
            }
        }


        private SeriesCollection _SeriesCollection;

        public SeriesCollection SeriesCollection
        {
            get { return _SeriesCollection; }
            set
            {
                _SeriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public SeriesCollection _series3 { get; set; }
        public SeriesCollection SeriesCollectionLineRow3
        {
            get { return _series3; }
            set
            {
                _series3 = value;
                OnPropertyChanged("SeriesCollectionLineRow3");
            }
        }

        public DoughnutSeries _doughnutSeries { get; set; }
        public DoughnutSeries DoughnutSeries
        {
            get { return _doughnutSeries; }
            set
            {
                _doughnutSeries = value;
                OnPropertyChanged("DoughnutSeries");
            }
        }


        public SeriesCollection _seriesClubsLineRow2;
        public SeriesCollection SeriesCollectionClubsLineRow2
        {
            get { return _seriesClubsLineRow2; }
            set
            {
                _seriesClubsLineRow2 = value;
                OnPropertyChanged("SeriesCollectionClubsLineRow2");
            }
        }

        private SeriesCollection _SeriesCollectionPie;
        private SeriesCollection _DiversityEthnicityPie;
        private SeriesCollection _SeriesCollectionCitizenshipPie;
        private SeriesCollection _SeriesCollectionDOBPie;

        public SeriesCollection SeriesCollectionPie
        {
            get { return _SeriesCollectionPie; }
            set
            {
                _SeriesCollectionPie = value;
                OnPropertyChanged("SeriesCollectionPie");
            }
        }

        public SeriesCollection DiversityEthnicityPie
        {
            get { return _DiversityEthnicityPie; }
            set
            {
                _DiversityEthnicityPie = value;
                OnPropertyChanged("DiversityEthnicityPie");
            }
        }

        public SeriesCollection SeriesCollectionDOBPie
        {
            get { return _SeriesCollectionDOBPie; }
            set
            {
                _SeriesCollectionDOBPie = value;
                OnPropertyChanged("SeriesCollectionDOBPie");
            }
        }

        public SeriesCollection SeriesCollectionCitizenshipPie
        {
            get { return _SeriesCollectionCitizenshipPie; }
            set
            {
                _SeriesCollectionCitizenshipPie = value;
                OnPropertyChanged("SeriesCollectionCitizenshipPie");
            }
        }


        private SeriesCollection _SeriesCollectionPieSandboxRow2;

        public SeriesCollection SeriesCollectionPieSandboxRow2
        {
            get { return _SeriesCollectionPieSandboxRow2; }
            set
            {
                _SeriesCollectionPieSandboxRow2 = value;
                OnPropertyChanged("SeriesCollectionPieSandboxRow2");
            }
        }


        private ObservableCollection<EthnicityVSLanguageDS> _DS;

        public ObservableCollection<EthnicityVSLanguageDS> DS
        {
            get { return _DS; }
            set
            {
                _DS = value;
                OnPropertyChanged("DS");
            }
        }

        public string[] Labels { get; set; }

        public void setMonthColor()
        {
            MonthColor = new Dictionary<string, string>();

            MonthColor["Jan"] = "DodgerBlue";
            MonthColor["Feb"] = "DarkOrange";
            MonthColor["Mar"] = "Gold";
            MonthColor["Apr"] = "CadetBlue";
            MonthColor["May"] = "DarkGray";
            MonthColor["Jun"] = "DarkTurquoise";
            MonthColor["Jul"] = "DeepPink";
            MonthColor["Aug"] = "OrangeRed";
            MonthColor["Sep"] = "MediumSlateBlue";
            MonthColor["Oct"] = "YellowGreen";
            MonthColor["Nov"] = "SeaGreen";
            MonthColor["Dec"] = "OliveDrab";
        }

        public void setSportsColor()
        {
            SportsColor = new Dictionary<string, string>();

            SportsColor["THEATRE"] = "DodgerBlue";
            SportsColor["BALLET"] = "DarkOrange";
            SportsColor["BASKETBALL"] = "Gold";
            SportsColor["DIVING"] = "CadetBlue";
            SportsColor["HIPHOP"] = "DarkGray";
            SportsColor["SINGING"] = "DarkTurquoise";
            SportsColor["SWIMMING"] = "DeepPink";
            //SportsColor[""] = "OrangeRed";
        }

        public void setDiversityColor()
        {
            DiversityColor = new Dictionary<string, string>();

            DiversityColor["AMERICAN INDIAN"] = "DodgerBlue";
            DiversityColor["ASIAN"] = "DarkOrange";
            DiversityColor["BLACK"] = "Gold";
            DiversityColor["HISPANIC"] = "CadetBlue";
            DiversityColor["WHITE"] = "DarkGray";
            DiversityColor["EURASIAN"] = "OrangeRed";

            DiversityAbvr = new Dictionary<string, string>();
            DiversityAbvr["AMERICAN INDIAN"] = "AI";
            DiversityAbvr["ASIAN"] = "AS";
            DiversityAbvr["BLACK"] = "BL";
            DiversityAbvr["HISPANIC"] = "HP";
            DiversityAbvr["WHITE"] = "WH";
            DiversityAbvr["EURASIAN"] = "EU";
        }

        public void setSubjectColor()
        {
            SubjectColor = new Dictionary<int, string>();

            SubjectColor[0] = "#0078DE";
            SubjectColor[1] = "#ff8c00";
            SubjectColor[2] = "#FFB900";
            SubjectColor[3] = "#7A7574";
            SubjectColor[4] = "CadetBlue";
            SubjectColor[5] = "DarkTurquoise";
            SubjectColor[6] = "DeepPink";
            SubjectColor[7] = "OrangeRed";
            SubjectColor[8] = "MediumSlateBlue";
            SubjectColor[9] = "YellowGreen";
            SubjectColor[10] = "SeaGreen";
            SubjectColor[11] = "OliveDrab";
        }

        public void setSubjectScore()
        {
            ScoreColor = new Dictionary<int, Color>();

            ScoreColor[0] = (Color)ColorConverter.ConvertFromString("#068dff");
            ScoreColor[1] = (Color)ColorConverter.ConvertFromString("#2d9fff");
            ScoreColor[2] = (Color)ColorConverter.ConvertFromString("#55b1ff");
            ScoreColor[3] = (Color)ColorConverter.ConvertFromString("#90ccff");
            ScoreColor[4] = (Color)ColorConverter.ConvertFromString("#a3d5ff");
            ScoreColor[5] = (Color)ColorConverter.ConvertFromString("#cae7ff");
            ScoreColor[6] = (Color)ColorConverter.ConvertFromString("#d3d3d3");
            ScoreColor[7] = (Color)ColorConverter.ConvertFromString("#068dff");
            ScoreColor[8] = (Color)ColorConverter.ConvertFromString("#2d9fff");
            ScoreColor[9] = (Color)ColorConverter.ConvertFromString("#55b1ff");
            ScoreColor[10] = (Color)ColorConverter.ConvertFromString("#90ccff");
            ScoreColor[11] = (Color)ColorConverter.ConvertFromString("#a3d5ff");
        }

        public int getLanguageCount(string ethnicity, string language)
        {
            var ds = KirinEntities.EthnicityVSLanguageDS().ToList();
            var res = (from langcount in ds
                       where langcount.ETHNICITY_DIVERSITY == ethnicity
                       && langcount.LANGUAGES == language
                       select langcount.LanguageCount.Value);

            if (res.Count() == 0)
            {
                return 0;
            }
            else
            {
                return Int32.Parse(res.First().ToString());
            }
        }

        public void EthnicityVSLanguageGraph()
        {
            List<string> list = new List<string>();

            List<string> listUniqueLanguages = new List<string>();

            var EvsLDS = KirinEntities.EthnicityVSLanguageDS().ToList();
            SeriesCollection = new SeriesCollection();
            listUniqueLanguages = EvsLDS.Select(x => x.LANGUAGES).Distinct().ToList();
            List<KeyValuePair<string, int?>> LangToCount;

            List<KeyValuePair<string, List<KeyValuePair<string, int?>>>> chartobj = new List<KeyValuePair<string, List<KeyValuePair<string, int?>>>>();
            string eth;

            foreach (var element in EvsLDS.ToList())
            {
                LangToCount = new List<KeyValuePair<string, int?>>();

                if (!(list.Contains(element.ETHNICITY_DIVERSITY)))
                {

                    //add to Labels
                    eth = element.ETHNICITY_DIVERSITY.ToString();
                    list.Add(eth);

                    foreach (var langu in listUniqueLanguages)
                    {
                        LangToCount.Add(new KeyValuePair<string, int?>(langu, getLanguageCount(eth, langu)));
                    }

                    chartobj.Add(new KeyValuePair<string, List<KeyValuePair<string, int?>>>(eth, LangToCount));
                }
            }
            Labels = list.ToArray();

            //construct the stackedColumnSeries
            //get all the obj.lang[1st element] value
            List<double> stackedValues;
            for (int i = 0; i < chartobj.Count; i++)
            {
                StackedColumnSeries stackedColumn;
                stackedValues = new List<double>();
                for (int j = 0; j < chartobj[i].Value.Count; j++)
                {
                    stackedValues.Add(Convert.ToDouble(chartobj[j].Value[i].Value));
                }

                stackedColumn = new StackedColumnSeries
                {
                    Title = chartobj[i].Value[i].Key,
                    Values = new ChartValues<double>(stackedValues),
                    DataLabels = true
                };
                SeriesCollection.Add(stackedColumn);
            }
        }

        public void getBarChartDS(string subjectId)
        {
            var studentLanguageCount = KirinEntities.getLanguageCountofStudentsbySubject(subjectId, schoolId).ToList();

            BarchartDS = new ObservableCollection<BarchartValue>(from obj in studentLanguageCount
                                                                 select new BarchartValue
                                                                 {
                                                                     Lang = obj.LANGUAGE,
                                                                     LanguageCount = obj.LanguageCount,
                                                                     Progress = (int)obj.LanguageCount
                                                                 });

            BarchartDSTop10 = BarchartDS; //new ObservableCollection<BarcharValue>(BarchartDS.Take(10));
        }

        public SeriesCollection SeriesCollectionLine { get; set; }
        public string[] _labelsLine { get; set; }
        public string[] LabelsLine
        {
            get { return _labelsLine; }
            set
            {
                _labelsLine = value;
                OnPropertyChanged("LabelsLine");
            }
        }

        public Func<double, string> YFormatter { get; set; }


        public SeriesCollection _series;
        public SeriesCollection SeriesCollectionLineRow1
        {
            get { return _series; }
            set
            {
                _series = value;
                OnPropertyChanged("SeriesCollectionLineRow1");
            }
        }

        public SeriesCollection _seriesSportsRow2;
        public SeriesCollection SeriesCollectionSportsLineRow2
        {
            get { return _seriesSportsRow2; }
            set
            {
                _seriesSportsRow2 = value;
                OnPropertyChanged("SeriesCollectionSportsLineRow2");
            }
        }

        public SeriesCollection _seriesML;
        public SeriesCollection MLSandboxRow1Collection
        {
            get { return _seriesML; }
            set
            {
                _seriesML = value;
                OnPropertyChanged("MLSandboxRow1Collection");
            }
        }

        public string[] _labelsLineRow1;

        public string[] LabelsLineRow1
        {
            get { return _labelsLineRow1; }
            set
            {
                _labelsLineRow1 = value;
                OnPropertyChanged("LabelsLineRow1");
            }
        }

        public string[] _labelsLineSportsRow2;

        public string[] LabelsLineSportsRow2
        {
            get { return _labelsLineSportsRow2; }
            set
            {
                _labelsLineSportsRow2 = value;
                OnPropertyChanged("LabelsLineSportsRow2");
            }
        }

        public string[] _labelsLineClubsRow2;

        public string[] LabelsLineClubsRow2
        {
            get { return _labelsLineClubsRow2; }
            set
            {
                _labelsLineClubsRow2 = value;
                OnPropertyChanged("LabelsLineClubsRow2");
            }
        }

        public Func<double, string> YFormatterRow1 { get; set; }

        public Func<double, string> YFormatterClubsRow2 { get; set; }

        public Func<double, string> YFormatterSportsRow2 { get; set; }


        public string[] _labelsLineRow3;
        public string[] LabelsLineRow3
        {
            get { return _labelsLineRow3; }
            set
            {
                _labelsLineRow3 = value;
                OnPropertyChanged("LabelsLineRow3");
            }
        }
        public Func<double, string> YFormatterRow3 { get; set; }

        public SeriesCollection SeriesCollectionLineRow4 { get; set; }
        public string[] LabelsLineRow4 { get; set; }
        public Func<double, string> YFormatterRow4 { get; set; }

        public SeriesCollection _stackedbarRow4 { get; set; }
        public SeriesCollection StackedbarRow4
        {
            get { return _stackedbarRow4; }
            set
            {
                _stackedbarRow4 = value;
                OnPropertyChanged("StackedbarRow4");
            }
        }

        public SeriesCollection _transferStudentStackedBar { get; set; }
        public SeriesCollection TransferStudentStackedBar
        {
            get { return _transferStudentStackedBar; }
            set
            {
                _transferStudentStackedBar = value;
                OnPropertyChanged("TransferStudentStackedBar");
            }
        }

        public string[] _labelsStackedRow4 { get; set; }

        public string[] LabelsStackedRow4
        {
            get { return _labelsStackedRow4; }
            set
            {
                _labelsStackedRow4 = value;
                OnPropertyChanged("LabelsStackedRow4");
            }
        }

        public string[] _ylabelsStackedRow4 { get; set; }

        public string[] YlabelsStackedRow4
        {
            get { return _ylabelsStackedRow4; }
            set
            {
                _ylabelsStackedRow4 = value;
                OnPropertyChanged("YlabelsStackedRow4");
            }
        }


        public string[] _labelsTransferStacked { get; set; }

        public string[] LabelsTransferStacked
        {
            get { return _labelsTransferStacked; }
            set
            {
                _labelsTransferStacked = value;
                OnPropertyChanged("LabelsTransferStacked");
            }
        }

        public int _maxValueRow4 { get; set; }

        public int maxValueRow4
        {
            get { return _maxValueRow4; }
            set
            {
                _maxValueRow4 = value;
                OnPropertyChanged("maxValueRow4");
            }
        }

        public int _transferMaxVal { get; set; }

        public int TransferMaxVal
        {
            get { return _transferMaxVal; }
            set
            {
                _transferMaxVal = value;
                OnPropertyChanged("TransferMaxVal");
            }
        }

        public string[] _labelsStackedDate { get; set; }

        public string[] LabelsStackedDate
        {
            get { return _labelsStackedDate; }
            set
            {
                _labelsStackedDate = value;
                OnPropertyChanged("LabelsStackedDate");
            }
        }

        public string[] _labelsScatterX { get; set; }

        public string[] LabelsScatterX
        {
            get { return _labelsScatterX; }
            set
            {
                _labelsScatterX = value;
                OnPropertyChanged("LabelsScatterX");
            }
        }

        public int[] _labelsScatterY { get; set; }

        public int[] LabelsScatterY
        {
            get { return _labelsScatterY; }
            set
            {
                _labelsScatterY = value;
                OnPropertyChanged("LabelsScatterY");
            }
        }
        public Func<double, string> FormatterStackedRow4 { get; set; }
        public Func<double, string> FormatterTransferredStd { get; set; }

        public SeriesCollection SeriesCollectionDiversityStackedBar { get; set; }
        public string[] LabelsStackedBarDiversity { get; set; }
        public Func<double, string> FormatterStackedBarDiversity { get; set; }

        public SeriesCollection SeriesCollectionDiversityBar { get; set; }
        public string[] LabelsDiversityBar { get; set; }
        public Func<double, string> FormatterDiversityBar { get; set; }

        public SeriesCollection SeriesCollectionBar { get; set; }
        public string[] LabelsBar { get; set; }
        public Func<double, string> FormatterBar { get; set; }
        public string[] LabelsSportsBar { get; set; }
        public Func<double, string> FormatterSportsBar { get; set; }

        public string[] LabelsClubsBar { get; set; }
        public Func<double, string> FormatterClubsBar { get; set; }

        public string[] LabelsBarDOB { get; set; }
        public Func<double, string> FormatterBarDOB { get; set; }

        public SeriesCollection SeriesCollectionBarSandboxRow2 { get; set; }
        public string[] LabelsBarSandboxRow2 { get; set; }
        public Func<double, string> FormatterBarSandboxRow2 { get; set; }

        public string StudentCount { get; set; }
        public string FemaleCount { get; set; }
        public string MaleCount { get; set; }

        public string _citizenshipScrollFrom;
        public string CitizenshipScrollFrom
        {
            get { return _citizenshipScrollFrom; }
            set
            {
                _citizenshipScrollFrom = value;
                OnPropertyChanged("CitizenshipScrollFrom");
            }
        }

        public string _citizenshipScrollTo;
        public string CitizenshipScrollTo
        {
            get { return _citizenshipScrollTo; }
            set
            {
                _citizenshipScrollTo = value;
                OnPropertyChanged("CitizenshipScrollTo");
            }
        }

        public ChartValues<CustomVM> custTooltip { get; set; }
        public void BarChartSampleRow1(string subjectId)
        {
            var birthOriginCount = KirinEntities.getBirthOriginCountbySubjectID(subjectId).ToList();

            var studentCount = (from obj in birthOriginCount
                                select new getBirthOriginCountbySubjectID_Result
                                {
                                    TotalStudent = obj.TotalStudent,
                                    BirthCount = obj.BirthCount,
                                    ORIGIN_OF_BIRTH = obj.ORIGIN_OF_BIRTH
                                }).FirstOrDefault();

            StudentCount = studentCount.TotalStudent.ToString();

            ChartValues<double> chartVal = new ChartValues<double>();
            List<string> lableList = new List<string>();
            custTooltip = new ChartValues<CustomVM> { };

            foreach (getBirthOriginCountbySubjectID_Result item in birthOriginCount)
            {
                chartVal.Add(Convert.ToDouble(item.BirthCount));
                lableList.Add(item.ORIGIN_OF_BIRTH);
                custTooltip.Add(new CustomVM
                {
                    Name = item.ORIGIN_OF_BIRTH,
                    Value = item.BirthCount.ToString()
                });
            }

            var customVmMapper = Mappers.Xy<CustomVM>()
               .X((value, index) => index)
               .Y(value => Convert.ToDouble(value.Value));

            //lets save the mapper globally
            Charting.For<CustomVM>(customVmMapper);
        }

        public string _lineRow1From;
        public string LineRow1From
        {
            get { return _lineRow1From; }
            set
            {
                _lineRow1From = value;
                OnPropertyChanged("LineRow1From");
            }
        }

        public string _lineRow1To;
        public string LineRow1To
        {
            get { return _lineRow1To; }
            set
            {
                _lineRow1To = value;
                OnPropertyChanged("LineRow1To");
            }
        }

        public ObservableCollection<DoughnutChartRow3> _doughnutChartRow3Data;
        public ObservableCollection<DoughnutChartRow3> DoughnutChartRow3Data
        {
            get { return _doughnutChartRow3Data; }
            set
            {
                _doughnutChartRow3Data = value;
                OnPropertyChanged("DoughnutChartRow3Data");
            }
        }

        public ChartColorModel colorModel { get; set; }

        public void LineChartSampleRow3(List<ObservableCollection<double>> subScore, List<string> subjectName, List<string> xAxis)
        {
            SeriesCollectionLineRow3 = new SeriesCollection();
            //SeriesCollectionLineRow3 = new ChartSeriesCollection();

            for (int i = 0; i < subScore.Count(); i++)
            {
                var color = (Color)ColorConverter.ConvertFromString(SubjectColor[i]);

                LiveCharts.Wpf.LineSeries subjects = new LiveCharts.Wpf.LineSeries
                {
                    Title = subjectName[i],
                    LineSmoothness = 0,
                    Values = new ChartValues<double>(subScore[i]),
                    PointGeometry = DefaultGeometries.None,
                    PointGeometrySize = 0,
                    Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
                };

                SeriesCollectionLineRow3.Add(subjects);

                //Syncfusion.UI.Xaml.Charts.LineSeries series = new Syncfusion.UI.Xaml.Charts.LineSeries()
                //{
                //    XBindingPath = "Xlabel",
                //    YBindingPath = "Percentage",
                //    ItemsSource = subScore[i],
                //    ShowTooltip = false
                //};

                //ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
                //{
                //    ShowLabel = false,
                //    Symbol = ChartSymbol.Diamond,
                //    LabelPosition = AdornmentsLabelPosition.Auto,
                //};

                //series.AdornmentsInfo = adornmentInfo;

                //SeriesCollectionLineRow3.Add(series);
            }

            string[] xAxisLabel = xAxis.ToArray();
            LabelsLineRow3 = xAxisLabel;
            YFormatterRow3 = value => value.ToString("0.##");

            ////------Add Subject Legend-------//
            //SubjectList = new ObservableCollection<SubjectData>();
            //for (int i = 0; i < subjectName.Count; i++)
            //{
            //    SubjectData subjectData = new SubjectData();
            //    subjectData.subject = subjectName[i];
            //    subjectData.Colour = (Color)ColorConverter.ConvertFromString(SubjectColor[i]); //ScoreColor[i]; 

            //    SubjectList.Add(subjectData);
            //}
        }

        private ObservableCollection<LineChartRow1> _Row3DataPoints;

        public ObservableCollection<LineChartRow1> Row3DataPoints
        {
            get { return _Row3DataPoints; }
            set
            {
                _Row3DataPoints = value;
                OnPropertyChanged("Row3DataPoints");
            }
        }

        public void GetCourseList(string studentId)
        {
            var stdCourse = KirinEntities.GetSubjectIdforStudent(studentId).ToList();

            if (stdCourse.Count() > 0)
            {
                CourseList = new ObservableCollection<CourseData>(from obj in stdCourse
                                                                  select new CourseData
                                                                  {
                                                                      Id = obj.ID.ToString(),
                                                                      CourseName = obj.COURSE_NAME,
                                                                      CourseCode = obj.COURSE_CODE
                                                                  });
            }

        }

        public void LineChartRow3(string subjectId, string studentId)
        {
            this.Row3DataPoints = new ObservableCollection<LineChartRow1>();

            if (studentId != null)
            {
                var studentGrades = KirinEntities.GetGradesbySubjectId(subjectId, studentId, "Average").ToList();

                if (studentGrades.Count() > 0)
                {
                    int xVar = 0;
                    foreach (var item in studentGrades)
                    {
                        Row3DataPoints.Add(new LineChartRow1()
                        {
                            Assignment = item.Assignment,
                            POINTS = item.POINTS,
                            POINTS_POSSIBLE = item.POINTS_POSSIBLE.ToString(),
                            LabelDate = item.LabelDate,
                            Average = item.Average,
                            Percentage = item.Percentage.ToString(),
                            ClassAvg = item.ClassAvg.ToString() + "%",
                            Xlabel = xVar == item.Month ? item.Day : item.LabelDate,
                            Expected = "65%",
                            Actual = item.Percentage.ToString() + "%",
                            Variation = Math.Abs(item.Variation) + "%",
                            GrowthColor = item.Variation > 0 ? Brushes.Green : Brushes.Red,
                            ArrowType = item.Variation > 0 ? "ArrowUp" : "ArrowDown",
                            AssignmentDate = item.AssignmentDate
                        });

                        xVar = Convert.ToInt32(item.Month);
                    }

                }
            }
        }

        public BitmapImage _centerImage;
        public BitmapImage CenterImage
        {
            get { return _centerImage; }
            set
            {
                _centerImage = value;
                OnPropertyChanged("CenterImage");
            }
        }

        public void DoughnutChartRow3(string studentId)
        {
            //---------Stacked Doughnut Chart-------------//
            var doughnutChartData = KirinEntities.GetStudentScorebyClass(schoolId, studentId).ToList();
            DoughnutChartRow3Data = new ObservableCollection<DoughnutChartRow3>();
            StudentScoreData = new ObservableCollection<StudentScore>();
            var customBrushes = new List<Brush>();
            colorModel = new ChartColorModel();
            SubjectList = new ObservableCollection<SubjectData>();

            int i = 0;
            foreach (var item in doughnutChartData)
            {
                DoughnutChartRow3 data = new DoughnutChartRow3()
                {
                    Subject = item.COURSE_NAME,
                    Score = item.AverageScore,
                    Photo = ByteArrayToImage(item.IMG)
                };

                DoughnutChartRow3Data.Add(data);

                CenterImage = ByteArrayToImage(item.IMG);

                //------------Chart Legend-------------------//
                StudentScore studentData = new StudentScore()
                {
                    Subject = item.COURSE_NAME,
                    Score = item.AverageScore + "%",
                    Colour = ScoreColor[i],
                    SubjectId = item.SUBJECT_ID.ToString(),
                    StudentId = item.StudentId.ToString()
                };

                StudentScoreData.Add(studentData);

                //-----------Color Code-----------------//
                customBrushes.Add(new SolidColorBrush(ScoreColor[i]));

                //----------------Line graph subject list-------------------//
                SubjectData subjectData = new SubjectData();
                subjectData.subject = item.COURSE_NAME;
                subjectData.Colour = ScoreColor[i];

                SubjectList.Add(subjectData);

                i++;
            }
            colorModel.CustomBrushes = customBrushes;
        }

        private ObservableCollection<LineChartRow1> _Row1DataPoints;

        public ObservableCollection<LineChartRow1> Row1DataPoints
        {
            get { return _Row1DataPoints; }
            set
            {
                _Row1DataPoints = value;
                OnPropertyChanged("Row1DataPoints");
            }
        }

        public void LineChartSampleRow1(string subjectId, string studentId, string toggleType)
        {
            ObservableCollection<double> stdGrades = new ObservableCollection<double>();
            ObservableCollection<string> stdAssignment = new ObservableCollection<string>();
            List<string> xlist = new List<string>();

            this.Row1DataPoints = new ObservableCollection<LineChartRow1>();

            if (studentId != null)
            {
                var studentGrades = KirinEntities.GetGradesbySubjectId(subjectId, studentId, toggleType).ToList();

                if (studentGrades.Count() > 0)
                {
                    int xVar = 0;
                    foreach (var item in studentGrades)
                    {
                        Row1DataPoints.Add(new LineChartRow1()
                        {
                            Assignment = item.Assignment,
                            POINTS = item.POINTS,
                            POINTS_POSSIBLE = item.POINTS_POSSIBLE.ToString(),
                            LabelDate = item.LabelDate,
                            Average = item.Average,
                            Percentage = item.Percentage.ToString(),
                            ClassAvg = item.ClassAvg.ToString() + "%",
                            Xlabel = xVar == item.Month ? item.Day : item.LabelDate,
                            Expected = "65%",
                            Actual = toggleType.ToUpper() == "AVERAGE" ? item.AssignPercentage.ToString() + "%" : item.Percentage.ToString() + "%",
                            Variation = Math.Abs(item.Variation) + "%",
                            GrowthColor = item.Variation > 0 ? Brushes.Green : Brushes.Red,
                            ArrowType = item.Variation > 0 ? "ArrowUp" : "ArrowDown",
                            AverageHeight = toggleType.ToUpper() == "AVERAGE" ? "180" : "0",
                            AverageVisibility = toggleType.ToUpper() == "AVERAGE" ? Visibility.Visible : Visibility.Hidden,
                            ScoreHeight = toggleType.ToUpper() == "ASSIGNMENT" ? "180" : "0",
                            ScoreVisibility = toggleType.ToUpper() == "ASSIGNMENT" ? Visibility.Visible : Visibility.Hidden,
                            AssignmentDate = item.AssignmentDate
                        });

                        xVar = Convert.ToInt32(item.Month);

                        //stdGrades.Add(Convert.ToDouble(item.Percentage));
                        //stdAssignment.Add(item.Assignment);

                        //if (!xlist.Contains(item.LabelDate))
                        //{
                        //    xlist.Add(item.LabelDate);
                        //}
                        //else
                        //{
                        //    xlist.Add("");
                        //}
                    }
                    //LineRow1From = "0";
                    //LineRow1To = studentGrades.Count().ToString();
                }
            }


            int subId = !string.IsNullOrWhiteSpace(subjectId) ? Convert.ToInt32(subjectId) : 0;
            int scId = !string.IsNullOrWhiteSpace(schoolId) ? Convert.ToInt32(schoolId) : 0;

            var subject = KirinEntities.SUBJECTS.Where(s => s.ID == subId && s.SCHOOL_ID == scId).Select(s => s.COURSE_NAME).FirstOrDefault();

            //var chartValues = new ChartValues<Point>();

            //// Create a sine
            //for (int x = 0; x < 361; x++)
            //{
            //    var point = new Point() { X = x, Y = Math.Round(Math.Sin(x * Math.PI / 180), 2) };
            //    chartValues.Add(point);
            //}

            SeriesCollectionLineRow1 = new SeriesCollection
            {
                new LiveCharts.Wpf.LineSeries
                {
                    Title = subject + " ",
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Diamond,
                    PointGeometrySize = 10,
                    Values = new ChartValues<double> (stdGrades),
                    Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color },
                    Configuration = new CartesianMapper<Point>().X(point => point.X).Y(point => point.Y),
                    //LabelPoint = chartPoint => string.Format("{0}", stdAssignment[Convert.ToInt32(selectedChartPoint.X)]),
                }
            };

            string[] xAxisLabel = xlist.ToArray();
            LabelsLineRow1 = xAxisLabel;
            YFormatterRow1 = value => value.ToString("0.##");
        }

        public LiveCharts.Wpf.LineSeries predict1;
        public LiveCharts.Wpf.LineSeries predict2;
        public LiveCharts.Wpf.LineSeries predict3;
        public LiveCharts.Wpf.LineSeries predict4;
        public LiveCharts.Wpf.LineSeries subject2;
        public LiveCharts.Wpf.LineSeries subject3;
        public LiveCharts.Wpf.LineSeries subject4;

        public void MLLineChart()
        {
            predict1 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject1),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            predict2 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject2),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            predict3 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject3),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            predict4 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject4),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };

            subject2 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 2",
                LineSmoothness = 0,
                Values = new ChartValues<double>(Subject2),
                PointGeometrySize = 0,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            subject3 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 3",
                LineSmoothness = 0,
                Values = new ChartValues<double>(Subject3),
                PointGeometrySize = 0,

                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };

            subject4 = new LiveCharts.Wpf.LineSeries
            {
                Title = "Subject 4",
                Values = new ChartValues<double>(Subject4),
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometrySize = 0,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };


            MLSandboxRow1Collection = new SeriesCollection
            {
                new LiveCharts.Wpf.LineSeries
                {
                    Title = "Subject 1",
                    LineSmoothness = 0,
                    PointGeometrySize = 0,
                    Values = new ChartValues<double> (Subject1),
                    Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color },
                    }
            };

            MLSandboxRow1Collection.Add(subject2);
            MLSandboxRow1Collection.Add(subject3);
            MLSandboxRow1Collection.Add(subject4);
            MLSandboxRow1Collection.Add(predict1);
            MLSandboxRow1Collection.Add(predict2);
            MLSandboxRow1Collection.Add(predict3);
            MLSandboxRow1Collection.Add(predict4);

            LabelsLineRow1 = new[] { "Nov-2020", "Dec-2020", "Jan", "Feb", "March", "April", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "Jan-2022" };
            YFormatterRow1 = value => value.ToString("0.##");
        }

        //public void LineChartSampleRow4()
        //{
        //    SeriesCollectionLineRow4 = new SeriesCollection
        //    {
        //        new LiveCharts.Wpf.LineSeries
        //        {
        //            Title = "Series 1",
        //            Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
        //        },
        //        new LiveCharts.Wpf.LineSeries
        //        {
        //            Title = "Series 2",
        //            Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
        //            PointGeometry = null
        //        },
        //        new LiveCharts.Wpf.LineSeries
        //        {
        //            Title = "Series 3",
        //            Values = new ChartValues<double> { 4,2,7,2,7 },
        //            PointGeometry = DefaultGeometries.Square,
        //            PointGeometrySize = 3
        //        }
        //    };

        //    LabelsLineRow4 = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
        //    YFormatterRow4 = value => value.ToString("C");

        //    //modifying the series collection will animate and update the chart
        //    SeriesCollectionLineRow4.Add(new LiveCharts.Wpf.LineSeries
        //    {
        //        Title = "Series 4",
        //        Values = new ChartValues<double> { 5, 3, 2, 4 },
        //        LineSmoothness = 0, //0: straight lines, 1: really smooth lines
        //        PointGeometrySize = 3,
        //        PointForeground = Brushes.Gray
        //    });

        //    //modifying any series values will also animate and update the chart
        //    SeriesCollectionLineRow4[3].Values.Add(5d);

        //}

        public void TransferredStudentsStackedBar(string subjectId, string schoolId)
        {
            var stackbarData = KirinEntities.GetTransferredStudentDataforStackedBar(schoolId, subjectId).ToList();
            List<string> monthlist = new List<string>();
            List<List<double>> typeList = new List<List<double>>();

            var transferredType = KirinEntities.GetTransferType().ToList();
            List<string> type = transferredType.Select(x => x.TRANSFER_TYPE).ToList();
            List<string> colorCode = transferredType.Select(x => x.COLOR).ToList();
            var maxValue = stackbarData.Max(x => x.Total);
            TransferMaxVal = Convert.ToInt32(maxValue) + 1;

            int tCount = type.Count();

            List<double> transferCountList = new List<double>();
            TransferStudentStackedBar = new SeriesCollection { };

            if (stackbarData != null)
            {
                foreach (var item in stackbarData)
                {
                    monthlist.Add(item.MonthName);
                }

                for (int i = 0; i < tCount; i++)
                {
                    transferCountList = new List<double>();
                    foreach (var item in stackbarData)
                    {
                        switch (i)
                        {
                            case 0:
                                transferCountList.Add(Convert.ToDouble(item.Course));
                                break;
                            case 1:
                                transferCountList.Add(Convert.ToDouble(item.Classroom));
                                break;
                            case 2:
                                transferCountList.Add(Convert.ToDouble(item.School));
                                break;
                            case 3:
                                transferCountList.Add(Convert.ToDouble(item.Behavioral));
                                break;
                        }
                    }
                    typeList.Add(transferCountList);
                }

                for (int i = 0; i < typeList.Count(); i++)
                {
                    var color = (Color)ColorConverter.ConvertFromString(colorCode[i]);

                    StackedColumnSeries transferCount = new StackedColumnSeries
                    {
                        Values = new ChartValues<double>(typeList[i]),
                        StackMode = StackMode.Values,
                        DataLabels = true,
                        Title = type[i],
                        LabelsPosition = BarLabelPosition.Perpendicular,
                        Fill = new SolidColorBrush() { Opacity = 1, Color = color },
                        MaxColumnWidth = 30
                    };

                    TransferStudentStackedBar.Add(transferCount);
                }
            }

            string[] xAxis = monthlist.ToArray();
            LabelsTransferStacked = xAxis;

            FormatterTransferredStd = value => value.ToString("0.#");

            //----------Stacked Legend Data-----------------//

            var stackbarLegend = KirinEntities.GetStudentTransfferedStackBarLegend(schoolId, subjectId).ToList();

            TransferStdList = new ObservableCollection<TransferStudentData>(from ts in stackbarLegend
                                                                            select new TransferStudentData
                                                                            {
                                                                                Label = ts.Label,
                                                                                Colour = (Color)ColorConverter.ConvertFromString(ts.Color),
                                                                                StudentCount = ts.StudentCount.ToString(),
                                                                                Progress = Convert.ToInt32(ts.StudentCount)
                                                                            });
        }

        public void StackedBarRow4(string fromDate, string toDate, string subjectId, string schoolId)
        {
            string fdate = Convert.ToDateTime(fromDate).ToString("yyyy/MM/dd");
            string tdate = Convert.ToDateTime(toDate).ToString("yyyy/MM/dd");

            var stackbarData = KirinEntities.GetStackedBarAttandanceData(fdate, tdate, Convert.ToInt32(subjectId), Convert.ToInt32(schoolId)).ToList();
            List<string> datelist = new List<string>();
            List<string> stackeddatelist = new List<string>();
            List<List<double>> codeList = new List<List<double>>();

            var attendanceCode = KirinEntities.GetAttendanceCode().ToList();
            List<string> code = attendanceCode.Select(x => x.CODE).ToList();
            List<string> colorCode = attendanceCode.Select(x => x.COLOR).ToList();
            //List<string> opecityList = attendanceCode.Select(x => x.WEIGHT).ToList();
            var maxValue = stackbarData.Max(x => x.Total);
            maxValueRow4 = Convert.ToInt32(maxValue) + 6;

            int codeCount = code.Count();

            List<double> codeCountList = new List<double>();
            StackedbarRow4 = new SeriesCollection { };

            if (stackbarData != null)
            {
                int xVar = 0;
                foreach (var item in stackbarData)
                {
                    stackeddatelist.Add(item.OriginalDate.ToString());
                    string Xlabel = xVar == item.Month ? item.Day : item.DATE;
                    datelist.Add(Xlabel);

                    xVar = Convert.ToInt32(item.Month);
                }

                for (int i = 0; i < codeCount; i++)
                {
                    codeCountList = new List<double>();
                    foreach (var item in stackbarData)
                    {
                        switch (i)
                        {
                            case 0:
                                codeCountList.Add(Convert.ToDouble(item.P));
                                break;
                            case 1:
                                codeCountList.Add(Convert.ToDouble(item.PL));
                                break;
                            case 2:
                                codeCountList.Add(Convert.ToDouble(item.LI));
                                break;
                            case 3:
                                codeCountList.Add(Convert.ToDouble(item.LU));
                                break;
                            case 4:
                                codeCountList.Add(Convert.ToDouble(item.AI));
                                break;
                            case 5:
                                codeCountList.Add(Convert.ToDouble(item.AU));
                                break;
                            case 6:
                                codeCountList.Add(Convert.ToDouble(item.BD));
                                break;
                            case 7:
                                codeCountList.Add(Convert.ToDouble(item.BC));
                                break;
                            case 8:
                                codeCountList.Add(Convert.ToDouble(item.DC));
                                break;
                            case 9:
                                codeCountList.Add(Convert.ToDouble(item.FT));
                                break;
                            case 10:
                                codeCountList.Add(Convert.ToDouble(item.TS));
                                break;
                        }
                    }
                    codeList.Add(codeCountList);
                }

                for (int i = 0; i < codeList.Count(); i++)
                {
                    var color = (Color)ColorConverter.ConvertFromString(colorCode[i]);
                    //var color = (Color)ColorConverter.ConvertFromString("#247BC0");
                    StackedColumnSeries attandanceCount = new StackedColumnSeries
                    {
                        Values = new ChartValues<double>(codeList[i]),
                        StackMode = StackMode.Values,
                        DataLabels = true,
                        Title = (i == 0) ? "P" : code[i],
                        LabelsPosition = BarLabelPosition.Perpendicular,
                        Fill = new SolidColorBrush() { Opacity = 1, Color = color },
                        MaxColumnWidth = 20
                    };

                    Func<LiveCharts.ChartPoint, string> labelPoint = chartPoint => string.Format("{0}", attandanceCount.Title);
                    attandanceCount.LabelPoint = labelPoint;

                    StackedbarRow4.Add(attandanceCount);
                }
            }

            string[] xAxis = datelist.ToArray();
            LabelsStackedRow4 = xAxis;

            string[] xDate = stackeddatelist.ToArray();
            LabelsStackedDate = xDate;

            FormatterStackedRow4 = value => value.ToString("0.#");
        }

        public Dictionary<string, double> CitizenshipGeomapValues { get; set; }

        public GradientStopCollection citizenshipGradient;
        public void CitizenshipGeomap(string schoolId, string subjectId)
        {
            //---------Create Gredient Color Coding----------------//
            citizenshipGradient = new GradientStopCollection();
            citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#B4C9F6"), 0.0));
            citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#274DA0"), 1.0));

            //citizenshipGradient = new GradientStopCollection();
            //citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#A020F0"), 0.0));
            //citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#B24BF3"), 0.25));
            //citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#0000FF"), 0.5));
            //citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FBBE3C"), 0.75));
            //citizenshipGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#F7651E"), 1.0));
            
            var citizenshipCount = KirinEntities.GetStudentCitizenshipCountbySubjectID(subjectId, schoolId).ToList();

            CitizenshipGeoMap = new ObservableCollection<CitizenshipValue>(from obj in citizenshipCount
                                                                           select new CitizenshipValue
                                                                           {
                                                                               Country = obj.CITIZENSHIP + " [" + obj.CountryCode + "]",
                                                                               CountryCount = obj.CitizenshipCount,
                                                                               Progress = (int)obj.CitizenshipCount,
                                                                               PHOTO = ByteArrayToImage(obj.Image),
                                                                           });

            CitizenshipGeomapValues = new Dictionary<string, double>();

            foreach (var item in citizenshipCount)
            {
                CitizenshipGeomapValues[item.CITIZENSHIP] = Convert.ToDouble(item.CitizenshipCount);
            }
        }

        public Color GetColorCode(double count, double maxCount)
        {
            double finalval = count / maxCount;
            Color colorcode = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            switch (Math.Round(finalval, 2))
            {
                case 0.0:
                    colorcode = (Color)ColorConverter.ConvertFromString("#A020F0");
                    break;
                case 0.25:
                    colorcode = (Color)ColorConverter.ConvertFromString("#B24BF3");
                    break;
                case 0.3:
                    colorcode = (Color)ColorConverter.ConvertFromString("#D7A1F9");
                    break;
                case 0.33:
                    colorcode = (Color)ColorConverter.ConvertFromString("#D7A1F9");
                    break;
                case 0.4:
                    colorcode = (Color)ColorConverter.ConvertFromString("#0000D1");
                    break;
                case 0.5:
                    colorcode = (Color)ColorConverter.ConvertFromString("#0000FF");
                    break;
                case 0.6:
                    colorcode = (Color)ColorConverter.ConvertFromString("#2E2EFF");
                    break;
                case 0.67:
                    colorcode = (Color)ColorConverter.ConvertFromString("#2E2EFF");
                    break;
                case 0.75:
                    colorcode = (Color)ColorConverter.ConvertFromString("#FBBE3C");
                    break;
                case 0.8:
                    colorcode = (Color)ColorConverter.ConvertFromString("#FB9A2F");
                    break;
                case 0.9:
                    colorcode = (Color)ColorConverter.ConvertFromString("#E37907");
                    break;
                case 1.0:
                    colorcode = (Color)ColorConverter.ConvertFromString("#F7651E");
                    break;
            }

            return colorcode;
        }

        public BitmapImage ByteArrayToImage(byte[] array)
        {
            if (array is null)
            {
                BitmapImage tmp = new BitmapImage();
                tmp.BeginInit();
                tmp.UriSource = new Uri("pack://application:,,,/Images/Logo.png");

                tmp.DecodePixelWidth = 240;
                tmp.EndInit();

                return tmp;
            }
            else
            {
                using (var ms = new MemoryStream(array))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
        }

        public System.Drawing.Image byteArrayToImg(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage = null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                returnImage = System.Drawing.Image.FromStream(ms);
            }
            return returnImage;
        }

        private ObservableCollection<EthnicityData> _EthnicityList;
        public ObservableCollection<EthnicityData> EthnicityList
        {
            get { return _EthnicityList; }
            set
            {
                _EthnicityList = value;
                OnPropertyChanged(nameof(EthnicityList));
            }
        }

        private ObservableCollection<SubjectData> _SubjectList;
        public ObservableCollection<SubjectData> SubjectList
        {
            get { return _SubjectList; }
            set
            {
                _SubjectList = value;
                OnPropertyChanged(nameof(SubjectList));
            }
        }

        private ObservableCollection<CourseData> _CourseList;
        public ObservableCollection<CourseData> CourseList
        {
            get { return _CourseList; }
            set
            {
                _CourseList = value;
                OnPropertyChanged(nameof(CourseList));
            }
        }

        private ObservableCollection<StudentScore> _StudentScoreData;
        public ObservableCollection<StudentScore> StudentScoreData
        {
            get { return _StudentScoreData; }
            set
            {
                _StudentScoreData = value;
                OnPropertyChanged(nameof(StudentScoreData));
            }
        }


        private ObservableCollection<TransferStudentData> _TransferStdList;
        public ObservableCollection<TransferStudentData> TransferStdList
        {
            get { return _TransferStdList; }
            set
            {
                _TransferStdList = value;
                OnPropertyChanged(nameof(TransferStdList));
            }
        }

        private ObservableCollection<TransferStudentDataPie> _TransferStdListPie;
        public ObservableCollection<TransferStudentDataPie> TransferStdListPie
        {
            get { return _TransferStdListPie; }
            set
            {
                _TransferStdListPie = value;
                OnPropertyChanged(nameof(TransferStdListPie));
            }
        }


        private ObservableCollection<DobStudentData> _dobStdList;
        public ObservableCollection<DobStudentData> DobStdList
        {
            get { return _dobStdList; }
            set
            {
                _dobStdList = value;
                OnPropertyChanged(nameof(DobStdList));
            }
        }


        private ObservableCollection<DobStudentScatterData> _dobStdScatterList;
        public ObservableCollection<DobStudentScatterData> DOBStdScatterList
        {
            get { return _dobStdScatterList; }
            set
            {
                _dobStdScatterList = value;
                OnPropertyChanged(nameof(DOBStdScatterList));
            }
        }


        private ObservableCollection<StudentSportsData> _SportsStdList;
        public ObservableCollection<StudentSportsData> SportsStdList
        {
            get { return _SportsStdList; }
            set
            {
                _SportsStdList = value;
                OnPropertyChanged(nameof(SportsStdList));
            }
        }

        private ObservableCollection<CitizenshipLegend> _CitizenshipLegend;
        public ObservableCollection<CitizenshipLegend> CitizenshipLegend
        {
            get { return _CitizenshipLegend; }
            set
            {
                _CitizenshipLegend = value;
                OnPropertyChanged(nameof(CitizenshipLegend));
            }
        }

        private ObservableCollection<StudentList> _StudentListData;
        public ObservableCollection<StudentList> StudentListData
        {
            get { return _StudentListData; }
            set
            {
                _StudentListData = value;
                OnPropertyChanged(nameof(StudentListData));
            }
        }


        private ObservableCollection<ClubStudentData> _ClubStdList;
        public ObservableCollection<ClubStudentData> ClubStdList
        {
            get { return _ClubStdList; }
            set
            {
                _ClubStdList = value;
                OnPropertyChanged(nameof(ClubStdList));
            }
        }

        private ObservableCollection<EthnicityLegend> _EthnicityLegend;
        public ObservableCollection<EthnicityLegend> EthnicityLegend
        {
            get { return _EthnicityLegend; }
            set
            {
                _EthnicityLegend = value;
                OnPropertyChanged(nameof(EthnicityLegend));
            }
        }

        private ObservableCollection<DiversityStackedList> _DiversityLegend;
        public ObservableCollection<DiversityStackedList> DiversityLegend
        {
            get { return _DiversityLegend; }
            set
            {
                _DiversityLegend = value;
                OnPropertyChanged(nameof(DiversityLegend));
            }
        }

        private ObservableCollection<LanguagePriority> _PriorityList;
        public ObservableCollection<LanguagePriority> PriorityList
        {
            get { return _PriorityList; }
            set
            {
                _PriorityList = value;
                OnPropertyChanged(nameof(PriorityList));
            }
        }

        private ObservableCollection<StudentDiversityData> _DiversityStdList;
        public ObservableCollection<StudentDiversityData> DiversityStdList
        {
            get { return _DiversityStdList; }
            set
            {
                _DiversityStdList = value;
                OnPropertyChanged(nameof(DiversityStdList));
            }
        }

        public Dictionary<string, double> EthnicityGeomapValues { get; set; }
        public GradientStopCollection EthnicityGradient;
        public void EthnicityGeomap()
        {
            var ethnicityData = KirinEntities.GetEthnicityData(schoolId, subjectId).ToList();

            EthnicityList = new ObservableCollection<EthnicityData>(from e in ethnicityData
                                                                    select new EthnicityData
                                                                    {
                                                                        Ethnicity = e.ETHNICITY,
                                                                        PHOTO = ByteArrayToImage(e.Image),
                                                                        EthnicityCount = e.EthnicityCount.ToString(),
                                                                        Progress = Convert.ToInt32(e.EthnicityCount) + 3,
                                                                        //Colour = GetColorCode(Convert.ToDouble(e.EthnicityCount), Convert.ToDouble(e.MaxCount)),
                                                                        //MaxCount = Convert.ToInt32(e.MaxCount)
                                                                    });

            //---------Create Gredient Color Coding----------------//
            EthnicityGradient = new GradientStopCollection();
            EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#B4C9F6"), 0.0));
            EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#274DA0"), 1.0));

            //EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#A020F0"), 0.0));
            //EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#B24BF3"), 0.25));
            //EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#0000FF"), 0.5));
            //EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FBBE3C"), 0.75));
            //EthnicityGradient.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#F7651E"), 1.0));

            EthnicityGeomapValues = new Dictionary<string, double>();

            foreach (var item in ethnicityData)
            {
                EthnicityGeomapValues[item.ETHNICITY] = Convert.ToDouble(item.EthnicityCount);
            }

        }

        public void populateSeriesCollectionsTransferStudents()
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //----------Student Transfer Student Bar-------------//
            TransferStudentBar();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //----------Student Transfer Student Pie-------------//
            TransferStudentPie();

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            /////----------Student Transfer Stacked Bar-----------//
            TransferredStudentsStackedBar(subjectId, schoolId);

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //--------Student Citizenship Pie and Bar Chart-------------//
            StudentCitizenshipRow2();
            GetPieTooltipData();

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //----------Student Date of Birth Bar,Pie Chart, Scatter---------------------//
            StudentDOBRow2();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ////------------Student Languages Bar,Pie Chart--------------////
            StudentLanguagesRow2();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ////------------Diversity Bar,Pie Chart------------////
            StudentDiversityRow2();


            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ////------------Diversity v/s Ethenicity Stacked Bar------------////
            StudentDiversityEthnicityStackedBarRow2();

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////--------------Sports/Cocurricular Bar,Pie,Line------------------////
            StudentExtraCoRow2();

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ////-------------Club Joined Bar, Pie ---------------////
            //ClubJoinedRow2();

        }

        public ChartValues<PieVM> pieTooltip { get; set; }

        public void GetPieTooltipData()
        {
            var studentCitizenshipData = KirinEntities.GetStudentCitizenshipCountbySubjectID(subjectId, schoolId).ToList();
            pieTooltip = new ChartValues<PieVM> { };

            foreach (var item in studentCitizenshipData)
            {
                pieTooltip.Add(new PieVM
                {
                    Title = item.CITIZENSHIP + " : " + item.CitizenshipCount,
                    Name = item.CITIZENSHIP,
                    Value = item.CitizenshipCount.ToString()
                });

            }
            var customVmMapper = Mappers.Xy<PieVM>()
               .X((value, index) => index)
               .Y(value => Convert.ToDouble(value.Value));

            //lets save the mapper globally
            Charting.For<PieVM>(customVmMapper);
        }

        public void StudentCitizenshipRow2()
        {
            var studentCitizenshipData = KirinEntities.GetStudentCitizenshipCountbySubjectID(subjectId, schoolId).ToList();

            ChartValues<double> citizenshipCount = new ChartValues<double>();
            List<string> lableList = new List<string>();
            SeriesCollection citizenshipPie = new SeriesCollection();
            var color = (Color)ColorConverter.ConvertFromString("#0078DE");

            foreach (var item in studentCitizenshipData)
            {
                citizenshipCount.Add(Convert.ToDouble(item.CitizenshipCount));
                lableList.Add(item.CITIZENSHIP);

                if (item.RNo == 1)
                {
                    CitizenshipScrollFrom = item.RNo.ToString();
                }

                if (item.RNo == studentCitizenshipData.Count())
                {
                    CitizenshipScrollTo = item.RNo.ToString();
                }

                /////----Citizenship Pie chartView------------//////
                LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = item.CITIZENSHIP, //+ " : " + item.CitizenshipCount,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.CitizenshipCount)) },
                    DataLabels = true,
                    LabelPosition = PieLabelPosition.InsideSlice,
                    Fill = new SolidColorBrush() { Opacity = Convert.ToDouble(item.Opecity), Color = color }
                };

                Func<LiveCharts.ChartPoint, string> labelPoint = chartPoint => string.Format("{0}", item.CountryCode);
                pieSeries.LabelPoint = labelPoint;

                citizenshipPie.Add(pieSeries);
            }

            SeriesCollectionCitizenshipBar = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = citizenshipCount,
                    MaxColumnWidth = 20
                }
            };

            string[] lableArr = lableList.ToArray();

            LabelsBarCitizenship = lableArr;
            FormatterBarCitizenship = value => value.ToString();


            ////-----------Citizenship Pie Chart------------//
            SeriesCollectionCitizenshipPie = citizenshipPie;

            //////--------------Citizenship Pie Legend------------------//
            //var citizenshipData = KirinEntities.GetStudentCitizenshipData(subjectId, schoolId).ToList();

            //CitizenshipLegend = new ObservableCollection<CitizenshipLegend>(from c in citizenshipData
            //                                                                select new CitizenshipLegend
            //                                                                {
            //                                                                    CITIZENSHIP1 = c.Column1 + " : " + c.Count1,
            //                                                                    CITIZENSHIP2 = c.Column2 + " : " + c.Count2,
            //                                                                    PHOTO1 = ByteArrayToImage(c.Image1),
            //                                                                    PHOTO2 = ByteArrayToImage(c.Image2),
            //                                                                });
            //CitizenshipLegend.Remove(new CitizenshipLegend { CITIZENSHIP2 = ":" });
        }


        public void StudentDOBRow2()
        {
            var dateofBirthData = KirinEntities.GetStudentDateofBirthCountbySubjectID(subjectId, schoolId).ToList();

            ChartValues<double> dateofBirthCount = new ChartValues<double>();
            List<string> dobLableList = new List<string>();
            SeriesCollection dobPie = new SeriesCollection();

            foreach (var item in dateofBirthData)
            {
                dateofBirthCount.Add(Convert.ToDouble(item.BirthCount));
                dobLableList.Add(item.MonthName);

                var color = (Color)ColorConverter.ConvertFromString(MonthColor[item.MonthName]);

                /////----Data of Birth Piechart View------------//////
                LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = item.MonthName,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.BirthCount)) },
                    DataLabels = true,
                    Fill = new SolidColorBrush() { Opacity = 1, Color = color }
                };

                Func<LiveCharts.ChartPoint, string> labelPoint = chartPoint => string.Format("{0}", pieSeries.Title);
                pieSeries.LabelPoint = labelPoint;

                dobPie.Add(pieSeries);
            }

            SeriesCollectionDOBBar = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "", //"Date of Birth",
                    Values = dateofBirthCount,
                    MaxColumnWidth = 20
                }
            };

            string[] labledobArr = dobLableList.ToArray();

            LabelsBarDOB = labledobArr;
            FormatterBarDOB = value => value.ToString("0.##");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            SeriesCollectionDOBPie = dobPie;


            //----------Pie Chart Legend Data-----------------//
            //var dobLegend = KirinEntities.GetStudentDOBdataforLegend(subjectId, schoolId).ToList();

            DobStdList = new ObservableCollection<DobStudentData>(from ts in dateofBirthData
                                                                  select new DobStudentData
                                                                  {
                                                                      MonthName = ts.MonthName,
                                                                      Count = ts.BirthCount.ToString(),
                                                                      Colour = (Color)ColorConverter.ConvertFromString(MonthColor[ts.MonthName]),
                                                                      Progress = Convert.ToInt32(ts.BirthCount) + 3,
                                                                      Initial = ts.MonthName.Substring(0, 1)
                                                                  });

            DobStdList = new ObservableCollection<DobStudentData>(DobStdList.OrderByDescending(d => d.Count));
        }

        public ChartValues<ObservablePoint> DobScatter { get; set; }

        public void GetDOBScatterLegend()
        {
            DOBStdScatterList = new ObservableCollection<DobStudentScatterData>();

            DOBStdScatterList.Add(new DobStudentScatterData
            {
                Label = "Normal",
                geometry = new EllipseGeometry(new Point(3, 3), 3, 3),
                Fill = Brushes.SteelBlue,
                Stroke = Brushes.SteelBlue,
                StrokeThickness = 10,
            });

            DOBStdScatterList.Add(new DobStudentScatterData
            {
                Label = "Birthday on Weekend",
                geometry = DefaultGeometries.Triangle,
                Fill = Brushes.DarkCyan,
                Stroke = Brushes.DarkCyan,
                StrokeThickness = 7,
            });

            DOBStdScatterList.Add(new DobStudentScatterData
            {
                Label = "Birthday on Holiday",
                geometry = DefaultGeometries.Diamond,
                Fill = Brushes.Gold,
                Stroke = Brushes.Gold,
                StrokeThickness = 10,
            });

            DOBStdScatterList.Add(new DobStudentScatterData
            {
                Label = "Nearest Birthday",
                geometry = DefaultGeometries.Square,
                Fill = Brushes.OliveDrab,
                Stroke = Brushes.OliveDrab,
                StrokeThickness = 10,
            });

            DOBStdScatterList.Add(new DobStudentScatterData
            {
                Label = "First or Last Day of Month",
                geometry = new EllipseGeometry(new Point(3, 3), 3, 3),
                Fill = Brushes.DarkOrange,
                Stroke = Brushes.DarkOrange,
                StrokeThickness = 10,
            });
        }

        public void DateofBirthScatter()
        {
            var dateofBirthData = KirinEntities.GetStudentDateofBirthData(subjectId, schoolId).ToList();

            DobScatter = new ChartValues<ObservablePoint>();
            List<string> lableList = new List<string>();

            SeriesCollection dobScatterPlot = new SeriesCollection();

            var currentBday = (Color)ColorConverter.ConvertFromString("OliveDrab");
            var firstorlastMonth = (Color)ColorConverter.ConvertFromString("DarkOrange");
            var normal = (Color)ColorConverter.ConvertFromString("SteelBlue");
            var weekend = (Color)ColorConverter.ConvertFromString("DarkCyan");
            var holiday = (Color)ColorConverter.ConvertFromString("Gold");

            int cMonth = DateTime.Now.Month;
            int cDate = DateTime.Now.Day;

            foreach (var item in dateofBirthData)
            {
                LiveCharts.Wpf.ScatterSeries scatterSeries = new LiveCharts.Wpf.ScatterSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<ObservablePoint> { new ObservablePoint(Convert.ToDouble(item.BirthMonth) - 1, Convert.ToDouble(item.BirthDay)) },
                    DataLabels = false,
                    Fill = item.BirthDay == 1 || item.BirthDay == item.LastDay ? new SolidColorBrush() { Opacity = 1, Color = firstorlastMonth } :
                           item.BirthMonth == cMonth && item.BirthDay >= cDate ? new SolidColorBrush() { Opacity = 1, Color = currentBday } :
                           item.DayofWeek == 1 || item.DayofWeek == 7 ? new SolidColorBrush() { Opacity = 1, Color = weekend } :
                           item.IsHoliday == 1 ? new SolidColorBrush() { Opacity = 1, Color = holiday } :
                           new SolidColorBrush() { Opacity = 1, Color = normal },
                    PointGeometry = item.BirthDay == 1 || item.BirthDay == item.LastDay ? DefaultGeometries.Circle :
                           item.BirthMonth == cMonth && item.BirthDay >= cDate ? DefaultGeometries.Square :
                           item.DayofWeek == 1 || item.DayofWeek == 7 ? DefaultGeometries.Triangle :
                           item.IsHoliday == 1 ? DefaultGeometries.Diamond : DefaultGeometries.Circle,
                };

                dobScatterPlot.Add(scatterSeries);
            }

            DOBScatterSeriesCollection = dobScatterPlot;
            string[] lableArr = { "Jan", "Feb", "Mar", "April", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            LabelsLine = lableArr;

            FormatterBarDOB = value => value.ToString("0.##");

            //-----------Call Legend--------------//
            GetDOBScatterLegend();
        }

        public void StudentLanguagesRow2()
        {
            var stdLanguageData = KirinEntities.getLanguageCountofStudentsbySubject(subjectId, schoolId).ToList();
            ChartValues<double> langCount = new ChartValues<double>();
            List<string> langLableList = new List<string>();

            foreach (var item in stdLanguageData)
            {
                langCount.Add(Convert.ToDouble(item.LanguageCount));
                langLableList.Add(item.LANGUAGE);
            }

            SeriesCollectionLanguagesBar = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = langCount,
                    MaxColumnWidth = 20
                }
            };

            string[] langlableArr = langLableList.ToArray();

            LabelsBarLanguages = langlableArr; //new[] { "English", "Hindi", "French", "Italian", "Mandarin", "Japanese" };
            FormatterBarLanguages = value => value.ToString();
        }

        public ChartValues<HeatPoint> Values { get; set; }
        public string[] Languages { get; set; }
        public List<string> Students { get; set; }
        public void HeatMapLanguages()
        {
            //X is Students
            //Y is the Language

            var stdLanguageData = KirinEntities.GetStudentLanguageDataforHeatmap(schoolId, subjectId).ToList();

            Values = new ChartValues<HeatPoint> { };
            Students = new List<string>();
            List<string> langList = new List<string>();

            foreach (var item in stdLanguageData)
            {
                HeatPoint heatPoint = new HeatPoint(Convert.ToDouble(item.SNo - 1), Convert.ToDouble(item.RNo - 1), Convert.ToDouble(item.Weight));

                if (!Values.Contains(heatPoint))
                {
                    Values.Add(heatPoint);
                }

                if (!langList.Contains(item.LANGUAGE))
                {
                    langList.Add(item.LANGUAGE);
                }

                if (!Students.Contains(item.Name))
                {
                    Students.Add(item.Name);
                }
            }

            Languages = langList.ToArray();

            //-------------Priority legend-----------------//
            PriorityList = new ObservableCollection<LanguagePriority>();

            var priorityList = KirinEntities.LANGUAGE_PRIORITY.Take(7).ToList();

            foreach (var item in priorityList)
            {
                LanguagePriority languagePriority = new LanguagePriority
                {
                    languageType = item.PRIORITY + " : " + item.ORDER,
                    Colour = (Color)ColorConverter.ConvertFromString(item.COLOR)
                };

                PriorityList.Add(languagePriority);
            }
        }

        public void StudentDiversityRow2()
        {
            var diversityData = KirinEntities.GetDiversityData(schoolId, subjectId).ToList();

            ChartValues<double> diversityCount = new ChartValues<double>();
            List<string> divLableList = new List<string>();
            SeriesCollection diversityPie = new SeriesCollection();

            foreach (var item in diversityData)
            {
                diversityCount.Add(Convert.ToDouble(item.DiversityCount));
                divLableList.Add(item.DIVERSITY);
                var color = (Color)ColorConverter.ConvertFromString(DiversityColor[item.DIVERSITY]);

                /////-------Diversity Pie chartView------------//////
                LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = item.DIVERSITY, //+ " : " + item.DiversityCount,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.DiversityCount)) },
                    DataLabels = true,
                    Fill = new SolidColorBrush() { Opacity = 1, Color = color },
                    LabelPosition = PieLabelPosition.InsideSlice
                };
                Func<LiveCharts.ChartPoint, string> labelPoint = chartPoint => string.Format("{0}", DiversityAbvr[item.DIVERSITY]);
                pieSeries.LabelPoint = labelPoint;

                diversityPie.Add(pieSeries);
            }

            SeriesCollectionDiversityBar = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = diversityCount,
                    MaxColumnWidth = 20
                }
            };

            string[] divlableArr = divLableList.ToArray();

            LabelsDiversityBar = divlableArr;
            FormatterDiversityBar = value => value.ToString();

            DiversityEthnicityPie = diversityPie;

            //----------Pie Chart Legend Data-----------------//
            DiversityStdList = new ObservableCollection<StudentDiversityData>(from d in diversityData
                                                                              select new StudentDiversityData
                                                                              {
                                                                                  Diversity = d.DIVERSITY + " [" + DiversityAbvr[d.DIVERSITY] + "]",
                                                                                  StudentCount = d.DiversityCount.ToString(),
                                                                                  Colour = (Color)ColorConverter.ConvertFromString(DiversityColor[d.DIVERSITY]),
                                                                                  Progress = (int)d.DiversityCount + 3
                                                                              });
        }

        public void StudentDiversityEthnicityStackedBarRow2()
        {
            SeriesCollectionDiversityStackedBar = new SeriesCollection { };
            var result = GetStackedDEData(schoolId, subjectId);

            List<string> xList = new List<string>();
            List<List<double>> deList = new List<List<double>>();

            List<string> ColumnList = new List<string>();

            List<double> DECountList = new List<double>();

            if (result != null && result.Rows.Count > 0)
            {
                int codeCount = Convert.ToInt32(result.Rows[0]["ColumnCnt"]);

                foreach (DataColumn column in result.Columns)
                {
                    if (column.ColumnName != "DIVERSITY" && column.ColumnName != "ColumnCnt")
                    {
                        ColumnList.Add(column.ColumnName);
                    }
                }

                for (int item = 0; item < result.Rows.Count; item++)
                {
                    xList.Add(result.Rows[item]["DIVERSITY"].ToString());
                }

                for (int i = 0; i < ColumnList.Count; i++)
                {
                    DECountList = new List<double>();
                    for (int j = 0; j < result.Rows.Count; j++)
                    {
                        switch (ColumnList[i])
                        {
                            case "Australia":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Australia"].ToString()));
                                break;
                            case "Austria":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Austria"].ToString()));
                                break;
                            case "Belgium":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Belgium"].ToString()));
                                break;
                            case "Brazil":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Brazil"].ToString()));
                                break;
                            case "Canada":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Canada"].ToString()));
                                break;
                            case "Chile":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Chile"].ToString()));
                                break;
                            case "Colombia":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Colombia"].ToString()));
                                break;
                            case "CostaRica":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["CostaRica"].ToString()));
                                break;
                            case "France":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["France"].ToString()));
                                break;
                            case "Germany":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Germany"].ToString()));
                                break;
                            case "India":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["India"].ToString()));
                                break;
                            case "Indonesia":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Indonesia"].ToString()));
                                break;
                            case "Ireland":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Ireland"].ToString()));
                                break;
                            case "Italy":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Italy"].ToString()));
                                break;
                            case "Mexico":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Mexico"].ToString()));
                                break;
                            case "Netherlands":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Netherlands"].ToString()));
                                break;
                            case "NewZealand":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["NewZealand"].ToString()));
                                break;
                            case "Nigeria":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Nigeria"].ToString()));
                                break;
                            case "Pakistan":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Pakistan"].ToString()));
                                break;
                            case "Peru":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Peru"].ToString()));
                                break;
                            case "Poland":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Poland"].ToString()));
                                break;
                            case "RussianFederation":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["RussianFederation"].ToString()));
                                break;
                            case "SouthKorea":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["SouthKorea"].ToString()));
                                break;
                            case "Spain":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Spain"].ToString()));
                                break;
                            case "Sweden":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Sweden"].ToString()));
                                break;
                            case "Turkey":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["Turkey"].ToString()));
                                break;
                            case "UnitedKingdom":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["UnitedKingdom"].ToString()));
                                break;
                            case "UnitedStates":
                                DECountList.Add(Convert.ToDouble(result.Rows[j]["UnitedStates"].ToString()));
                                break;
                        }
                    }
                    deList.Add(DECountList);
                }


                for (int i = 0; i < deList.Count(); i++)
                {
                    string country = ColumnList[i];
                    var colorCode = KirinEntities.COUNTRIES.Where(x => x.Country1.Replace(" ", "") == country).Select(x => x.Color).FirstOrDefault();

                    var color = (Color)ColorConverter.ConvertFromString(colorCode);

                    StackedColumnSeries DECountdata = new StackedColumnSeries
                    {
                        Values = new ChartValues<double>(deList[i]),
                        StackMode = StackMode.Values,
                        DataLabels = false,
                        Title = ColumnList[i],
                        Fill = new SolidColorBrush() { Opacity = 1, Color = color },
                        MaxColumnWidth = 30
                    };

                    SeriesCollectionDiversityStackedBar.Add(DECountdata);
                }
            }

            string[] xAxis = xList.ToArray();
            LabelsStackedBarDiversity = xAxis;
            FormatterStackedBarDiversity = value => value.ToString("0.##");

            //////--------------Diversity/Ethnicity Stacked bar Legend------------------//

            var diversityList = KirinEntities.GetStackedDiversityData(schoolId, subjectId).ToList();

            DiversityLegend = new ObservableCollection<DiversityStackedList>(from d in diversityList
                                                                             select new DiversityStackedList
                                                                             {
                                                                                 Diversity = d.DIVERSITY,
                                                                                 ethnicityList = KirinEntities.GetEthnicityfromDiversity(schoolId, subjectId, d.DIVERSITY).
                                                                                 Select(x => new EthnicityList
                                                                                 {
                                                                                     Ethnicity = x.ETHNICITY,
                                                                                     Color = (Color)ColorConverter.ConvertFromString(x.Color),
                                                                                     EthnicityCount = x.EthnicityCount.ToString(),
                                                                                     Progress = (int)x.EthnicityCount
                                                                                 }).ToList(),
                                                                             });
        }

        public void StudentExtraCoRow2()
        {
            var extraCoData = KirinEntities.GetStudentExtraCo(subjectId, schoolId).ToList();

            SeriesCollection extraCoPie = new SeriesCollection();
            ChartValues<double> extraCoCount = new ChartValues<double>();
            List<string> extracoLableList = new List<string>();
            ObservableCollection<double> stdExtraCo = new ObservableCollection<double>();
            List<string> xlist = new List<string>();

            foreach (var item in extraCoData)
            {
                extraCoCount.Add(Convert.ToDouble(item.ExtraCoCount));
                extracoLableList.Add(item.EXTRA_CO);
                var color = (Color)ColorConverter.ConvertFromString(SportsColor[item.EXTRA_CO]);

                /////----------Citizenship Pie chartView------------//////
                LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = item.EXTRA_CO, //+ " : " + item.ExtraCoCount,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.ExtraCoCount)) },
                    DataLabels = true,
                    Fill = new SolidColorBrush() { Opacity = 1, Color = color }
                };

                Func<LiveCharts.ChartPoint, string> labelPoint = chartPoint => string.Format("{0}", item.ABBREVIATION);
                pieSeries.LabelPoint = labelPoint;

                extraCoPie.Add(pieSeries);

                stdExtraCo.Add(Convert.ToDouble(item.ExtraCoCount));

                if (!xlist.Contains(item.EXTRA_CO))
                {
                    xlist.Add(item.EXTRA_CO);
                }
                else
                {
                    xlist.Add("");
                }
            }

            SeriesCollectionSportsJoinedBar = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "",
                    Values = extraCoCount,
                    MaxColumnWidth = 20
                }
            };

            string[] extracolableArr = extracoLableList.ToArray();
            LabelsSportsBar = extracolableArr;
            FormatterSportsBar = value => value.ToString();

            //--------Pie Chart-------------
            SeriesCollectionSportsJoinedPie = extraCoPie;

            //----------Pie Chart Legend Data-----------------//
            SportsStdList = new ObservableCollection<StudentSportsData>(from s in extraCoData
                                                                        select new StudentSportsData
                                                                        {
                                                                            Sport = s.EXTRA_CO + " [" + s.ABBREVIATION + "]",
                                                                            StudentCount = s.ExtraCoCount.ToString(),
                                                                            Colour = (Color)ColorConverter.ConvertFromString(SportsColor[s.EXTRA_CO]),
                                                                            Progress = (int)s.ExtraCoCount + 3
                                                                        });

            ////-------------------Sports Line graph-------------------//
            SeriesCollectionSportsLineRow2 = new SeriesCollection
            {
                new LiveCharts.Wpf.LineSeries
                {
                    Title = "",
                    LineSmoothness = 0,
                    PointGeometrySize = 0,
                    Values = new ChartValues<double> (stdExtraCo),
                    Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color },
                }
            };

            string[] xAxisLabel = xlist.ToArray();
            LabelsLineSportsRow2 = xAxisLabel;
            YFormatterSportsRow2 = value => value.ToString("0.##");
        }

        public void StudentGenderCount()
        {
            SeriesCollection genderPie = new SeriesCollection();

            var genderData = KirinEntities.GetStudentGenderCount(subjectId, schoolId).ToList();

            foreach (var item in genderData)
            {
                var color = (Color)ColorConverter.ConvertFromString(item.Color);

                /////----------Gender Wheel chartView------------//////
                LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                {
                    Title = item.GENDER, //+ "\n" + item.CountInPercentage + "%",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.GenderCount)) },
                    DataLabels = false,
                    Fill = new SolidColorBrush() { Opacity = 1, Color = color }
                };

                int totalCount = Convert.ToInt32(item.TotalCount);
                if (item.GENDER.ToLower() == "female")
                {
                    FemaleCount = ((item.GenderCount * 100) / totalCount).ToString() + "%";
                }
                else if (item.GENDER.ToLower() == "male")
                {
                    MaleCount = ((item.GenderCount * 100) / totalCount).ToString() + "%";
                }

                genderPie.Add(pieSeries);
            }

            //-----------------Wheel Chart----------------//
            SeriesCollectionGenderPie = genderPie;
        }

        public void TransferStudentPie()
        {
            var transferStudentData = KirinEntities.GetStudentTransfferedCount(schoolId, subjectId).ToList();

            if (transferStudentData.Count() > 0)
            {
                SeriesCollection transferStdPie = new SeriesCollection();

                foreach (var item in transferStudentData)
                {
                    var color = (Color)ColorConverter.ConvertFromString(item.Color);
                    LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                    {
                        Title = item.Label, //+ " : " + item.StudentCount,
                        Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.StudentCount)) },
                        DataLabels = true,
                        Fill = new SolidColorBrush() { Opacity = 1, Color = color }
                    };
                    transferStdPie.Add(pieSeries);
                }

                //--------Pie Chart-------------//
                SeriesCollectionTransferStudentsPie = transferStdPie;

                //----------Pie Chart Legend Data-----------------//
                TransferStdListPie = new ObservableCollection<TransferStudentDataPie>(from ts in transferStudentData
                                                                                      select new TransferStudentDataPie
                                                                                      {
                                                                                          Label = ts.Label,
                                                                                          StudentCount = ts.StudentCount.ToString(),
                                                                                          Colour = (Color)ColorConverter.ConvertFromString(ts.Color),
                                                                                          Progress = Convert.ToInt32(ts.StudentCount)
                                                                                      });
            }
        }

        public void TransferStudentBar()
        {

            var transferStudentData = KirinEntities.GetTransferredStudentDataforBarGraph(schoolId, subjectId).ToList();

            if (transferStudentData.Count() > 0)
            {
                ChartValues<double> transferStdCount = new ChartValues<double>();
                List<string> transferLableList = new List<string>();

                foreach (var item in transferStudentData)
                {
                    transferStdCount.Add(Convert.ToDouble(item.StudentCount));
                    transferLableList.Add(item.MonthName);
                }

                SeriesCollectionTransferStudentsBar = new SeriesCollection
                {
                    new LiveCharts.Wpf.ColumnSeries
                    {
                        Title = "",
                        Values = transferStdCount,
                        MaxColumnWidth = 20
                    }
                };

                string[] transferlableArr = transferLableList.ToArray();
                LabelsBarTransferStudents = transferlableArr;
                FormatterBarTransferStudents = value => value.ToString();
            }
        }

        public void ClubJoinedRow2()
        {
            var clubJoinedData = KirinEntities.GetStudentClubData(schoolId, subjectId).ToList();

            if (clubJoinedData.Count() > 0)
            {
                ChartValues<double> clubJoinedCount = new ChartValues<double>();
                List<string> clubJoinedLableList = new List<string>();
                SeriesCollection clubJoinedPie = new SeriesCollection();
                List<string> xlist = new List<string>();
                ObservableCollection<double> stdClubJoined = new ObservableCollection<double>();

                foreach (var item in clubJoinedData)
                {
                    clubJoinedCount.Add(Convert.ToDouble(item.StdCount));
                    clubJoinedLableList.Add(item.ClubName);
                    var color = (Color)ColorConverter.ConvertFromString(item.COLOR);

                    LiveCharts.Wpf.PieSeries pieSeries = new LiveCharts.Wpf.PieSeries
                    {
                        Title = item.ClubName, //+ " : " + item.StdCount,
                        Values = new ChartValues<ObservableValue> { new ObservableValue(Convert.ToDouble(item.StdCount)) },
                        DataLabels = true,
                        Fill = new SolidColorBrush() { Opacity = 1, Color = color }
                    };
                    clubJoinedPie.Add(pieSeries);

                    stdClubJoined.Add(Convert.ToDouble(item.StdCount));

                    if (!xlist.Contains(item.ClubName))
                    {
                        xlist.Add(item.ClubName);
                    }
                    else
                    {
                        xlist.Add("");
                    }
                }

                SeriesCollectionClubsJoinedBar = new SeriesCollection
                {
                    new LiveCharts.Wpf.ColumnSeries
                    {
                        Title = "",
                        Values = clubJoinedCount,
                        MaxColumnWidth = 20
                    }
                };

                string[] clubJoinedlableArr = clubJoinedLableList.ToArray();
                LabelsClubsBar = clubJoinedlableArr;
                FormatterClubsBar = value => value.ToString();

                //------------Pie Chart--------------//
                SeriesCollectionClubsJoinedPie = clubJoinedPie;

                // ----------Pie Chart Legend Data---------------- -//
                ClubStdList = new ObservableCollection<ClubStudentData>(from c in clubJoinedData
                                                                        select new ClubStudentData
                                                                        {
                                                                            Club = c.ClubName + " : " + c.StdCount,
                                                                            Colour = (Color)ColorConverter.ConvertFromString(c.COLOR)
                                                                        });

                ////-------------------Sports Line graph-------------------//
                SeriesCollectionClubsLineRow2 = new SeriesCollection
                {
                    new LiveCharts.Wpf.LineSeries
                    {
                        Title = "",
                        LineSmoothness = 0,
                        PointGeometrySize = 0,
                        Values = new ChartValues<double> (stdClubJoined),
                        Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color },
                    }
                };

                string[] xAxisLabel = xlist.ToArray();
                LabelsLineClubsRow2 = xAxisLabel;
                YFormatterClubsRow2 = value => value.ToString("0.##");
            }

        }

        private ObservableCollection<Row4Legend> _Row4LegendList;

        public ObservableCollection<Row4Legend> Row4LegendList
        {
            get { return _Row4LegendList; }
            set
            {
                _Row4LegendList = value;
                OnPropertyChanged("Row4LegendList");
            }
        }

        private ObservableCollection<ScatterChartModel> _DataPoints;
        public ObservableCollection<ScatterChartModel> DataPoints
        {
            get { return _DataPoints; }
            set
            {
                _DataPoints = value;
                OnPropertyChanged("DataPoints");
            }
        }

        public void ScatterPlotSample()
        {
            var scatterPlotData = KirinEntities.GetStudentScatterPlotData(subjectId, schoolId).ToList();

            SeriesCollection ScatterPlotRow4 = new SeriesCollection();
            //DataPoints = new ObservableCollection<ScatterChartModel>();
            
            foreach (var item in scatterPlotData)
            {
                LiveCharts.Wpf.ScatterSeries scatterSeries = new LiveCharts.Wpf.ScatterSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<ObservablePoint> { new ObservablePoint(Math.Round(Convert.ToDouble(item.Grade), 2), Math.Round(Convert.ToDouble(item.Attendance), 2)) },
                    DataLabels = false,
                };

                ScatterPlotRow4.Add(scatterSeries);

                //DataPoints.Add(new ScatterChartModel() { grade = item.Grade, attendance = item.Attendance });
            }
            StudentScoreScatterSeriesCollection = ScatterPlotRow4;

            string[] xList = { "0", "15", "30", "45", "60", "70", "75", "80", "90", "100", "105" };
            LabelsScatterX = xList;

            int[] yList = { 0, 15, 30, 45, 60, 75, 80, 85, 90, 100, 105 };
            LabelsScatterY = yList.ToArray();
            
            //------------------Legend View--------------------------//
            Row4LegendList = new ObservableCollection<Row4Legend>(from s in scatterPlotData
                                                                  select new Row4Legend
                                                                  {
                                                                     Name = s.Name,
                                                                     Attendance = s.Attendance,
                                                                     Grade = s.Grade,
                                                                     Class = s.Class
                                                                  });
        }

        public static DataTable GetStackedDEData(string schoolId, string subjectId)
        {
            SqlConnection sqlCon = null;
            DataTable dt = new DataTable();

            try
            {
                using (sqlCon = new SqlConnection(Utils.GetConnectionStrings()))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("GetStackedDiversityEthnicityData", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@SchoolID", SqlDbType.NVarChar).Value = schoolId;
                    sql_cmnd.Parameters.AddWithValue("@SubjectID", SqlDbType.NVarChar).Value = subjectId;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sql_cmnd;

                    da.Fill(dt);
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (sqlCon != null)
                {
                    sqlCon.Close();
                }
            }
            return dt;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<StudentModel> Models { get; set; }

        private ObservableCollection<StudentModel> _StudentLatLong;

        public ObservableCollection<StudentModel> StudentLatLong
        {
            get { return _StudentLatLong; }
            set
            {
                _StudentLatLong = value;
                OnPropertyChanged("StudentLatLong");
            }
        }

        private ObservableCollection<StudentModel> GridStudentLatLong()
        {
            var studentData = KirinEntities.GetStudentMapData(schoolId, subjectId).ToList();

            StudentLatLong = new ObservableCollection<StudentModel>(from s in studentData
                                                                    select new StudentModel
                                                                    {
                                                                        ID = s.ID,
                                                                        FULLNAME = s.LAST_NAME + ", " + s.FIRST_NAME,
                                                                        Latitude = s.LATITUDE,
                                                                        Longitude = s.LONGITUDE
                                                                    });

            //Random rd = new Random();
            //-----------------Map Legend--------------//s
            StudentMapDataList = new ObservableCollection<StudentMapData>(from s in studentData
                                                                          select new StudentMapData
                                                                          {
                                                                              Name = s.LAST_NAME + ", " + s.FIRST_NAME + " [" + s.TransportationAbvr + "]",
                                                                              PHOTO = ByteArrayToImage(s.IMG),
                                                                              Distance = s.Distance.ToString() + " km", //rd.Next(1, 10) + " km",
                                                                              Progress = Convert.ToInt32(s.Distance), //rd.Next(1, 10),
                                                                              ID = s.ID.ToString(),
                                                                              Latitude = s.LATITUDE,
                                                                              Longitude = s.LONGITUDE
                                                                          });

            StudentMapDataList = new ObservableCollection<StudentMapData>(StudentMapDataList.OrderByDescending(d => d.Progress));
            return StudentLatLong;
        }

        public class StudentModel
        {
            public int ID { get; set; }
            public string FULLNAME { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }

        public class Country
        {
            public string NAME { get; set; }

        }
        public class MapViewModel
        {
            public ObservableCollection<Country> OceaniaList { get; set; }
            public MapViewModel()
            {
                this.OceaniaList = new ObservableCollection<Country>();
                this.OceaniaList.Add(new Country() { NAME = "London" });
                this.OceaniaList.Add(new Country() { NAME = "Queensland" });
                this.OceaniaList.Add(new Country() { NAME = "Tasmania" });
                this.OceaniaList.Add(new Country() { NAME = "Western Australia" });
            }
        }
    }

    public class Row4Legend
    {
        public string Name { get; set; }
        public string Attendance { get; set; }
        public string Grade { get; set; }
        public string StudentId { get; set; }
        public string Class { get; set; }
    }

    public class LineChartRow1
    {
        public string Assignment { get; set; }
        public string POINTS_POSSIBLE { get; set; }
        public string POINTS { get; set; }
        public string Average { get; set; }
        public string Percentage { get; set; }
        public string LabelDate { get; set; }
        public string ClassAvg { get; set; }
        public string Variation { get; set; }
        public string Xlabel { get; set; }
        public string Actual { get; set; }
        public string Expected { get; set; }
        public Visibility ScoreVisibility { get; set; }
        public Visibility AverageVisibility { get; set; }
        public SolidColorBrush GrowthColor { get; set; }
        public string ArrowType { get; set; }
        public string ScoreHeight { get; set; }
        public string AverageHeight { get; set; }
        public string AssignmentDate { get; set; }

    }

    public class EthnicityVSLanguageDS
    {
        private string _Ethnicity;

        public string Ethnicity
        {
            get { return _Ethnicity; }
            set { _Ethnicity = value; }
        }

        private string _Languages;

        public string Languages
        {
            get { return _Languages; }
            set
            {
                _Languages = value;
                OnPropertyChanged("Languages");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class BarchartValue
    {
        private string _Lang;

        public string Lang
        {
            get { return _Lang; }
            set
            {
                _Lang = value;
                OnPropertyChanged("Lang");
            }
        }


        private int _Progress;

        public int Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
                OnPropertyChanged("Progress");
            }
        }

        private int? _LanguageCount;

        public int? LanguageCount
        {
            get { return _LanguageCount; }
            set
            {
                _LanguageCount = value;
                OnPropertyChanged("LanguageCount");
            }
        }

        public bool canExecuteMethod(object param)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CitizenshipValue
    {
        private string _Country;

        public string Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                OnPropertyChanged("Country");
            }
        }


        private int _Progress;

        public int Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
                OnPropertyChanged("Progress");
            }
        }

        private int? _CountryCount;

        public int? CountryCount
        {
            get { return _CountryCount; }
            set
            {
                _CountryCount = value;
                OnPropertyChanged("CountryCount");
            }
        }

        public BitmapImage PHOTO { get; set; }

        public bool canExecuteMethod(object param)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EthnicityData
    {
        public string EthnicityCount { get; set; }
        public string Ethnicity { get; set; }
        public byte[] imgbin { get; set; }
        public BitmapImage PHOTO { get; set; }
        public int MaxCount { get; set; }
        public Color Colour { get; set; }
        public int Progress { get; set; }

    }

    public class SubjectData
    {
        public string subject { get; set; }
        public Color Colour { get; set; }
    }

    public class CourseData
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
    }

    public class StudentScore
    {
        public string SubjectId { get; set; }
        public string StudentId { get; set; }
        public string Subject { get; set; }
        public string Score { get; set; }
        public Color Colour { get; set; }
    }

    public class TransferStudentDataPie
    {
        public string StudentCount { get; set; }
        public string Label { get; set; }
        public Color Colour { get; set; }
        public int Progress { get; set; }
    }

    public class TransferStudentData
    {
        public string StudentCount { get; set; }
        public string Label { get; set; }
        public Color Colour { get; set; }
        public int Progress { get; set; }
    }

    public class DobStudentData
    {
        public Color Colour { get; set; }
        public Color Colour2 { get; set; }
        public string Count { get; set; }
        public string Count2 { get; set; }
        public string MonthName { get; set; }
        public string MonthName2 { get; set; }
        public int Progress { get; set; }
        public string Initial { get; set; }
    }

    public class DobStudentScatterData
    {
        public Color Colour { get; set; }
        public string Label { get; set; }
        public Geometry geometry { get; set; }
        public Brush Fill { get; set; }
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class StudentSportsData
    {
        public string StudentCount { get; set; }
        public string Sport { get; set; }
        public Color Colour { get; set; }
        public int Progress { get; set; }
    }

    public class StudentDiversityData
    {
        public string StudentCount { get; set; }
        public string Diversity { get; set; }
        public Color Colour { get; set; }
        public int Progress { get; set; }
    }

    public class LanguagePriority
    {
        public string languageType { get; set; }
        public Color Colour { get; set; }
    }

    public class ClubStudentData
    {
        public string StudentCount { get; set; }
        public string Club { get; set; }
        public Color Colour { get; set; }
    }

    public class CitizenshipLegend
    {
        public int SNo { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public BitmapImage PHOTO1 { get; set; }
        public BitmapImage PHOTO2 { get; set; }
        public string Count1 { get; set; }
        public string Count2 { get; set; }
        public string CITIZENSHIP1 { get; set; }
        public string CITIZENSHIP2 { get; set; }
    }

    public class StudentList
    {
        public int SNo { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string ID1 { get; set; }
        public string ID2 { get; set; }
    }

    public class EthnicityLegend
    {
        public int SNo { get; set; }
        public string Ethenicity1 { get; set; }
        public string Ethenicity2 { get; set; }
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
    }

    public class DiversityStackedList
    {
        public int SNo { get; set; }
        public string Diversity { get; set; }
        public List<EthnicityList> ethnicityList { get; set; }
    }

    public class EthnicityList
    {
        public string Ethnicity { get; set; }
        public string EthnicityCount { get; set; }
        public Color Color { get; set; }
        public int Progress { get; set; }
    }

    public class StudentMapData
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Progress { get; set; }
        public string Distance { get; set; }
        public BitmapImage PHOTO { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class DoughnutChartRow3
    {
        public string Subject { get; set; }
        public string SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Score { get; set; }
        public System.Drawing.Image Image { get; set; }
        public BitmapImage Photo { get; set; }
    }

    public class ScatterChartModel
    {
        public string attendance { get; set; }

        public string grade { get; set; }
        //public string size { get; set; }
    }

}
