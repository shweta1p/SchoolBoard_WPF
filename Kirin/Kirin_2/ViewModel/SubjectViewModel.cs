using Kirin_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DayOfWeek = System.DayOfWeek;

namespace Kirin_2.ViewModel
{
    public class SubjectViewModel : INotifyPropertyChanged
    {
        public IEnumerable<SchoolData> _schooldata { get; set; }
        public IEnumerable<ClassData> _classdata { get; set; }
        public IEnumerable<ExtraCoData> _extracodata { get; set; }
        public ICommand SaveSubjectStatus { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string userName = string.Empty;

        public IEnumerable<ClassData> _subjectdata { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Class_DayofWeek> _subjectSchedule;
        public ObservableCollection<Class_DayofWeek> SubjectSchedule
        {
            get { return _subjectSchedule; }
            set
            {
                _subjectSchedule = value;
                OnPropertyChanged(nameof(SubjectSchedule));
            }
        }


        private ObservableCollection<Class_DayofWeek> _selectedsubjectSchedule;
        public ObservableCollection<Class_DayofWeek> SelectedSubjectSchedule
        {
            get { return _selectedsubjectSchedule; }
            set
            {
                _subjectSchedule = value;
                _selectedsubjectSchedule = new ObservableCollection<Class_DayofWeek>(SubjectSchedule.Where(subSched => subSched.ClassID == SelectedSubject.ID));


                //for (int i = 0; i < _selectedsubjectSchedule.Count; i++)
                //{
                //    _selectedsubjectSchedule[i].DayOfWeekDesc = Enum.GetName(typeof(DayOfWeek), _selectedsubjectSchedule[i].DayOfWeekID-1);
                //}
                OnPropertyChanged(nameof(SelectedSubjectSchedule));

            }
        }

        public int isCurrentlyActive(int subjectID)
        {
            int res = 0;
            DateTime dt = DateTime.Now;
            int currentDayOfWeek;
            TimeSpan currentTime;

            currentDayOfWeek = (int)dt.DayOfWeek + 1;
            currentTime = dt.TimeOfDay;

            //get schedule of current subject
            var subjectsched = kirinEntities.Class_DayofWeek.Where(subj => subj.ClassID == subjectID)
                                                .Where(subj => subj.DayOfWeekID == currentDayOfWeek)
                                                .Where(subj => currentTime >= subj.StartTime && currentTime <= subj.EndTime).ToList();

            if (subjectsched.Count >= 1)
            {
                res = 1;
            }
            return res;


            //(Class_DayofWeek)subjectEntities..Where(subj => subj.ID == subjectID).First();
        }



        private SUBJECT _subject;

        public SUBJECT Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        private string dayOfWeekDesc;

        public string DayOfWeekDesc
        {
            get { return dayOfWeekDesc; }
            set
            {
                dayOfWeekDesc = value;
                OnPropertyChanged("DayOfWeekDesc");
            }
        }


        private ObservableCollection<SUBJECT> _lstSubject;

        public ObservableCollection<SUBJECT> LstSubject
        {
            get { return _lstSubject; }
            set
            {
                _lstSubject = value;
                OnPropertyChanged("LstSubject");
            }
        }

        private ObservableCollection<SUBJECT> _filteredLstSubject;
        public ObservableCollection<SUBJECT> FilteredLstSubject
        {
            get { return _filteredLstSubject; }
            set
            {
                _filteredLstSubject = value;
                OnPropertyChanged("FilteredLstSubject");
            }
        }

        KIRINEntities kirinEntities;
        public SubjectViewModel()
        {
            kirinEntities = new KIRINEntities();

            if (App.Current != null)
            {
                userName = App.Current.Properties["USERNAME"].ToString();
            }
            //else
            //{
            //    userName = "dduke@sabc.on.ca";
            //}

            SaveSubjectStatus = new CommandVM(SaveStatus, canExecuteMethod);

            LoadSubject();

            FillSchoolData(userName);
        }

        public void FillSchoolData(string userName)
        {
            var schoolData = kirinEntities.GetSchoolNameforUser(userName).ToList();
            ObservableCollection<SchoolData> schoolList = new ObservableCollection<SchoolData>();

            if (schoolData.Count > 0)
            {
                foreach (var school in schoolData)
                {
                    schoolList.Add(new SchoolData
                    {
                        SchoolName = school.SCHOOL_NAME,
                        Email = school.EMAIL,
                        Phone = school.PHONE_NUMBER,
                        SchoolId = school.SchoolId.ToString(),
                        TeacherId = school.TeacherID.ToString(),
                        Semester = school.Semester,
                        SemesterId = school.SemesterId.ToString()
                    });
                }
                _schooldata = schoolList;

                string schoolId = schoolList[0].SchoolId;
                string teacherId = schoolList[0].TeacherId;

                FillSelectedStaffData(teacherId, schoolId);
                FillSelectedSubjectData(teacherId, schoolId);
            }

        }

        public void FillSelectedStaffData(string teacherId, string schoolId)
        {
            ObservableCollection<ClassData> classList = new ObservableCollection<ClassData>();
            ObservableCollection<ExtraCoData> extracoList = new ObservableCollection<ExtraCoData>();

            var classData = kirinEntities.GetClassDataforTeacher(teacherId, schoolId).ToList();
            foreach (var c in classData)
            {
                classList.Add(new ClassData
                {
                    ClassName = c.COURSE_NAME,
                    ClassCode = c.COURSE_CODE,
                    SchoolId = c.SCHOOL_ID.ToString(),
                    TeacherId = c.TeacherID.ToString(),
                    ClassId = c.SubjectID.ToString()
                });
            }
            //classList.Add(new ClassData
            //{
            //    ClassName = "",
            //    ClassCode = "",
            //    SchoolId = schoolId,
            //    TeacherId = teacherId,
            //    ClassId = ""
            //});
            //classList.Add(new ClassData
            //{
            //    ClassName = "",
            //    ClassCode = "",
            //    SchoolId = schoolId,
            //    TeacherId = teacherId,
            //    ClassId = ""
            //});
            //classList.Add(new ClassData
            //{
            //    ClassName = "",
            //    ClassCode = "",
            //    SchoolId = schoolId,
            //    TeacherId = teacherId,
            //    ClassId = ""
            //});
            //classList.Add(new ClassData
            //{
            //    ClassName = "",
            //    ClassCode = "",
            //    SchoolId = schoolId,
            //    TeacherId = teacherId,
            //    ClassId = ""
            //});
            //classList.Add(new ClassData
            //{
            //    ClassName = "",
            //    ClassCode = "",
            //    SchoolId = schoolId,
            //    TeacherId = teacherId,
            //    ClassId = ""
            //});
            //classList.Add(new ClassData
            //{
            //    ClassName = "",
            //    ClassCode = "",
            //    SchoolId = schoolId,
            //    TeacherId = teacherId,
            //    ClassId = ""
            //});
            _classdata = classList;

            var extraCoData = kirinEntities.GetExtraCoDataforTeacher(teacherId, schoolId).ToList();
            foreach (var extraco in extraCoData)
            {
                extracoList.Add(new ExtraCoData
                {
                    ExtraCoName = extraco.EXTRA_CO,
                    ExtraCoCode = extraco.ExtraCoId.ToString(),
                    SchoolId = extraco.SCHOOL_ID.ToString(),
                    TeacherId = extraco.TeacherID.ToString()
                });
            }
            _extracodata = extracoList;
        }

        public void FillSelectedSubjectData(string teacherId, string schoolId)
        {
            ObservableCollection<ClassData> classList = new ObservableCollection<ClassData>();
            //ObservableCollection<ExtraCoData> extracoList = new ObservableCollection<ExtraCoData>();

            var classData = kirinEntities.GetClassDataforTeacher(teacherId, schoolId).ToList();
            foreach (var c in classData)
            {
                classList.Add(new ClassData
                {
                    ClassName = c.COURSE_NAME,
                    ClassCode = c.COURSE_CODE,
                    SchoolId = c.SCHOOL_ID.ToString(),
                    TeacherId = c.TeacherID.ToString(),
                    ClassId = c.SubjectID.ToString(),
                    visibility = Visibility.Visible
                });
            }

            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            classList.Add(new ClassData
            {
                ClassName = "",
                ClassCode = "",
                SchoolId = schoolId,
                TeacherId = teacherId,
                ClassId = "",
                visibility = Visibility.Hidden
            });
            
            _subjectdata = classList;

            //var extraCoData = kirinEntities.GetExtraCoDataforTeacher(teacherId, schoolId).ToList();
            //foreach (var extraco in extraCoData)
            //{
            //    extracoList.Add(new ExtraCoData
            //    {
            //        ExtraCoName = extraco.EXTRA_CO,
            //        ExtraCoCode = extraco.ExtraCoId.ToString(),
            //        SchoolId = extraco.SCHOOL_ID.ToString(),
            //        TeacherId = extraco.TeacherID.ToString()
            //    });
            //}
            //_extracodata = extracoList;
        }


        private void LoadSubject()
        {

            LstSubject = new ObservableCollection<SUBJECT>(this.kirinEntities.SUBJECTS);
            SubjectSchedule = new ObservableCollection<Class_DayofWeek>(kirinEntities.Class_DayofWeek);


            //for (int i = 0; i < LstSubject.Count; i++)
            //{
            //    LstSubject[i].isActive = isCurrentlyActive(LstSubject[i].ID);
            //}

        }

        private SCHOOL _selectedSchool;
        public SCHOOL SelectedSchool
        {
            get { return _selectedSchool; }
            set
            {
                _selectedSchool = value;
                OnPropertyChanged("SelectedSchool");
                FilteredLstSubject = new ObservableCollection<SUBJECT>(LstSubject.Where(sub => sub.SCHOOL_ID == SelectedSchool.ID));
                // FilteredLstSubject = LstSubject.Where(sub => sub.SCHOOL_ID == SelectedSchool.ID) as ObservableCollection<SUBJECT>;

            }
        }


        private SUBJECT _selectedSubject;
        public SUBJECT SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                OnPropertyChanged("SelectedSubject");
                //  Subject = (SUBJECT)subjectEntities.SUBJECTS.Where(subj => subj.ID == Subject.ID);
            }
        }

        public SUBJECT getSubject(int id)
        {

            return (SUBJECT)kirinEntities.SUBJECTS.Where(subj => subj.ID == id).First();

        }

        public ObservableCollection<Class_DayofWeek> getSubjectSchedule(int id)
        {
            SubjectSchedule = new ObservableCollection<Class_DayofWeek>(SubjectSchedule.Where(subSched => subSched.ClassID == id));
            return SubjectSchedule;
        }


        public bool canExecuteMethod(object param)
        {
            return true;
        }

        private void SaveStatus(object param)
        {

            var sub = (SUBJECT)param;
            //save active status of selected subject in DB
            SUBJECT subj = (from s in kirinEntities.SUBJECTS
                            where s.ID == sub.ID
                            select s).SingleOrDefault();
            //subj.isActive = sub.isActive;

            kirinEntities.SaveChanges();

            //not reflecting :(
            OnPropertyChanged("LstSubject");
            OnPropertyChanged("FilteredLstSubject");
        }


        private ObservableCollection<classDetails> _CLASSLIST;

        public ObservableCollection<classDetails> CLASSLIST
        {
            get { return _CLASSLIST; }
            set
            {
                _CLASSLIST = value;
                OnPropertyChanged("CLASSLIST");
            }
        }

    }

    public class SchoolData
    {
        public string SchoolName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SchoolId { get; set; }
        public string TeacherId { get; set; }
        public string Semester { get; set; }
        public string SemesterId { get; set; }
    }

    public class ClassData
    {
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public string ClassId { get; set; }
        public string SchoolId { get; set; }
        public string TeacherId { get; set; }
        public Visibility visibility { get; set; }

    }

    public class ExtraCoData
    {
        public string ExtraCoName { get; set; }
        public string ExtraCoCode { get; set; }
        public string SchoolId { get; set; }
        public string TeacherId { get; set; }
    }

}
