using Kirin_2.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<string> chkBoxLst = new List<string>();
        public UC_StaffProfileModal staffProfileModal = new UC_StaffProfileModal("","");
        public UC_StudentProfileModal studentProfileModal = new UC_StudentProfileModal();

        public UC_ClassSelector classSelector = new UC_ClassSelector();
        public UC_ViewBulletin viewBulletin = new UC_ViewBulletin();
        public UC_AddColumn addColumn = new UC_AddColumn();
        public UC_ContextMenu contextmenu = new UC_ContextMenu();
        public UC_AddAssignment addAssignment = new UC_AddAssignment();
        public UC_AttendanceComment attendanceComment = new UC_AttendanceComment();
        public UC_LearningSkills score_LearningSkills = new UC_LearningSkills();
        public UC_StudentViewforPieChart studentViewforPieChart = new UC_StudentViewforPieChart();
        public UC_StudentViewforScatterPoint studentViewforScatter = new UC_StudentViewforScatterPoint();
        public UC_StudentViewforCitizenship citizenshipView = new UC_StudentViewforCitizenship();
        public UC_StudentViewforAttendanceCode attendaceCodeView = new UC_StudentViewforAttendanceCode();
        public UC_StudentViewforDiversity ethnicityView = new UC_StudentViewforDiversity();
        public UC_AddAlerts addAlerts = new UC_AddAlerts();
        public UC_SVCitizenshipBar citizenshipBar = new UC_SVCitizenshipBar();
        public UC_SVTransferStudentBar transferStdBar = new UC_SVTransferStudentBar();
        public UC_SVDOBBar dobBar = new UC_SVDOBBar();
        public UC_SVDOBPie dobPie = new UC_SVDOBPie();
        public UC_SVSportsPie sportsPie = new UC_SVSportsPie();
        public UC_SVSportsBar sportsBar = new UC_SVSportsBar();
        public UC_SVSportsAttendance sportsAttendance = new UC_SVSportsAttendance();
        public UC_SVClub clubjoined = new UC_SVClub();
        public UC_SVDiversityBar diversityBar = new UC_SVDiversityBar();
        public UC_SVDiversityPie diversityPie = new UC_SVDiversityPie();
        public UC_SVLanguageBar languageBar = new UC_SVLanguageBar();
        public UC_DeleteAssignment deleteAssignment = new UC_DeleteAssignment();
        public UC_SVTransfferedStudentStackbar tsStackedBar = new UC_SVTransfferedStudentStackbar();
        public SyncFusionCalender fusionCalender = new SyncFusionCalender();
        public SingleDayCalender singleDay = new SingleDayCalender();
        public UC_SVMap svMapView = new UC_SVMap();
        public UC_TransportAbvr transportAbvr = new UC_TransportAbvr();
        public UC_StudentSubjectList subjectListView = new UC_StudentSubjectList();
        public InsightFeedback insightFeedback = new InsightFeedback();
        public UC_StudentAssignmentList assignmentList = new UC_StudentAssignmentList();
        public UC_SVEthenicityVsSports sportsList = new UC_SVEthenicityVsSports();
        public UC_SVEthenicityVsLanguage languageList = new UC_SVEthenicityVsLanguage();
        public UC_StudentScatterPlotLegendView scatterLegend = new UC_StudentScatterPlotLegendView();
        public UC_ImageWindowMessage imageMessage = new UC_ImageWindowMessage();
        public UC_AccountAccess accountAccess = new UC_AccountAccess();
        public UC_iOSStream iosStream = new UC_iOSStream();
        public UC_EditDataFilter dataFilter = new UC_EditDataFilter();
       

        //public UC_FeedbackWindow feedbackview = new UC_FeedbackWindow(new System.Windows.Media.Imaging.BitmapImage()); 

        public double MainFrameWidth;

        void App_Startup(object sender, StartupEventArgs e)
        {
           // SplashScreen screen = new SplashScreen("Images/Logo.png");
           // screen.Show(false);
        }
       
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
           //log.Fatal("An unexpected application exception occurred", args.Exception);

            MessageBox.Show("An unexpected exception has occurred. Shutting down the application. Please check the log file for more details.");

            // Prevent default unhandled exception processing
            args.Handled = true;

            Environment.Exit(0);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //var startupView = new LoginPage.MainWindow();
            //startupView.ShowDialog();

            //base.OnStartup(e);

           //this.StartupUri = new Uri("Login.xaml", UriKind.Relative);
           this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }

    }
}
