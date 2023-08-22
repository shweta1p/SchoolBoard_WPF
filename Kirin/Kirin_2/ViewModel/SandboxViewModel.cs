using Kirin_2.Models;
using LiveCharts;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kirin_2.ViewModel
{
    public class SandboxViewModel : INotifyPropertyChanged
    {
        KIRINEntities1 KirinEntities;
        string subjectId = App.Current.Properties["SubjectId"].ToString();
        string schoolId = App.Current.Properties["SchoolId"].ToString();
        public SandboxViewModel()
        {
            KirinEntities = new KIRINEntities1();

            ////Radar chart for Subject vs Grades
            //GetSubjectGrades();

            // Bubble Chart Diversity vs Grades
            GetGradeVSDiversityBubbleData();

            //// BoxPlot for Diversity vs Grades
            //GetGradeVSDiversityBoxPlotData();

            // Radar chart for Diversity vs Grades
            GetGradesVSDiversityRadarChartData();

            // Area Chart for Diversity vs Grades
            GetGradesVSDiversityAreaChartData();

            // Bubble Chart Citizenship vs Grades
            GetGradeVSCitizenshipBubbleData();

            // Radar chart for Citizenship vs Grades
            GetGradesVSCitizenshipRadarChartData();

            // Area Chart for Citizenship vs Grades
            GetGradesVSCitizenshipAreaChartData();

            // Bubble Chart Language vs Grades
            GetGradeVSLanguageBubbleData();

            // Radar chart for Language vs Grades
            GetGradesVSLanguageRadarChartData();

            // Area Chart for Language vs Grades
            GetGradesVSLanguageAreaChartData();

            //// Bubble Chart Attendance vs Grades
            //GetGradeVSAttendanceBubbleData();

            //// Area Chart Attendance vs Grades
            //GetGradesVSAttendanceAreaChartData();

            //// Histogram Chart Attendance vs Grades
            //GetGradeVSAttendanceHistogramData();

            //// Bubble Chart Ethnicity vs Sports
            //GetEthnicityVSSportsBubbleData();

            // Stacked Bar Ethnicity vs Sports
            GetEthnicityVsCocurricularStackedBarData();

            //// Bubble Chart Ethnicity vs Sports
            //GetEthnicityVSLanguagesBubbleData();

            // Stacked Bar Ethnicity vs Languages
            GetEthnicityVsLanguagesStackedBarData();

            //this.PlantDetails = new ObservableCollection<RadarChartModel>();
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "North", Weed = 63, Flower = 42, Tree = 80 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "NorthEast", Weed = 70, Flower = 40, Tree = 85 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "East", Weed = 45, Flower = 25, Tree = 78 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "SouthEast", Weed = 70, Flower = 40, Tree = 90 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "South", Weed = 47, Flower = 20, Tree = 78 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "SouthWest", Weed = 65, Flower = 45, Tree = 83 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "West", Weed = 58, Flower = 40, Tree = 79 });
            //this.PlantDetails.Add(new RadarChartModel() { Direction = "NorthWest", Weed = 73, Flower = 28, Tree = 88 });

            //this.BoxWhiskerData = new ObservableCollection<BoxWhiskerChartModel>();
            ////DateTime date = new DateTime(2017, 1, 1);

            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Development", Age = new List<double> { 22, 22, 23, 25, 25, 25, 26, 27, 27, 28, 28, 29, 30, 32, 34, 32, 34, 36, 35, 38 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Testing", Age = new List<double> { 22, 33, 23, 25, 26, 28, 29, 30, 34, 33, 32, 31, 50 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "HR", Age = new List<double> { 22, 24, 25, 30, 32, 34, 36, 38, 39, 41, 35, 36, 40, 56 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Finance", Age = new List<double> { 26, 27, 28, 30, 32, 34, 35, 37, 35, 37, 45 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "R&D", Age = new List<double> { 26, 27, 29, 32, 34, 35, 36, 37, 38, 39, 41, 43, 58 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Sales", Age = new List<double> { 27, 26, 28, 29, 29, 29, 32, 35, 32, 38, 53 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Inventry", Age = new List<double> { 21, 23, 24, 25, 26, 27, 28, 30, 34, 36, 38 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Graphics", Age = new List<double> { 26, 28, 29, 30, 32, 33, 35, 36, 52 } });
            //this.BoxWhiskerData.Add(new BoxWhiskerChartModel() { Department = "Training", Age = new List<double> { 28, 29, 30, 31, 32, 34, 35, 36 } });

            //BubbleData = new ObservableCollection<BubbleChartModel>
            //{
            //    new BubbleChartModel(92.2, 7.8, 1.347, "China"),
            //    new BubbleChartModel(74, 6.5, 1.241, "India"),
            //    new BubbleChartModel(90.4, 6.0, 0.238, "Indonesia"),
            //    new BubbleChartModel(99.4, 2.2, 0.312, "US"),
            //    new BubbleChartModel(88.6, 1.3, 0.197, "Brazil"),
            //    new BubbleChartModel(99, 4.7, 0.0818, "Germany"),
            //    new BubbleChartModel(72, 2.0, 0.0826, "Egypt"),
            //    new BubbleChartModel(99.6, 3.4, 0.143, "Russia"),
            //    new BubbleChartModel(99, 0.2, 0.128, "Japan"),
            //    new BubbleChartModel(86.1, 4.0, 0.115, "Mexico"),
            //    new BubbleChartModel(92.6, 4.6, 0.096, "Philippines"),
            //    new BubbleChartModel(61.3, 1.45, 0.162, "Nigeria"),
            //    new BubbleChartModel(82.2, 4.97, 0.7, "Hong Kong"),
            //    new BubbleChartModel(79.2, 3.9,0.162, "Netherland"),
            //    new BubbleChartModel(72.5, 4.5,0.7, "Jordan"),
            //    new BubbleChartModel(81, 3.5, 0.21, "Australia"),
            //    new BubbleChartModel(66.8, 3.9, 0.028, "Mongolia"),
            //    new BubbleChartModel(79.2, 2.9, 0.231, "Taiwan"),
            //};

            //ExpenditureData = new List<DoughnutChartPopulations>
            //{
            //    new DoughnutChartPopulations(){Category="Vehicle", Expenditure = 62.7, Image= new Uri(@"Logo.png",UriKind.RelativeOrAbsolute) },
            //    new DoughnutChartPopulations(){Category="Education", Expenditure = 29.5, Image= new Uri(@"Logo.png",UriKind.RelativeOrAbsolute) },
            //    new DoughnutChartPopulations(){Category="Home", Expenditure = 85.2, Image= new Uri(@"Logo.png",UriKind.RelativeOrAbsolute) },
            //    new DoughnutChartPopulations(){Category="Personal", Expenditure = 45.6, Image= new Uri(@"Logo.png",UriKind.RelativeOrAbsolute) },
            //};

            //this.Performance = new ObservableCollection<AreaChartModel>();

            //Performance.Add(new AreaChartModel() { Load = 2005, Server1 = 23, Server2 = 16, Server3 = 5 });
            //Performance.Add(new AreaChartModel() { Load = 2006, Server1 = 40, Server2 = 25, Server3 = 13 });
            //Performance.Add(new AreaChartModel() { Load = 2007, Server1 = 15, Server2 = 22, Server3 = 43 });
            //Performance.Add(new AreaChartModel() { Load = 2008, Server1 = 10, Server2 = 55, Server3 = 25 });
            //Performance.Add(new AreaChartModel() { Load = 2009, Server1 = 62, Server2 = 6, Server3 = 39 });
            //Performance.Add(new AreaChartModel() { Load = 2010, Server1 = 10, Server2 = 40, Server3 = 19 });
            //Performance.Add(new AreaChartModel() { Load = 2011, Server1 = 29, Server2 = 22, Server3 = 59 });
            //Performance.Add(new AreaChartModel() { Load = 2012, Server1 = 74, Server2 = 54, Server3 = 40 });
            //Performance.Add(new AreaChartModel() { Load = 2013, Server1 = 20, Server2 = 62, Server3 = 45 });



            //this.StockPriceDetails = new ObservableCollection<HiLoChartModel>();
            //DateTime date = new DateTime(2012, 1, 1);

            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(0), Open = 873.8, High = 878.85, Low = 855.5, Close = 860.5 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(1), Open = 873.8, High = 878.85, Low = 855.5, Close = 860.5 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(2), Open = 861, High = 868.4, Low = 835.2, Close = 843.45 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(3), Open = 846.15, High = 853, Low = 838.5, Close = 847.5 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(4), Open = 842, High = 847.75, Low = 827, Close = 832 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(5), Open = 841, High = 845, Low = 827.85, Close = 838.65 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(6), Open = 846, High = 858.5, Low = 842, Close = 847.75 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(7), Open = 865, High = 872, Low = 851, Close = 858.9 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(8), Open = 846, High = 860.75, Low = 841, Close = 855 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(9), Open = 841, High = 845, Low = 827.85, Close = 838.65 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(10), Open = 846, High = 874.5, Low = 841, Close = 860.75 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(11), Open = 878, High = 872, Low = 855, Close = 864.9 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(12), Open = 873.8, High = 878.85, Low = 855.5, Close = 860.5 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(13), Open = 873.8, High = 878.85, Low = 855.5, Close = 860.5 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(14), Open = 861, High = 868.4, Low = 835.2, Close = 843.45 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(15), Open = 846.15, High = 853, Low = 838.5, Close = 847.5 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(16), Open = 846, High = 860.75, Low = 841, Close = 855 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(17), Open = 841, High = 845, Low = 827.85, Close = 838.65 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(18), Open = 846, High = 860.75, Low = 841, Close = 855 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(18), Open = 841, High = 845, Low = 827.85, Close = 838.65 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(19), Open = 846, High = 874.5, Low = 841, Close = 860.75 });
            //this.StockPriceDetails.Add(new HiLoChartModel() { Date = date.AddDays(20), Open = 865, High = 872, Low = 865, Close = 868.9 });
        }

        private ObservableCollection<RadarChartModelforSubject> _subjectDetails;

        public ObservableCollection<RadarChartModelforSubject> subjectDetails
        {
            get { return _subjectDetails; }
            set
            {
                _subjectDetails = value;
                OnPropertyChanged("subjectDetails");
            }
        }

        private ObservableCollection<RadarChartModelforSubject> _StdSubjectList;
        public ObservableCollection<RadarChartModelforSubject> StdSubjectList
        {
            get { return _StdSubjectList; }
            set
            {
                _StdSubjectList = value;
                OnPropertyChanged(nameof(StdSubjectList));
            }
        }

        private ObservableCollection<SubjectView> _StdSubjectView;
        public ObservableCollection<SubjectView> StdSubjectView
        {
            get { return _StdSubjectView; }
            set
            {
                _StdSubjectView = value;
                OnPropertyChanged(nameof(StdSubjectView));
            }
        }


        public void GetSubjectGrades(string studentId)
        {
            var data = KirinEntities.GetGradesbySubjectforRadar(schoolId, studentId).ToList();

            subjectDetails = new ObservableCollection<RadarChartModelforSubject>();

            foreach (var item in data)
            {
                subjectDetails.Add(new RadarChartModelforSubject()
                {
                    SubjectName = item.SUBJECT,
                    Score = Convert.ToDouble(item.AverageScore)
                });
            }

            ////-------------------Legend View------------------//
            //StdSubjectList = new ObservableCollection<RadarChartModelforSubject>(from s in data
            //                                                                     select new RadarChartModelforSubject
            //                                                                     {
            //                                                                         SubjectName = s.SUBJECT,
            //                                                                         Score = Convert.ToDouble(s.AverageScore)
            //                                                                     });

            //--------------Collepsed Legend View-------------------//
            StdSubjectView = new ObservableCollection<SubjectView>(from s in data
                                                                   select new SubjectView
                                                                   {
                                                                       Id = s.ID.ToString(),
                                                                       SubjectName = s.SUBJECT,
                                                                       Score = Convert.ToDouble(s.AverageScore),
                                                                       childView = KirinEntities.GetCourseInstructorData(schoolId, studentId, s.ID.ToString()).
                                                                        Select(c => new SubjectTeacherView
                                                                        {
                                                                            CourseId = c.CourseID.ToString(),
                                                                            CourseName = c.COURSE_NAME,
                                                                            Name = c.TeacherName,
                                                                            label = c.TeacherName + " (" + c.COURSE_NAME + ")",
                                                                            subChildView = KirinEntities.GetAssignmentDatabyCourseID(schoolId, studentId, c.CourseID.ToString()).
                                                                            Select(a => new SubAssignmentView
                                                                            {
                                                                                AssignmentId = a.ASSIGNMENT_ID,
                                                                                Assignment = a.Assignment,
                                                                                AssignmentDate = a.AssignmentDate,
                                                                                PointsPossible = a.POINTS_POSSIBLE.ToString(),
                                                                                PointsEarned = a.POINTS,
                                                                                Percentage = a.Percentage + "%",
                                                                                GrowthColor = Convert.ToDouble(a.Percentage) - Convert.ToDouble(s.AverageScore) >= 0 ? Brushes.Green : Brushes.Red,
                                                                                ArrowType = Convert.ToDouble(a.Percentage) - Convert.ToDouble(s.AverageScore) >= 0 ? "ArrowUp" : "ArrowDown",
                                                                            }).ToList()
                                                                        }).ToList()
                                                                   });

        }

        private ObservableCollection<BubbleChartDiversity> _diversityDetails;

        public ObservableCollection<BubbleChartDiversity> DiversityDetails
        {
            get { return _diversityDetails; }
            set
            {
                _diversityDetails = value;
                OnPropertyChanged("DiversityDetails");
            }
        }

        private ObservableCollection<DiversityData> _DiversityStdList;
        public ObservableCollection<DiversityData> DiversityStdList
        {
            get { return _DiversityStdList; }
            set
            {
                _DiversityStdList = value;
                OnPropertyChanged(nameof(DiversityStdList));
            }
        }

        private ObservableCollection<DiversityRadarData> _DiversityRadarList;
        public ObservableCollection<DiversityRadarData> DiversityRadarList
        {
            get { return _DiversityRadarList; }
            set
            {
                _DiversityRadarList = value;
                OnPropertyChanged(nameof(DiversityRadarList));
            }
        }

        private ObservableCollection<DiversityData> _DiversityAreaList;
        public ObservableCollection<DiversityData> DiversityAreaList
        {
            get { return _DiversityAreaList; }
            set
            {
                _DiversityAreaList = value;
                OnPropertyChanged(nameof(DiversityAreaList));
            }
        }

        public void GetGradeVSDiversityBubbleData()
        {
            var data = KirinEntities.GetGradesVsDiversityBubbleChartData(schoolId, subjectId).ToList();

            DiversityDetails = new ObservableCollection<BubbleChartDiversity>();

            foreach (var item in data)
            {
                DiversityDetails.Add(new BubbleChartDiversity()
                {
                    Label = item.DIVERSITY,
                    Value = item.Grade,
                    Size = Convert.ToDouble(item.StudentCount)
                });
            }

            //----------Diversity Legend Data-----------------//
            DiversityStdList = new ObservableCollection<DiversityData>(from d in data
                                                                       select new DiversityData
                                                                       {
                                                                           Diversity = d.DIVERSITY,
                                                                           StudentCount = d.StudentCount.ToString(),
                                                                           Grade = d.Grade,
                                                                           Progress = (int)d.StudentCount + 3,
                                                                           StdList = KirinEntities.GetGradesbyDiversity(schoolId, subjectId, d.DIVERSITY).
                                                                           Select(s => new GradeStudentData
                                                                           {
                                                                               Name = s.NAME,
                                                                               Score = s.Score,
                                                                               StudentId = s.STUDENT_ID.ToString()
                                                                           }).ToList()
                                                                       });

        }

        private ObservableCollection<BoxPlotDiversity> _boxPlotDiversity;

        public ObservableCollection<BoxPlotDiversity> BoxPlotDiversity
        {
            get { return _boxPlotDiversity; }
            set
            {
                _boxPlotDiversity = value;
                OnPropertyChanged("BoxPlotDiversity");
            }
        }

        public void GetGradeVSDiversityBoxPlotData()
        {
            var data = KirinEntities.GetGradesVsDiversityBoxPlotChartData(schoolId, subjectId).ToList();

            BoxPlotDiversity = new ObservableCollection<BoxPlotDiversity>();

            if (data.Count > 0)
            {
                string diversity = string.Empty;
                int i = 0;
                List<double> scores = new List<double>();

                foreach (var item in data)
                {
                    if (i == 0)
                    {
                        diversity = item.DIVERSITY;
                    }

                    if (item.DIVERSITY == diversity)
                    {
                        scores.Add(Convert.ToDouble(item.Score));
                    }
                    else
                    {
                        BoxPlotDiversity.Add(new BoxPlotDiversity()
                        {
                            Diversity = diversity,
                            ScoreList = scores,
                            StudentID = item.STUDENT_ID.ToString()
                        });

                        diversity = item.DIVERSITY;
                        scores = new List<double>();
                        scores.Add(Convert.ToDouble(item.Score));
                    }
                    i++;
                }
            }

        }

        private ObservableCollection<RadarChartforDiversity> _diversityRadarChartDetails;

        public ObservableCollection<RadarChartforDiversity> DiversityRadarChartDetails
        {
            get { return _diversityRadarChartDetails; }
            set
            {
                _diversityRadarChartDetails = value;
                OnPropertyChanged("DiversityRadarChartDetails");
            }
        }

        public void GetGradesVSDiversityRadarChartData()
        {
            var data = KirinEntities.GetGradesVsDiversityRadarChartData(schoolId, subjectId).ToList();

            DiversityRadarChartDetails = new ObservableCollection<RadarChartforDiversity>();

            foreach (var item in data)
            {
                DiversityRadarChartDetails.Add(new RadarChartforDiversity()
                {
                    Diversity = item.DIVERSITY,
                    Grade = Convert.ToDouble(item.Grade)
                });
            }

            //----------Diversity Legend Data-----------------//
            DiversityRadarList = new ObservableCollection<DiversityRadarData>(from d in data
                                                                              select new DiversityRadarData
                                                                              {
                                                                                  Diversity = d.DIVERSITY,
                                                                                  StudentCount = d.StudentCount.ToString(),
                                                                                  Grade = d.Grade
                                                                              });

        }

        private ObservableCollection<AreaChartforDiversity> _diversityAreaChartDetails;

        public ObservableCollection<AreaChartforDiversity> DiversityAreaChartDetails
        {
            get { return _diversityAreaChartDetails; }
            set
            {
                _diversityAreaChartDetails = value;
                OnPropertyChanged("DiversityAreaChartDetails");
            }
        }

        public void GetGradesVSDiversityAreaChartData()
        {
            var data = KirinEntities.GetGradesVsDiversityAreaChartData(schoolId, subjectId).ToList();

            DiversityAreaChartDetails = new ObservableCollection<AreaChartforDiversity>();

            foreach (var item in data)
            {
                DiversityAreaChartDetails.Add(new AreaChartforDiversity()
                {
                    Diversity = item.DIVERSITY,
                    Grade = Convert.ToDouble(item.Grade)
                });
            }

            //----------Diversity Legend Data-----------------//
            DiversityAreaList = new ObservableCollection<DiversityData>(from d in data
                                                                        select new DiversityData
                                                                        {
                                                                            Diversity = d.DIVERSITY,
                                                                            StudentCount = d.StudentCount.ToString(),
                                                                            Grade = d.Grade
                                                                        });
        }

        private ObservableCollection<CitizenshipDetails> _citizenshipBubbleChart;

        public ObservableCollection<CitizenshipDetails> CitizenshipBubbleChart
        {
            get { return _citizenshipBubbleChart; }
            set
            {
                _citizenshipBubbleChart = value;
                OnPropertyChanged("CitizenshipBubbleChart");
            }
        }

        private ObservableCollection<CitizenshipBubbleData> _CitizenshipBubbleList;
        public ObservableCollection<CitizenshipBubbleData> CitizenshipBubbleList
        {
            get { return _CitizenshipBubbleList; }
            set
            {
                _CitizenshipBubbleList = value;
                OnPropertyChanged(nameof(CitizenshipBubbleList));
            }
        }

        public void GetGradeVSCitizenshipBubbleData()
        {
            var data = KirinEntities.GetGradesVsCitizenshipBubbleChartData(schoolId, subjectId).ToList();

            CitizenshipBubbleChart = new ObservableCollection<CitizenshipDetails>();

            foreach (var item in data)
            {
                if (item.StudentCount != 0)
                {
                    CitizenshipBubbleChart.Add(new CitizenshipDetails()
                    {
                        StudentCount = Convert.ToInt32(item.StudentCount),
                        Citizenship = item.CITIZENSHIP,
                        Grade = item.Grade
                    });
                }
            }

            //----------Diversity Legend Data-----------------//
            CitizenshipBubbleList = new ObservableCollection<CitizenshipBubbleData>(from d in data
                                                                                    where d.StudentCount != 0
                                                                                    select new CitizenshipBubbleData
                                                                                    {
                                                                                        Citizenship = d.CITIZENSHIP,
                                                                                        StudentCount = d.StudentCount.ToString(),
                                                                                        Grade = d.Grade,
                                                                                        Progress = (int)d.StudentCount + 3,
                                                                                        PHOTO = ByteArrayToImage(d.Image),
                                                                                        StdList = KirinEntities.GetGradesbyCitizenship(schoolId, subjectId, d.CITIZENSHIP).
                                                                                             Select(s => new GradeStudentData
                                                                                             {
                                                                                                 Name = s.NAME,
                                                                                                 Score = s.Score,
                                                                                                 StudentId = s.STUDENT_ID.ToString()
                                                                                             }).ToList()
                                                                                    });
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


        private ObservableCollection<CitizenshipDetails> _citizenshipRadarChartDetails;

        public ObservableCollection<CitizenshipDetails> CitizenshipRadarChartDetails
        {
            get { return _citizenshipRadarChartDetails; }
            set
            {
                _citizenshipRadarChartDetails = value;
                OnPropertyChanged("CitizenshipRadarChartDetails");
            }
        }

        private ObservableCollection<CitizenshipData> _CitizenshipRadarList;
        public ObservableCollection<CitizenshipData> CitizenshipRadarList
        {
            get { return _CitizenshipRadarList; }
            set
            {
                _CitizenshipRadarList = value;
                OnPropertyChanged(nameof(CitizenshipRadarList));
            }
        }

        public void GetGradesVSCitizenshipRadarChartData()
        {
            var data = KirinEntities.GetGradesVsCitizenshipRadarChartData(schoolId, subjectId).ToList();

            CitizenshipRadarChartDetails = new ObservableCollection<CitizenshipDetails>();

            foreach (var item in data)
            {
                if (item.StudentCount != 0)
                {
                    CitizenshipRadarChartDetails.Add(new CitizenshipDetails()
                    {
                        StudentCount = Convert.ToInt32(item.StudentCount),
                        Citizenship = item.CITIZENSHIP,
                        Grade = item.Grade
                    });
                }
            }

            //----------Diversity Legend Data-----------------//
            CitizenshipRadarList = new ObservableCollection<CitizenshipData>(from d in data
                                                                             where d.StudentCount != 0
                                                                             select new CitizenshipData
                                                                             {
                                                                                 Citizenship = d.CITIZENSHIP,
                                                                                 StudentCount = d.StudentCount.ToString(),
                                                                                 Grade = d.Grade,
                                                                                 //Progress = (int)d.StudentCount + 3,
                                                                                 PHOTO = ByteArrayToImage(d.Image)
                                                                             });
        }

        private ObservableCollection<CitizenshipDetails> _citizenshipAreaChartDetails;

        public ObservableCollection<CitizenshipDetails> CitizenshipAreaChartDetails
        {
            get { return _citizenshipAreaChartDetails; }
            set
            {
                _citizenshipAreaChartDetails = value;
                OnPropertyChanged("CitizenshipAreaChartDetails");
            }
        }

        private ObservableCollection<CitizenshipData> _CitizenshipAreaList;
        public ObservableCollection<CitizenshipData> CitizenshipAreaList
        {
            get { return _CitizenshipAreaList; }
            set
            {
                _CitizenshipAreaList = value;
                OnPropertyChanged(nameof(CitizenshipAreaList));
            }
        }

        public void GetGradesVSCitizenshipAreaChartData()
        {
            var data = KirinEntities.GetGradesVsCitizenshipAreaChartData(schoolId, subjectId).ToList();

            CitizenshipAreaChartDetails = new ObservableCollection<CitizenshipDetails>();

            foreach (var item in data)
            {
                if (item.StudentCount != 0)
                {
                    CitizenshipAreaChartDetails.Add(new CitizenshipDetails()
                    {
                        StudentCount = Convert.ToInt32(item.StudentCount),
                        Citizenship = item.CITIZENSHIP,
                        Grade = item.Grade
                    });
                }
            }

            //----------Diversity Legend Data-----------------//
            CitizenshipAreaList = new ObservableCollection<CitizenshipData>(from d in data
                                                                            where d.StudentCount != 0
                                                                            select new CitizenshipData
                                                                            {
                                                                                Citizenship = d.CITIZENSHIP,
                                                                                StudentCount = d.StudentCount.ToString(),
                                                                                Grade = d.Grade,
                                                                                //Progress = (int)d.StudentCount + 3,
                                                                                PHOTO = ByteArrayToImage(d.Image)
                                                                            });
        }

        private ObservableCollection<LanguageDetails> _languageBubbleChart;

        public ObservableCollection<LanguageDetails> LanguageBubbleChartDetails
        {
            get { return _languageBubbleChart; }
            set
            {
                _languageBubbleChart = value;
                OnPropertyChanged("LanguageBubbleChartDetails");
            }
        }

        private ObservableCollection<LanguageBubbleData> _LanguageBubbleList;
        public ObservableCollection<LanguageBubbleData> LanguageBubbleList
        {
            get { return _LanguageBubbleList; }
            set
            {
                _LanguageBubbleList = value;
                OnPropertyChanged(nameof(LanguageBubbleList));
            }
        }

        public void GetGradeVSLanguageBubbleData()
        {
            var data = KirinEntities.GetGradesVsLanguagesChartData(schoolId, subjectId).ToList();

            LanguageBubbleChartDetails = new ObservableCollection<LanguageDetails>();

            foreach (var item in data)
            {
                if (item.StudentCount != 0)
                {
                    LanguageBubbleChartDetails.Add(new LanguageDetails()
                    {
                        StudentCount = Convert.ToInt32(item.StudentCount),
                        Language = item.LANGUAGE,
                        Grade = item.Grade
                    });
                }
            }

            //----------Diversity Legend Data-----------------//
            LanguageBubbleList = new ObservableCollection<LanguageBubbleData>(from d in data
                                                                              where d.StudentCount != 0
                                                                              select new LanguageBubbleData
                                                                              {
                                                                                  Language = d.LANGUAGE,
                                                                                  StudentCount = d.StudentCount.ToString(),
                                                                                  Progress = (int)d.StudentCount + 3,
                                                                                  Grade = d.Grade,
                                                                                  LangId = d.ID.ToString(),
                                                                                  StdList = KirinEntities.GetGradesbyLanguage(schoolId, subjectId, d.ID.ToString()).
                                                                                                   Select(s => new GradeStudentData
                                                                                                   {
                                                                                                       Name = s.NAME,
                                                                                                       Score = s.Score,
                                                                                                       StudentId = s.STUDENT_ID.ToString()
                                                                                                   }).ToList()
                                                                              });
        }

        private ObservableCollection<LanguageDetails> _languageRadarChartDetails;

        public ObservableCollection<LanguageDetails> LanguageRadarChartDetails
        {
            get { return _languageRadarChartDetails; }
            set
            {
                _languageRadarChartDetails = value;
                OnPropertyChanged("CitizenshipRadarChartDetails");
            }
        }

        private ObservableCollection<LanguageData> _LanguageRadarList;
        public ObservableCollection<LanguageData> LanguageRadarList
        {
            get { return _LanguageRadarList; }
            set
            {
                _LanguageRadarList = value;
                OnPropertyChanged(nameof(LanguageRadarList));
            }
        }

        public void GetGradesVSLanguageRadarChartData()
        {
            var data = KirinEntities.GetGradesVsLanguagesChartData(schoolId, subjectId).ToList();

            LanguageRadarChartDetails = new ObservableCollection<LanguageDetails>();

            foreach (var item in data)
            {
                if (item.StudentCount != 0)
                {
                    LanguageRadarChartDetails.Add(new LanguageDetails()
                    {
                        StudentCount = Convert.ToInt32(item.StudentCount),
                        Language = item.LANGUAGE,
                        Grade = item.Grade
                    });
                }
            }

            //----------Diversity Legend Data-----------------//
            LanguageRadarList = new ObservableCollection<LanguageData>(from d in data
                                                                       where d.StudentCount != 0
                                                                       select new LanguageData
                                                                       {
                                                                           Language = d.LANGUAGE,
                                                                           StudentCount = d.StudentCount.ToString(),
                                                                           Grade = d.Grade
                                                                       });

        }

        private ObservableCollection<LanguageDetails> _languageAreaChartDetails;

        public ObservableCollection<LanguageDetails> LanguageAreaChartDetails
        {
            get { return _languageAreaChartDetails; }
            set
            {
                _languageAreaChartDetails = value;
                OnPropertyChanged("LanguageAreaChartDetails");
            }
        }

        private ObservableCollection<LanguageData> _LanguageAreaList;
        public ObservableCollection<LanguageData> LanguageAreaList
        {
            get { return _LanguageAreaList; }
            set
            {
                _LanguageAreaList = value;
                OnPropertyChanged(nameof(LanguageAreaList));
            }
        }

        public void GetGradesVSLanguageAreaChartData()
        {
            var data = KirinEntities.GetGradesVsLanguagesChartData(schoolId, subjectId).ToList();

            LanguageAreaChartDetails = new ObservableCollection<LanguageDetails>();

            foreach (var item in data)
            {
                if (item.StudentCount != 0)
                {
                    LanguageAreaChartDetails.Add(new LanguageDetails()
                    {
                        StudentCount = Convert.ToInt32(item.StudentCount),
                        Language = item.LANGUAGE,
                        Grade = item.Grade
                    });
                }
            }

            //----------Diversity Legend Data-----------------//
            LanguageAreaList = new ObservableCollection<LanguageData>(from d in data
                                                                      where d.StudentCount != 0
                                                                      select new LanguageData
                                                                      {
                                                                          Language = d.LANGUAGE,
                                                                          StudentCount = d.StudentCount.ToString(),
                                                                          Grade = d.Grade
                                                                      });
        }

        private ObservableCollection<AttendanceDetails> _attendanceAreaChartDetails;

        public ObservableCollection<AttendanceDetails> AttendanceAreaChartDetails
        {
            get { return _attendanceAreaChartDetails; }
            set
            {
                _attendanceAreaChartDetails = value;
                OnPropertyChanged("AttendanceAreaChartDetails");
            }
        }

        public void GetGradesVSAttendanceAreaChartData()
        {
            var data = KirinEntities.GetGradesVsAttendanceData(subjectId, schoolId).ToList();

            AttendanceAreaChartDetails = new ObservableCollection<AttendanceDetails>();

            foreach (var item in data)
            {
                AttendanceAreaChartDetails.Add(new AttendanceDetails()
                {
                    Attandance = item.Attendance,
                    Grade = item.Grade
                });
            }
        }

        private ObservableCollection<AttendanceDetails> _attendanceBubbleChart;

        public ObservableCollection<AttendanceDetails> AttendanceBubbleChartDetails
        {
            get { return _attendanceBubbleChart; }
            set
            {
                _attendanceBubbleChart = value;
                OnPropertyChanged("AttendanceBubbleChartDetails");
            }
        }

        public void GetGradeVSAttendanceBubbleData()
        {
            var data = KirinEntities.GetGradesVsAttendanceData(schoolId, subjectId).ToList();

            AttendanceBubbleChartDetails = new ObservableCollection<AttendanceDetails>();

            Random rd = new Random();
            foreach (var item in data)
            {
                AttendanceBubbleChartDetails.Add(new AttendanceDetails()
                {
                    StudentCount = rd.Next(1, 10),
                    Attandance = item.Attendance,
                    Grade = item.Grade
                });
            }
        }

        private ObservableCollection<AttendanceHistogramDetails> _attendanceHistogram;

        public ObservableCollection<AttendanceHistogramDetails> AttendanceHistogramDetails
        {
            get { return _attendanceHistogram; }
            set
            {
                _attendanceHistogram = value;
                OnPropertyChanged("AttendanceHistogramDetails");
            }
        }

        public void GetGradeVSAttendanceHistogramData()
        {
            var data = KirinEntities.GetGradesVsAttendanceHistogramData(schoolId, subjectId).ToList();

            AttendanceHistogramDetails = new ObservableCollection<AttendanceHistogramDetails>();

            foreach (var item in data)
            {
                AttendanceHistogramDetails.Add(new AttendanceHistogramDetails()
                {
                    Attandance = Convert.ToDouble(item.Attendance),
                    Grade = Convert.ToDouble(item.Grade)
                });
            }
        }

        private ObservableCollection<SportsDetails> _sportsBubbleChart;

        public ObservableCollection<SportsDetails> SportsBubbleChartDetails
        {
            get { return _sportsBubbleChart; }
            set
            {
                _sportsBubbleChart = value;
                OnPropertyChanged("SportsBubbleChartDetails");
            }
        }

        public void GetEthnicityVSSportsBubbleData()
        {
            var data = KirinEntities.GetEthnicityVsCocurricularBubbleChartData(schoolId, subjectId).ToList();

            SportsBubbleChartDetails = new ObservableCollection<SportsDetails>();

            foreach (var item in data)
            {
                SportsBubbleChartDetails.Add(new SportsDetails()
                {
                    StudentCount = Convert.ToInt32(item.StudentCount),
                    Ethnicity = item.ETHNICITY,
                    ExtraCo = item.EXTRA_CO,
                    ExtraCoId = Convert.ToInt32(item.ExtraCoID)
                });
            }
        }

        public static DataTable GetStackedEthnicityVsSportsData(string schoolId, string subjectId)
        {
            SqlConnection sqlCon = null;
            DataTable dt = new DataTable();

            try
            {
                using (sqlCon = new SqlConnection(Utils.GetConnectionStrings()))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("GetStackedEthnicityVsSportsData", sqlCon);
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

        public SeriesCollection SeriesCollectionSportsStackedBar { get; set; }
        public string[] LabelsStackedBarEthnicity { get; set; }
        public Func<double, string> FormatterStackedBarEthnicity { get; set; }

        private ObservableCollection<EthnicityStackedList> _ExtraCoLegend;
        public ObservableCollection<EthnicityStackedList> ExtraCoLegend
        {
            get { return _ExtraCoLegend; }
            set
            {
                _ExtraCoLegend = value;
                OnPropertyChanged(nameof(ExtraCoLegend));
            }
        }

        public void GetEthnicityVsCocurricularStackedBarData()
        {
            SeriesCollectionSportsStackedBar = new SeriesCollection { };
            var result = GetStackedEthnicityVsSportsData(schoolId, subjectId);

            List<string> xList = new List<string>();
            List<List<double>> esList = new List<List<double>>();

            List<string> ColumnList = new List<string>();

            List<double> ESCountList = new List<double>();

            if (result != null && result.Rows.Count > 0)
            {
                int codeCount = Convert.ToInt32(result.Rows[0]["ColumnCnt"]);

                foreach (DataColumn column in result.Columns)
                {
                    if (column.ColumnName != "Ethnicity" && column.ColumnName != "ColumnCnt")
                    {
                        ColumnList.Add(column.ColumnName);
                    }
                }

                for (int item = 0; item < result.Rows.Count; item++)
                {
                    xList.Add(result.Rows[item]["Ethnicity"].ToString());
                }

                for (int i = 0; i < ColumnList.Count; i++)
                {
                    ESCountList = new List<double>();
                    for (int j = 0; j < result.Rows.Count; j++)
                    {
                        switch (ColumnList[i])
                        {
                            case "BALLET":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["BALLET"].ToString()));
                                break;
                            case "BASKETBALL":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["BASKETBALL"].ToString()));
                                break;
                            case "DIVING":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["DIVING"].ToString()));
                                break;
                            case "HIPHOP":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["HIPHOP"].ToString()));
                                break;
                            case "SINGING":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["SINGING"].ToString()));
                                break;
                            case "SWIMMING":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["SWIMMING"].ToString()));
                                break;
                            case "THEATRE":
                                ESCountList.Add(Convert.ToDouble(result.Rows[j]["THEATRE"].ToString()));
                                break;
                        }
                    }
                    esList.Add(ESCountList);
                }


                for (int i = 0; i < esList.Count(); i++)
                {
                    string country = ColumnList[i];
                    //var colorCode = KirinEntities.COUNTRIES.Where(x => x.Country1.Replace(" ", "") == country).Select(x => x.Color).FirstOrDefault();

                    //var color = (Color)ColorConverter.ConvertFromString(colorCode);

                    StackedColumnSeries ECountdata = new StackedColumnSeries
                    {
                        Values = new ChartValues<double>(esList[i]),
                        StackMode = StackMode.Values,
                        DataLabels = false,
                        Title = ColumnList[i],
                        //Fill = new SolidColorBrush() { Opacity = 1, Color = color },
                        MaxColumnWidth = 30
                    };

                    SeriesCollectionSportsStackedBar.Add(ECountdata);
                }
            }

            string[] xAxis = xList.ToArray();
            LabelsStackedBarEthnicity = xAxis;
            FormatterStackedBarEthnicity = value => value.ToString("0.##");

            //------------------Ethnicity/Sports Stacked bar Legend------------------//
            var ethnicityList = KirinEntities.GetStackedEthnicityData(schoolId, subjectId).ToList();

            ExtraCoLegend = new ObservableCollection<EthnicityStackedList>(from d in ethnicityList
                                                                           select new EthnicityStackedList
                                                                           {
                                                                               Ethnicity = d.ETHNICITY,
                                                                               extraCoList = KirinEntities.GetSportsfromEthnicity(schoolId, subjectId, d.ETHNICITY).
                                                                                 Select(x => new ExtraCoList
                                                                                 {
                                                                                     ExtraCo = x.EXTRA_CO,
                                                                                     //Color = (Color)ColorConverter.ConvertFromString(x.Color),
                                                                                     SportsCount = x.SportsCount.ToString(),
                                                                                     Progress = (int)x.SportsCount + 3,
                                                                                     Ethnicity = x.ETHNICITY
                                                                                 }).ToList(),
                                                                           });
        }

        private ObservableCollection<LanguagesDetails> _languagesBubbleChart;

        public ObservableCollection<LanguagesDetails> LanguagesBubbleChartDetails
        {
            get { return _languagesBubbleChart; }
            set
            {
                _languagesBubbleChart = value;
                OnPropertyChanged("LanguagesBubbleChartDetails");
            }
        }

        public void GetEthnicityVSLanguagesBubbleData()
        {
            var data = KirinEntities.GetEthnicityVsLanguagesBubbleChartData(schoolId, subjectId).ToList();

            LanguagesBubbleChartDetails = new ObservableCollection<LanguagesDetails>();

            foreach (var item in data)
            {
                LanguagesBubbleChartDetails.Add(new LanguagesDetails()
                {
                    StudentCount = Convert.ToInt32(item.StudentCount),
                    Ethnicity = item.ETHNICITY,
                    Language = item.LANGUAGE,
                    LangId = Convert.ToInt32(item.langID)
                });
            }
        }

        public static DataTable GetStackedEthnicityVsLanguageData(string schoolId, string subjectId)
        {
            SqlConnection sqlCon = null;
            DataTable dt = new DataTable();

            try
            {
                using (sqlCon = new SqlConnection(Utils.GetConnectionStrings()))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("GetStackedEthnicityVsLanguagesData", sqlCon);
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

        public SeriesCollection SeriesCollectionLanguageStackedBar { get; set; }
        public string[] LabelsStackedBarLanguage { get; set; }
        public Func<double, string> FormatterStackedBarLanguage { get; set; }

        private ObservableCollection<EthnicityLangList> _LanguageLegend;
        public ObservableCollection<EthnicityLangList> LanguageLegend
        {
            get { return _LanguageLegend; }
            set
            {
                _LanguageLegend = value;
                OnPropertyChanged(nameof(LanguageLegend));
            }
        }

        public void GetEthnicityVsLanguagesStackedBarData()
        {
            SeriesCollectionLanguageStackedBar = new SeriesCollection { };
            var result = GetStackedEthnicityVsLanguageData(schoolId, subjectId);

            List<string> xList = new List<string>();
            List<List<double>> elList = new List<List<double>>();

            List<string> ColumnList = new List<string>();

            List<double> ELCountList = new List<double>();

            if (result != null && result.Rows.Count > 0)
            {
                int codeCount = Convert.ToInt32(result.Rows[0]["ColumnCnt"]);

                foreach (DataColumn column in result.Columns)
                {
                    if (column.ColumnName != "Ethnicity" && column.ColumnName != "ColumnCnt")
                    {
                        ColumnList.Add(column.ColumnName);
                    }
                }

                for (int item = 0; item < result.Rows.Count; item++)
                {
                    xList.Add(result.Rows[item]["Ethnicity"].ToString());
                }

                for (int i = 0; i < ColumnList.Count; i++)
                {
                    ELCountList = new List<double>();
                    for (int j = 0; j < result.Rows.Count; j++)
                    {
                        switch (ColumnList[i])
                        {
                            case "English":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["English"].ToString()));
                                break;
                            case "French":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["French"].ToString()));
                                break;
                            case "German":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["German"].ToString()));
                                break;
                            case "Hindi":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Hindi"].ToString()));
                                break;
                            case "Indonesian":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Indonesian"].ToString()));
                                break;
                            case "Italian":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Italian"].ToString()));
                                break;
                            case "Korean":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Korean"].ToString()));
                                break;
                            case "Mandarin":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Mandarin"].ToString()));
                                break;
                            case "Polish":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Polish"].ToString()));
                                break;
                            case "Portuguese":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Portuguese"].ToString()));
                                break;
                            case "Russian":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Russian"].ToString()));
                                break;
                            case "Spanish":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Spanish"].ToString()));
                                break;
                            case "Swedish":
                                ELCountList.Add(Convert.ToDouble(result.Rows[j]["Swedish"].ToString()));
                                break;
                        }
                    }
                    elList.Add(ELCountList);
                }


                for (int i = 0; i < elList.Count(); i++)
                {
                    string country = ColumnList[i];
                    //var colorCode = KirinEntities.COUNTRIES.Where(x => x.Country1.Replace(" ", "") == country).Select(x => x.Color).FirstOrDefault();

                    //var color = (Color)ColorConverter.ConvertFromString(colorCode);

                    StackedColumnSeries DECountdata = new StackedColumnSeries
                    {
                        Values = new ChartValues<double>(elList[i]),
                        StackMode = StackMode.Values,
                        DataLabels = false,
                        Title = ColumnList[i],
                        //Fill = new SolidColorBrush() { Opacity = 1, Color = color },
                        MaxColumnWidth = 30
                    };

                    SeriesCollectionLanguageStackedBar.Add(DECountdata);
                }
            }

            string[] xAxis = xList.ToArray();
            LabelsStackedBarLanguage = xAxis;
            FormatterStackedBarLanguage = value => value.ToString("0.##");

            //------------------Ethnicity/Language Stacked bar Legend------------------//
            var ethnicityList = KirinEntities.GetStackedEthnicityData(schoolId, subjectId).ToList();

            LanguageLegend = new ObservableCollection<EthnicityLangList>(from d in ethnicityList
                                                                         select new EthnicityLangList
                                                                         {
                                                                             Ethnicity = d.ETHNICITY,
                                                                             LangList = KirinEntities.GetLanguagefromEthnicity(schoolId, subjectId, d.ETHNICITY).
                                                                               Select(x => new LanguageList
                                                                               {
                                                                                   Language = x.LANGUAGE,
                                                                                   //Color = (Color)ColorConverter.ConvertFromString(x.Color),
                                                                                   LangCount = x.LangCount.ToString(),
                                                                                   Progress = (int)x.LangCount + 3,
                                                                                   Ethnicity = x.ETHNICITY
                                                                               }).ToList(),
                                                                         });
        }

        public ObservableCollection<RadarChartModel> PlantDetails { get; set; }

        public ObservableCollection<BoxWhiskerChartModel> BoxWhiskerData { get; set; }


        public ObservableCollection<BubbleChartModel> BubbleData { get; set; }

        public IList<DoughnutChartPopulations> ExpenditureData
        {
            get; set;
        }
        public ObservableCollection<AreaChartModel> Performance
        {
            get; set;
        }
        public ObservableCollection<HiLoChartModel> StockPriceDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RadarChartModelforSubject
    {
        public string SubjectName { get; set; }
        public double Score { get; set; }
        public string SubjectCode { get; set; }
    }

    public class BubbleChartDiversity
    {
        public string Label { get; set; }

        public string Value { get; set; }

        public double Size { get; set; }
    }

    public class DiversityRadarData
    {
        public string StudentCount { get; set; }
        public string Diversity { get; set; }
        public int Progress { get; set; }
        public string Grade { get; set; }
    }

    public class DiversityData
    {
        public string StudentCount { get; set; }
        public string Diversity { get; set; }
        public int Progress { get; set; }
        public string Grade { get; set; }

        public List<GradeStudentData> StdList { get; set; }
    }

    public class GradeStudentData
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Score { get; set; }
    }

    public class CitizenshipBubbleData
    {
        public string StudentCount { get; set; }
        public string Citizenship { get; set; }
        public int Progress { get; set; }
        public string Grade { get; set; }
        public BitmapImage PHOTO { get; set; }
        public List<GradeStudentData> StdList { get; set; }
    }

    public class CitizenshipData
    {
        public string StudentCount { get; set; }
        public string Citizenship { get; set; }
        public int Progress { get; set; }
        public string Grade { get; set; }
        public BitmapImage PHOTO { get; set; }
    }

    public class LanguageBubbleData
    {
        public string StudentCount { get; set; }
        public string Language { get; set; }
        public int Progress { get; set; }
        public string Grade { get; set; }
        public string LangId { get; set; }
        public List<GradeStudentData> StdList { get; set; }
    }

    public class LanguageData
    {
        public string StudentCount { get; set; }
        public string Language { get; set; }
        public int Progress { get; set; }
        public string Grade { get; set; }
    }

    public class BoxPlotDiversity
    {
        public string StudentID { get; set; }
        public string Diversity { get; set; }
        public List<double> ScoreList { get; set; }
    }

    public class RadarChartforDiversity
    {
        public string Diversity { get; set; }
        public double Grade { get; set; }
    }

    public class AreaChartforDiversity
    {
        public string Diversity { get; set; }
        public double Grade { get; set; }
    }

    public class CitizenshipDetails
    {
        public string Citizenship { get; set; }

        public string Grade { get; set; }

        public int StudentCount { get; set; }
    }

    public class LanguageDetails
    {
        public string Language { get; set; }

        public string Grade { get; set; }

        public int StudentCount { get; set; }
    }

    public class AttendanceDetails
    {
        public string Attandance { get; set; }

        public string Grade { get; set; }

        public string Name { get; set; }

        public int StudentCount { get; set; }
    }

    public class SportsDetails
    {
        public string Ethnicity { get; set; }

        public string ExtraCo { get; set; }

        public int ExtraCoId { get; set; }

        public int StudentCount { get; set; }
    }

    public class LanguagesDetails
    {
        public string Ethnicity { get; set; }

        public string Language { get; set; }

        public int LangId { get; set; }

        public int StudentCount { get; set; }
    }

    public class EthnicityStackedList
    {
        public int SNo { get; set; }
        public string Ethnicity { get; set; }
        public List<ExtraCoList> extraCoList { get; set; }
    }

    public class ExtraCoList
    {
        public string ExtraCo { get; set; }
        public string SportsCount { get; set; }
        public Color Color { get; set; }
        public int Progress { get; set; }
        public string Ethnicity { get; set; }
    }

    public class EthnicityLangList
    {
        public int SNo { get; set; }
        public string Ethnicity { get; set; }
        public List<LanguageList> LangList { get; set; }
    }

    public class LanguageList
    {
        public string Language { get; set; }
        public string LangCount { get; set; }
        public string Ethnicity { get; set; }
        public Color Color { get; set; }
        public int Progress { get; set; }
    }

    public class AttendanceHistogramDetails
    {
        public double Attandance { get; set; }

        public double Grade { get; set; }
    }

    public class RadarChartModel
    {
        public string Direction { get; set; }
        public double Weed { get; set; }
        public double Flower { get; set; }
        public double Tree { get; set; }
    }

    public class BoxWhiskerChartModel
    {
        public string Department { get; set; }
        public List<double> Age { get; set; }
    }

    public class BubbleChartModel
    {
        public string Label { get; set; }

        public double Value { get; set; }

        public double Value1 { get; set; }

        public double Size { get; set; }

        public BubbleChartModel(double value1, double value, double size, string label)
        {
            Value1 = value1;
            Value = value;
            Size = size;
            Label = label;
        }
    }

    public class DoughnutChartPopulations
    {
        public string Category { get; set; }

        public double Expenditure { get; set; }

        public Uri Image { get; set; }
    }

    public class AreaChartModel
    {
        public double Server1
        {
            get;
            set;
        }

        public double Server2
        {
            get;
            set;
        }

        public double Server3
        {
            get;
            set;
        }

        public double Load
        {
            get;
            set;
        }
    }

    public class HiLoChartModel
    {
        public DateTime Date { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
    }

    public class SubjectView
    {
        public string Id { get; set; }
        public string SubjectName { get; set; }
        public double Score { get; set; }
        public List<SubjectTeacherView> childView { get; set; }
    }

    public class SubjectTeacherView
    {
        public int Id { get; set; }
        public string label { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseId { get; set; }
        public List<SubAssignmentView> subChildView { get; set; }
    }

    public class SubAssignmentView
    {
        public int AssignmentId { get; set; }
        public string Assignment { get; set; }
        public string PointsPossible { get; set; }
        public string PointsEarned { get; set; }
        public string AssignmentDate { get; set; }
        public string Percentage { get; set; }
        public SolidColorBrush GrowthColor { get; set; }
        public string ArrowType { get; set; }
    }
}

