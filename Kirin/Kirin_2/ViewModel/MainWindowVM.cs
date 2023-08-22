using Kirin_2.Model;
using Kirin_2.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Kirin_2.Model.Menu;

namespace Kirin_2.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private SubMenuItemData subMenuItem;
        public List<ItemMenu> MenuList;
        public App app = (App)Application.Current;
        public ICommand cGetClassList { get; set; }

        public KIRINEntities1 KirinEntities1;


        private ObservableCollection<GetSubjectList_Result> _ClassListing;
        public ObservableCollection<GetSubjectList_Result> ClassListing
        {
            get { return this._ClassListing; }
            set
            {
                this._ClassListing = value;
                OnPropertyChanged("ClassListing");
            }
        }


        public void GetClassListing(string param)
        {
            //var schoolID = Convert.ToInt32(param);
            var schoolID = param;
            var clisting = KirinEntities1.getClassListing(schoolID).ToList();

            ClassListing = new ObservableCollection<GetSubjectList_Result>(from list in clisting
                                                                           select new GetSubjectList_Result
                                                                           {
                                                                               COURSE_NAME = list.COURSE_NAME,
                                                                               SCHOOL_ID = list.SCHOOL_ID,
                                                                               SEMESTER_ID = Convert.ToInt32(list.SEMESTER_ID),
                                                                               COURSE_CODE = list.COURSE_CODE,
                                                                               TEACHER_ID = Convert.ToInt32(list.TEACHER_ID),
                                                                               ID = list.ID
                                                                           });
        }


        private void Execute()
        {
            // //our logic comes here


            //// string SMT = subMenuItem.SubMenuText.Replace(" ", string.Empty);
            // if (!string.IsNullOrEmpty(menutext))
            //     navigateToPage(menutext);
        }

        private void navigateToPage(string Menu)
        {
            //We will search for our Main Window in open windows and then will access the frame inside it to set the navigation to desired page..
            //lets see how... ;)
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
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


        //to call resource dictionary in our code behind
        public ICommand SubMenuCommand { get; private set; }

        public string userNameRole { get; set; }
        public string subjectCode { get; set; }
        public string schoolId;
        public MainWindowVM(string userName)
        {
            KirinEntities1 = new KIRINEntities1();

            GetHelpListData();

            var userData = KirinEntities1.GetUserData(userName).ToList();

            var userInfo = (from obj in userData
                            select new GetUserData_Result
                            {
                                Name = obj.Name,
                                Role = obj.Role,
                                COURSE_CODE = obj.COURSE_CODE,
                                SubjectId = obj.SubjectId,
                                SCHOOL_ID = obj.SCHOOL_ID
                            }).FirstOrDefault();

            if (userData.Count > 0)
            {
                userNameRole = userInfo.Name + " > " + userInfo.Role;
                subjectCode = !string.IsNullOrWhiteSpace(userInfo.COURSE_CODE) ? "[" + userInfo.COURSE_CODE + "]" : "";
                app.Properties["SubjectId"] = userInfo.SubjectId;
                app.Properties["SchoolId"] = userInfo.SCHOOL_ID;

                schoolId = app.Properties["SchoolId"].ToString();

                GetNewNotifications(schoolId, userName);

                GetEarlierNotifications(schoolId, userName);

                GetSchoolAlerts(schoolId, userName);

                GetResentSearches();

                GetAdvancedSearchData(schoolId, userInfo.SubjectId.ToString(), userName, "Students");
                GetAdvancedSearchData(schoolId, "", "", "Staff");
                GetAdvancedSearchData(schoolId, "", "", "Assignment");
                GetAdvancedSearchData(schoolId, userInfo.SubjectId.ToString(), userName, "");
            }

            //ResourceDictionary dict = Application.LoadComponent(new Uri("/Kirin_2;component/Assets/IconDictionary.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
            //subMenuItem = new SubMenuItemData();
            ////SubMenuCommand = new CommandVM(Execute);

            //MenuList = new List<ItemMenu>();

            //var menuAnalytics = new List<SubItem>();
            //menuAnalytics.Add(new SubItem("Dashboard", "Dashboard")); ;
            ////menuAnalytics.Add(new SubItem("Data Sandbox",  "DataSandBox"));
            ////menuAnalytics.Add(new SubItem("Users",  "Users", subItemUsers));
            //var itemAnalytics = new ItemMenu("Analytics", menuAnalytics, PackIconKind.ChartLine, "Analytics");

            //var menuPlanner = new List<SubItem>();
            //menuPlanner.Add(new SubItem("Grade Book", "GradeBook"));
            //menuPlanner.Add(new SubItem("Attendance", "Attendance"));
            //menuPlanner.Add(new SubItem("Class Calendar", "Classcalendar"));
            //var itemPlanner = new ItemMenu("Planner", menuPlanner, PackIconKind.Diary, "Planner");

            //var menuMachineLearning = new List<SubItem>();
            //menuMachineLearning.Add(new SubItem("Predictive Analysis", "Machinelearning"));
            //menuMachineLearning.Add(new SubItem("Sandbox", "Machinelearning"));
            //var itemMachineLearning = new ItemMenu("Machine Learning", menuMachineLearning, PackIconKind.BulbOn, "Machinelearning");

            //var menuResourceManagement = new List<SubItem>();
            //menuResourceManagement.Add(new SubItem("Finical mapping", "ResourceManagement"));
            //menuResourceManagement.Add(new SubItem("Assessments", "ResourceManagement"));
            //var itemResourceManagement = new ItemMenu("Resource Management", menuResourceManagement, PackIconKind.KeyChainVariant, "ResourceManagement");

            //MenuList.Add(itemAnalytics);
            //MenuList.Add(itemPlanner);
            //MenuList.Add(itemMachineLearning);
            //MenuList.Add(itemResourceManagement);
        }

        private ObservableCollection<HelpTopBar> _HelpList;
        public ObservableCollection<HelpTopBar> HelpList
        {
            get { return _HelpList; }
            set
            {
                _HelpList = value;
                OnPropertyChanged(nameof(HelpList));
            }
        }

        public void GetHelpListData()
        {
            HelpList = new ObservableCollection<HelpTopBar>();

            HelpList.Add(new HelpTopBar { ID = "1", Help = "Set up Analytics for a website (Universal Analytics)" });
            HelpList.Add(new HelpTopBar { ID = "2", Help = "Tracking ID and property number" });
            HelpList.Add(new HelpTopBar { ID = "3", Help = "[GA4]Set up Analytics for a website and/or app" });
            HelpList.Add(new HelpTopBar { ID = "4", Help = "Get started with Analytics " });
            HelpList.Add(new HelpTopBar { ID = "5", Help = "Admin page" });
        }

        private ObservableCollection<NewNotifications> _newNotificationList;
        public ObservableCollection<NewNotifications> NewNotificationList
        {
            get { return _newNotificationList; }
            set
            {
                _newNotificationList = value;
                OnPropertyChanged(nameof(NewNotificationList));
            }
        }

        public void GetNewNotifications(string schoolId, string userName)
        {
            var newNotification = KirinEntities1.GetNewNotifications(schoolId, userName);

            NewNotificationList = new ObservableCollection<NewNotifications>(from n in newNotification
                                                                             select new NewNotifications
                                                                             {
                                                                                 ID = n.ID.ToString(),
                                                                                 Message = n.MESSAGE,
                                                                                 Type = n.TYPE,
                                                                                 Template = n.TEMPLATE,
                                                                                 Student = n.STUDENT,
                                                                                 Class = n.CLASS,
                                                                                 UpdatedBy = n.UPDATEDBY,
                                                                                 Document = n.DOCUMENT,
                                                                                 UpdatedDate = n.UpdatedDate,
                                                                                 Icon = ByteArrayToImage(n.ICON),
                                                                                 DesignedMessage = GetNewMessage(n),
                                                                                 Navigate = n.PAGE_LINK,
                                                                                 Originaldate = (DateTime)n.UPDATED_DATE
                                                                             });
        }


        private ObservableCollection<EarlierNotifications> _EarlierNotificationList;
        public ObservableCollection<EarlierNotifications> EarlierNotificationList
        {
            get { return _EarlierNotificationList; }
            set
            {
                _EarlierNotificationList = value;
                OnPropertyChanged(nameof(EarlierNotificationList));
            }
        }

        public void GetEarlierNotifications(string schoolId, string userName)
        {
            var eNotification = KirinEntities1.GetEarlierNotification(schoolId, userName);

            EarlierNotificationList = new ObservableCollection<EarlierNotifications>(from e in eNotification
                                                                                     select new EarlierNotifications
                                                                                     {
                                                                                         ID = e.ID.ToString(),
                                                                                         Message = e.MESSAGE,
                                                                                         Type = e.TYPE,
                                                                                         Template = e.TEMPLATE,
                                                                                         Student = e.STUDENT,
                                                                                         Class = e.CLASS,
                                                                                         UpdatedBy = e.UPDATEDBY,
                                                                                         Document = e.DOCUMENT,
                                                                                         UpdatedDate = e.UpdatedDate,
                                                                                         Icon = ByteArrayToImage(e.ICON),
                                                                                         DesignedMessage = GetEarlierMessage(e),
                                                                                         Navigate = e.PAGE_LINK,
                                                                                         Originaldate = (DateTime)e.UPDATED_DATE
                                                                                     });
        }

        public static string GetNewMessage(GetNewNotifications_Result n)
        {
            string finalMsg = string.Empty;

            switch (n.TYPE)
            {
                case "Attendance":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.STUDENT + "").
                        Replace("<2>", "" + n.CLASS + "");
                    break;
                case "Comment":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.UPDATEDBY + "").
                       Replace("<2>", "" + n.STUDENT + "");
                    break;
                case "Insight":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.STUDENT + "");
                    break;
                case "Download":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.DOCUMENT + "");
                    break;
            }

            return finalMsg;
        }

        public static string GetEarlierMessage(GetEarlierNotification_Result n)
        {
            string finalMsg = string.Empty;

            switch (n.TYPE)
            {
                case "Attendance":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.STUDENT + "").
                        Replace("<2>", "" + n.CLASS + "");
                    break;
                case "Comment":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.UPDATEDBY + "").
                       Replace("<2>", "" + n.STUDENT + "");
                    break;
                case "Insight":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.STUDENT + "");
                    break;
                case "Download":
                    finalMsg = n.TEMPLATE.Replace("<1>", "" + n.DOCUMENT + "");
                    break;
            }

            return finalMsg;
        }


        private ObservableCollection<SchoolAlerts> _SchoolAlertsList;
        public ObservableCollection<SchoolAlerts> SchoolAlertsList
        {
            get { return _SchoolAlertsList; }
            set
            {
                _SchoolAlertsList = value;
                OnPropertyChanged(nameof(SchoolAlertsList));
            }
        }

        public void GetSchoolAlerts(string schoolId, string userName)
        {
            var sAlerts = KirinEntities1.GetSchoolAlerts(schoolId, userName);

            SchoolAlertsList = new ObservableCollection<SchoolAlerts>(from s in sAlerts
                                                                      select new SchoolAlerts
                                                                      {
                                                                          ID = s.ID.ToString(),
                                                                          Message = s.MESSAGE,
                                                                          Type = s.TYPE,
                                                                          Template = s.TEMPLATE,
                                                                          Student = s.STUDENT,
                                                                          Class = s.CLASS,
                                                                          UpdatedBy = s.UPDATEDBY,
                                                                          Document = s.DOCUMENT,
                                                                          UpdatedDate = s.UpdatedDate,
                                                                          Icon = ByteArrayToImage(s.ICON),
                                                                          Navigate = s.PAGE_LINK,
                                                                          MovedType = "S",
                                                                          Visibility = Visibility.Hidden,
                                                                          date = Convert.ToDateTime(s.UPDATED_DATE)
                                                                      });
        }

        private ObservableCollection<ResentSearches> _ResentSearchList;
        public ObservableCollection<ResentSearches> ResentSearchList
        {
            get { return _ResentSearchList; }
            set
            {
                _ResentSearchList = value;
                OnPropertyChanged(nameof(ResentSearchList));
            }
        }

        public void GetResentSearches()
        {
            ResentSearchList = new ObservableCollection<ResentSearches>();
            ResentSearches r1 = new ResentSearches();
            r1.ID = "1";
            r1.Keyword = "Spencer";
            ResentSearchList.Add(r1);

            ResentSearches r2 = new ResentSearches();
            r2.ID = "2";
            r2.Keyword = "Jolene";
            ResentSearchList.Add(r2);

            ResentSearches r3 = new ResentSearches();
            r3.ID = "3";
            r3.Keyword = "Attendance";
            ResentSearchList.Add(r3);
        }

        public ObservableCollection<AdvancedSearch> _AdvancedSearchList;
        public ObservableCollection<AdvancedSearch> AdvancedSearchList
        {
            get { return _AdvancedSearchList; }
            set
            {
                _AdvancedSearchList = value;
                OnPropertyChanged(nameof(AdvancedSearchList));
            }
        }

        public ObservableCollection<AdvancedSearchStaff> _AdvancedSearchStaffList;
        public ObservableCollection<AdvancedSearchStaff> AdvancedSearchStaffList
        {
            get { return _AdvancedSearchStaffList; }
            set
            {
                _AdvancedSearchStaffList = value;
                OnPropertyChanged(nameof(AdvancedSearchStaffList));
            }
        }

        public ObservableCollection<SearchAssignment> _SearchAssignmentList;
        public ObservableCollection<SearchAssignment> SearchAssignmentList
        {
            get { return _SearchAssignmentList; }
            set
            {
                _SearchAssignmentList = value;
                OnPropertyChanged(nameof(SearchAssignmentList));
            }
        }

        public ObservableCollection<GlobalSearch> _GlobalSearchList;
        public ObservableCollection<GlobalSearch> GlobalSearchList
        {
            get { return _GlobalSearchList; }
            set
            {
                _GlobalSearchList = value;
                OnPropertyChanged(nameof(GlobalSearchList));
            }
        }


        public void GetAdvancedSearchData(string schoolID, string subjectId, string userName, string role)
        {
            if (role == "Students")
            {
                var students = KirinEntities1.GetStudentsforSearch(schoolID, subjectId, userName).ToList();

                AdvancedSearchList = new ObservableCollection<AdvancedSearch>(from s in students
                                                                              select new AdvancedSearch
                                                                              {
                                                                                  ID = s.ID.ToString(),
                                                                                  Name = s.FIRST_NAME + " " + s.LAST_NAME,
                                                                                  LastName = s.LAST_NAME,
                                                                                  FirstName = s.FIRST_NAME,
                                                                                  Reference = s.Reference,
                                                                                  Role = "STUDENT"
                                                                              });
            }
            else if (role == "Staff")
            {
                var staff = KirinEntities1.GetStaffforSearch(schoolID).ToList();

                AdvancedSearchStaffList = new ObservableCollection<AdvancedSearchStaff>(from s in staff
                                                                                        select new AdvancedSearchStaff
                                                                                        {
                                                                                            ID = s.ID.ToString(),
                                                                                            Name = s.FIRST_NAME + " " + s.LAST_NAME,
                                                                                            LastName = s.LAST_NAME,
                                                                                            FirstName = s.FIRST_NAME,
                                                                                            Reference = s.Reference,
                                                                                            Role = s.ROLE
                                                                                        });
            }
            else if (role == "Assignment")
            {
                var assignments = KirinEntities1.GetAssignmentsforSearch().ToList();

                SearchAssignmentList = new ObservableCollection<SearchAssignment>(from a in assignments
                                                                                  select new SearchAssignment
                                                                                  {
                                                                                      ID = a.ASSIGNMENT_ID.ToString(),
                                                                                      Name = a.NAME.Trim(),
                                                                                      LastName = "",
                                                                                      FirstName = "",
                                                                                      Reference = a.Reference,
                                                                                      Role = "Assignment"
                                                                                  });
            }
            else
            {
                var students = KirinEntities1.GetStudentsforSearch(schoolID, subjectId, userName).ToList();

                var staff = KirinEntities1.GetStaffforSearch(schoolID).ToList();

                var assignments = KirinEntities1.GetAssignmentsforSearch().ToList();

                //-------------------Student Search----------------------//
                GlobalSearchList = new ObservableCollection<GlobalSearch>(from s in students
                                                                          select new GlobalSearch
                                                                          {
                                                                              ID = s.ID.ToString(),
                                                                              Name = s.FIRST_NAME + " " + s.LAST_NAME,
                                                                              LastName = s.LAST_NAME,
                                                                              FirstName = s.FIRST_NAME,
                                                                              Reference = s.Reference,
                                                                              Role = "STUDENT"
                                                                          });

                //------------------Staff Search---------------------//
                foreach (var s in staff)
                {
                    GlobalSearchList.Add(new GlobalSearch
                    {
                        ID = s.ID.ToString(),
                        Name = s.FIRST_NAME + " " + s.LAST_NAME,
                        LastName = s.LAST_NAME,
                        FirstName = s.FIRST_NAME,
                        Reference = s.Reference,
                        Role = s.ROLE
                    });
                }

                //---------------Assignment Search------------------//
                foreach (var a in assignments)
                {
                    GlobalSearchList.Add(new GlobalSearch
                    {
                        ID = a.ASSIGNMENT_ID.ToString(),
                        Name = a.NAME.Trim(),
                        LastName = "",
                        FirstName = "",
                        Reference = a.Reference,
                        Role = "Assignment"
                    });
                }

                //--------------------Page wise Search-----------------------//
                GlobalSearchList.Add(new GlobalSearch
                {
                    ID = "1",
                    Name = "SB Database",
                    LastName = "",
                    FirstName = "",
                    Reference = ":in Pages",
                    Role = "Page"
                });

                GlobalSearchList.Add(new GlobalSearch
                {
                    ID = "2",
                    Name = "School Directory",
                    LastName = "",
                    FirstName = "",
                    Reference = ":in Pages",
                    Role = "Page"
                });

                GlobalSearchList.Add(new GlobalSearch
                {
                    ID = "3",
                    Name = "Class Directory",
                    LastName = "",
                    FirstName = "",
                    Reference = ":in Pages",
                    Role = "Page"
                });

                GlobalSearchList.Add(new GlobalSearch
                {
                    ID = "4",
                    Name = "ScoreSheet",
                    LastName = "",
                    FirstName = "",
                    Reference = ":in Pages",
                    Role = "Page"
                });

                GlobalSearchList.Add(new GlobalSearch
                {
                    ID = "5",
                    Name = "Alerts",
                    LastName = "",
                    FirstName = "",
                    Reference = ":in Pages",
                    Role = "Page"
                });

                GlobalSearchList.Add(new GlobalSearch
                {
                    ID = "5",
                    Name = "Assignment",
                    LastName = "",
                    FirstName = "",
                    Reference = ":in Pages",
                    Role = "Page"
                });
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

        public class SubMenuItemsData
        {
            private SubMenuItemData subMenuItem;

            public SubMenuItemsData()
            {
                //SubMenuCommand = new CommandVM(Execute);
                subMenuItem = new SubMenuItemData();
            }

            public ICommand SubMenuCommand { get; }

            private void Execute()
            {
                //our logic comes here
                string SMT = subMenuItem.SubMenuText.Replace(" ", string.Empty);
                if (!string.IsNullOrEmpty(SMT))
                    navigateToPage(SMT);
            }

            private void navigateToPage(string Menu)
            {
                //We will search for our Main Window in open windows and then will access the frame inside it to set the navigation to desired page..
                //lets see how... ;)
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                    }
                }
            }
        }

        public class SelectorClassInfo : INotifyPropertyChanged
        {
            public string ClassName
            {
                get; set;
            }



            public string ClassCode
            {
                get;
                set;
            }


            public string SchoolID
            {
                get;
                set;
            }


            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public class HelpTopBar
        {
            public string ID { get; set; }
            public string Help { get; set; }

        }

        public class NewNotifications
        {
            public string ID { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public string UpdatedDate { get; set; }
            public string Template { get; set; }
            public string Student { get; set; }
            public string Class { get; set; }
            public string UpdatedBy { get; set; }
            public string Document { get; set; }
            public string DesignedMessage { get; set; }
            public string Navigate { get; set; }
            public DateTime Originaldate { get; set; }
            public BitmapImage Icon { get; set; }
        }

        public class EarlierNotifications
        {
            public string ID { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public string UpdatedDate { get; set; }
            public string Template { get; set; }
            public string Student { get; set; }
            public string Class { get; set; }
            public string UpdatedBy { get; set; }
            public string Document { get; set; }
            public string DesignedMessage { get; set; }
            public string Navigate { get; set; }
            public DateTime Originaldate { get; set; }
            public BitmapImage Icon { get; set; }
        }

        public class SchoolAlerts
        {
            public string ID { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public string UpdatedDate { get; set; }
            public string Template { get; set; }
            public string Student { get; set; }
            public string Class { get; set; }
            public string UpdatedBy { get; set; }
            public string Document { get; set; }
            public string Navigate { get; set; }
            public string MovedType { get; set; }
            public BitmapImage Icon { get; set; }
            public Visibility Visibility { get; set; }
            public DateTime date { get; set; }
        }

        public class ResentSearches
        {
            public string ID { get; set; }
            public string Keyword { get; set; }
        }

        public class AdvancedSearch
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Reference { get; set; }
            public string Role { get; set; }
            public BitmapImage profile { get; set; }
        }

        public class AdvancedSearchStaff
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Reference { get; set; }
            public string Role { get; set; }
            public BitmapImage profile { get; set; }
        }

        public class SearchAssignment
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Reference { get; set; }
            public string Role { get; set; }
            public BitmapImage profile { get; set; }
        }

        public class GlobalSearch
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Reference { get; set; }
            public string Role { get; set; }
            public BitmapImage profile { get; set; }
        }

    }
}
