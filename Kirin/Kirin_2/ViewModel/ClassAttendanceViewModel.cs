using Kirin_2.Models;
using Kirin_2.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kirin_2.ViewModel
{
    class ClassAttendanceViewModel : INotifyPropertyChanged
    {
        public App app;
        //string userName = App.Current.Properties["USERNAME"].ToString();
        public static string subjectId = App.Current.Properties["SubjectId"].ToString();
        public static string schoolId = App.Current.Properties["SchoolId"].ToString();
        public ClassAttendanceViewModel()
        {
            app = (App)Application.Current;
            KirinEntities = new KIRINEntities1();
            SubjectViewModel subjVM = new SubjectViewModel();

            getAttendanceCodes();
            getAttendanceCodesCombobox();
            SaveSubjectAttendance = new CommandVM(SaveAttendance, canExecuteMethod);
            SaveSubjectMultiDayAttendance = new CommandVM(SaveMultiDayAttendance, canExecuteMethod);
            cViewHealthAlert = new CommandVM(ViewHealthAlert, canExecuteMethod);
            cViewComment = new CommandVM(ViewComment, canExecuteMethod);
            cViewBdayAlert = new CommandVM(ViewBdayAlert, canExecuteMethod);
            cViewSPEDAlert = new CommandVM(ViewSPEDAlert, canExecuteMethod);
            cViewOtherAlert = new CommandVM(ViewOtherAlert, canExecuteMethod);
            cCheckCommentIconVisibility = new CommandVM(CheckCommentIconVisibility, canExecuteMethod);
            EditDateRangeCommand = new CommandVM(EditDateRange, canExecuteMethod);
            InitDateRange();

            //DataTableCollection = getMultiDayClassAttendance();
        }

        public DataTable DataTableCollection { get; set; }

        KIRINEntities1 KirinEntities;
        public ICommand SaveSubjectAttendance { get; set; }
        public ICommand SaveSubjectMultiDayAttendance { get; set; }
        public ICommand cViewHealthAlert { get; set; }
        public ICommand cViewComment { get; set; }
        public ICommand cViewBdayAlert { get; set; }
        public ICommand cViewSPEDAlert { get; set; }
        public ICommand cViewOtherAlert { get; set; }
        public ICommand cCheckCommentIconVisibility { get; set; }
        public ICommand EditDateRangeCommand { get; set; }

        private bool _IsCodeVisible;

        public bool IsCodeVisible
        {
            get { return _IsCodeVisible; }
            set
            {
                _IsCodeVisible = value;
                OnPropertyChanged("IsCodeVisible");
            }
        }

        private bool _IsCmbCodeVisible;

        public bool IsCmbCodeVisible
        {
            get { return _IsCmbCodeVisible; }
            set
            {
                _IsCmbCodeVisible = value;
                OnPropertyChanged("IsCmbCodeVisible");
            }
        }


        public List<DateTime> DateRange { get; set; }

        private string _STUDENT_NAME;

        public string STUDENT_NAME
        {
            get { return _STUDENT_NAME; }
            set
            {
                _STUDENT_NAME = value;
                OnPropertyChanged("STUDENT_NAME");
            }
        }

        private string _STUDENT_ID;

        public string STUDENT_ID
        {
            get { return _STUDENT_ID; }
            set
            {
                _STUDENT_ID = value;
                OnPropertyChanged("STUDENT_ID");
            }
        }

        private DateTime _DateRangeFilter_From = DateTime.Now;

        public DateTime DateRangeFilter_From
        {
            get { return _DateRangeFilter_From; }
            set
            {
                _DateRangeFilter_From = value;
                OnPropertyChanged("DateRangeFilter_From");
            }
        }

        private DateTime _DateRangeFilter_To;
        public DateTime DateRangeFilter_To
        {
            get { return _DateRangeFilter_To; }
            set
            {
                _DateRangeFilter_To = value;
                OnPropertyChanged("DateRangeFilter_To");
            }
        }

        private int _CURRENTCLASSID;

        public int CURRENTCLASSID
        {
            get { return _CURRENTCLASSID; }
            set
            {
                _CURRENTCLASSID = value;
                OnPropertyChanged("CURRENTCLASSID");
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
                //getClassListAttendanceMultiDay(CURRENTCLASSID);
                //updateAttendanceView();
            }
        }

        //private string _SubmitBackground;
        //public string SubmitBackground
        //{
        //    get { return _SubmitBackground; }
        //    set
        //    {
        //        _SubmitBackground = value;
        //        OnPropertyChanged("SubmitBackground");
        //    }
        //}

        private Color _SubmitBackground;

        public Color SubmitBackground
        {
            get
            {
                return _SubmitBackground;
            }
            set
            {
                _SubmitBackground = value;
                OnPropertyChanged(nameof(SubmitBackground));
            }
        }

        public void updateAttendanceView()
        {
            //to be implemented
            //get fromDate week


            //get toDate week
        }

        public void InitDateRange()
        {
            DateTime _now = DateTime.Now;

            //set _DateRangeFilter_From = 7 Days prior
            _DateRangeFilter_From = _now.AddDays(-(int)_now.DayOfWeek + (int)DayOfWeek.Monday).AddDays(-7);

            //set _DateRangeFilter_To = 7 Days After
            _DateRangeFilter_To = _now.AddDays(-(int)_now.DayOfWeek + (int)DayOfWeek.Friday).AddDays(+14);


            DateRangeFilter_lbl = _DateRangeFilter_From.ToString("MM/dd/yyyy") + " - " + _DateRangeFilter_To.ToString("MM/dd/yyyy");
        }


        private List<DateTime> submittedDates = new List<DateTime>();

        public List<DateTime> SubmittedDates
        {
            get { return submittedDates; }
            set
            {
                submittedDates = value;
                OnPropertyChanged("SubmittedDates");
            }
        }


        //binding for green color day

        private Visibility _PREVWEEK_MON_ISCURRENTDAY;

        public Visibility PREVWEEK_MON_ISCURRENTDAY
        {
            get { return _PREVWEEK_MON_ISCURRENTDAY; }
            set
            {
                _PREVWEEK_MON_ISCURRENTDAY = value;
                OnPropertyChanged("PREVWEEK_MON_ISCURRENTDAY");
            }
        }


        private Visibility _CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;

        public Visibility CURRWEEK_MON_ISCURRENTDAY
        {
            get { return _CURRWEEK_MON_ISCURRENTDAY; }
            set
            {
                _CURRWEEK_MON_ISCURRENTDAY = value;
                OnPropertyChanged("CURRWEEK_MON_ISCURRENTDAY");
            }
        }

        private Visibility _CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;

        public Visibility CURRWEEK_TUE_ISCURRENTDAY
        {
            get { return _CURRWEEK_TUE_ISCURRENTDAY; }
            set
            {
                _CURRWEEK_TUE_ISCURRENTDAY = value;
                OnPropertyChanged("CURRWEEK_TUE_ISCURRENTDAY");
            }
        }

        private Visibility _CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;

        public Visibility CURRWEEK_WED_ISCURRENTDAY
        {
            get { return _CURRWEEK_WED_ISCURRENTDAY; }
            set
            {
                _CURRWEEK_WED_ISCURRENTDAY = value;
                OnPropertyChanged("CURRWEEK_WED_ISCURRENTDAY");
            }
        }

        private Visibility _CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;

        public Visibility CURRWEEK_THU_ISCURRENTDAY
        {
            get { return _CURRWEEK_THU_ISCURRENTDAY; }
            set
            {
                _CURRWEEK_THU_ISCURRENTDAY = value;
                OnPropertyChanged("CURRWEEK_THU_ISCURRENTDAY");
            }
        }

        private Visibility _CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

        public Visibility CURRWEEK_FRI_ISCURRENTDAY
        {
            get { return _CURRWEEK_FRI_ISCURRENTDAY; }
            set
            {
                _CURRWEEK_FRI_ISCURRENTDAY = value;
                OnPropertyChanged("CURRWEEK_FRI_ISCURRENTDAY");
            }
        }

        public Visibility PREVWEEK_TUE_ISCURRENTDAY { get; set; }
        public Visibility PREVWEEK_WED_ISCURRENTDAY { get; set; }
        public Visibility PREVWEEK_THU_ISCURRENTDAY { get; set; }
        public Visibility PREVWEEK_FRI_ISCURRENTDAY { get; set; }

        public Visibility NEXTWEEK_MON_ISCURRENTDAY { get; set; }
        public Visibility NEXTWEEK_TUE_ISCURRENTDAY { get; set; }
        public Visibility NEXTWEEK_WED_ISCURRENTDAY { get; set; }
        public Visibility NEXTWEEK_THU_ISCURRENTDAY { get; set; }
        public Visibility NEXTWEEK_FRI_ISCURRENTDAY { get; set; }

        public Visibility WEEK4_MON_ISCURRENTDAY { get; set; }
        public Visibility WEEK4_TUE_ISCURRENTDAY { get; set; }
        public Visibility WEEK4_WED_ISCURRENTDAY { get; set; }
        public Visibility WEEK4_THU_ISCURRENTDAY { get; set; }
        public Visibility WEEK4_FRI_ISCURRENTDAY { get; set; }


        public bool canExecuteMethod(object param)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string className;
        public string ClassName
        {
            get { return className; }
            set
            {
                className = value;
                OnPropertyChanged("ClassName");
            }
        }


        private string birthdayIconVisibility;
        public string BirthdayIconVisibility
        {
            get { return birthdayIconVisibility; }
            set
            {
                birthdayIconVisibility = value;
                OnPropertyChanged("BirthdayIconVisibility");

            }
        }


        private int classID;
        public int ClassID
        {
            get { return classID; }
            set
            {
                classID = value;
                OnPropertyChanged("classID");
            }
        }


        private string attendanceHeaderCurrent;
        public string AttendanceHeaderCurrent
        {
            get { return attendanceHeaderCurrent; } //"Attendance: " + DateTime.Now.ToString("dddd, dd MMMM yyyy")
            set
            {
                attendanceHeaderCurrent = value;
                OnPropertyChanged("AttendanceHeaderCurrent");
            }
        }

        private DateTime attendanceDate = DateTime.Now;
        public DateTime AttendanceDate
        {
            get { return attendanceDate; }
            set
            {
                attendanceDate = value;
                OnPropertyChanged("AttendanceDate");

            }
        }


        private ObservableCollection<AttendanceCode> attendanceCodes;
        public ObservableCollection<AttendanceCode> AttendanceCodes
        {
            get { return this.attendanceCodes; }
            set
            {
                attendanceCodes = value;

                OnPropertyChanged("AttendanceCodes");
                //   OnPropertyChanged("AttendanceCodesCombo");

            }
        }


        private AttendanceCode attendanceCodeSelectedItemMultiDayView;

        public AttendanceCode AttendanceCodeSelectedItemMultiDayView
        {
            get { return attendanceCodeSelectedItemMultiDayView; }
            set
            {
                if (value != attendanceCodeSelectedItemMultiDayView)
                {
                    attendanceCodeSelectedItemMultiDayView = value;

                    //setAttendanceCodes(attendanceCodeSelectedItemMultiDayView);
                    setMultiDayAttendanceCodes(attendanceCodeSelectedItemMultiDayView);
                    //checkTotalAbsencesTardiness();
                    checkTotalAbsencesLates();
                    OnPropertyChanged("AttendanceCodeSelectedItemMultiDayView");
                }
            }
        }


        private AttendanceCode attendanceCodeSelectedItem;

        public AttendanceCode AttendanceCodeSelectedItem
        {
            get { return attendanceCodeSelectedItem; }
            set
            {
                attendanceCodeSelectedItem = value;
                OnPropertyChanged("AttendanceCodeSelectedItem");

                setSingleDayAttendanceCodes(attendanceCodeSelectedItem);
            }
        }


        private ObservableCollection<AttendanceCode> attendanceCodesCombo;
        public ObservableCollection<AttendanceCode> AttendanceCodesCombo
        {
            get { return this.attendanceCodesCombo; }
            set
            {
                attendanceCodesCombo = value;
                OnPropertyChanged("AttendanceCodesCombo");

            }
        }


        private ObservableCollection<CLASS_STUDENT> classlist;
        public ObservableCollection<CLASS_STUDENT> ClassList
        {
            get { return classlist; }
            set
            {
                classlist = value;
                OnPropertyChanged("ClassList");

            }
        }

        private ObservableCollection<StudentDef> clist;
        public ObservableCollection<StudentDef> CList
        {
            get { return clist; }
            set
            {
                clist = value;
                OnPropertyChanged("CList");

            }
        }

        private ObservableCollection<StudentDefMultiDay> clistAttendanceMultiDay;
        public ObservableCollection<StudentDefMultiDay> CListAttendanceMultiDay
        {
            get { return clistAttendanceMultiDay; }
            set
            {
                clistAttendanceMultiDay = value;
                OnPropertyChanged("CListAttendanceMultiDay");

            }
        }

        private ObservableCollection<StudentDefMultiDay> clistMultiDayAttendance;
        public ObservableCollection<StudentDefMultiDay> CListMultiDayAttendance
        {
            get { return clistMultiDayAttendance; }
            set
            {
                clistMultiDayAttendance = value;
                OnPropertyChanged("CListMultiDayAttendance");

            }
        }

        private string week1DateRange;

        public string Week1DateRange
        {
            get { return week1DateRange; }
            set
            {
                week1DateRange = value;
                OnPropertyChanged("Week1DateRange");
            }
        }


        private string week2DateRange;

        public string Week2DateRange
        {
            get { return week2DateRange; }
            set
            {
                week2DateRange = value;
                OnPropertyChanged("Week2DateRange");
            }
        }

        private string week3DateRange;

        public string Week3DateRange
        {
            get { return week3DateRange; }
            set
            {
                week3DateRange = value;
                OnPropertyChanged("Week3DateRange");
            }
        }

        private string week4DateRange;

        public string Week4DateRange
        {
            get { return week4DateRange; }
            set
            {
                week4DateRange = value;
                OnPropertyChanged("Week4DateRange");
            }
        }

        //Start:: Properties for each day of prevWeek

        private string prevWeekMon;
        public string PrevWeekMon
        {
            get { return prevWeekMon; }
            set { prevWeekMon = value; }
        }

        private string prevWeekTues;
        public string PrevWeekTues
        {
            get { return prevWeekTues; }
            set { prevWeekTues = value; }
        }

        private string prevWeekWed;
        public string PrevWeekWed
        {
            get { return prevWeekWed; }
            set { prevWeekWed = value; }
        }

        private string prevWeekThu;
        public string PrevWeekThu
        {
            get { return prevWeekThu; }
            set { prevWeekThu = value; }
        }

        private string prevWeekFri;
        public string PrevWeekFri
        {
            get { return prevWeekFri; }
            set { prevWeekFri = value; }
        }
        //End:: Properties for each day of prevWeek

        //Start:: Properties for each day of currWeek

        private string currWeekMon;
        public string CurrWeekMon
        {
            get { return currWeekMon; }
            set { currWeekMon = value; }
        }

        private string currWeekTues;
        public string CurrWeekTues
        {
            get { return currWeekTues; }
            set { currWeekTues = value; }
        }

        private string currWeekWed;
        public string CurrWeekWed
        {
            get { return currWeekWed; }
            set { currWeekWed = value; }
        }

        private string currWeekThu;
        public string CurrWeekThu
        {
            get { return currWeekThu; }
            set { currWeekThu = value; }
        }

        private string currWeekFri;
        public string CurrWeekFri
        {
            get { return currWeekFri; }
            set { currWeekFri = value; }
        }
        //End:: Properties for each day of CurrentWeek

        //Start:: Properties for each day of nextWeek

        private string nextWeekMon;
        public string NextWeekMon
        {
            get { return nextWeekMon; }
            set { nextWeekMon = value; }
        }

        private string nextWeekTues;
        public string NextWeekTues
        {
            get { return nextWeekTues; }
            set { nextWeekTues = value; }
        }

        private string nextWeekWed;
        public string NextWeekWed
        {
            get { return nextWeekWed; }
            set { nextWeekWed = value; }
        }

        private string nextWeekThu;
        public string NextWeekThu
        {
            get { return nextWeekThu; }
            set { nextWeekThu = value; }
        }

        private string nextWeekFri;
        public string NextWeekFri
        {
            get { return nextWeekFri; }
            set { nextWeekFri = value; }
        }
        //End:: Properties for each day of NextWeek


        public void setSingleDayAttendanceCodes(AttendanceCode selectedItem)
        {
            for (int i = 0; i < CList.Count; i++)
            {
                CList[i].ATTENDANCE_CODE = selectedItem;

                if (selectedItem.desc.StartsWith("L") || selectedItem.desc.Contains("Uninformed"))
                {
                    CList[i].COMMENTICONVISIBILITY = "VISIBLE";
                }
                else
                {
                    CList[i].COMMENTICONVISIBILITY = "COLLAPSED";
                }

            }
        }

        public void setMultiDayAttendanceCodes(AttendanceCode attCode)
        {
            string dayofweek = AttendanceDate.ToString("ddd");
            for (int j = 0; j < DateRange.Count; j++)
            {
                for (int i = 0; i < CListMultiDayAttendance.Count; i++)
                {
                    if (DateRange[j].ToString("yyyy/MM/dd") == AttendanceDate.ToString("yyyy/MM/dd"))
                    {
                        if (dayofweek.ToUpper() == "MON")
                        {
                            this.CListMultiDayAttendance[i].CURRWEEK_MON_ATT_CODE = attCode.code;
                        }
                        else if (dayofweek.ToUpper() == "TUE")
                        {
                            this.CListMultiDayAttendance[i].CURRWEEK_TUE_ATT_CODE = attCode.code;
                        }
                        else if (dayofweek.ToUpper() == "WED")
                        {
                            this.CListMultiDayAttendance[i].CURRWEEK_WED_ATT_CODE = attCode.code;
                        }
                        else if (dayofweek.ToUpper() == "THU")
                        {
                            this.CListMultiDayAttendance[i].CURRWEEK_THU_ATT_CODE = attCode.code;
                        }
                        else if (dayofweek.ToUpper() == "FRI")
                        {
                            this.CListMultiDayAttendance[i].CURRWEEK_FRI_ATT_CODE = attCode.code;
                        }
                    }
                }
            }
        }

        public void setAttendanceCodes(AttendanceCode attCode)
        {
            for (int i = 0; i < CListAttendanceMultiDay.Count; i++)
            {
                if (this.CListAttendanceMultiDay[i].PREVWEEK_MON_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].PREVWEEK_MON_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].PREVWEEK_TUE_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].PREVWEEK_TUE_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].PREVWEEK_WED_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].PREVWEEK_WED_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].PREVWEEK_THU_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].PREVWEEK_THU_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].PREVWEEK_FRI_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].PREVWEEK_FRI_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].CURRWEEK_MON_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].CURRWEEK_MON_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].CURRWEEK_TUE_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].CURRWEEK_TUE_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].CURRWEEK_WED_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].CURRWEEK_WED_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].CURRWEEK_THU_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].CURRWEEK_THU_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].CURRWEEK_FRI_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].CURRWEEK_FRI_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].NEXTWEEK_MON_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].NEXTWEEK_MON_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].NEXTWEEK_TUE_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].NEXTWEEK_TUE_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].NEXTWEEK_WED_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].NEXTWEEK_WED_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].NEXTWEEK_THU_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].NEXTWEEK_THU_ATT_CODE = attCode.code;
                }
                if (this.CListAttendanceMultiDay[i].NEXTWEEK_FRI_ISSUBMITTED == Visibility.Hidden)
                {
                    this.CListAttendanceMultiDay[i].NEXTWEEK_FRI_ATT_CODE = attCode.code;
                }

            }
        }

        public Visibility hasSubmittedEntries(DateTime dateTobeChecked, int classID)
        {
            try
            {
                string code = (from classAttendance in KirinEntities.CLASS_ATTENDANCE
                               where classAttendance.SUBJECT_ID == classID
                               && classAttendance.DATE == System.Data.Entity.DbFunctions.TruncateTime(dateTobeChecked)
                               select classAttendance.ATTENDANCE_CODE).FirstOrDefault()?.ToString();

                if (code != null)
                {
                    SubmittedDates.Add(dateTobeChecked);
                    return Visibility.Visible;
                }
                else return Visibility.Hidden;
            }
            catch (Exception e)
            {
                return Visibility.Hidden;
            }
        }

        public Visibility hasSubmittedEntries(string dateTobeChecked)
        {
            try
            {
                DateTime date = Convert.ToDateTime(dateTobeChecked);

                string code = (from classAttendance in KirinEntities.CLASS_ATTENDANCE
                               where classAttendance.SUBJECT_ID == classID
                               && classAttendance.DATE == System.Data.Entity.DbFunctions.TruncateTime(date)
                               select classAttendance.ATTENDANCE_CODE).FirstOrDefault()?.ToString();

                if (code != null)
                {
                    SubmittedDates.Add(date);
                    return Visibility.Visible;
                }
                else return Visibility.Hidden;
            }
            catch (Exception e)
            {
                return Visibility.Hidden;
            }
        }

        public string getAttendanceOnDay(DateTime dateTobeChecked, int studentID, int classID)
        {
            try
            {
                string code = (from classAttendance in KirinEntities.CLASS_ATTENDANCE
                               where classAttendance.SUBJECT_ID == classID
                               && classAttendance.STUDENT_ID == studentID
                               && classAttendance.DATE == System.Data.Entity.DbFunctions.TruncateTime(dateTobeChecked)
                               select classAttendance.ATTENDANCE_CODE).FirstOrDefault()?.ToString();

                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {

            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = (((int)day - (int)start.DayOfWeek + 7) % 7) > 0 ? (((int)day - (int)start.DayOfWeek + 7) % 7) : 7;

            return start.AddDays(daysToAdd);
        }

        public void getClassListAttendanceMultiDay(int classID)
        {
            //temporary selected date = currentdate
            DateTime currDate = DateTime.Now;
            currDate = DateRangeFilter_From;
            //currDate = DateTime.ParseExact("09/01/2021", "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime mondayOfCurrentWeek = currDate.AddDays(-(int)currDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime tuesdayOfCurrentWeek = currDate.AddDays(-(int)currDate.DayOfWeek + (int)DayOfWeek.Tuesday);
            DateTime wednesdayOfCurrentWeek = currDate.AddDays(-(int)currDate.DayOfWeek + (int)DayOfWeek.Wednesday);
            DateTime thursdayOfCurrentWeek = currDate.AddDays(-(int)currDate.DayOfWeek + (int)DayOfWeek.Thursday);
            DateTime fridayOfCurrentWeek = currDate.AddDays(-(int)currDate.DayOfWeek + (int)DayOfWeek.Friday);

            DateTime mondayOfLastWeek = mondayOfCurrentWeek.AddDays(-7);
            DateTime tuesdayOfLastWeek = tuesdayOfCurrentWeek.AddDays(-7);
            DateTime wednesdayOfLastWeek = wednesdayOfCurrentWeek.AddDays(-7);
            DateTime thursdayOfLastWeek = thursdayOfCurrentWeek.AddDays(-7);
            DateTime fridayOfLastWeek = fridayOfCurrentWeek.AddDays(-7);

            DateTime mondayofNextWeek = mondayOfCurrentWeek.AddDays(+7);
            DateTime tuesdayofNextWeek = tuesdayOfCurrentWeek.AddDays(+7);
            DateTime wednesdayofNextWeek = wednesdayOfCurrentWeek.AddDays(+7);
            DateTime thursdayofNextWeek = thursdayOfCurrentWeek.AddDays(+7);
            DateTime fridayofNextWeek = fridayOfCurrentWeek.AddDays(+7);

            if (mondayOfLastWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Visible;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (tuesdayOfLastWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Visible;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (wednesdayOfLastWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Visible;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (thursdayOfLastWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Visible;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (fridayOfLastWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Visible;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (mondayOfCurrentWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Visible;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (tuesdayOfCurrentWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Visible;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (wednesdayOfCurrentWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Visible;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (thursdayOfCurrentWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Visible;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (fridayOfCurrentWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Visible;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (mondayofNextWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Visible;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (tuesdayofNextWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Visible;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (wednesdayofNextWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Visible;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (thursdayofNextWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Visible;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }
            else if (fridayofNextWeek == currDate)
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Visible;
            }
            else
            {
                PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;
            }


            CListAttendanceMultiDay = new ObservableCollection<StudentDefMultiDay>((from student in KirinEntities.STUDENTs
                                                                                    join class_attendance in KirinEntities.CLASS_ATTENDANCE on student.ID equals class_attendance.STUDENT_ID
                                                                                    join subject in KirinEntities.SUBJECTS on class_attendance.SUBJECT_ID equals subject.ID
                                                                                    where class_attendance.SUBJECT_ID == classID
                                                                                    select new StudentDefMultiDay
                                                                                    {
                                                                                        ID = student.ID,
                                                                                        STUDENT_FULLNAME = student.LAST_NAME + ", " + student.FIRST_NAME,
                                                                                        ClassID = class_attendance.SUBJECT_ID

                                                                                    }).AsEnumerable()
                                                                                   .Select(a => new StudentDefMultiDay()
                                                                                   {
                                                                                       ID = a.ID,
                                                                                       STUDENT_FULLNAME = a.STUDENT_FULLNAME,
                                                                                       ClassID = a.ClassID,
                                                                                       TotalAbsences = 1,
                                                                                       TotalTardiness = 0,
                                                                                       PREVWEEK_MON_ATT_CODE = (getAttendanceOnDay(mondayOfLastWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(mondayOfLastWeek, a.ID, a.ClassID)),
                                                                                       PREVWEEK_TUE_ATT_CODE = (getAttendanceOnDay(tuesdayOfLastWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(tuesdayOfLastWeek, a.ID, a.ClassID)),
                                                                                       PREVWEEK_WED_ATT_CODE = (getAttendanceOnDay(wednesdayOfLastWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(wednesdayOfLastWeek, a.ID, a.ClassID)),
                                                                                       PREVWEEK_THU_ATT_CODE = (getAttendanceOnDay(thursdayOfLastWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(thursdayOfLastWeek, a.ID, a.ClassID)),
                                                                                       PREVWEEK_FRI_ATT_CODE = (getAttendanceOnDay(fridayOfLastWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(fridayOfLastWeek, a.ID, a.ClassID)),

                                                                                       CURRWEEK_MON_ATT_CODE = (getAttendanceOnDay(mondayOfCurrentWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(mondayOfCurrentWeek, a.ID, a.ClassID)),
                                                                                       CURRWEEK_TUE_ATT_CODE = (getAttendanceOnDay(tuesdayOfCurrentWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(tuesdayOfCurrentWeek, a.ID, a.ClassID)),
                                                                                       CURRWEEK_WED_ATT_CODE = (getAttendanceOnDay(wednesdayOfCurrentWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(wednesdayOfCurrentWeek, a.ID, a.ClassID)),
                                                                                       CURRWEEK_THU_ATT_CODE = (getAttendanceOnDay(thursdayOfCurrentWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(thursdayOfCurrentWeek, a.ID, a.ClassID)),
                                                                                       CURRWEEK_FRI_ATT_CODE = (getAttendanceOnDay(fridayOfCurrentWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(fridayOfCurrentWeek, a.ID, a.ClassID)),

                                                                                       NEXTWEEK_MON_ATT_CODE = (getAttendanceOnDay(mondayofNextWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(mondayofNextWeek, a.ID, a.ClassID)),
                                                                                       NEXTWEEK_TUE_ATT_CODE = (getAttendanceOnDay(tuesdayofNextWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(tuesdayofNextWeek, a.ID, a.ClassID)),

                                                                                       NEXTWEEK_WED_ATT_CODE = (getAttendanceOnDay(wednesdayofNextWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(wednesdayofNextWeek, a.ID, a.ClassID)),
                                                                                       NEXTWEEK_THU_ATT_CODE = (getAttendanceOnDay(thursdayofNextWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(thursdayofNextWeek, a.ID, a.ClassID)),
                                                                                       NEXTWEEK_FRI_ATT_CODE = (getAttendanceOnDay(fridayofNextWeek, a.ID, a.ClassID) == null ? string.Empty : getAttendanceOnDay(fridayofNextWeek, a.ID, a.ClassID)),
                                                                                       PREVWEEK_MON_ISSUBMITTED = hasSubmittedEntries(mondayOfLastWeek, a.ClassID),
                                                                                       PREVWEEK_TUE_ISSUBMITTED = hasSubmittedEntries(tuesdayOfLastWeek, a.ClassID),
                                                                                       PREVWEEK_WED_ISSUBMITTED = hasSubmittedEntries(wednesdayOfLastWeek, a.ClassID),
                                                                                       PREVWEEK_THU_ISSUBMITTED = hasSubmittedEntries(thursdayOfLastWeek, a.ClassID),
                                                                                       PREVWEEK_FRI_ISSUBMITTED = hasSubmittedEntries(fridayOfLastWeek, a.ClassID),
                                                                                       CURRWEEK_MON_ISSUBMITTED = hasSubmittedEntries(mondayOfCurrentWeek, a.ClassID),
                                                                                       CURRWEEK_TUE_ISSUBMITTED = hasSubmittedEntries(tuesdayOfCurrentWeek, a.ClassID),
                                                                                       CURRWEEK_WED_ISSUBMITTED = hasSubmittedEntries(wednesdayOfCurrentWeek, a.ClassID),
                                                                                       CURRWEEK_THU_ISSUBMITTED = hasSubmittedEntries(thursdayOfCurrentWeek, a.ClassID),
                                                                                       CURRWEEK_FRI_ISSUBMITTED = hasSubmittedEntries(fridayOfCurrentWeek, a.ClassID),
                                                                                       NEXTWEEK_MON_ISSUBMITTED = hasSubmittedEntries(mondayofNextWeek, a.ClassID),
                                                                                       NEXTWEEK_TUE_ISSUBMITTED = hasSubmittedEntries(tuesdayofNextWeek, a.ClassID),
                                                                                       NEXTWEEK_WED_ISSUBMITTED = hasSubmittedEntries(wednesdayofNextWeek, a.ClassID),
                                                                                       NEXTWEEK_THU_ISSUBMITTED = hasSubmittedEntries(thursdayofNextWeek, a.ClassID),
                                                                                       NEXTWEEK_FRI_ISSUBMITTED = hasSubmittedEntries(fridayofNextWeek, a.ClassID),

                                                                                       PREVWEEK_MON_DATE = mondayOfLastWeek,
                                                                                       PREVWEEK_TUE_DATE = tuesdayOfLastWeek,
                                                                                       PREVWEEK_WED_DATE = wednesdayOfLastWeek,
                                                                                       PREVWEEK_THU_DATE = thursdayOfLastWeek,
                                                                                       PREVWEEK_FRI_DATE = fridayOfLastWeek,

                                                                                       CURRWEEK_MON_DATE = mondayOfCurrentWeek,
                                                                                       CURRWEEK_TUE_DATE = tuesdayOfCurrentWeek,
                                                                                       CURRWEEK_WED_DATE = wednesdayOfCurrentWeek,
                                                                                       CURRWEEK_THU_DATE = thursdayOfCurrentWeek,
                                                                                       CURRWEEK_FRI_DATE = fridayOfCurrentWeek,

                                                                                       NEXTWEEK_MON_DATE = mondayofNextWeek,
                                                                                       NEXTWEEK_TUE_DATE = tuesdayofNextWeek,
                                                                                       NEXTWEEK_WED_DATE = wednesdayofNextWeek,
                                                                                       NEXTWEEK_THU_DATE = thursdayofNextWeek,
                                                                                       NEXTWEEK_FRI_DATE = fridayofNextWeek
                                                                                   }).GroupBy(x => new { x.ID, x.ClassID, x.STUDENT_FULLNAME }).Select(x => x.First()).ToList()); //this will remove duplicates 

            checkTotalAbsencesTardiness();

            DateRange = new List<DateTime> { mondayOfLastWeek, tuesdayOfLastWeek, wednesdayOfLastWeek , thursdayOfLastWeek , fridayOfLastWeek ,
                                                                                        mondayOfCurrentWeek,tuesdayOfCurrentWeek,wednesdayOfCurrentWeek,thursdayOfCurrentWeek,fridayOfCurrentWeek,
                                                                                        mondayofNextWeek,tuesdayofNextWeek,wednesdayofNextWeek,thursdayofNextWeek,fridayofNextWeek
                                                                                       };
            DateRange = DateRange.Except(SubmittedDates).ToList();

            Week1DateRange = mondayOfLastWeek.ToString("MM/dd") + " - " + fridayOfLastWeek.ToString("MM/dd");
            Week2DateRange = mondayOfCurrentWeek.ToString("MM/dd") + " - " + fridayOfCurrentWeek.ToString("MM/dd");
            Week3DateRange = mondayofNextWeek.ToString("MM/dd") + " - " + fridayofNextWeek.ToString("MM/dd");
            Week4DateRange = mondayofNextWeek.ToString("MM/dd") + " - " + fridayofNextWeek.ToString("MM/dd");
        }

        public void getMultiDayClassAttendance()
        {
            try
            {
                DateTime currDate = DateTime.Now;
                string fromDate = DateRangeFilter_From.ToString("yyyy-MM-dd");
                string toDate = DateRangeFilter_To.ToString("yyyy-MM-dd");
                DataTable result = GetMultiDayAttendanceData(fromDate, toDate);

                CListMultiDayAttendance = new ObservableCollection<StudentDefMultiDay>();

                List<string> ColumnList = new List<string>();

                foreach (DataColumn column in result.Columns)
                {
                    if (column.ColumnName != "ID" && column.ColumnName != "Name" && column.ColumnName != "SubjectID" && column.ColumnName != "STUDENT_ID"
                        && column.ColumnName != "COURSE_NAME" && column.ColumnName != "COURSE_CODE" && column.ColumnName != "TeacherName")
                    {
                        ColumnList.Add(column.ColumnName);
                    }
                }

                string currDateToStr = currDate.ToString("yyyy-MM-dd");

                if (ColumnList[0] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Visible;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[1] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Visible;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[2] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Visible;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[3] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Visible;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[4] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Visible;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[5] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Visible;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[6] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Visible;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[7] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Visible;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[8] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Visible;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[9] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Visible;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[10] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Visible;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[11] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Visible;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[12] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Visible;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[13] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Visible;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[14] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Visible;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[15] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Visible;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[16] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Visible;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[17] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Visible;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[18] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Visible;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }
                else if (ColumnList[19] == currDateToStr)
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Visible;
                }
                else
                {
                    PREVWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    PREVWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    CURRWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    CURRWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    NEXTWEEK_MON_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_TUE_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_WED_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_THU_ISCURRENTDAY = Visibility.Hidden;
                    NEXTWEEK_FRI_ISCURRENTDAY = Visibility.Hidden;

                    WEEK4_MON_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_TUE_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_WED_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_THU_ISCURRENTDAY = Visibility.Hidden;
                    WEEK4_FRI_ISCURRENTDAY = Visibility.Hidden;
                }

                foreach (DataRow row in result.Rows)
                {
                    CListMultiDayAttendance.Add(new StudentDefMultiDay
                    {
                        ID = Convert.ToInt32(row["ID"].ToString()),
                        STUDENT_FULLNAME = row["Name"].ToString(),
                        ClassID = Convert.ToInt32(row["SubjectID"].ToString()),
                        TotalAbsences = 1,
                        TotalTardiness = 0,
                        PREVWEEK_MON_ATT_CODE = row[ColumnList[0]] != DBNull.Value ? row[ColumnList[0]].ToString() : "",
                        PREVWEEK_TUE_ATT_CODE = row[ColumnList[1]] != DBNull.Value ? row[ColumnList[1]].ToString() : "",
                        PREVWEEK_WED_ATT_CODE = row[ColumnList[2]] != DBNull.Value ? row[ColumnList[2]].ToString() : "",
                        PREVWEEK_THU_ATT_CODE = row[ColumnList[3]] != DBNull.Value ? row[ColumnList[3]].ToString() : "",
                        PREVWEEK_FRI_ATT_CODE = row[ColumnList[4]] != DBNull.Value ? row[ColumnList[4]].ToString() : "",

                        CURRWEEK_MON_ATT_CODE = row[ColumnList[5]] != DBNull.Value ? row[ColumnList[5]].ToString() : "",
                        CURRWEEK_TUE_ATT_CODE = row[ColumnList[6]] != DBNull.Value ? row[ColumnList[6]].ToString() : "",
                        CURRWEEK_WED_ATT_CODE = row[ColumnList[7]] != DBNull.Value ? row[ColumnList[7]].ToString() : "",
                        CURRWEEK_THU_ATT_CODE = row[ColumnList[8]] != DBNull.Value ? row[ColumnList[8]].ToString() : "",
                        CURRWEEK_FRI_ATT_CODE = row[ColumnList[9]] != DBNull.Value ? row[ColumnList[9]].ToString() : "",

                        NEXTWEEK_MON_ATT_CODE = row[ColumnList[10]] != DBNull.Value ? row[ColumnList[10]].ToString() : "",
                        NEXTWEEK_TUE_ATT_CODE = row[ColumnList[11]] != DBNull.Value ? row[ColumnList[11]].ToString() : "",
                        NEXTWEEK_WED_ATT_CODE = row[ColumnList[12]] != DBNull.Value ? row[ColumnList[12]].ToString() : "",
                        NEXTWEEK_THU_ATT_CODE = row[ColumnList[13]] != DBNull.Value ? row[ColumnList[13]].ToString() : "",
                        NEXTWEEK_FRI_ATT_CODE = row[ColumnList[14]] != DBNull.Value ? row[ColumnList[14]].ToString() : "",

                        WEEK4_MON_ATT_CODE = row[ColumnList[15]] != DBNull.Value ? row[ColumnList[15]].ToString() : "",
                        WEEK4_TUE_ATT_CODE = row[ColumnList[16]] != DBNull.Value ? row[ColumnList[16]].ToString() : "",
                        WEEK4_WED_ATT_CODE = row[ColumnList[17]] != DBNull.Value ? row[ColumnList[17]].ToString() : "",
                        WEEK4_THU_ATT_CODE = row[ColumnList[18]] != DBNull.Value ? row[ColumnList[18]].ToString() : "",
                        WEEK4_FRI_ATT_CODE = row[ColumnList[19]] != DBNull.Value ? row[ColumnList[19]].ToString() : "",

                        PREVWEEK_MON_ISSUBMITTED = hasSubmittedEntries(ColumnList[0]),
                        PREVWEEK_TUE_ISSUBMITTED = hasSubmittedEntries(ColumnList[1]),
                        PREVWEEK_WED_ISSUBMITTED = hasSubmittedEntries(ColumnList[2]),
                        PREVWEEK_THU_ISSUBMITTED = hasSubmittedEntries(ColumnList[3]),
                        PREVWEEK_FRI_ISSUBMITTED = hasSubmittedEntries(ColumnList[4]),
                        CURRWEEK_MON_ISSUBMITTED = hasSubmittedEntries(ColumnList[5]),
                        CURRWEEK_TUE_ISSUBMITTED = hasSubmittedEntries(ColumnList[6]),
                        CURRWEEK_WED_ISSUBMITTED = hasSubmittedEntries(ColumnList[7]),
                        CURRWEEK_THU_ISSUBMITTED = hasSubmittedEntries(ColumnList[8]),
                        CURRWEEK_FRI_ISSUBMITTED = hasSubmittedEntries(ColumnList[9]),
                        NEXTWEEK_MON_ISSUBMITTED = hasSubmittedEntries(ColumnList[10]),
                        NEXTWEEK_TUE_ISSUBMITTED = hasSubmittedEntries(ColumnList[11]),
                        NEXTWEEK_WED_ISSUBMITTED = hasSubmittedEntries(ColumnList[12]),
                        NEXTWEEK_THU_ISSUBMITTED = hasSubmittedEntries(ColumnList[13]),
                        NEXTWEEK_FRI_ISSUBMITTED = hasSubmittedEntries(ColumnList[14]),
                        WEEK4_MON_ISSUBMITTED = hasSubmittedEntries(ColumnList[15]),
                        WEEK4_TUE_ISSUBMITTED = hasSubmittedEntries(ColumnList[16]),
                        WEEK4_WED_ISSUBMITTED = hasSubmittedEntries(ColumnList[17]),
                        WEEK4_THU_ISSUBMITTED = hasSubmittedEntries(ColumnList[18]),
                        WEEK4_FRI_ISSUBMITTED = hasSubmittedEntries(ColumnList[19]),

                        //PREVWEEK_MON_DATE = mondayOfLastWeek,
                        //PREVWEEK_TUE_DATE = tuesdayOfLastWeek,
                        //PREVWEEK_WED_DATE = wednesdayOfLastWeek,
                        //PREVWEEK_THU_DATE = thursdayOfLastWeek,
                        //PREVWEEK_FRI_DATE = fridayOfLastWeek,

                        //CURRWEEK_MON_DATE = mondayOfCurrentWeek,
                        //CURRWEEK_TUE_DATE = tuesdayOfCurrentWeek,
                        //CURRWEEK_WED_DATE = wednesdayOfCurrentWeek,
                        //CURRWEEK_THU_DATE = thursdayOfCurrentWeek,
                        //CURRWEEK_FRI_DATE = fridayOfCurrentWeek,

                        //NEXTWEEK_MON_DATE = mondayofNextWeek,
                        //NEXTWEEK_TUE_DATE = tuesdayofNextWeek,
                        //NEXTWEEK_WED_DATE = wednesdayofNextWeek,
                        //NEXTWEEK_THU_DATE = thursdayofNextWeek,
                        //NEXTWEEK_FRI_DATE = fridayofNextWeek
                    });
                }

                checkTotalAbsencesLates();

                DateRange = new List<DateTime>();
                for (int i = 0; i < ColumnList.Count; i++)
                {
                    DateRange.Add(Convert.ToDateTime(ColumnList[i]));
                }

                DateRange = DateRange.Except(SubmittedDates).ToList();

                Week1DateRange = Convert.ToDateTime(ColumnList[0]).ToString("MM/dd") + " - " + Convert.ToDateTime(ColumnList[4]).ToString("MM/dd");
                Week2DateRange = Convert.ToDateTime(ColumnList[5]).ToString("MM/dd") + " - " + Convert.ToDateTime(ColumnList[9]).ToString("MM/dd");
                Week3DateRange = Convert.ToDateTime(ColumnList[10]).ToString("MM/dd") + " - " + Convert.ToDateTime(ColumnList[14]).ToString("MM/dd");
                Week4DateRange = Convert.ToDateTime(ColumnList[15]).ToString("MM/dd") + " - " + Convert.ToDateTime(ColumnList[19]).ToString("MM/dd");
            }
            catch (Exception ex)
            {

            }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public string getClassName(int classID)
        {
            var subj = KirinEntities.SUBJECTS.Where(subject => subject.ID == classID).FirstOrDefault();

            ClassID = classID;
            OnPropertyChanged("ClassID");

            return subj.COURSE_NAME + " - " + subj.COURSE_CODE;
        }

        public void checkTotalAbsencesTardiness()
        {
            for (int i = 0; i < CListAttendanceMultiDay.Count; i++)
            {
                CListAttendanceMultiDay[i].TotalAbsences = (CListAttendanceMultiDay[i].PREVWEEK_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0);

                CListAttendanceMultiDay[i].TotalTardiness = (CListAttendanceMultiDay[i].PREVWEEK_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].PREVWEEK_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].CURRWEEK_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (CListAttendanceMultiDay[i].NEXTWEEK_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0);
            }
        }

        public void checkTotalAbsencesLates()
        {
            for (int i = 0; i < CListMultiDayAttendance.Count; i++)
            {
                CListMultiDayAttendance[i].TotalAbsences = (CListMultiDayAttendance[i].PREVWEEK_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].PREVWEEK_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].PREVWEEK_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].PREVWEEK_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].PREVWEEK_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].CURRWEEK_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].CURRWEEK_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].CURRWEEK_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].CURRWEEK_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].CURRWEEK_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].NEXTWEEK_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].NEXTWEEK_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].NEXTWEEK_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].NEXTWEEK_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].NEXTWEEK_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].WEEK4_MON_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].WEEK4_TUE_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].WEEK4_WED_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].WEEK4_THU_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (CListMultiDayAttendance[i].WEEK4_FRI_ATT_CODE.ToString().StartsWith("A") == true ? 1 : 0);

                CListMultiDayAttendance[i].TotalTardiness = (CListMultiDayAttendance[i].PREVWEEK_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].PREVWEEK_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].PREVWEEK_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].PREVWEEK_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].PREVWEEK_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].CURRWEEK_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].CURRWEEK_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].CURRWEEK_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].CURRWEEK_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].CURRWEEK_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].NEXTWEEK_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].NEXTWEEK_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].NEXTWEEK_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].NEXTWEEK_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].NEXTWEEK_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].WEEK4_MON_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].WEEK4_TUE_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].WEEK4_WED_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].WEEK4_THU_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0) +
                                                    (CListMultiDayAttendance[i].WEEK4_FRI_ATT_CODE.ToString().StartsWith("L") == true ? 1 : 0);
            }
        }

        public void getClassList(int classID, DateTime selectedDate)
        {
            //DateTime currDate = DateTime.Now;
            AttendanceHeaderCurrent = "Attendance: " + selectedDate.ToString("dddd, dd MMMM yyyy");

            var classAttendanceData = KirinEntities.GetClassAttendanceData(classID.ToString(), selectedDate).ToList();

            if (classAttendanceData.Count() > 0)
            {
                CList = new ObservableCollection<StudentDef>(from student in classAttendanceData
                                                             select new StudentDef
                                                             {
                                                                 ID = Convert.ToInt32(student.StudentId),
                                                                 SubjectId = student.SubjectId.ToString(),
                                                                 ClassAttendanceId = Convert.ToInt32(student.ClassAttendanceId),
                                                                 Comment = student.COMMENT,
                                                                 STUDENT_FULLNAME = student.LAST_NAME + ", " + student.FIRST_NAME,
                                                                 MEDICAL_ALERT = student.HEALTH_ALERT,
                                                                 //ATT_SPED_ALERT = student.SPECIAL_NEEDS,
                                                                 OTHER_ALERT = student.OTHER_ALERT,
                                                                 BIRTHDAY = (DateTime)student.BIRTHDATE,
                                                                 CODE = student.CODE + " (" + student.DESCRIPTION + ")",
                                                                 ATTENDANCE_CODE = new AttendanceCode
                                                                 {
                                                                     code = student.CODE,
                                                                     desc = student.DESCRIPTION,
                                                                     AttendanceCodeId = Convert.ToInt32(student.AttendanceCodeId)
                                                                 },
                                                                 //SPEDICONVISIBILITY = (a.ATT_SPED_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED"),
                                                                 BDAYICONVISIBILITY = (GetDaysUntilBirthday(Convert.ToDateTime(student.BIRTHDATE)) <= 7 ? Visibility.Visible : Visibility.Collapsed),
                                                                 MEDICALICONVISIBILITY = (student.HEALTH_ALERT.Length > 0 ? Visibility.Visible : Visibility.Collapsed),
                                                                 OTHERICONVISIBILITY = (student.OTHER_ALERT.Length > 0 ? Visibility.Visible : Visibility.Collapsed),
                                                                 SECRETARY_COMMENT = student.SECRETARY_COMMENT,
                                                                 ButtonBackground = !string.IsNullOrWhiteSpace(student.SECRETARY_COMMENT) || !string.IsNullOrWhiteSpace(student.COMMENT) ? "#247BC0" : "Gray"
                                                             });
            }
            IsCodeVisible = false;
            IsCmbCodeVisible = true;

            //CList = new ObservableCollection<StudentDef>((from classstudent in KirinEntities.CLASS_STUDENT
            //                                              join subjects in KirinEntities.SUBJECTS on classstudent.SUBJECT_ID equals subjects.ID
            //                                              join student in KirinEntities.STUDENTs on classstudent.STUDENT_ID equals student.ID
            //                                              join classAttendance in KirinEntities.CLASS_ATTENDANCE on student.ID equals classAttendance.STUDENT_ID
            //                                              where classstudent.SUBJECT_ID == classID
            //                                              && classAttendance.DATE == currDate
            //                                              select new StudentDef
            //                                              {
            //                                                  ID = student.ID,
            //                                                  STUDENT_FULLNAME = student.LAST_NAME + ", " + student.FIRST_NAME,
            //                                                  //MEDICAL_ALERT = student.HEALTH_ALERT,
            //                                                  //ATT_SPED_ALERT = student.SPECIAL_NEEDS,
            //                                                  //OTHER_ALERT = student.OTHER_ALERT,
            //                                                  BIRTHDAY = (DateTime)student.BIRTHDATE,
            //                                                  //ATTENDANCE_CODE = ""
            //                                              })
            //                                              .AsEnumerable()
            //                                              .Select(a => new StudentDef()
            //                                              {
            //                                                  ID = a.ID,
            //                                                  STUDENT_FULLNAME = a.STUDENT_FULLNAME,
            //                                                  MEDICAL_ALERT = a.MEDICAL_ALERT,
            //                                                  //ATT_SPED_ALERT = a.ATT_SPED_ALERT,
            //                                                  OTHER_ALERT = a.OTHER_ALERT,
            //                                                  BIRTHDAY = a.BIRTHDAY,
            //                                                  ATTENDANCE_CODE = a.ATTENDANCE_CODE,
            //                                                  //SPEDICONVISIBILITY = (a.ATT_SPED_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED"),
            //                                                  BDAYICONVISIBILITY = (GetDaysUntilBirthday(a.BIRTHDAY) <= 7 ? "VISIBLE" : "COLLAPSED"),
            //                                                  MEDICALICONVISIBILITY = (a.MEDICAL_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED"),
            //                                                  OTHERICONVISIBILITY = (a.OTHER_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED")
            //                                              }).ToList());


            ////if no record of attendance for the day
            //if (CList.Count == 0)
            //{
            //    //get all students of the class

            //    CList = new ObservableCollection<StudentDef>((from classstudent in KirinEntities.CLASS_STUDENT
            //                                                  join subjects in KirinEntities.SUBJECTS on classstudent.SUBJECT_ID equals subjects.ID
            //                                                  join student in KirinEntities.STUDENTs on classstudent.STUDENT_ID equals student.ID
            //                                                  where classstudent.SUBJECT_ID == classID
            //                                                  select new StudentDef
            //                                                  {
            //                                                      ID = student.ID,
            //                                                      STUDENT_FULLNAME = student.LAST_NAME + ", " + student.FIRST_NAME,
            //                                                      //MEDICAL_ALERT = student.HEALTH_ALERT,
            //                                                      //ATT_SPED_ALERT = student.SPECIAL_NEEDS,
            //                                                      //OTHER_ALERT = student.OTHER_ALERT,
            //                                                      BIRTHDAY = (DateTime)student.BIRTHDATE,
            //                                                  }).Distinct()
            //                                                  .AsEnumerable()
            //                                                  .Select(a => new StudentDef()
            //                                                  {
            //                                                      ID = a.ID,
            //                                                      STUDENT_FULLNAME = a.STUDENT_FULLNAME,
            //                                                      //MEDICAL_ALERT = a.MEDICAL_ALERT,
            //                                                      //ATT_SPED_ALERT = a.ATT_SPED_ALERT,
            //                                                      //OTHER_ALERT = a.OTHER_ALERT,
            //                                                      BIRTHDAY = a.BIRTHDAY,
            //                                                      ATTENDANCE_CODE = a.ATTENDANCE_CODE,
            //                                                      //SPEDICONVISIBILITY = (a.ATT_SPED_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED"),
            //                                                      BDAYICONVISIBILITY = (GetDaysUntilBirthday(a.BIRTHDAY) <= 7 ? "VISIBLE" : "COLLAPSED"),
            //                                                      //MEDICALICONVISIBILITY = (a.MEDICAL_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED"),
            //                                                      //OTHERICONVISIBILITY = (a.OTHER_ALERT.Length > 0 ? "VISIBLE" : "COLLAPSED"),
            //                                                      COMMENTICONVISIBILITY = "COLLAPSED" //(a.ATTENDANCE_CODE.ToString().StartsWith("L") == true ? "VISIBLE" : "COLLAPSED")
            //                                                  }).ToList());
            //}
        }

        public void getAttendanceCodes()
        {
            AttendanceCodes = new ObservableCollection<AttendanceCode>(
                    from codes in KirinEntities.ATTENDANCE_CODES.Where(c => c.CODE != "P")
                    select new AttendanceCode()
                    {
                        code = codes.CODE,
                        desc = codes.CODE + " (" + codes.DESCRIPTION + ")",
                        AttendanceCodeId = codes.ID
                    }
                );
        }

        public string getAttendanceCodeId(string code)
        {
            var attendanceCodeId = KirinEntities.ATTENDANCE_CODES.Where(c => c.CODE == code).Select(c => c.ID).FirstOrDefault();

            if (attendanceCodeId == 0) {
                attendanceCodeId = 1;
            }
            return attendanceCodeId.ToString();
        }

        private static int GetDaysUntilBirthday(DateTime birthday)
        {
            var nextBirthday = birthday.AddYears(DateTime.Today.Year - birthday.Year);
            if (nextBirthday < DateTime.Today)
            {
                nextBirthday = nextBirthday.AddYears(1);
            }
            return (nextBirthday - DateTime.Today).Days;
        }


        public void getAttendanceCodesCombobox()
        {
            AttendanceCodesCombo = new ObservableCollection<AttendanceCode>(
                    from codes in KirinEntities.ATTENDANCE_CODES.Where(c => c.CODE != "P")
                    select new AttendanceCode()
                    {
                        code = codes.CODE,
                        desc = codes.CODE + " (" + codes.DESCRIPTION + ")",
                        AttendanceCodeId = codes.ID
                    }
                );
        }

        private void SaveAttendance(object param)
        {
            var date = AttendanceDate.ToString("yyyy/MM/dd");
            var currentDate = DateTime.Now;
            TimeSpan ts = currentDate - AttendanceDate;

            if (ts.TotalHours <= 24)
            {
                var x = (ObservableCollection<StudentDef>)param;

                for (int i = 0; i < x.Count; i++)
                {
                    CLASS_ATTENDANCE ca = new CLASS_ATTENDANCE();
                    ca.STUDENT_ID = x[i].ID;
                    ca.SUBJECT_ID = ClassID;
                    ca.DATE = Convert.ToDateTime(date);
                    ca.ATTENDANCE_CODE = x[i].ATTENDANCE_CODE?.code == null ? "" : x[i].ATTENDANCE_CODE.AttendanceCodeId.ToString();

                    //to be implemented separately
                    ca.COMMENT = "";

                    if (KirinEntities.CLASS_ATTENDANCE.Where(c => c.SUBJECT_ID == ca.SUBJECT_ID
                         && c.STUDENT_ID == ca.STUDENT_ID && c.DATE == ca.DATE).Select(c => c.ID).Count() > 0)
                    {
                        //Update Existing Record
                        CLASS_ATTENDANCE update = KirinEntities.CLASS_ATTENDANCE.Where(c => c.SUBJECT_ID == ca.SUBJECT_ID
                         && c.STUDENT_ID == ca.STUDENT_ID && c.DATE == ca.DATE).FirstOrDefault();

                        update.ATTENDANCE_CODE = ca.ATTENDANCE_CODE;
                        KirinEntities.SaveChanges();
                    }
                    else
                    {
                        KirinEntities.CLASS_ATTENDANCE.Add(ca);
                        KirinEntities.SaveChanges();
                    }
                    //attendancelist.Add(ca);
                }

                MessageBox.Show("Class Attendence saved successfully!");
                getClassList(Convert.ToInt32(subjectId), AttendanceDate);
            }
            else
            {
                MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (mw != null)
                {
                    string subjectId = App.Current.Properties["SubjectId"].ToString();
                    //SINGLE DAY VIEW
                    mw.MainWindowFrame.Content = new ClassAttendance(subjectId, DateTime.Now);
                }
                MessageBox.Show("Cannot Update Attendance as Selected Date has expired");
            }
        }

        private void SaveMultiDayAttendance(object param)
        {
            var date = AttendanceDate.ToString("yyyy/MM/dd");
            var studentDefMultiDays = (ObservableCollection<StudentDefMultiDay>)param;
            string dayofweek = AttendanceDate.ToString("ddd");

            List<CLASS_ATTENDANCE> attendancelist = new List<CLASS_ATTENDANCE>();

            for (int j = 0; j < DateRange.Count; j++)
            {
                for (int i = 0; i < studentDefMultiDays.Count; i++)
                {
                    CLASS_ATTENDANCE ca = new CLASS_ATTENDANCE();
                    ca.STUDENT_ID = studentDefMultiDays[i].ID;
                    ca.SUBJECT_ID = ClassID;

                    ca.DATE = Convert.ToDateTime(date);
                    //ca.DATE = DateRange[j];
                    ca.COMMENT = "";

                    string attendanceCode = string.Empty;
                    if (DateRange[j].ToString("yyyy/MM/dd") == date)
                    {
                        if (dayofweek.ToUpper() == "MON")
                        {
                            attendanceCode = studentDefMultiDays[i].CURRWEEK_MON_ATT_CODE;
                        }
                        else if (dayofweek.ToUpper() == "TUE")
                        {
                            attendanceCode = studentDefMultiDays[i].CURRWEEK_TUE_ATT_CODE;
                        }
                        else if (dayofweek.ToUpper() == "WED")
                        {
                            attendanceCode = studentDefMultiDays[i].CURRWEEK_WED_ATT_CODE;
                        }
                        else if (dayofweek.ToUpper() == "THU")
                        {
                            attendanceCode = studentDefMultiDays[i].CURRWEEK_THU_ATT_CODE;
                        }
                        else if (dayofweek.ToUpper() == "FRI")
                        {
                            attendanceCode = studentDefMultiDays[i].CURRWEEK_FRI_ATT_CODE;
                        }
                        
                        ca.ATTENDANCE_CODE = getAttendanceCodeId(attendanceCode);

                        if (KirinEntities.CLASS_ATTENDANCE.Where(c => c.SUBJECT_ID == ca.SUBJECT_ID
                       && c.STUDENT_ID == ca.STUDENT_ID && c.DATE == ca.DATE).Select(c => c.ID).Count() > 0)
                        {
                            //Update Existing Record
                            CLASS_ATTENDANCE update = KirinEntities.CLASS_ATTENDANCE.Where(c => c.SUBJECT_ID == ca.SUBJECT_ID
                             && c.STUDENT_ID == ca.STUDENT_ID && c.DATE == ca.DATE).FirstOrDefault();

                            update.ATTENDANCE_CODE = ca.ATTENDANCE_CODE;
                            KirinEntities.SaveChanges();
                        }
                        else
                        {
                            KirinEntities.CLASS_ATTENDANCE.Add(ca);
                            KirinEntities.SaveChanges();
                        }
                    }


                    //if (DateRange[j] == studentDefMultiDays[i].PREVWEEK_MON_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].PREVWEEK_MON_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].PREVWEEK_TUE_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].PREVWEEK_TUE_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].PREVWEEK_WED_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].PREVWEEK_WED_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].PREVWEEK_THU_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].PREVWEEK_THU_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].PREVWEEK_FRI_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].PREVWEEK_FRI_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].CURRWEEK_MON_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].CURRWEEK_MON_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].CURRWEEK_TUE_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].CURRWEEK_TUE_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].CURRWEEK_WED_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].CURRWEEK_WED_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].CURRWEEK_THU_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].CURRWEEK_THU_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].CURRWEEK_FRI_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].CURRWEEK_FRI_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].NEXTWEEK_MON_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].NEXTWEEK_MON_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].NEXTWEEK_TUE_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].NEXTWEEK_TUE_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].NEXTWEEK_WED_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].NEXTWEEK_WED_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].NEXTWEEK_THU_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].NEXTWEEK_THU_ATT_CODE;
                    //}
                    //if (DateRange[j] == studentDefMultiDays[i].NEXTWEEK_FRI_DATE)
                    //{
                    //    ca.ATTENDANCE_CODE = studentDefMultiDays[i].NEXTWEEK_FRI_ATT_CODE;
                    //}


                    //attendancelist.Add(ca);
                    //KirinEntities.CLASS_ATTENDANCE.AddRange(attendancelist);
                }
            }
            KirinEntities.SaveChanges();
        }

        private void ViewHealthAlert(object param)
        {
            //string details = param.ToString();
            var student = (StudentDef)param;
            Alerts alertBox = new Alerts("Medical", "Medical Alert " + student.STUDENT_FULLNAME, student.STUDENT_FULLNAME, student.MEDICAL_ALERT, "Alert Expires: Never");
            alertBox.Show();
            //MessageBox.Show(student.MEDICAL_ALERT + "<br/><br/>"+"Alert Expires: Never" , "Medical Alert "+ student.STUDENT_FULLNAME,MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ViewComment(object param)
        {
            var student = (StudentDef)param;

            STUDENT_NAME = student.STUDENT_FULLNAME;
            app.attendanceComment.Visibility = Visibility.Visible;
            app.attendanceComment.ClassAttendanceId.Content = student.ClassAttendanceId.ToString();
            app.attendanceComment.SubjectId.Content = student.SubjectId;
            app.attendanceComment.SchoolId.Content = schoolId;

            if (!string.IsNullOrWhiteSpace(student.SECRETARY_COMMENT))
            {
                app.attendanceComment.StudentId.Content = student.ID.ToString();
                app.attendanceComment.StudentName.Content = student.STUDENT_FULLNAME;
                app.attendanceComment.commentbox.Text = student.Comment;
                app.attendanceComment.message.Text = student.SECRETARY_COMMENT;
                app.attendanceComment.GridMessage.Visibility = Visibility.Visible;
                app.attendanceComment.GridComment.Visibility = Visibility.Hidden;
            }
            else
            {
                app.attendanceComment.StudentId.Content = student.ID.ToString();
                app.attendanceComment.StudentName2.Content = student.STUDENT_FULLNAME;
                app.attendanceComment.commentbox2.Text = student.Comment;
                app.attendanceComment.GridComment.Visibility = Visibility.Visible;
                app.attendanceComment.GridMessage.Visibility = Visibility.Hidden;
            }
        }

        private void ViewBdayAlert(object param)
        {
            string details = param.ToString();
            var student = (StudentDef)param;
            Alerts alertBox = new Alerts("Birthday", "Birthday Alert", student.STUDENT_FULLNAME, student.STUDENT_FULLNAME + " birthday is on " + student.BIRTHDAY.Month + "/" + student.BIRTHDAY.Day, "");
            alertBox.Show();
            // MessageBox.Show(student.STUDENT_FULLNAME +"<br/> birthday is on "+ student.BIRTHDAY.Month +"/" + student.BIRTHDAY.Day, "Birthday Alert " , MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewSPEDAlert(object param)
        {
            string details = param.ToString();
            var student = (StudentDef)param;
            Alerts alertBox = new Alerts("SPED", "Att Sped Alert", student.STUDENT_FULLNAME, "Special Education", "");
            alertBox.Show();
            //MessageBox.Show(student.STUDENT_FULLNAME + "</br> Special Education", "Att Sped Alert " , MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ViewOtherAlert(object param)
        {
            string details = param.ToString();
            var student = (StudentDef)param;
            Alerts alertBox = new Alerts("Other", "Other Alert", student.STUDENT_FULLNAME, "Mother is deceased", "Alert Expires: Never");
            alertBox.Show();
            //MessageBox.Show(student.STUDENT_FULLNAME +"<br/>" + student.OTHER_ALERT, "Other Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void CheckCommentIconVisibility(object param)
        {
            var cmb = (ComboBox)param;
            AttendanceCode content = (AttendanceCode)cmb.SelectedItem;
            //if (content != null && content.code.StartsWith("L"))
            //{
            //    COMMENTICONVISIBILITY = Visibility.Hidden;
            //}
        }

        private void EditDateRange(object param)
        {
            CustomMsgBox customMsgBox = new CustomMsgBox();
            customMsgBox.DataContext = param;
            customMsgBox.ShowDialog();
        }

        public static DataTable GetMultiDayAttendanceData(string fromDate, string toDate)
        {
            SqlConnection sqlCon = null;
            DataTable dt = new DataTable();

            try
            {
                using (sqlCon = new SqlConnection(Utils.GetConnectionStrings()))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("GetMultiClassAttendanceData", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@SchoolID", SqlDbType.NVarChar).Value = schoolId;
                    sql_cmnd.Parameters.AddWithValue("@SubjectID", SqlDbType.NVarChar).Value = subjectId;
                    sql_cmnd.Parameters.AddWithValue("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                    sql_cmnd.Parameters.AddWithValue("@ToDate", SqlDbType.NVarChar).Value = toDate;

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
    }

    public class StudentDef : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int ClassAttendanceId { get; set; }
        public string Comment { get; set; }
        public string SubjectId { get; set; }
        public string STUDENT_FULLNAME { get; set; }
        public string MEDICAL_ALERT { get; set; }
        public DateTime BIRTHDAY { get; set; }
        public string ATT_SPED_ALERT { get; set; }
        public string OTHER_ALERT { get; set; }
        public string CODE { get; set; }
        public string SECRETARY_COMMENT { get; set; }

        private string _ButtonBackground;
        public string ButtonBackground
        {
            get { return _ButtonBackground; }
            set
            {
                _ButtonBackground = value;
                OnPropertyChanged("ButtonBackground");
            }
        }


        private string _COMMENTICONVISIBILITY;

        public string COMMENTICONVISIBILITY
        {
            get { return _COMMENTICONVISIBILITY; }
            set
            {
                _COMMENTICONVISIBILITY = value;
                OnPropertyChanged("COMMENTICONVISIBILITY");
            }
        }

        private AttendanceCode _ATTENDANCE_CODE;

        public AttendanceCode ATTENDANCE_CODE
        {
            get { return _ATTENDANCE_CODE; }
            set
            {
                if (_ATTENDANCE_CODE == value)
                    return;

                _ATTENDANCE_CODE = value;

                OnPropertyChanged("ATTENDANCE_CODE");
                if (value != null && (value.desc.ToString().StartsWith("L") || value.desc.ToString().Contains("Uninformed")))
                {
                    COMMENTICONVISIBILITY = "VISIBLE";
                }
                else
                {
                    COMMENTICONVISIBILITY = "COLLAPSED";
                }
            }
        }

        public Visibility BDAYICONVISIBILITY { get; set; }
        public Visibility MEDICALICONVISIBILITY { get; set; }
        public Visibility OTHERICONVISIBILITY { get; set; }
        public Visibility SPEDICONVISIBILITY { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AttendanceCode
    {
        public string code { get; set; }
        public string desc { get; set; }
        public string attendance_code { get; set; }
        public int AttendanceCodeId { get; set; }
    }

    public class StudentDefMultiDay : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string STUDENT_FULLNAME { get; set; }
        public int ClassID { get; set; }

        private string _PREVWEEK_MON_ATT_CODE;

        public string PREVWEEK_MON_ATT_CODE
        {
            get { return _PREVWEEK_MON_ATT_CODE; }
            set
            {
                _PREVWEEK_MON_ATT_CODE = value;
                OnPropertyChanged("PREVWEEK_MON_ATT_CODE");
            }
        }

        private string _PREVWEEK_TUE_ATT_CODE;

        public string PREVWEEK_TUE_ATT_CODE
        {
            get { return _PREVWEEK_TUE_ATT_CODE; }
            set
            {
                _PREVWEEK_TUE_ATT_CODE = value;
                OnPropertyChanged("PREVWEEK_TUE_ATT_CODE");
            }
        }

        private string _PREVWEEK_WED_ATT_CODE;

        public string PREVWEEK_WED_ATT_CODE
        {
            get { return _PREVWEEK_WED_ATT_CODE; }
            set
            {
                _PREVWEEK_WED_ATT_CODE = value;
                OnPropertyChanged("PREVWEEK_WED_ATT_CODE");
            }
        }

        private string _PREVWEEK_THU_ATT_CODE;

        public string PREVWEEK_THU_ATT_CODE
        {
            get { return _PREVWEEK_THU_ATT_CODE; }
            set
            {
                _PREVWEEK_THU_ATT_CODE = value;
                OnPropertyChanged("PREVWEEK_THU_ATT_CODE");
            }
        }

        private string _PREVWEEK_FRI_ATT_CODE;

        public string PREVWEEK_FRI_ATT_CODE
        {
            get { return _PREVWEEK_FRI_ATT_CODE; }
            set
            {
                _PREVWEEK_FRI_ATT_CODE = value;
                OnPropertyChanged("PREVWEEK_FRI_ATT_CODE");
            }
        }


        private string _CURRWEEK_MON_ATT_CODE;

        public string CURRWEEK_MON_ATT_CODE
        {
            get { return _CURRWEEK_MON_ATT_CODE; }
            set
            {
                _CURRWEEK_MON_ATT_CODE = value;
                OnPropertyChanged("CURRWEEK_MON_ATT_CODE");
            }
        }

        private string _CURRWEEK_TUE_ATT_CODE;

        public string CURRWEEK_TUE_ATT_CODE
        {
            get { return _CURRWEEK_TUE_ATT_CODE; }
            set
            {
                _CURRWEEK_TUE_ATT_CODE = value;
                OnPropertyChanged("CURRWEEK_TUE_ATT_CODE");
            }
        }

        private string _CURRWEEK_WED_ATT_CODE;

        public string CURRWEEK_WED_ATT_CODE
        {
            get { return _CURRWEEK_WED_ATT_CODE; }
            set
            {
                _CURRWEEK_WED_ATT_CODE = value;
                OnPropertyChanged("CURRWEEK_WED_ATT_CODE");
            }
        }

        private string _CURRWEEK_THU_ATT_CODE;

        public string CURRWEEK_THU_ATT_CODE
        {
            get { return _CURRWEEK_THU_ATT_CODE; }
            set
            {
                _CURRWEEK_THU_ATT_CODE = value;
                OnPropertyChanged("CURRWEEK_THU_ATT_CODE");
            }
        }

        private string _CURRWEEK_FRI_ATT_CODE;

        public string CURRWEEK_FRI_ATT_CODE
        {
            get { return _CURRWEEK_FRI_ATT_CODE; }
            set
            {
                _CURRWEEK_FRI_ATT_CODE = value;
                OnPropertyChanged("CURRWEEK_FRI_ATT_CODE");
            }
        }

        private string _NEXTWEEK_MON_ATT_CODE;

        public string NEXTWEEK_MON_ATT_CODE
        {
            get { return _NEXTWEEK_MON_ATT_CODE; }
            set
            {
                _NEXTWEEK_MON_ATT_CODE = value;
                OnPropertyChanged("NEXTWEEK_MON_ATT_CODE");
            }
        }

        private string _NEXTWEEK_TUE_ATT_CODE;

        public string NEXTWEEK_TUE_ATT_CODE
        {
            get { return _NEXTWEEK_TUE_ATT_CODE; }
            set
            {
                _NEXTWEEK_TUE_ATT_CODE = value;
                OnPropertyChanged("NEXTWEEK_TUE_ATT_CODE");
            }
        }

        private string _NEXTWEEK_WED_ATT_CODE;

        public string NEXTWEEK_WED_ATT_CODE
        {
            get { return _NEXTWEEK_WED_ATT_CODE; }
            set
            {
                _NEXTWEEK_WED_ATT_CODE = value;
                OnPropertyChanged("NEXTWEEK_WED_ATT_CODE");
            }
        }

        private string _NEXTWEEK_THU_ATT_CODE;

        public string NEXTWEEK_THU_ATT_CODE
        {
            get { return _NEXTWEEK_THU_ATT_CODE; }
            set
            {
                _NEXTWEEK_THU_ATT_CODE = value;
                OnPropertyChanged("NEXTWEEK_THU_ATT_CODE");
            }
        }

        private string _NEXTWEEK_FRI_ATT_CODE;

        public string NEXTWEEK_FRI_ATT_CODE
        {
            get { return _NEXTWEEK_FRI_ATT_CODE; }
            set
            {
                _NEXTWEEK_FRI_ATT_CODE = value;
                OnPropertyChanged("NEXTWEEK_FRI_ATT_CODE");
            }
        }

        private string _WEEK4_MON_ATT_CODE;

        public string WEEK4_MON_ATT_CODE
        {
            get { return _WEEK4_MON_ATT_CODE; }
            set
            {
                _WEEK4_MON_ATT_CODE = value;
                OnPropertyChanged("WEEK4_MON_ATT_CODE");
            }
        }

        private string _WEEK4_TUE_ATT_CODE;

        public string WEEK4_TUE_ATT_CODE
        {
            get { return _WEEK4_TUE_ATT_CODE; }
            set
            {
                _WEEK4_TUE_ATT_CODE = value;
                OnPropertyChanged("WEEK4_TUE_ATT_CODE");
            }
        }

        private string _WEEK4_WED_ATT_CODE;

        public string WEEK4_WED_ATT_CODE
        {
            get { return _WEEK4_WED_ATT_CODE; }
            set
            {
                _WEEK4_WED_ATT_CODE = value;
                OnPropertyChanged("WEEK4_WED_ATT_CODE");
            }
        }

        private string _WEEK4_THU_ATT_CODE;

        public string WEEK4_THU_ATT_CODE
        {
            get { return _WEEK4_THU_ATT_CODE; }
            set
            {
                _WEEK4_THU_ATT_CODE = value;
                OnPropertyChanged("WEEK4_THU_ATT_CODE");
            }
        }

        private string _WEEK4_FRI_ATT_CODE;

        public string WEEK4_FRI_ATT_CODE
        {
            get { return _WEEK4_FRI_ATT_CODE; }
            set
            {
                _WEEK4_FRI_ATT_CODE = value;
                OnPropertyChanged("WEEK4_FRI_ATT_CODE");
            }
        }


        public DateTime PREVWEEK_MON_DATE { get; set; }
        public DateTime PREVWEEK_TUE_DATE { get; set; }
        public DateTime PREVWEEK_WED_DATE { get; set; }
        public DateTime PREVWEEK_THU_DATE { get; set; }
        public DateTime PREVWEEK_FRI_DATE { get; set; }

        public DateTime CURRWEEK_MON_DATE { get; set; }
        public DateTime CURRWEEK_TUE_DATE { get; set; }
        public DateTime CURRWEEK_WED_DATE { get; set; }
        public DateTime CURRWEEK_THU_DATE { get; set; }
        public DateTime CURRWEEK_FRI_DATE { get; set; }

        public DateTime NEXTWEEK_MON_DATE { get; set; }
        public DateTime NEXTWEEK_TUE_DATE { get; set; }
        public DateTime NEXTWEEK_WED_DATE { get; set; }
        public DateTime NEXTWEEK_THU_DATE { get; set; }
        public DateTime NEXTWEEK_FRI_DATE { get; set; }

        public DateTime WEEK4_MON_DATE { get; set; }
        public DateTime WEEK4_TUE_DATE { get; set; }
        public DateTime WEEK4_WED_DATE { get; set; }
        public DateTime WEEK4_THU_DATE { get; set; }
        public DateTime WEEK4_FRI_DATE { get; set; }

        //BINDING FOR GRAY COLORED DATES
        public Visibility PREVWEEK_MON_ISSUBMITTED { get; set; }
        public Visibility PREVWEEK_TUE_ISSUBMITTED { get; set; }
        public Visibility PREVWEEK_WED_ISSUBMITTED { get; set; }
        public Visibility PREVWEEK_THU_ISSUBMITTED { get; set; }
        public Visibility PREVWEEK_FRI_ISSUBMITTED { get; set; }
        public Visibility CURRWEEK_MON_ISSUBMITTED { get; set; }
        public Visibility CURRWEEK_TUE_ISSUBMITTED { get; set; }
        public Visibility CURRWEEK_WED_ISSUBMITTED { get; set; }
        public Visibility CURRWEEK_THU_ISSUBMITTED { get; set; }
        public Visibility CURRWEEK_FRI_ISSUBMITTED { get; set; }
        public Visibility NEXTWEEK_MON_ISSUBMITTED { get; set; }
        public Visibility NEXTWEEK_TUE_ISSUBMITTED { get; set; }
        public Visibility NEXTWEEK_WED_ISSUBMITTED { get; set; }
        public Visibility NEXTWEEK_THU_ISSUBMITTED { get; set; }
        public Visibility NEXTWEEK_FRI_ISSUBMITTED { get; set; }
        public Visibility WEEK4_MON_ISSUBMITTED { get; set; }
        public Visibility WEEK4_TUE_ISSUBMITTED { get; set; }
        public Visibility WEEK4_WED_ISSUBMITTED { get; set; }
        public Visibility WEEK4_THU_ISSUBMITTED { get; set; }
        public Visibility WEEK4_FRI_ISSUBMITTED { get; set; }


        private int totalAbsences;

        public int TotalAbsences
        {
            get { return totalAbsences; }
            set
            {
                totalAbsences = value;
                OnPropertyChanged("TotalAbsences");
            }
        }

        private int totalTardiness;

        public int TotalTardiness
        {
            get { return totalTardiness; }
            set
            {
                totalTardiness = value;
                OnPropertyChanged("TotalTardiness");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
