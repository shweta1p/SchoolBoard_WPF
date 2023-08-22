﻿using Kirin_2.Models;
using Kirin_2.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kirin_2.ViewModel
{

    public class GradeBookViewModel : INotifyPropertyChanged
    {
        public App app;
        public ICommand cDisplayNewColumn { get; set; }
        public ICommand cViewStudentProfile { get; set; }
        public ICommand cAddNewAssignmentItemCommand { get; set; }
        public ICommand cSaveAssignmentCommand { get; set; }
        public ICommand cDeleteAssignmentItemCommand { get; set; }
        public ICommand cViewStudentScoreSheet { get; set; }
        public ICommand cDisplayColumns { get; set; }

        public ICommand c_UC_LearningSkills { get; set; }


        KIRINEntities1 KirinEntities;
        public GradeBookViewModel()
        {
            app = (App)Application.Current;
            KirinEntities = new KIRINEntities1();

            cDisplayNewColumn = new CommandVM(DisplayNewColumn, canExecuteMethod);
            cViewStudentProfile = new CommandVM(ViewStudentProfile, canExecuteMethod);
            cAddNewAssignmentItemCommand = new CommandVM(AddNewAssignmentItemCommand, canExecuteMethod);
            cSaveAssignmentCommand = new CommandVM(SaveAssignmentCommand, canExecuteMethod);
            cDeleteAssignmentItemCommand = new CommandVM(DeleteAssignmentItemCommand, canExecuteMethod);
            cDisplayColumns = new CommandVM(DisplayColumnsCommand, canExecuteMethod);
            c_UC_LearningSkills = new CommandVM(show_UC_Responsibility, canExecuteMethod);
            //  cViewStudentScoreSheet = new CommandVM(ViewStudentScoreSheet, canExecuteMethod);

            getAssignmentCategory();

            CMBLIST = new ObservableCollection<cmbItem>(){
                  new cmbItem(){ ID = 1, DESCRIPTION = "Class 1" },
                  new cmbItem(){ ID = 2, DESCRIPTION = "Class 2" },
                  new cmbItem(){ ID = 3, DESCRIPTION = "Class 3" },

            };

            SCORETYPE = new ObservableCollection<string>()
            { "Alphabetical","Numerical", "Progress Report"};

            ScoreSheet_footer = new ObservableCollection<ScoreSheet_footerDesc>()
            {
                new ScoreSheet_footerDesc()
                {
                    col1 = "mean", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                },
                //new ScoreSheet_footerDesc()
                //{
                //    col1 = "weighted mean", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                //},
                new ScoreSheet_footerDesc()
                {
                    col1 = "median", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                },
                //new ScoreSheet_footerDesc()
                //{
                //    col1 = "mode", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                //},
                new ScoreSheet_footerDesc()
                {
                    col1 = "highest", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                },
                new ScoreSheet_footerDesc()
                {
                    col1 = "Final Score", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                },
                //new ScoreSheet_footerDesc()
                //{
                //    col1 = "most recent - 3", col2 ="", col3="", col4 = "", col5 ="", col6 ="", col7 = ""
                //},
                //new ScoreSheet_footerDesc()
                //{
                //    col1 = "time assessed", col2 ="0", col3="0", col4 = "0", col5 ="0", col6 ="0", col7 = "0"
                //}
            };
        }

        public byte[] imgbin { get; set; }

        private string _SELECTED_SCORETYPE;

        public string SELECTED_SCORETYPE
        {
            get { return _SELECTED_SCORETYPE; }
            set
            {

                if (_SELECTED_SCORETYPE == value)
                { return; }
                else
                {
                    _SELECTED_SCORETYPE = value;

                }
                OnPropertyChanged("SELECTED_SCORETYPE");
            }
        }

        private int _LanguageDisplayIndex;

        public int LanguageDisplayIndex
        {
            get { return _LanguageDisplayIndex; }
            set
            {
                _LanguageDisplayIndex = value;
                OnPropertyChanged("LanguageDisplayIndex");
            }
        }


        private int _GenderDisplayIndex;

        public int GenderDisplayIndex
        {
            get { return _GenderDisplayIndex; }
            set
            {
                _GenderDisplayIndex = value;
                OnPropertyChanged("GenderDisplayIndex");
            }
        }

        private int _CitizenshipDisplayIndex;

        public int CitizenshipDisplayIndex
        {
            get { return _CitizenshipDisplayIndex; }
            set
            {
                _CitizenshipDisplayIndex = value;
                OnPropertyChanged("CitizenshipDisplayIndex");
            }
        }

        private int _EthnicityDisplayIndex;

        public int EthnicityDisplayIndex
        {
            get { return _EthnicityDisplayIndex; }
            set
            {
                _EthnicityDisplayIndex = value;
                OnPropertyChanged("EthnicityDisplayIndex");
            }
        }


        private ObservableCollection<cmbItem> _CMBLIST;

        public ObservableCollection<cmbItem> CMBLIST
        {
            get { return _CMBLIST; }
            set
            {
                _CMBLIST = value;
                OnPropertyChanged("CMBLIST");
            }
        }


        private BitmapImage _PHOTO;

        public BitmapImage PHOTO
        {
            get { return _PHOTO; }
            set
            {
                _PHOTO = value;
                OnPropertyChanged("PHOTO");
            }
        }


        private bool _IsGenderVisible;

        public bool IsGenderVisible
        {
            get { return _IsGenderVisible; }
            set
            {
                _IsGenderVisible = value;
                OnPropertyChanged("IsGenderVisible");
            }
        }

        private bool _IsBirthdateVisible;

        public bool IsBirthdateVisible
        {
            get { return _IsBirthdateVisible; }
            set
            {
                _IsBirthdateVisible = value;
                OnPropertyChanged("IsBirthdateVisible");
            }
        }


        private bool _IsLanguageVisible;

        public bool IsLanguageVisible
        {
            get { return _IsLanguageVisible; }
            set
            {
                _IsLanguageVisible = value;
                OnPropertyChanged("IsLanguageVisible");
            }
        }


        private bool _IsEthnicityVisible;

        public bool IsEthnicityVisible
        {
            get { return _IsEthnicityVisible; }
            set
            {
                _IsEthnicityVisible = value;
                OnPropertyChanged("IsEthnicityVisible");
            }
        }

        private bool _IsOriginOfBirthVisible;

        public bool IsOriginOfBirthVisible
        {
            get { return _IsOriginOfBirthVisible; }
            set
            {
                _IsOriginOfBirthVisible = value;
                OnPropertyChanged("IsOriginOfBirthVisible");
            }
        }

        private bool _IsCitizenshipVisible;

        public bool IsCitizenshipVisible
        {
            get { return _IsCitizenshipVisible; }
            set
            {
                _IsCitizenshipVisible = value;
                OnPropertyChanged("IsCitizenshipVisible");
            }
        }

        private bool _IsExtraCoVisible;

        public bool IsExtraCoVisible
        {
            get { return _IsExtraCoVisible; }
            set
            {
                _IsExtraCoVisible = value;
                OnPropertyChanged("IsExtraCoVisible");
            }
        }


        private string _SELECTED_FULLNAME;

        public string SELECTED_FULLNAME
        {
            get { return _SELECTED_FULLNAME; }
            set
            {
                _SELECTED_FULLNAME = value;
                OnPropertyChanged("SELECTED_FULLNAME");
            }
        }

        private string _Absent;

        public string Absent
        {
            get { return _Absent; }
            set
            {
                _Absent = value;
                OnPropertyChanged("Absent");
            }
        }

        private string _Late;

        public string Late
        {
            get { return _Late; }
            set
            {
                _Late = value;
                OnPropertyChanged("Late");
            }
        }

        private string _StudentId;

        public string StudentId
        {
            get { return _StudentId; }
            set
            {
                _StudentId = value;
                OnPropertyChanged("StudentId");
            }
        }

        private ObservableCollection<STUDENTSCORESHEET> _STUDENTSCORESHEETLIST;

        public ObservableCollection<STUDENTSCORESHEET> STUDENTSCORESHEETLIST
        {
            get { return _STUDENTSCORESHEETLIST; }
            set
            {
                _STUDENTSCORESHEETLIST = value;
                OnPropertyChanged("STUDENTSCORESHEETLIST");
            }
        }


        private Visibility _IsFullNameVisible;
        public Visibility IsFullNameVisible
        {
            get { return _IsFullNameVisible; }
            set
            {
                _IsFullNameVisible = value;
                OnPropertyChanged("IsFullNameVisible");
            }
        }


        private SCHOOL _SchoolSelected;

        public SCHOOL SchoolSelected
        {
            get { return _SchoolSelected; }
            set
            {
                _SchoolSelected = value;
                OnPropertyChanged("SchoolSelected");
                getSubjectList();
            }
        }

        private ASSIGNMENTDESC _SelectedAssignment;

        public ASSIGNMENTDESC SelectedAssignment
        {
            get { return _SelectedAssignment; }
            set
            {
                _SelectedAssignment = value;
                OnPropertyChanged("SelectedAssignment");

            }
        }


        private SEMESTER _SemesterSelected;

        public SEMESTER SemesterSelected
        {
            get { return _SemesterSelected; }
            set
            {
                _SemesterSelected = value;
                OnPropertyChanged("SemesterSelected");
                getSubjectList();
            }
        }

        private ObservableCollection<ScoreSheet_footerDesc> _ScoreSheet_footer;

        public ObservableCollection<ScoreSheet_footerDesc> ScoreSheet_footer
        {
            get { return _ScoreSheet_footer; }
            set { _ScoreSheet_footer = value; }
        }


        private ObservableCollection<STUDENTDESC> _STUDENTLIST;
        public ObservableCollection<STUDENTDESC> STUDENTLIST
        {
            get { return _STUDENTLIST; }
            set
            {
                _STUDENTLIST = value;
                OnPropertyChanged(nameof(STUDENTLIST));
            }
        }

        private ObservableCollection<ASSIGNMENT_CATEGORY> _ASSIGNMENT_CATEGORY;
        public ObservableCollection<ASSIGNMENT_CATEGORY> ASSIGNMENT_CATEGORY
        {
            get { return _ASSIGNMENT_CATEGORY; }
            set
            {
                _ASSIGNMENT_CATEGORY = value;
                OnPropertyChanged("ASSIGNMENT_CATEGORY");
            }
        }


        private ObservableCollection<string> _SCORETYPE;

        public ObservableCollection<string> SCORETYPE
        {
            get { return _SCORETYPE; }
            set
            {
                _SCORETYPE = value;
                OnPropertyChanged("SCORETYPE");
            }
        }


        private ObservableCollection<GetSubjectList_Result> _SUBJECTLIST;
        public ObservableCollection<GetSubjectList_Result> SUBJECTLIST
        {
            get { return _SUBJECTLIST; }
            set
            {
                _SUBJECTLIST = value;
                OnPropertyChanged("SUBJECTLIST");
            }
        }


        private string _GridHeaderTemplateColor = Properties.Settings.Default.MainPalette_Blue;

        public string GridHeaderTemplateColor
        {
            get { return _GridHeaderTemplateColor; }
            set
            {
                _GridHeaderTemplateColor = value;
                OnPropertyChanged("GridHeaderTemplateColor");
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


        private ASSIGNMENT_CATEGORY _assignmentcatselected;

        public ASSIGNMENT_CATEGORY Assignmentcatselected
        {
            get { return _assignmentcatselected; }
            set
            {
                if (_assignmentcatselected == value) return;
                _assignmentcatselected = value;
                OnPropertyChanged("Assignmentcatselected");
            }
        }


        private int classID;

        public int ClassID
        {
            get { return classID; }
            set
            {
                classID = value;
                OnPropertyChanged("ClassID");
            }
        }

        private SCHOOL school_item;

        public SCHOOL School_item
        {
            get { return school_item; }
            set
            {
                school_item = value;
                OnPropertyChanged("School_item");
            }
        }


        private ObservableCollection<GetSchoolList_Result> _SchoolList;

        public ObservableCollection<GetSchoolList_Result> SchoolList
        {
            get { return _SchoolList; }
            set
            {
                _SchoolList = value;
                OnPropertyChanged("SchoolList");
                getSubjectList();
            }
        }


        private SEMESTER _Semester_ID;

        public SEMESTER Semester_ID
        {
            get { return _Semester_ID; }
            set
            {
                _Semester_ID = value;
                OnPropertyChanged("Semester_ID");
            }
        }


        private ObservableCollection<GetSemesterList_Result> _Semester_List;

        public ObservableCollection<GetSemesterList_Result> Semester_List
        {
            get { return _Semester_List; }
            set
            {
                _Semester_List = value;
                OnPropertyChanged("Semester_List");
            }
        }

        private ObservableCollection<ASSIGNMENTDESC> _Assignment_List;

        public ObservableCollection<ASSIGNMENTDESC> Assignment_List
        {
            get { return _Assignment_List ?? (Assignment_List = new ObservableCollection<ASSIGNMENTDESC>()); }

            set
            {
                _Assignment_List = value;
                OnPropertyChanged("Assignment_List");
            }
        }

        private ObservableCollection<AssignmentList> _AssignmentList;

        public ObservableCollection<AssignmentList> AssignmentList
        {
            get { return _AssignmentList; }
            set
            {
                _AssignmentList = value;
                OnPropertyChanged("AssignmentList");
            }
        }

        public void getAssignmentList(string subjectId, string schoolId, string semesterId)
        {
            var assignentData = KirinEntities.GetAssignmentList(subjectId, schoolId, semesterId).ToList();

            if (assignentData.Count() > 0)
            {
                Assignment_List = new ObservableCollection<ASSIGNMENTDESC>(from assignment in assignentData
                                                                           select new ASSIGNMENTDESC
                                                                           {
                                                                               ABRV = assignment.ABRV.Trim(),
                                                                               ASSIGNMENT_ID = assignment.ASSIGNMENT_ID,
                                                                               DATE_DUE = assignment.DATE_DUE,
                                                                               SUBJECT_ID = assignment.SUBJECT_ID,
                                                                               NAME = assignment.NAME.Trim(),
                                                                               POINTS_POSSIBLE = assignment.POINTS_POSSIBLE.ToString(),
                                                                               REPORTING_TERM = assignment.REPORTING_TERM,
                                                                               SCORE_TYPE = assignment.SCORE_TYPE,
                                                                               WEIGHT = assignment.WEIGHT.ToString(),
                                                                               CATEGORY = assignment.CATEGORY,
                                                                               ISLOADEDstr = (assignment.ISLOADED == 1 ? "TRUE" : "FALSE"),
                                                                               ISDIRTY = 0,
                                                                               Visible = Visibility.Visible
                                                                           });

                Assignment_List.Add(new ASSIGNMENTDESC
                {
                    ABRV = "",
                    NAME = "",
                    POINTS_POSSIBLE = "",
                    REPORTING_TERM = "",
                    SCORE_TYPE = "",
                    WEIGHT = "",
                    CATEGORY = "",
                    Visible = Visibility.Hidden,

                });
                Assignment_List.Add(new ASSIGNMENTDESC
                {
                    ABRV = "",
                    NAME = "",
                    POINTS_POSSIBLE = "",
                    REPORTING_TERM = "",
                    SCORE_TYPE = "",
                    WEIGHT = "",
                    CATEGORY = "",
                    Visible = Visibility.Hidden,

                });
                Assignment_List.Add(new ASSIGNMENTDESC
                {
                    ABRV = "",
                    NAME = "",
                    POINTS_POSSIBLE = "",
                    REPORTING_TERM = "",
                    SCORE_TYPE = "",
                    WEIGHT = "",
                    CATEGORY = "",
                    Visible = Visibility.Hidden,

                });

                Assignment_List.Add(new ASSIGNMENTDESC
                {
                    ABRV = "",
                    NAME = "",
                    POINTS_POSSIBLE = "",
                    REPORTING_TERM = "",
                    SCORE_TYPE = "",
                    WEIGHT = "",
                    CATEGORY = "",
                    Visible = Visibility.Hidden,

                });
            }
        }

        public void getAssignmentData(string subjectId, string schoolId, string semesterId)
        {

            var assignentData = KirinEntities.GetAssignmentList(subjectId, schoolId, semesterId).ToList();

            if (assignentData.Count() > 0)
            {
                AssignmentList = new ObservableCollection<AssignmentList>(from a in assignentData
                                                                          select new AssignmentList
                                                                          {
                                                                              ID = a.ASSIGNMENT_ID,
                                                                              Name = a.NAME,
                                                                              Abbreviation = a.ABRV,
                                                                              IsDeleted = 0
                                                                          });
            }
        }


        public void getAssignmentCategory()
        {
            var categoryList = KirinEntities.GetAssignmentCategory().ToList();

            ASSIGNMENT_CATEGORY = new ObservableCollection<ASSIGNMENT_CATEGORY>(from ac in categoryList
                                                                                select new ASSIGNMENT_CATEGORY
                                                                                {
                                                                                    ASSIGNMENT_CATEGORY_ID = ac.ASSIGNMENT_CATEGORY_ID,
                                                                                    CATEGORY = ac.CATEGORY
                                                                                });
        }

        public ASSIGNMENT_CATEGORY getAssignmentCategory(string assignmentCategory)
        {
            return (ASSIGNMENT_CATEGORY)(from cat in KirinEntities.ASSIGNMENT_CATEGORY
                                         where assignmentCategory == cat.CATEGORY
                                         select cat).FirstOrDefault();
        }

        public void getSemesterList()
        {

            var semesterList = KirinEntities.GetSemesterList().ToList();

            Semester_List = new ObservableCollection<GetSemesterList_Result>(from semester in semesterList
                                                                             select new GetSemesterList_Result
                                                                             {
                                                                                 ID = semester.ID,
                                                                                 SEMESTER = semester.SEMESTER
                                                                             });
        }

        public void getSubjectList()
        {

            if (SemesterSelected is null || SchoolSelected is null)
            {
                var subjectList = KirinEntities.GetSubjectList("0", "0").ToList();

                SUBJECTLIST = new ObservableCollection<GetSubjectList_Result>(from subj1 in subjectList
                                                                              select subj1);

            }
            else
            {
                var subjectList = KirinEntities.GetSubjectList(SchoolSelected.ID.ToString(), SemesterSelected.ID.ToString()).ToList();

                SUBJECTLIST = new ObservableCollection<GetSubjectList_Result>(from x in subjectList
                                                                              select x);
            }
        }

        public void getSchoolList()
        {
            string schoolID = "1";
            var schoolList = KirinEntities.GetSchoolList(schoolID).ToList();
            SchoolList = new ObservableCollection<GetSchoolList_Result>(from school in schoolList
                                                                        select school);
        }

        public void getStudentList(string subjectId, string schoolId, string userName) 
        {
            var studentList = KirinEntities.GetStudentListbySubject(subjectId, schoolId, userName).ToList();

            STUDENTLIST = new ObservableCollection<STUDENTDESC>(from student in studentList
                                                                select new STUDENTDESC
                                                                {
                                                                    ID = student.ID,
                                                                    ADDRESS = student.ADDRESS,
                                                                    BIRTHDATE = (DateTime)student.BIRTHDATE,
                                                                    FULLNAME = student.LAST_NAME + ", " + student.FIRST_NAME,
                                                                    CITIZENSHIP = student.CITIZENSHIP,
                                                                    DIVERSITY = student.DIVERSITY,
                                                                    //SHSM_ALERT = !string.IsNullOrEmpty(student.SHSM_ALERT) ? student.SHSM_ALERT.TrimEnd(',') : student.SHSM_ALERT,
                                                                    ETHNICITY = student.ETHNICITY,
                                                                    //HEALTH_ALERT = !string.IsNullOrEmpty(student.HEALTH_ALERT) ? student.HEALTH_ALERT.TrimEnd(',') : student.HEALTH_ALERT,
                                                                    GENDER = student.GENDER,
                                                                    HOMEROOM = student.HOMEROOM,
                                                                    LANGUAGE = !string.IsNullOrEmpty(student.LANGUAGE) ? student.LANGUAGE.TrimEnd(',') : student.LANGUAGE,
                                                                    ORIGIN_OF_BIRTH = student.ORIGIN_OF_BIRTH,
                                                                    Grade = Math.Round(Convert.ToDouble(student.Grade), 2),
                                                                    Absent = student.sumAbsent.ToString(),
                                                                    Late = student.sumLate.ToString(),
                                                                    STUDENT_ID = student.STUDENT_ID,
                                                                    //OTHER_ALERT = !string.IsNullOrEmpty(student.OTHER_ALERT) ? student.OTHER_ALERT.TrimEnd(',') : student.OTHER_ALERT,
                                                                    //PLUS18_ALERT = !string.IsNullOrEmpty(student.PLUS18_ALERT) ? student.PLUS18_ALERT.TrimEnd(',') : student.PLUS18_ALERT,
                                                                    //SPECIAL_NEEDS = !string.IsNullOrEmpty(student.SPECIAL_NEEDS) ? student.SPECIAL_NEEDS.TrimEnd(',') : student.SPECIAL_NEEDS,
                                                                    EXTRACO = !string.IsNullOrEmpty(student.EXTRA_CO) ? student.EXTRA_CO.TrimEnd(',') : student.EXTRA_CO,
                                                                    PHOTO = ByteArrayToImage(student.IMG)
                                                                    //imgbin = student.IMG
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
                using (var ms = new System.IO.MemoryStream(array))
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

        private void DisplayNewColumn(object param)
        {
            //getListofColumns
            DisplayNewColumns newcolumnwin = new DisplayNewColumns((GradeBookViewModel)param);

            //newcolumnwin.DataContext = ;

            newcolumnwin.Show();

            // MessageBox.Show(student.STUDENT_FULLNAME +"<br/> birthday is on "+ student.BIRTHDAY.Month +"/" + student.BIRTHDAY.Day, "Birthday Alert " , MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewStudentProfile(object param)
        {
            var studentDesc = (STUDENTDESC)param;
            //View Student Profile
            //  StudentProfile studprof = new StudentProfile(studentDesc);
            //  studprof.ShowDialog();

            ((App)Application.Current).studentProfileModal.lblName.Content = studentDesc.FULLNAME;
            ((App)Application.Current).studentProfileModal.FullName.Content = studentDesc.FULLNAME;
            ((App)Application.Current).studentProfileModal.Address.Content = studentDesc.ADDRESS;
            ((App)Application.Current).studentProfileModal.Gender.Content = studentDesc.GENDER;
            ((App)Application.Current).studentProfileModal.Ethnicity.Content = studentDesc.ETHNICITY;
            ((App)Application.Current).studentProfileModal.Language.Content = studentDesc.LANGUAGE;
            ((App)Application.Current).studentProfileModal.Citizenship.Content = studentDesc.CITIZENSHIP;
            ((App)Application.Current).studentProfileModal.Birthdate.Content = studentDesc.BIRTHDATE;
            ((App)Application.Current).studentProfileModal.ProfPic.ImageSource = studentDesc.PHOTO;
            ((App)Application.Current).studentProfileModal.Visibility = Visibility.Visible;
            ((App)Application.Current).studentProfileModal.txtComment.Text = "";
            ((App)Application.Current).studentProfileModal.StudentId.Content = studentDesc.ID;

            var studentComment = KirinEntities.GetStudentComments(studentDesc.ID.ToString()).ToList();
            if (studentComment.Count > 0)
            {

                StudentCommentList = new ObservableCollection<StudentComment>(from s in studentComment
                                                                              select new StudentComment
                                                                              {
                                                                                  ID = s.ID.ToString(),
                                                                                  StudentId = s.STUDENTID,
                                                                                  UpdatedBy = s.UPDATEDBY,
                                                                                  UpdatedDate = s.UPDATEDDATE.ToString(),
                                                                                  Comment = s.UpdatedComment
                                                                              });

                ((App)Application.Current).studentProfileModal.DGComments.Visibility = Visibility.Visible;

                ((App)Application.Current).studentProfileModal.DGComments.ItemsSource = StudentCommentList;
                ((App)Application.Current).studentProfileModal.modelBorder.Height = 380 + studentComment.Count * 70;
                ((App)Application.Current).studentProfileModal.lblAlert.Visibility = Visibility.Collapsed;
            }
            else
            {
                ((App)Application.Current).studentProfileModal.DGComments.Visibility = Visibility.Collapsed;
                ((App)Application.Current).studentProfileModal.modelBorder.Height = 370;
                ((App)Application.Current).studentProfileModal.lblAlert.Visibility = Visibility.Collapsed;
            }
        }

        private ObservableCollection<StudentComment> _studentCommentList;
        public ObservableCollection<StudentComment> StudentCommentList
        {
            get { return _studentCommentList; }
            set
            {
                _studentCommentList = value;
                OnPropertyChanged(nameof(StudentCommentList));
            }
        }

        private void AddNewAssignmentItemCommand(object param)
        {
            Assignment_List.Add(new ASSIGNMENTDESC());
            OnPropertyChanged("Assignment_List");
        }

        private void DeleteAssignmentItemCommand(object param)
        {
            MessageBoxResult result = MessageBox.Show("Proceed?",
                                          "Delete Assignment",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var ctx = new KIRINEntities())
                    {
                        ASSIGNMENT deletedAssignment = (from assignment in ctx.ASSIGNMENTs
                                                        where assignment.ASSIGNMENT_ID == SelectedAssignment.ASSIGNMENT_ID
                                                        select assignment).FirstOrDefault();

                        ctx.ASSIGNMENTs.Remove(deletedAssignment);
                        ctx.SaveChanges();
                        Assignment_List.Remove(Assignment_List.Where(i => i.ASSIGNMENT_ID == SelectedAssignment.ASSIGNMENT_ID).Single());
                    }
                }
                catch (Exception e)
                {

                }
            }

        }

        private void DisplayColumnsCommand(object param)
        {
            var columnList = (List<string>)param;

            if (columnList.Contains("Gender"))
            {
                GenderDisplayIndex = 5 + columnList.FindIndex(a => a.Contains("Gender"));
                IsGenderVisible = true;
            }
            else
            {
                IsGenderVisible = false;
            }

            if (columnList.Contains("Ethnicity"))
            {
                EthnicityDisplayIndex = 5 + columnList.FindIndex(a => a.Contains("Ethnicity"));
                IsEthnicityVisible = true;
            }
            else
            {
                IsEthnicityVisible = false;
            }


            if (columnList.Contains("Language"))
            {
                LanguageDisplayIndex = 5 + columnList.FindIndex(a => a.Contains("Language")); // added 6 since DGClasses contains 5 default columns
                IsLanguageVisible = true;
            }
            else
            {
                IsLanguageVisible = false;
            }


            if (columnList.Contains("Citizenship"))
            {
                CitizenshipDisplayIndex = 5 + columnList.FindIndex(a => a.Contains("Citizenship")); //should find the current number of columns displayed.
                IsCitizenshipVisible = true;
            }
            else
            {
                IsCitizenshipVisible = false;
            }
        }

        public void ViewStudentScoreSheet(int ID, string semesterId)
        {
            var scorelist = KirinEntities.getStudentScoreSheet(ID.ToString(), semesterId).ToList();

            if (scorelist.Count() > 0)
            {
                SELECTED_FULLNAME = (from student in scorelist
                                     where student.STUDENT_ID == ID
                                     select student.FULLNAME).First();

                PHOTO = ByteArrayToImage((from student in scorelist
                                          where student.STUDENT_ID == ID
                                          select student.IMG).First());

                Absent = "Absent : " + (from student in scorelist
                                        where student.STUDENT_ID == ID
                                        select student.Absent).First().ToString();

                Late = "Late : " + (from student in scorelist
                                    where student.STUDENT_ID == ID
                                    select student.Late).First().ToString();

                StudentId = ID.ToString();

                STUDENTSCORESHEETLIST = new ObservableCollection<STUDENTSCORESHEET>((from list in scorelist
                                                                                     where list.STUDENT_ID == ID
                                                                                     select new STUDENTSCORESHEET
                                                                                     {
                                                                                         FULLNAME = list.FULLNAME,
                                                                                         imgbin = list.IMG,
                                                                                         ASSIGNMENT_NAME = list.name,
                                                                                         ASSIGNMENT_SCORE = list.SCORE_ON_TYPE_OF_EXAM,
                                                                                         LEARNINGSKILL_COLLABORATION_SCORE = "",
                                                                                         LEARNINGSKILL_INDEPENDENTWORK_SCORE = "",
                                                                                         LEARNINGSKILL_INITIATIVE_SCORE = "",
                                                                                         LEARNINGSKILL_ORGANIZATION_SCORE = "",
                                                                                         LEARNINGSKILL_RESPONSIBILITY_SCORE = "",
                                                                                         LEARNINGSKILL_SELFREGULATION_SCORE = "",
                                                                                         Late = list.Late.ToString(),
                                                                                         Absent = list.Absent.ToString(),
                                                                                         PHOTO = ByteArrayToImage(list.IMG)
                                                                                     }).AsEnumerable()
                                                                    .Select(a => new STUDENTSCORESHEET()
                                                                    {
                                                                        FULLNAME = a.FULLNAME,
                                                                        ASSIGNMENT_NAME = a.ASSIGNMENT_NAME,
                                                                        ASSIGNMENT_SCORE = a.ASSIGNMENT_SCORE,
                                                                        LEARNINGSKILL_COLLABORATION_SCORE = a.ASSIGNMENT_NAME.Contains("Progress Report") == true ? "E" : "",
                                                                        LEARNINGSKILL_INDEPENDENTWORK_SCORE = a.ASSIGNMENT_NAME.Contains("Progress Report") == true ? "N" : "",
                                                                        LEARNINGSKILL_INITIATIVE_SCORE = a.ASSIGNMENT_NAME.Contains("Progress Report") == true ? "E" : "",
                                                                        LEARNINGSKILL_ORGANIZATION_SCORE = a.ASSIGNMENT_NAME.Contains("Progress Report") == true ? "E" : "",
                                                                        LEARNINGSKILL_RESPONSIBILITY_SCORE = a.ASSIGNMENT_NAME.Contains("Progress Report") == true ? "E" : "",
                                                                        LEARNINGSKILL_SELFREGULATION_SCORE = a.ASSIGNMENT_NAME.Contains("Progress Report") == true ? "E" : "",
                                                                        Late = a.Late.ToString(),
                                                                        Absent = a.Absent.ToString(),
                                                                        PHOTO = a.PHOTO
                                                                    }));
            }

        }

        private void getStudentAssignmentList(int studentID, int classID)
        {
            //STUDENTSCORESHEETlist
        }

        private void SaveAssignmentCommand(object param)
        {
            try
            {
                using (var ctx = new KIRINEntities())
                {
                    foreach (var assignment in Assignment_List)
                    {
                        //check for dirty items
                        if (assignment.ASSIGNMENT_ID == 0)
                        {
                            ASSIGNMENT newAssignmentItem = new ASSIGNMENT()
                            {
                                ABRV = assignment.ABRV,
                                CATEGORY = assignment.CATEGORYObj.CATEGORY,
                                DATE_DUE = DateTime.Now,
                                NAME = assignment.NAME,
                                POINTS_POSSIBLE = Convert.ToInt32(assignment.POINTS_POSSIBLE),
                                REPORTING_TERM = "",
                                SCORE_TYPE = assignment.SCORE_TYPE,
                                SUBJECT_ID = 1,
                                WEIGHT = Convert.ToDouble(assignment.WEIGHT),
                                ISLOADED = assignment.ISLOADED
                            };
                            ctx.ASSIGNMENTs.Add(newAssignmentItem);
                        }
                    }

                    ctx.SaveChanges();
                    //getAssignmentList();
                    MessageBox.Show("New assignment added successfully!");
                    OnPropertyChanged("Assignment_List");
                }

                ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new AssignmentMaker());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }

        public void DeleteAssignment(ObservableCollection<AssignmentList> assignmentList)
        {
            try
            {
                using (var ctx = new KIRINEntities())
                {
                    foreach (var assignment in assignmentList)
                    {
                        if (assignment.ID != 0)
                        {
                            //Update Existing Record
                            ASSIGNMENT update = KirinEntities.ASSIGNMENTs.Where(c => c.ASSIGNMENT_ID == assignment.ID).FirstOrDefault();

                            update.IsDeleted = assignment.IsDeleted == 0 ? false : true;
                            KirinEntities.SaveChanges();
                        }
                    }
                    //MessageBox.Show("Assignment deleted successfully!");
                    OnPropertyChanged("AssignmentList");
                    app.deleteAssignment.Visibility = Visibility.Collapsed;

                    ((Application.Current.MainWindow as MainWindow).MainWindowFrame.NavigationService).Navigate(new AssignmentMaker());
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        private void show_UC_Responsibility(object param)
        {
            string learningSkills = param.ToString();
            app.score_LearningSkills.Visibility = Visibility.Visible;

            switch (learningSkills)
            {
                case "Responsibility":
                    app.score_LearningSkills.ResponsibilityView.Visibility = Visibility.Visible;
                    app.score_LearningSkills.OrganizationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.IndependentView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.CollaborationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.InitiativeView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.SelfRegulationView.Visibility = Visibility.Collapsed;
                    break;
                case "Organization":
                    app.score_LearningSkills.OrganizationView.Visibility = Visibility.Visible;
                    app.score_LearningSkills.ResponsibilityView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.IndependentView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.CollaborationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.InitiativeView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.SelfRegulationView.Visibility = Visibility.Collapsed;
                    break;
                case "Independent":
                    app.score_LearningSkills.IndependentView.Visibility = Visibility.Visible;
                    app.score_LearningSkills.OrganizationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.ResponsibilityView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.CollaborationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.InitiativeView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.SelfRegulationView.Visibility = Visibility.Collapsed;
                    break;
                case "Collaboration":
                    app.score_LearningSkills.CollaborationView.Visibility = Visibility.Visible;
                    app.score_LearningSkills.IndependentView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.OrganizationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.ResponsibilityView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.InitiativeView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.SelfRegulationView.Visibility = Visibility.Collapsed;
                    break;
                case "Initiative":
                    app.score_LearningSkills.InitiativeView.Visibility = Visibility.Visible;
                    app.score_LearningSkills.CollaborationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.IndependentView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.OrganizationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.ResponsibilityView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.SelfRegulationView.Visibility = Visibility.Collapsed;
                    break;
                case "SelfRegulation":
                    app.score_LearningSkills.SelfRegulationView.Visibility = Visibility.Visible;
                    app.score_LearningSkills.CollaborationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.IndependentView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.OrganizationView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.ResponsibilityView.Visibility = Visibility.Collapsed;
                    app.score_LearningSkills.InitiativeView.Visibility = Visibility.Collapsed;

                    break;
                default:
                    break;
            }
        }

    }
    public class ImageClass
    {
        public string Name { get; set; }
        public BitmapImage Image { get; set; }
    }


    public class STUDENTDESC
    {
        public int ID { get; set; }
        public string FULLNAME { get; set; }
        public System.DateTime BIRTHDATE { get; set; }
        public string SPECIAL_NEEDS { get; set; }
        public string OTHER_ALERT { get; set; }
        public string HEALTH_ALERT { get; set; }
        public string DIVERSITY { get; set; }
        public string ORIGIN_OF_BIRTH { get; set; }
        public string CITIZENSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string LANGUAGE { get; set; }
        public string SHSM_ALERT { get; set; }
        public string PLUS18_ALERT { get; set; }
        public string GENDER { get; set; }
        public string ETHNICITY { get; set; }
        public string HOMEROOM { get; set; }
        public string EXTRACO { get; set; }
        public string STUDENT_ID { get; set; }
        public string Absent { get; set; }
        public string Late { get; set; }
        public double Grade { get; set; }
        public byte[] imgbin { get; set; }
        public BitmapImage PHOTO { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AssignmentList : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        private int _IsDeleted;
        public int IsDeleted
        {
            get { return _IsDeleted; }
            set
            {
                if (_IsDeleted == value)
                { return; }
                else
                {
                    _IsDeleted = value;
                }
                OnPropertyChanged("IsDeleted");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ASSIGNMENTDESC : INotifyPropertyChanged, IEditableObject
    {
        public int ASSIGNMENT_ID { get; set; }

        public Visibility Visible { get; set; }

        private string _REPORTING_TERM;
        public string REPORTING_TERM
        {
            get { return _REPORTING_TERM; }
            set
            {
                if (_REPORTING_TERM == value)
                { return; }
                else
                {
                    _REPORTING_TERM = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("REPORTING_TERM");

            }
        }

        private string _NAME;
        public string NAME
        {
            get { return _NAME; }
            set
            {
                if (_NAME == value)
                { return; }
                else
                {
                    _NAME = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("NAME");
            }
        }

        private string _ABRV;
        public string ABRV
        {
            get { return _ABRV; }
            set
            {
                if (_ABRV == value)
                { return; }
                else
                {
                    _ABRV = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("NAME");
            }
        }

        private string _CATEGORY;
        public string CATEGORY
        {
            get { return _CATEGORY; }
            set
            {
                if (_CATEGORY == value)
                { return; }
                else
                {
                    _CATEGORY = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("CATEGORY");
            }

        }

        private ASSIGNMENT_CATEGORY _CATEGORYoBJ;
        public ASSIGNMENT_CATEGORY CATEGORYObj
        {
            get { return _CATEGORYoBJ; }
            set
            {

                if (_CATEGORYoBJ == value)
                { return; }
                else
                {
                    _CATEGORYoBJ = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("CATEGORYObj");
            }
        }

        private string _POINTS_POSSIBLE;
        public string POINTS_POSSIBLE
        {
            get { return _POINTS_POSSIBLE; }
            set
            {
                if (_POINTS_POSSIBLE == value)
                { return; }
                else
                {
                    _POINTS_POSSIBLE = value;
                    ISDIRTY = 1;
                }


                OnPropertyChanged("POINTS_POSSIBLE");
            }
        }

        private string _WEIGHT;
        public string WEIGHT
        {
            get { return _WEIGHT; }
            set
            {
                if (_WEIGHT == value)
                { return; }
                else
                {
                    _WEIGHT = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("WEIGHT");
            }
        }

        private System.DateTime _DATE_DUE;
        public System.DateTime DATE_DUE
        {
            get { return _DATE_DUE; }
            set
            {
                if (_DATE_DUE == value)
                { return; }
                else
                {
                    _DATE_DUE = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("DATE_DUE");
            }
        }

        private int _SUBJECT_ID;
        public int SUBJECT_ID
        {
            get { return _SUBJECT_ID; }
            set
            {
                if (_SUBJECT_ID == value)
                { return; }
                else
                {
                    _SUBJECT_ID = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("SUBJECT_ID");
            }
        }

        private string _SCORE_TYPE;
        public string SCORE_TYPE
        {
            get { return _SCORE_TYPE; }
            set
            {
                if (_SCORE_TYPE == value)
                { return; }
                else
                {
                    _SCORE_TYPE = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("SCORE_TYPE");
            }
        }

        private int _ISLOADED;
        public int ISLOADED
        {
            get { return _ISLOADED; }
            set
            {
                if (_ISLOADED == value)
                { return; }
                else
                {
                    _ISLOADED = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("ISLOADED");
            }
        }

        private string _ISLOADEDstr;
        public string ISLOADEDstr
        {
            get { return _ISLOADEDstr; }
            set
            {
                if (_ISLOADEDstr == value)
                { return; }
                else
                {
                    _ISLOADEDstr = value;
                    ISDIRTY = 1;
                }
                OnPropertyChanged("ISLOADEDstr");
            }
        }

        private int _ISDIRTY = 0;
        public int ISDIRTY
        {
            get { return _ISDIRTY; }
            set
            {
                if (_ISDIRTY == value)
                { return; }
                else
                {
                    _ISDIRTY = value;
                    ISDIRTY = 1;
                }

                OnPropertyChanged("ISDIRTY");

            }
        }
        public void BeginEdit()
        {
        }

        public void CancelEdit()
        {
        }
        public void EndEdit()
        {
            // this method is called, when user has ended editing
            // TODO: call service layer to update model
            if (this.ASSIGNMENT_ID != 0)
            {
                using (var ctx = new KIRINEntities())
                {
                    ASSIGNMENT assignment_tobeupdated = (from a in ctx.ASSIGNMENTs
                                                         where a.ASSIGNMENT_ID == this.ASSIGNMENT_ID
                                                         select a).First();

                    assignment_tobeupdated.ABRV = this.ABRV;
                    assignment_tobeupdated.CATEGORY = this.CATEGORYObj == null ? (from cat in ctx.ASSIGNMENT_CATEGORY
                                                                                  where cat.CATEGORY == this.CATEGORY
                                                                                  select cat.ASSIGNMENT_CATEGORY_ID.ToString()).FirstOrDefault() : this.CATEGORYObj.ASSIGNMENT_CATEGORY_ID.ToString();
                    assignment_tobeupdated.DATE_DUE = this.DATE_DUE;
                    assignment_tobeupdated.ISLOADED = this.ISLOADED;
                    assignment_tobeupdated.POINTS_POSSIBLE = Convert.ToInt32(this.POINTS_POSSIBLE);
                    assignment_tobeupdated.NAME = this.NAME;
                    assignment_tobeupdated.REPORTING_TERM = this.REPORTING_TERM;
                    assignment_tobeupdated.SCORE_TYPE = this.SCORE_TYPE;
                    assignment_tobeupdated.WEIGHT = Convert.ToDouble(this.WEIGHT);
                    ctx.SaveChanges();
                    OnPropertyChanged("Assignment_List");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class STUDENTSCORESHEET
    {
        public byte[] imgbin { get; set; }
        public string Absent { get; set; }
        public string Late { get; set; }
        public BitmapImage PHOTO { get; set; }

        private string _FULLNAME;

        public string FULLNAME
        {
            get { return _FULLNAME; }
            set { _FULLNAME = value; }
        }


        private string _ASSIGNMENT_NAME;
        public string ASSIGNMENT_NAME
        {
            get { return _ASSIGNMENT_NAME; }
            set { _ASSIGNMENT_NAME = value; }
        }

        private double _ASSIGNMENT_SCORE;
        public double ASSIGNMENT_SCORE
        {
            get { return _ASSIGNMENT_SCORE; }
            set { _ASSIGNMENT_SCORE = value; }
        }

        private string _LEARNINGSKILL_RESPONSIBILITY_SCORE;
        public string LEARNINGSKILL_RESPONSIBILITY_SCORE
        {
            get { return _LEARNINGSKILL_RESPONSIBILITY_SCORE; }
            set { _LEARNINGSKILL_RESPONSIBILITY_SCORE = value; }
        }

        private string _LEARNINGSKILL_ORGANIZATION_SCORE;
        public string LEARNINGSKILL_ORGANIZATION_SCORE
        {
            get { return _LEARNINGSKILL_ORGANIZATION_SCORE; }
            set { _LEARNINGSKILL_ORGANIZATION_SCORE = value; }
        }

        private string _LEARNINGSKILL_INDEPENDENTWORK_SCORE;
        public string LEARNINGSKILL_INDEPENDENTWORK_SCORE
        {
            get { return _LEARNINGSKILL_INDEPENDENTWORK_SCORE; }
            set { _LEARNINGSKILL_INDEPENDENTWORK_SCORE = value; }
        }

        private string _LEARNINGSKILL_COLLABORATION_SCORE;
        public string LEARNINGSKILL_COLLABORATION_SCORE
        {
            get { return _LEARNINGSKILL_COLLABORATION_SCORE; }
            set { _LEARNINGSKILL_COLLABORATION_SCORE = value; }
        }

        private string _LEARNINGSKILL_INITIATIVE_SCORE;
        public string LEARNINGSKILL_INITIATIVE_SCORE
        {
            get { return _LEARNINGSKILL_INITIATIVE_SCORE; }
            set { _LEARNINGSKILL_INITIATIVE_SCORE = value; }
        }

        private string _LEARNINGSKILL_SELFREGULATION_SCORE;
        public string LEARNINGSKILL_SELFREGULATION_SCORE
        {
            get { return _LEARNINGSKILL_SELFREGULATION_SCORE; }
            set { _LEARNINGSKILL_SELFREGULATION_SCORE = value; }
        }


    }

    public class ScoreSheet_footerDesc
    {
        private string _col1;

        public string col1
        {
            get { return _col1; }
            set { _col1 = value; }
        }


        private string _col2;

        public string col2
        {
            get { return _col2; }
            set { _col2 = value; }
        }

        private string _col3;

        public string col3
        {
            get { return _col3; }
            set { _col3 = value; }
        }


        private string _col4;

        public string col4
        {
            get { return _col4; }
            set { _col4 = value; }
        }


        private string _col5;

        public string col5
        {
            get { return _col5; }
            set { _col5 = value; }
        }

        private string _col6;

        public string col6
        {
            get { return _col6; }
            set { _col6 = value; }
        }

        private string _col7;

        public string col7
        {
            get { return _col7; }
            set { _col7 = value; }
        }

        private string _col8;

        public string col8
        {
            get { return _col8; }
            set { _col8 = value; }
        }


    }

    public class cmbItem : INotifyPropertyChanged
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
            }
        }

        private string _DESCRIPTION;

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class StudentComment
    {
        public string ID { get; set; }
        public string StudentId { get; set; }
        public string Comment { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}






