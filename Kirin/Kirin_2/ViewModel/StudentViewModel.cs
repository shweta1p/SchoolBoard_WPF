using Kirin_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kirin_2.ViewModel
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        KIRINEntities1 kirinEntities;
        public string subjectId = string.Empty;
        public string schoolId = string.Empty;
        public string selectedLabel = string.Empty;

        public StudentViewModel()
        {
            kirinEntities = new KIRINEntities1();
        }

        public void GetStudentList(string schoolId, string subjectId, string label)
        {

            var studentData = kirinEntities.GetPieTransfferedStudentData(schoolId, subjectId, label).ToList();

            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = s.ID,
                                                                        Name = s.Name
                                                                    });
            }
        }

        public string _transferType;
        public string TransferType
        {
            get { return _transferType; }
            set
            {
                _transferType = value;
                OnPropertyChanged("TransferType");
            }
        }

        public string _ethnicity;
        public string Ethnicity
        {
            get { return _ethnicity; }
            set
            {
                _ethnicity = value;
                OnPropertyChanged("Ethnicity");
            }
        }

        public string _extraCo;
        public string ExtraCo
        {
            get { return _extraCo; }
            set
            {
                _extraCo = value;
                OnPropertyChanged("ExtraCo");
            }
        }

        public string _citizenship;
        public string Citizenship
        {
            get { return _citizenship; }
            set
            {
                _citizenship = value;
                OnPropertyChanged("Citizenship");
            }
        }

        public string _sports;
        public string Sports
        {
            get { return _sports; }
            set
            {
                _sports = value;
                OnPropertyChanged("Sports");
            }
        }

        public string _transferMonth;
        public string TransferMonth
        {
            get { return _transferMonth; }
            set
            {
                _transferMonth = value;
                OnPropertyChanged("TransferMonth");
            }
        }

        public string _clubName;
        public string ClubName
        {
            get { return _clubName; }
            set
            {
                _clubName = value;
                OnPropertyChanged("ClubName");
            }
        }

        public string _diversity;
        public string Diversity
        {
            get { return _diversity; }
            set
            {
                _diversity = value;
                OnPropertyChanged("Diversity");
            }
        }

        public string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged("Language");
            }
        }


        public void GetStudentCitizenshipList(string schoolId, string subjectId, string citizenship)
        {
            var studentData = kirinEntities.GetStudentDatabyCitizenship(subjectId, schoolId, citizenship).ToList();
            Citizenship = citizenship;
            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = s.ID,
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetTransferStudentData(string schoolId, string subjectId, string month)
        {
            var studentData = kirinEntities.GetTransferStudentDatabyMonth(subjectId, schoolId, month).ToList();

            if (studentData.Count() > 0)
            {
                TransferMonth = studentData.Select(x => x.MonthName).FirstOrDefault();
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetTransferStudentDataforStackBar(string schoolId, string subjectId, string transferType, string month)
        {
            var studentData = kirinEntities.GetTransferStudentDatabyType(subjectId, schoolId, transferType, month).ToList();

            TransferType = transferType;

            if (studentData.Count() > 0)
            {
                TransferredList = new ObservableCollection<TransferredStudentData>(from s in studentData
                                                                                   select new TransferredStudentData
                                                                                   {
                                                                                       ID = Convert.ToInt32(s.ID),
                                                                                       Name = s.Name,
                                                                                       DATE = s.TRANSFERRED_DATE.ToString()
                                                                                   });
            }
        }

        public void GetDOBDatabyBirthMonth(string schoolId, string subjectId, string month)
        {
            var studentData = kirinEntities.GetDOBDatabyMonth(subjectId, schoolId, month).ToList();

            if (studentData.Count() > 0)
            {
                TransferMonth = studentData.Select(x => x.MonthName).FirstOrDefault();
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentSportsData(string schoolId, string subjectId, string sport)
        {
            var studentData = kirinEntities.GetStudentDatabySports(subjectId, schoolId, sport).ToList();
            Sports = sport;
            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentAttendanceData(string studentId, string subjectId)
        {
            var attendanceData = kirinEntities.GetStudentAttendanceData(studentId, subjectId).ToList();

            if (attendanceData.Count() > 0)
            {
                StudentName = attendanceData.Select(x => x.Name).FirstOrDefault();

                StudentAttendance = new ObservableCollection<StudentAttendance>(from s in attendanceData
                                                                                select new StudentAttendance
                                                                                {
                                                                                    Name = s.Name,
                                                                                    CODE = s.CODE,
                                                                                    DESCRIPTION = s.DESCRIPTION,
                                                                                    DATE = s.DATE.ToString()
                                                                                });
            }
        }

        public void GetStudentDatabyClub(string schoolId, string subjectId, string club)
        {
            var studentData = kirinEntities.GetStudentDatabyClub(subjectId, schoolId, club).ToList();
            ClubName = club;
            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentDatabyDiversity(string schoolId, string subjectId, string diversity)
        {
            var studentData = kirinEntities.GetStudentDatabyDiversity(subjectId, schoolId, diversity).ToList();
            Diversity = diversity;
            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentEthnicityList(string schoolId, string subjectId, string ethenicity)
        {
            var studentData = kirinEntities.GetStudentDatabyEthnicity(subjectId, schoolId, ethenicity).ToList();
            Ethnicity = ethenicity;

            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = s.ID,
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentDatabyEthnicityandDivesity(string schoolId, string subjectId, string ethenicity, string diversity)
        {
            var studentData = kirinEntities.GetStudentDatabyEthnicityandDivesity(subjectId, schoolId, ethenicity, diversity).ToList();
            Ethnicity = ethenicity;

            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = s.ID,
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentDatabyEthnicityandSports(string schoolId, string subjectId, string ethenicity, string extraCo)
        {
            var studentData = kirinEntities.GetStudentDatabyEthnicityandSports(subjectId, schoolId, ethenicity, extraCo).ToList();
            ExtraCo = extraCo;

            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = s.ID,
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentDatabyEthnicityandLanguage(string schoolId, string subjectId, string ethenicity, string language)
        {
            var studentData = kirinEntities.GetStudentDatabyEthnicityandLanguage(subjectId, schoolId, ethenicity, language).ToList();
            Language = language;

            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = s.ID,
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentDatabyLanguage(string schoolId, string subjectId, string language)
        {
            var studentData = kirinEntities.GetStudentDatabyLanguage(subjectId, schoolId, language).ToList();
            Language = language;
            if (studentData.Count() > 0)
            {
                StudentList = new ObservableCollection<StudentData>(from s in studentData
                                                                    select new StudentData
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.Name
                                                                    });
            }
        }

        public void GetStudentMapData(string studentId)
        {
            var studentData = kirinEntities.GetStudentDataforMapPoint(studentId).ToList();

            if (studentData.Count() > 0)
            {
                StudentMview = new ObservableCollection<StudentMapView>(from s in studentData
                                                                    select new StudentMapView
                                                                    {
                                                                        ID = Convert.ToInt32(s.ID),
                                                                        Name = s.LAST_NAME + ", " + s.FIRST_NAME,
                                                                        HouseholdIncome = s.HouseholdIncome,
                                                                        SiblingCount = s.SiblingCount.ToString(),
                                                                        ParentCount = s.ParentCount.ToString(),
                                                                        ParentalInvolvement = s.Parantal_Involvement,
                                                                        Distance = s.Distance,
                                                                        Transportation = s.Transportation,
                                                                        BIRTHDATE = !string.IsNullOrEmpty(s.BIRTHDATE.ToString()) ? Convert.ToDateTime(s.BIRTHDATE).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                                                                    });
            }
        }

        private string _StudentName;
        public string StudentName
        {
            get { return _StudentName; }
            set
            {
                _StudentName = value;
                OnPropertyChanged("StudentName");
            }
        }

        public void GetStudentAssignmentData(string schoolId, string subjectId, string name)
        {
            string fname = !string.IsNullOrWhiteSpace(name) ? name.Split(' ')[0] : "";
            string lname = !string.IsNullOrWhiteSpace(name) ? name.Split(' ')[1] : "";
            var studentData = kirinEntities.GetStudentDataforScatterPoint(subjectId, schoolId, fname, lname).ToList();

            StudentName = name;
            if (studentData.Count() > 0)
            {
                StudentScatterList = new ObservableCollection<StudentScatterData>(from s in studentData
                                                                                  select new StudentScatterData
                                                                                  {
                                                                                      ID = s.STUDENT_ID,
                                                                                      Name = s.Name,
                                                                                      Assignment = s.Assignment,
                                                                                      POINTS_POSSIBLE = s.POINTS_POSSIBLE.ToString(),
                                                                                      POINTS = s.POINTS,
                                                                                      Percentage = Convert.ToDouble(s.Percentage)
                                                                                  });
            }

            var studentAttendanceData = kirinEntities.GetStudentAttendance(subjectId, schoolId, fname, lname).ToList();

            if (studentAttendanceData.Count > 0)
            {
                StudentAttendance = new ObservableCollection<StudentAttendance>(from s in studentAttendanceData
                                                                                select new StudentAttendance
                                                                                {
                                                                                    ID = Convert.ToInt32(s.ID),
                                                                                    Name = s.Name,
                                                                                    DATE = s.DATE.ToString(),
                                                                                    CODE = s.CODE
                                                                                });
            }

            AttendanceCodeList = new ObservableCollection<AttendanceCodeList>(from a in kirinEntities.ATTENDANCE_CODES
                                                                          select new AttendanceCodeList
                                                                          {
                                                                              ID = a.ID,
                                                                              CODE = a.CODE,
                                                                              DESCRIPTION = a.DESCRIPTION,
                                                                              WEIGHT = a.WEIGHT
                                                                          });
        }

        private ObservableCollection<StudentCourseData> _StudentCourses;
        public ObservableCollection<StudentCourseData> StudentCourses
        {
            get { return _StudentCourses; }
            set
            {
                _StudentCourses = value;
                OnPropertyChanged(nameof(StudentCourses));
            }
        }

        public void GetStudentCourseData(string studentId, string year) 
        {
            StudentCourses = new ObservableCollection<StudentCourseData>();
            StudentCourses.Add(new StudentCourseData() { Course = "English I", CourseCode = "ENG1DB", Score="98", Rank = "1" });
            StudentCourses.Add(new StudentCourseData() { Course = "Algebra I", CourseCode = "MAT1L", Score = "94", Rank = "2"});
            StudentCourses.Add(new StudentCourseData() { Course = "Precalculus", CourseCode = "MCT4C", Score = "90", Rank = "4"});
            StudentCourses.Add(new StudentCourseData() { Course = "Biology", CourseCode = "SNC1DB", Score = "84", Rank = "6"});
            StudentCourses.Add(new StudentCourseData() { Course = "Astronomy", CourseCode = "SBI3C", Score = "92", Rank = "3"});
            StudentCourses.Add(new StudentCourseData() { Course = "W Geography", CourseCode = "WGE9U", Score = "80", Rank = "7"});
            StudentCourses.Add(new StudentCourseData() { Course = "Economics", CourseCode = "ECO12U", Score = "85", Rank = "5"});
            StudentCourses.Add(new StudentCourseData() { Course = "Psychology", CourseCode = "PHY11U", Score = "79", Rank = "8"});
        }

        private ObservableCollection<StudentAssessmentData> _StudentAssessment;
        public ObservableCollection<StudentAssessmentData> StudentAssessment
        {
            get { return _StudentAssessment; }
            set
            {
                _StudentAssessment = value;
                OnPropertyChanged(nameof(StudentAssessment));
            }
        }

        public void GetStudentAssessmentData(string studentId, string year)
        {
            StudentAssessment = new ObservableCollection<StudentAssessmentData>();
            StudentAssessment.Add(new StudentAssessmentData() { ID=1, Name="Assessment#", Course1= "ENG1DB", Course2= "MAT1L", Course3= "SBI3C", Course4= "MCT4C" });
            StudentAssessment.Add(new StudentAssessmentData() { ID=2, Name = "1", Course1 = "92", Course2 = "90", Course3 = "87", Course4 = "84" });
            StudentAssessment.Add(new StudentAssessmentData() { ID=3, Name="2", Course1="90", Course2="87", Course3="81", Course4="90"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=4, Name="3", Course1="86", Course2="90", Course3="79", Course4="84"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=5, Name="4", Course1="99", Course2="85", Course3="84", Course4="82"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=6, Name="5", Course1="95", Course2="84", Course3="88", Course4="78"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=7, Name="6", Course1="88", Course2="80", Course3="90", Course4="70"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=8, Name="7", Course1="91", Course2="86", Course3="76", Course4="79"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=9, Name="8", Course1="96", Course2="81", Course3="83", Course4="85"});
            StudentAssessment.Add(new StudentAssessmentData() { ID=10, Name = "9", Course1 = "93", Course2 = "86", Course3 = "80", Course4 = "79" });
            StudentAssessment.Add(new StudentAssessmentData() { ID=11, Name = "Total", Course1 = "98%", Course2 = "95%", Course3 = "90%", Course4 = "86%" });
        }

        private string _AttendanceCode;
        public string AttendanceCode
        {
            get { return _AttendanceCode; }
            set
            {
                _AttendanceCode = value;
                OnPropertyChanged("_AttendanceCode");
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");

            }
        }
        public void GetStudentAttendanceData(string schoolId, string subjectId, string date, string attendanceCode)
        {
            var studentData = kirinEntities.GetStudentAttendancebyCode(Convert.ToInt32(subjectId), Convert.ToInt32(schoolId), date, attendanceCode).ToList();

            if (studentData.Count() > 0)
            {
                StudentAttendanceList = new ObservableCollection<StudentAttendanceData>(from s in studentData
                                                                                        select new StudentAttendanceData
                                                                                        {
                                                                                            ID = Convert.ToInt32(s.ID),
                                                                                            Name = s.Name,
                                                                                            CODE = s.CODE,
                                                                                            DESCRIPTION = s.DESCRIPTION
                                                                                        });

                SelectedDate = studentData.Select(s => s.DATE).FirstOrDefault();
                var dateToStr = studentData.Select(s => s.DATE).FirstOrDefault().ToString("dd/MM/yyyy").Replace('-', '/');
                AttendanceCode = studentData.Select(s => s.CODE).FirstOrDefault() + "(" + dateToStr + ")";
            }
        }

        public void GetTransportationData(string schoolId)
        {
            var transportationData = kirinEntities.GetStudentModeofTransportation(schoolId).ToList();

            if (transportationData.Count() > 0)
            {
                ModeList = new ObservableCollection<TransportationData>(from s in transportationData
                                                                        select new TransportationData
                                                                        {
                                                                            Transportation = s.Transportation,
                                                                            TransportationAbvr = s.TransportationAbvr
                                                                        });
            }           
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<StudentData> _StudentList;
        public ObservableCollection<StudentData> StudentList
        {
            get { return _StudentList; }
            set
            {
                _StudentList = value;
                OnPropertyChanged(nameof(StudentList));
            }
        }

        private ObservableCollection<StudentMapView> _StudentMview;
        public ObservableCollection<StudentMapView> StudentMview
        {
            get { return _StudentMview; }
            set
            {
                _StudentMview = value;
                OnPropertyChanged(nameof(_StudentMview));
            }
        }
        

        private ObservableCollection<TransferredStudentData> _transferredList;
        public ObservableCollection<TransferredStudentData> TransferredList
        {
            get { return _transferredList; }
            set
            {
                _transferredList = value;
                OnPropertyChanged(nameof(_transferredList));
            }
        }


        private ObservableCollection<StudentScatterData> _StudentScatterList;
        public ObservableCollection<StudentScatterData> StudentScatterList
        {
            get { return _StudentScatterList; }
            set
            {
                _StudentScatterList = value;
                OnPropertyChanged(nameof(StudentScatterList));
            }
        }

        private ObservableCollection<TransportationData> _ModeList;
        public ObservableCollection<TransportationData> ModeList
        {
            get { return _ModeList; }
            set
            {
                _ModeList = value;
                OnPropertyChanged(nameof(ModeList));
            }
        }

        private ObservableCollection<StudentAttendance> _StudentAttendance;
        public ObservableCollection<StudentAttendance> StudentAttendance
        {
            get { return _StudentAttendance; }
            set
            {
                _StudentAttendance = value;
                OnPropertyChanged(nameof(StudentAttendance));
            }
        }

        private ObservableCollection<AttendanceCodeList> _AttendanceCodeList;
        public ObservableCollection<AttendanceCodeList> AttendanceCodeList
        {
            get { return _AttendanceCodeList; }
            set
            {
                _AttendanceCodeList = value;
                OnPropertyChanged(nameof(AttendanceCodeList));
            }
        }
        

        private ObservableCollection<StudentAttendanceData> _StudentAttendanceList;
        public ObservableCollection<StudentAttendanceData> StudentAttendanceList
        {
            get { return _StudentAttendanceList; }
            set
            {
                _StudentAttendanceList = value;
                OnPropertyChanged(nameof(StudentAttendanceList));
            }
        }
    }

    public class StudentData
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class StudentMapView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HouseholdIncome { get; set; }
        public string ParentalInvolvement { get; set; }
        public string ParentCount { get; set; }
        public string SiblingCount { get; set; }
        public string Distance { get; set; }
        public string Transportation { get; set; }
        public string BIRTHDATE { get; set; }
    }

    public class TransferredStudentData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DATE { get; set; }
    }

    public class StudentScatterData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Assignment { get; set; }
        public string POINTS_POSSIBLE { get; set; }
        public string POINTS { get; set; }
        public double Percentage { get; set; }
    }

    public class TransportationData
    {
        public string Transportation { get; set; }
        public string TransportationAbvr { get; set; }        
    }
    
    public class StudentAttendance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DATE { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class AttendanceCodeList
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string WEIGHT { get; set; }
    }

    public class StudentAttendanceData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class StudentCourseData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string Score { get; set; }
        public string Year { get; set; }
        public string Rank { get; set; }
    }

    public class StudentAssessmentData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Course1 { get; set; }
        public string Course2 { get; set; }
        public string Course3 { get; set; }
        public string Course4 { get; set; }
    }

}
