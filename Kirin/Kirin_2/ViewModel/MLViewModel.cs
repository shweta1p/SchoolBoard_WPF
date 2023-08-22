using Kirin_2.Models;
using Kirin_2.UserControls;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kirin_2.ViewModel
{
    public class MLViewModel : UserControl, INotifyPropertyChanged
    {
        KIRINEntities1 KirinEntities;
        string subjectId = App.Current.Properties["SubjectId"].ToString();
        string schoolId = App.Current.Properties["SchoolId"].ToString();
               
        public MLViewModel()
        {
            KirinEntities = new KIRINEntities1();
            setSubjectColor();

        }


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

        public SeriesCollection _series3;
        public SeriesCollection SeriesCollectionLineRow3
        {
            get { return _series3; }
            set
            {
                _series3 = value;
                OnPropertyChanged("SeriesCollectionLineRow3");
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

        public void setSubjectColor()
        {
            SubjectColor = new Dictionary<int, string>();

            SubjectColor[0] = "DodgerBlue";
            SubjectColor[1] = "DarkOrange";
            SubjectColor[2] = "Gold";
            SubjectColor[3] = "DarkGray";
            SubjectColor[4] = "CadetBlue";
            SubjectColor[5] = "DarkTurquoise";
            SubjectColor[6] = "DeepPink";
            SubjectColor[7] = "OrangeRed";
            SubjectColor[8] = "MediumSlateBlue";
            SubjectColor[9] = "YellowGreen";
            SubjectColor[10] = "SeaGreen";
            SubjectColor[11] = "OliveDrab";
        }

        public void LineChartSampleRow3(List<ObservableCollection<double>> subScore, List<string> subjectName, string[] xAxis)
        {
            SeriesCollectionLineRow3 = new SeriesCollection();

            for (int i = 0; i < subScore.Count(); i++)
            {
                var color = (Color)ColorConverter.ConvertFromString(SubjectColor[i]);

                LineSeries subjects = new LineSeries
                {
                    Title = subjectName[i],
                    LineSmoothness = 0,
                    Values = new ChartValues<double>(subScore[i]),
                    PointGeometry = DefaultGeometries.Diamond,
                    PointGeometrySize = 10,
                    Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
                };

                SeriesCollectionLineRow3.Add(subjects);
            }

            LabelsLineRow3 = xAxis;
            YFormatterRow3 = value => value.ToString("0.##");

            ////------Add Subject Legend-------//
            //SubjectList = new ObservableCollection<SubjectData>();
            //for (int i = 0; i < subjectName.Count; i++)
            //{
            //    SubjectData subjectData = new SubjectData();
            //    subjectData.subject = subjectName[i];
            //    subjectData.Colour = (Color)ColorConverter.ConvertFromString(SubjectColor[i]);

            //    SubjectList.Add(subjectData);
            //}
        }


        public Func<double, string> YFormatterRow1 { get; set; }

        public LineSeries predict1;
        public LineSeries predict2;
        public LineSeries predict3;
        public LineSeries predict4;
        public LineSeries subject2;
        public LineSeries subject3;
        public LineSeries subject4;

        public void MLLineChart()
        {
            predict1 = new LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject1),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            predict2 = new LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject2),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            predict3 = new LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject3),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            predict4 = new LineSeries
            {
                Title = "Subject 1",
                LineSmoothness = 0,
                StrokeDashArray = new DoubleCollection { 2 },
                Values = new ChartValues<double>(PredictionSubject4),

                PointGeometry = DefaultGeometries.Circle,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };

            subject2 = new LineSeries
            {
                Title = "Subject 2",
                LineSmoothness = 0,
                Values = new ChartValues<double>(Subject2),
                PointGeometrySize = 0,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };
            subject3 = new LineSeries
            {
                Title = "Subject 3",
                LineSmoothness = 0,
                Values = new ChartValues<double>(Subject3),
                PointGeometrySize = 0,

                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };

            subject4 = new LineSeries
            {
                Title = "Subject 4",
                Values = new ChartValues<double>(Subject4),
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometrySize = 0,
                Fill = new SolidColorBrush() { Opacity = 0.15, Color = Brushes.Transparent.Color }
            };


            MLSandboxRow1Collection = new SeriesCollection
            {
                new LineSeries
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

}
