using Kirin_2.Models;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Kirin_2.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
       public ICommand cViewStaffProfile { get; set; }
      
        public static string schoolId = "1";
        //public static string schoolId = App.Current.Properties["SchoolId"].ToString();

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<SCHOOLDB_DESC> _SCHOOLDB_ALL;

        public ObservableCollection<SCHOOLDB_DESC> SCHOOLDB_ALL
        {
            get { return _SCHOOLDB_ALL; }
            set
            {
                _SCHOOLDB_ALL = value;
                OnPropertyChanged("SCHOOLDB_ALL");
            }
        }

        private string _PhoneNumberLabel;
        public string PhoneNumberLabel
        {
            get { return _PhoneNumberLabel; }
            set
            {
                _PhoneNumberLabel = value;
                OnPropertyChanged("PhoneNumberLabel");
            }
        }

        private bool _IsRoleVisible;

        public bool IsRoleVisible
        {
            get { return _IsRoleVisible; }
            set
            {
                _IsRoleVisible = value;
                OnPropertyChanged("IsRoleVisible");
            }
        }

        private bool _IsContactVisible;

        public bool IsContactVisible
        {
            get { return _IsContactVisible; }
            set
            {
                _IsContactVisible = value;
                OnPropertyChanged("IsContactVisible");
            }
        }

        private bool _IsRelationVisible;

        public bool IsRelationVisible
        {
            get { return _IsRelationVisible; }
            set
            {
                _IsRelationVisible = value;
                OnPropertyChanged("IsRelationVisible");
            }
        }

        private bool _IsStatusVisible;

        public bool IsStatusVisible
        {
            get { return _IsStatusVisible; }
            set
            {
                _IsStatusVisible = value;
                OnPropertyChanged("IsStatusVisible");
            }
        }

        private bool? isselectAll = null;
        public bool? IsSelectAll
        {
            get
            {
                return isselectAll;
            }
            set
            {
                isselectAll = value;
                OnPropertyChanged("IsSelectAll");
            }
        }

        public ObservableCollection<STAFF_DIRECTORY_DESC> _STAFFDIRECTORY;

        public ObservableCollection<STAFF_DIRECTORY_DESC> STAFFDIRECTORY
        {
            get { return _STAFFDIRECTORY; }
            set
            {
                _STAFFDIRECTORY = value;
                OnPropertyChanged("STAFFDIRECTORY");
            }
        }

        private STUDENTPROFILE _STUDENTPROF;

        public STUDENTPROFILE STUDENTPROF
        {
            get { return _STUDENTPROF; ; }
            set
            {
                _STUDENTPROF = value;
                OnPropertyChanged("STUDENTPROF");
            }
        }

        private TEACHERPROFILE _TEACHERPROF;

        public TEACHERPROFILE TEACHERPROF
        {
            get { return _TEACHERPROF; }
            set
            {
                _TEACHERPROF = value;
                OnPropertyChanged("TEACHERPROF");
            }
        }

        private STAFF_DIRECTORY_DESC _STAFFPROF;

        public STAFF_DIRECTORY_DESC STAFFPROF
        {
            get { return _STAFFPROF; }
            set
            {
                _STAFFPROF = value;
                OnPropertyChanged("STAFFPROF");
            }
        }

        KIRINEntities1 kirinentities;
        public App app;
        public UserViewModel()
        {
            kirinentities = new KIRINEntities1();
            kirinentities.Database.CommandTimeout = 10 * 60;
            app = (App)Application.Current;
        
            cViewStaffProfile = new CommandVM(ViewStaffProfile, canExecuteMethod);
            //LoadData();
        }

        private string _Role = "All";
        public string Role
        {
            get { return _Role; }
            set
            {
                _Role = value;
                OnPropertyChanged("Role");
            }
        }

        private IncrementalList<SCHOOLDB_DESC> _incrementalItemsSource;

        public IncrementalList<SCHOOLDB_DESC> IncrementalItemsSource
        {
            get { return _incrementalItemsSource; }
            set { _incrementalItemsSource = value; }
        }

        private ObservableCollection<STUDENTPROFILE_DETAILS> _studentProfileDetailsList = new ObservableCollection<STUDENTPROFILE_DETAILS>();

        public ObservableCollection<STUDENTPROFILE_DETAILS> StudentProfileDetailsList
        {
            get { return _studentProfileDetailsList; }
            set
            {
                _studentProfileDetailsList = value;
                OnPropertyChanged("StudentProfileDetailsList");
            }
        }

        private void ViewStaffProfile(object param)
        {
            var staffDesc = (SCHOOLDB_DESC)param;

            GetStaffProfile(staffDesc.ID.ToString(), staffDesc.ROLE);

            if (STAFFPROF != null)
            {
                app.staffProfileModal.title.Content = STAFFPROF.ROLE;
                app.staffProfileModal.Visibility = Visibility.Visible;
                app.staffProfileModal.Email.Content = STAFFPROF.EMAIL;
                //app.staffProfileModal.EmpID.Content = staffDesc.ID;
                app.staffProfileModal.Name.Content = STAFFPROF.NAME;
                app.staffProfileModal.Position.Content = STAFFPROF.ROLE;
                app.staffProfileModal.School.Content = STAFFPROF.SCHOOL;
                app.staffProfileModal.Jurisdiction.Content = STAFFPROF.JURISDICTION;
                app.staffProfileModal.dob.Content = STAFFPROF.DOB;
                app.staffProfileModal.status.Content = STAFFPROF.Status;
                app.staffProfileModal.grade.Content = STAFFPROF.Grade;
                app.staffProfileModal.departmentHead.Content = STAFFPROF.SUPERVISOR;
                app.staffProfileModal.supervisor.Content = STAFFPROF.SUPERVISOR;

                //string[] emailcomponents = STAFFPROF.EMAIL.Split('@');
                //app.staffProfileModal.Username.Content = emailcomponents[0];
                app.staffProfileModal.ProfPic.ImageSource = STAFF_PHOTO;

                //-------------Update Control Visibility-------------//
                if (staffDesc.ROLE.ToUpper() == "STUDENT")
                {
                    app.staffProfileModal.Name.Visibility = Visibility.Visible;
                    app.staffProfileModal.School.Visibility = Visibility.Visible;
                    app.staffProfileModal.Email.Visibility = Visibility.Visible;
                    app.staffProfileModal.dob.Visibility = Visibility.Visible;
                    app.staffProfileModal.grade.Visibility = Visibility.Visible;
                    app.staffProfileModal.Position.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.Jurisdiction.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.status.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.departmentHead.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.supervisor.Visibility = Visibility.Collapsed;

                    app.staffProfileModal.lblName.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblSchool.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblEmail.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblDob.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblGrade.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblPosition.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblJurisdiction.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblStatus.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblDepartmentHead.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblSupervisor.Visibility = Visibility.Collapsed;
                }
                else if (staffDesc.ROLE.ToUpper() == "DIRECTOR" || staffDesc.ROLE.ToUpper() == "SECRETARY" || staffDesc.ROLE.ToUpper() == "SUPERINTENDENT")
                {
                    app.staffProfileModal.Name.Visibility = Visibility.Visible;
                    app.staffProfileModal.Position.Visibility = Visibility.Visible;
                    app.staffProfileModal.School.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.Email.Visibility = Visibility.Visible;
                    app.staffProfileModal.Jurisdiction.Visibility = Visibility.Visible;
                    app.staffProfileModal.dob.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.grade.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.status.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.departmentHead.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.supervisor.Visibility = Visibility.Collapsed;

                    app.staffProfileModal.lblName.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblSchool.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblEmail.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblDob.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblGrade.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblPosition.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblJurisdiction.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblStatus.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblDepartmentHead.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblSupervisor.Visibility = Visibility.Collapsed;
                }
                else if (STAFFPROF.ROLE.ToUpper() == "TEACHER")
                {
                    app.staffProfileModal.Name.Visibility = Visibility.Visible;
                    app.staffProfileModal.School.Visibility = Visibility.Visible;
                    app.staffProfileModal.Email.Visibility = Visibility.Visible;
                    app.staffProfileModal.status.Visibility = Visibility.Visible;
                    app.staffProfileModal.departmentHead.Visibility = Visibility.Visible;
                    app.staffProfileModal.Jurisdiction.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.dob.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.grade.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.supervisor.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.Position.Visibility = Visibility.Collapsed;

                    app.staffProfileModal.lblName.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblSchool.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblEmail.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblDob.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblGrade.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblPosition.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblJurisdiction.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblStatus.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblDepartmentHead.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblSupervisor.Visibility = Visibility.Collapsed;
                }
                else
                {
                    app.staffProfileModal.Name.Visibility = Visibility.Visible;
                    app.staffProfileModal.School.Visibility = Visibility.Visible;
                    app.staffProfileModal.Email.Visibility = Visibility.Visible;
                    app.staffProfileModal.status.Visibility = Visibility.Visible;
                    app.staffProfileModal.supervisor.Visibility = Visibility.Visible;
                    app.staffProfileModal.departmentHead.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.Jurisdiction.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.dob.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.grade.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.Position.Visibility = Visibility.Collapsed;

                    app.staffProfileModal.lblName.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblSchool.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblEmail.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblDob.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblGrade.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblPosition.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblJurisdiction.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblStatus.Visibility = Visibility.Visible;
                    app.staffProfileModal.lblDepartmentHead.Visibility = Visibility.Collapsed;
                    app.staffProfileModal.lblSupervisor.Visibility = Visibility.Visible;
                }
            }

            PhoneNumberLabel = "Phone #";
        }

        public void LoadStaffDirectory(string sID, string role)
        {
            kirinentities.Database.CommandTimeout = 8 * 60;
            var staffData = kirinentities.GetStaffData(sID, role).ToList();

            STAFFDIRECTORY = new ObservableCollection<STAFF_DIRECTORY_DESC>(
                from dir in staffData
                select new STAFF_DIRECTORY_DESC
                {
                    RNo = (int)dir.RNo,
                    EMAIL = dir.EMAIL,
                    ID = dir.ID,
                    NAME = dir.LAST_NAME + ", " + dir.FIRST_NAME,
                    PHONE_NUMBER = dir.PHONE_NUMBER,
                    ROLE = dir.ROLE,
                    ROOM_NUMBER = dir.ROOM_NUMBER,
                    IsSelected = false
                });
        }

        public ObservableCollection<SCHOOLDB_DESC> LoadSchoolDB()
        {
            var studentData = kirinentities.GetSchoolDBData(this.Role, schoolId).ToList();

            SCHOOLDB_ALL = new ObservableCollection<SCHOOLDB_DESC>(from obj in studentData
                                                                   select new SCHOOLDB_DESC
                                                                   {
                                                                       RowNo = (int)obj.RNo,
                                                                       ID = obj.ID,
                                                                       NAME = obj.LAST_NAME + ", " + obj.FIRST_NAME,
                                                                       EMAIL = obj.EMAIL,
                                                                       ROOM_NUMBER = obj.ROOM_NUMBER,
                                                                       PHONE_NUMBER = obj.PHONE_NUMBER,
                                                                       PRIMARY_CONTACT = obj.PRIMARY_CONTACT,
                                                                       RELATIONSHIP = obj.RELATIONSHIP,
                                                                       ROLE = obj.ROLE,
                                                                       SCHOOL_NAME = obj.SCHOOL_NAME,
                                                                       STATUS = obj.STATUS,
                                                                       mROLE = obj.mRole
                                                                   });

            if (this.Role == "All")
            {
                IsRoleVisible = false;
                PhoneNumberLabel = "Phone #";
                IsContactVisible = true;
                IsRelationVisible = true;
                IsStatusVisible = true;
            }
            else if (this.Role == "Student")
            {
                IsRoleVisible = true;
                PhoneNumberLabel = "Emergency Phone #";
                IsContactVisible = false;
                IsRelationVisible = false;
                IsStatusVisible = true;
            }
            else if (this.Role == "Teacher")
            {
                IsRoleVisible = false;
                PhoneNumberLabel = "Phone #";
                IsContactVisible = true;
                IsRelationVisible = true;
                IsStatusVisible = false;
            }
            else if (this.Role == "Support")
            {
                IsRoleVisible = false;
                PhoneNumberLabel = "Phone #";
                IsContactVisible = true;
                IsRelationVisible = true;
                IsStatusVisible = true;
            }
            else if (this.Role == "Admin")
            {
                IsRoleVisible = false;
                PhoneNumberLabel = "Phone #";
                IsContactVisible = true;
                IsRelationVisible = true;
                IsStatusVisible = true;
            }

            return SCHOOLDB_ALL;
        }

        public void LoadData() {
            IncrementalItemsSource = new IncrementalList<SCHOOLDB_DESC>(LoadMoreItems) { MaxItemCount = 5000 };
        }

        /// <summary>
        /// Method to load items which assigned to the action of IncrementalList
        /// </summary>
        /// <param name="count"></param>
        /// <param name="baseIndex"></param>

        void LoadMoreItems(uint count, int baseIndex)
        {
            var _orders = LoadSchoolDB();
            var list = _orders.Skip(baseIndex).Take(100).ToList();
            IncrementalItemsSource.LoadItems(list);
        }

        private BitmapImage _STUDENT_PHOTO;

        public BitmapImage STUDENT_PHOTO
        {
            get { return _STUDENT_PHOTO; }
            set
            {
                _STUDENT_PHOTO = value;
                OnPropertyChanged("STUDENT_PHOTO");
            }
        }

        private BitmapImage _STAFF_PHOTO;

        public BitmapImage STAFF_PHOTO
        {
            get { return _STAFF_PHOTO; }
            set
            {
                _STAFF_PHOTO = value;
                OnPropertyChanged("STAFF_PHOTO");
            }
        }


        private BitmapImage _TEACHER_PHOTO;

        public BitmapImage TEACHER_PHOTO
        {
            get { return _TEACHER_PHOTO; }
            set
            {
                _TEACHER_PHOTO = value;
                OnPropertyChanged("TEACHER_PHOTO");
            }
        }

        public void ViewStudentProfile(int ID)
        {
            var studentProfile = kirinentities.GetStudentProfileData(ID).ToList();

            STUDENTPROF = (from student in studentProfile
                           select new STUDENTPROFILE
                           {
                               ID = student.ID,
                               ADDRESS = student.ADDRESS,
                               BIRTHDATE = !string.IsNullOrEmpty(student.BIRTHDATE.ToString()) ? Convert.ToDateTime(student.BIRTHDATE).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "",
                               CITIZENSHIP = student.CITIZENSHIP,
                               DIVERSITY = student.DIVERSITY,
                               ETHNICITY = student.ETHNICITY,
                               EXTRACO = !string.IsNullOrEmpty(student.EXTRA_CO) ? student.EXTRA_CO.TrimEnd(',') : student.EXTRA_CO,
                               FULLNAME = student.LAST_NAME + ", " + student.FIRST_NAME,
                               GENDER = student.GENDER,
                               HEALTH_ALERT = !string.IsNullOrEmpty(student.HEALTH_ALERT) ? student.HEALTH_ALERT.TrimEnd(',') : student.HEALTH_ALERT,
                               HOMEROOM = student.HOMEROOM,
                               LANGUAGE = student.LANGUAGE,
                               imgbin = student.IMG,
                               LEARNING_TYPE = student.LEARNING_TYPE,
                               STUDENT_ID = student.STUDENT_NUMBER,
                               SCHOOL_NAME = student.SCHOOL_NAME
                           }).FirstOrDefault();


            if (STUDENTPROF.imgbin is null || STUDENTPROF.imgbin is byte[])
            {
                BitmapImage tmp = new BitmapImage();
                tmp.BeginInit();
                tmp.UriSource = new Uri("pack://application:,,,/Images/Logo.png");

                tmp.DecodePixelWidth = 240;
                tmp.EndInit();

                STUDENT_PHOTO = tmp;

            }
            else
            {
                STUDENT_PHOTO = ByteArrayToImage(STUDENTPROF.imgbin);
            }

            //-----------------Get Attendance by Class-------------//
            DataTable result = GetStudentAttendanceData(ID.ToString());

            List<string> ColumnList = new List<string>();

            foreach (System.Data.DataColumn column in result.Columns)
            {
                if (column.ColumnName != "ID" && column.ColumnName != "EMPLOYEE_ID" && column.ColumnName != "Name" && column.ColumnName != "ROOM_NUMBER"
                    && column.ColumnName != "COURSE_CODE" && column.ColumnName != "COURSE_NAME" && column.ColumnName != "REPORTING_TERM" && column.ColumnName != "Grade"
                    && column.ColumnName != "STUDENT_ID" && column.ColumnName != "CourseID")
                {
                    ColumnList.Add(column.ColumnName);
                }
            }

            foreach (System.Data.DataRow row in result.Rows)
            {
                StudentProfileDetailsList.Add(
                new STUDENTPROFILE_DETAILS()
                {
                    EXP = row["COURSE_CODE"].ToString(),
                    LastWeekAttendance_M = row[ColumnList[0]] != DBNull.Value ? row[ColumnList[0]].ToString() : "-",
                    LastWeekAttendance_T = row[ColumnList[1]] != DBNull.Value ? row[ColumnList[1]].ToString() : "-",
                    LastWeekAttendance_W = row[ColumnList[2]] != DBNull.Value ? row[ColumnList[2]].ToString() : "-",
                    LastWeekAttendance_H = row[ColumnList[3]] != DBNull.Value ? row[ColumnList[3]].ToString() : "-",
                    LastWeekAttendance_F = row[ColumnList[4]] != DBNull.Value ? row[ColumnList[4]].ToString() : "-",
                    ThisWeekAttendance_M = row[ColumnList[5]] != DBNull.Value ? row[ColumnList[5]].ToString() : "-",
                    ThisWeekAttendance_T = row[ColumnList[6]] != DBNull.Value ? row[ColumnList[6]].ToString() : "-",
                    ThisWeekAttendance_W = row[ColumnList[7]] != DBNull.Value ? row[ColumnList[7]].ToString() : "-",
                    ThisWeekAttendance_F = row[ColumnList[8]] != DBNull.Value ? row[ColumnList[8]].ToString() : "-",
                    ThisWeekAttendance_H = row[ColumnList[9]] != DBNull.Value ? row[ColumnList[9]].ToString() : "-",
                    Course = row["COURSE_NAME"].ToString(),
                    Name = row["Name"].ToString(),
                    Room = row["ROOM_NUMBER"].ToString(),
                    S1 = "",
                    S2 = row["Grade"].ToString(),
                    Y1 = DateTime.Now.Year.ToString(),
                    Absences = 0,
                    Lates = 0
                });
            }

            checkTotalAbsencesLates();
        }

        public void checkTotalAbsencesLates()
        {
            for (int i = 0; i < StudentProfileDetailsList.Count; i++)
            {
                StudentProfileDetailsList[i].Absences = (StudentProfileDetailsList[i].LastWeekAttendance_M.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_T.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_W.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_T.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_F.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_M.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_T.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_W.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_T.ToString().StartsWith("A") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_F.ToString().StartsWith("A") == true ? 1 : 0);

                StudentProfileDetailsList[i].Lates = (StudentProfileDetailsList[i].LastWeekAttendance_M.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_T.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_W.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_T.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].LastWeekAttendance_F.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_M.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_T.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_W.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_T.ToString().StartsWith("L") == true ? 1 : 0) +
                                                (StudentProfileDetailsList[i].ThisWeekAttendance_F.ToString().StartsWith("L") == true ? 1 : 0);

            }
        }

        public static DataTable GetStudentAttendanceData(string studentId)
        {
            SqlConnection sqlCon = null;
            DataTable dt = new DataTable();

            try
            {
                using (sqlCon = new SqlConnection(Utils.GetConnectionStrings()))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("GetStudentProfileDetails", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@SchoolID", SqlDbType.NVarChar).Value = schoolId;
                    sql_cmnd.Parameters.AddWithValue("@StudentId", SqlDbType.NVarChar).Value = studentId;

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

        public void GetStaffProfile(string ID, string role)
        {
            int id = !string.IsNullOrEmpty(ID) ? Convert.ToInt32(ID) : 0;
            var staffProfile = kirinentities.GetStaffDatabyId(id, role).ToList();

            STAFFPROF = (from staff in staffProfile
                         select new STAFF_DIRECTORY_DESC
                         {
                             ID = Convert.ToInt32(staff.ID),
                             NAME = staff.Name,
                             EMAIL = staff.EMAIL,
                             PHONE_NUMBER = staff.PHONE_NUMBER,
                             ROOM_NUMBER = staff.ROOM_NUMBER,
                             STAFF_ID = staff.EMPLOYEE_ID,
                             imgbin = staff.IMG,
                             SCHOOL = staff.SCHOOL_NAME,
                             Grade = staff.Grade,
                             Status = staff.STATUS,
                             SUPERVISOR = staff.SUPERVISOR,
                             ROLE = staff.ROLE,
                             JURISDICTION = staff.JURISDICTION,
                             DOB = staff.BIRTHDATE

                         }).FirstOrDefault();


            if (STAFFPROF != null)
            {
                STAFF_PHOTO = ByteArrayToImage(STAFFPROF.imgbin);

                //if (STAFFPROF.imgbin is null || STAFFPROF.imgbin is byte[])
                //{
                //    BitmapImage tmp = new BitmapImage();
                //    tmp.BeginInit();
                //    tmp.UriSource = new Uri("pack://application:,,,/Images/Logo.png");

                //    tmp.DecodePixelWidth = 240;
                //    tmp.EndInit();

                //    STAFF_PHOTO = tmp;
                //}
                //else
                //{
                //    STAFF_PHOTO = ByteArrayToImage(STAFFPROF.imgbin);
                //}
            }

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

        public void ViewTeacherProfile(int ID)
        {
            var teacherProfile = kirinentities.GetTeacherProfileData(ID).ToList();


            TEACHERPROF = (from teacher in teacherProfile
                           select new TEACHERPROFILE
                           {
                               ID = teacher.ID,
                               ADDRESS = teacher.ADDRESS,
                               BIRTHDATE = (DateTime)teacher.BIRTHDATE,
                               CITIZENSHIP = teacher.CITIZENSHIP,
                               ETHNICITY = !string.IsNullOrEmpty(teacher.ETHNICITY_DIVERSITY) ? teacher.ETHNICITY_DIVERSITY.TrimEnd(',') : teacher.ETHNICITY_DIVERSITY,
                               EXTRACO = "",
                               FULLNAME = teacher.LAST_NAME + ", " + teacher.FIRST_NAME,
                               GENDER = teacher.GENDER,
                               HOMEROOM = teacher.HOMEROOM,
                               LANGUAGE = !string.IsNullOrEmpty(teacher.LANGUAGES) ? teacher.LANGUAGES.TrimEnd(',') : teacher.LANGUAGES,
                               imgbin = teacher.IMG,
                               TEACHER_ID = teacher.EMPLOYEE_ID,
                               EMAIL = teacher.EMAIL,
                               SCHOOL_NAME = teacher.SCHOOL_NAME
                           }).FirstOrDefault();

            if (TEACHERPROF.imgbin is null || TEACHERPROF.imgbin is byte[])
            {
                BitmapImage tmp = new BitmapImage();
                tmp.BeginInit();
                tmp.UriSource = new Uri("pack://application:,,,/Images/Logo.png");

                tmp.DecodePixelWidth = 240;
                tmp.EndInit();

                TEACHER_PHOTO = tmp;
            }
            else
            {
                TEACHER_PHOTO = ByteArrayToImage(TEACHERPROF.imgbin);
            }

        }

          public bool canExecuteMethod(object param)
        {
            return true;
        }

        public void ViewAdminProfile(int ID)
        {
            //var teacherProfile = kirinentities.GetTeacherProfileData(ID).ToList();


            //TEACHERPROF = (from teacher in teacherProfile
            //               select new TEACHERPROFILE
            //               {
            //                   ID = teacher.ID,
            //                   ADDRESS = teacher.ADDRESS,
            //                   BIRTHDATE = (DateTime)teacher.BIRTHDATE,
            //                   CITIZENSHIP = teacher.CITIZENSHIP,
            //                   ETHNICITY = !string.IsNullOrEmpty(teacher.ETHNICITY_DIVERSITY) ? teacher.ETHNICITY_DIVERSITY.TrimEnd(',') : teacher.ETHNICITY_DIVERSITY,
            //                   EXTRACO = "",
            //                   FULLNAME = teacher.LAST_NAME + ", " + teacher.FIRST_NAME,
            //                   GENDER = teacher.GENDER,
            //                   HOMEROOM = teacher.HOMEROOM,
            //                   LANGUAGE = !string.IsNullOrEmpty(teacher.LANGUAGES) ? teacher.LANGUAGES.TrimEnd(',') : teacher.LANGUAGES,
            //                   imgbin = teacher.IMG,
            //                   TEACHER_ID = teacher.EMPLOYEE_ID,
            //                   EMAIL = teacher.EMAIL,
            //                   SCHOOL_NAME = teacher.SCHOOL_NAME
            //               }).FirstOrDefault();

            //if (TEACHERPROF.imgbin is null)
            //{
            //    BitmapImage tmp = new BitmapImage();
            //    tmp.BeginInit();
            //    tmp.UriSource = new Uri("pack://application:,,,/Images/Logo.png");

            //    tmp.DecodePixelWidth = 240;
            //    tmp.EndInit();

            //    TEACHER_PHOTO = tmp;
            //}
            //else
            //{
            //    TEACHER_PHOTO = ByteArrayToImage(TEACHERPROF.imgbin);
            //}

        }
    }

    public class STAFF_DIRECTORY_DESC : INotifyPropertyChanged
    {
        public int RNo { get; set; }
        public int ID { get; set; }
        public string NAME { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string ROOM_NUMBER { get; set; }
        public string ROLE { get; set; }
        public string STAFF_ID { get; set; }
        public string SCHOOL { get; set; }
        public string SUPERVISOR { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public string JURISDICTION { get; set; }
        public string DOB { get; set; }
        public byte[] imgbin { get; set; }
        public BitmapImage PHOTO { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class STUDENTPROFILE_DETAILS
    {
        public string EXP { get; set; }
        public string LastWeekAttendance_M { get; set; }
        public string LastWeekAttendance_T { get; set; }
        public string LastWeekAttendance_W { get; set; }
        public string LastWeekAttendance_H { get; set; }
        public string LastWeekAttendance_F { get; set; }

        public string ThisWeekAttendance_M { get; set; }
        public string ThisWeekAttendance_T { get; set; }
        public string ThisWeekAttendance_W { get; set; }
        public string ThisWeekAttendance_H { get; set; }
        public string ThisWeekAttendance_F { get; set; }

        public string Course { get; set; }

        public string Name { get; set; }

        public string Room { get; set; }
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string Y1 { get; set; }
        public int Absences { get; set; }
        public int Lates { get; set; }

    }


    public class STUDENTPROFILE
    {
        public int ID { get; set; }
        public string FULLNAME { get; set; }
        public string BIRTHDATE { get; set; }
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
        public string LEARNING_TYPE { get; set; }
        public string SCHOOL_NAME { get; set; }

        public byte[] imgbin { get; set; }
        public BitmapImage PHOTO { get; set; }
    }

    public class TEACHERPROFILE
    {
        public int ID { get; set; }
        public string FULLNAME { get; set; }
        public string TEACHER_ID { get; set; }
        public System.DateTime BIRTHDATE { get; set; }
        public string SPECIAL_NEEDS { get; set; }
        public string CITIZENSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string LANGUAGE { get; set; }
        public string GENDER { get; set; }
        public string ETHNICITY { get; set; }
        public string HOMEROOM { get; set; }
        public string EXTRACO { get; set; }
        public string EMAIL { get; set; }
        public string SCHOOL_NAME { get; set; }
        public byte[] imgbin { get; set; }
        public BitmapImage PHOTO { get; set; }
    }

    public class SCHOOLDB_DESC : INotifyPropertyChanged
    {
        public int RowNo { get; set; }
        public int ID { get; set; }
        public string NAME { get; set; }
        public string ROOM_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string ROLE { get; set; }
        public string mROLE { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string PRIMARY_CONTACT { get; set; }
        public string RELATIONSHIP { get; set; }
        public string SCHOOL_NAME { get; set; }
        public string STATUS { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
