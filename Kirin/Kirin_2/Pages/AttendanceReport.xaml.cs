using BoldReports.UI.Xaml;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.Pages
{
    public partial class AttendanceReport : Page
    {
        public App app;
        string schoolId = string.Empty;
        string subjectId = string.Empty;

        public AttendanceReport()
        {
            app = (App)Application.Current;
            subjectId = App.Current.Properties["SubjectId"].ToString();
            schoolId = App.Current.Properties["SchoolId"].ToString();

            InitializeComponent();

            ReportParameterCollection parameters = new ReportParameterCollection();
            parameters.Add(new ReportParameter("SubjectID", subjectId));
            parameters.Add(new ReportParameter("SchoolID", schoolId));
            parameters.Add(new ReportParameter("FromDate", "2022-06-02"));
            parameters.Add(new ReportParameter("ToDate", "2022-06-09"));

            _reportViewer.Visible = true;
            _reportViewer.ShowCredentialPrompts = false;
            _reportViewer.ShowParameterPrompts = false;
            _reportViewer.ServerReport.ReportServerUrl = new Uri("http://laptop-90g4ari6/ReportServer");
            _reportViewer.ServerReport.ReportPath = "/Reports/AttendanceReport";
            _reportViewer.ServerReport.SetParameters(parameters);
            _reportViewer.ServerReport.Refresh();
            _reportViewer.RefreshReport();
            Globals.reset = 0;
        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
            string fromDate = fDate.SelectedDate.ToString();
            string toDate = tDate.SelectedDate.ToString();

            ReportParameterCollection parameters = new ReportParameterCollection();
            parameters.Add(new ReportParameter("SubjectID", subjectId));
            parameters.Add(new ReportParameter("SchoolID", schoolId));
            parameters.Add(new ReportParameter("FromDate", fromDate));
            parameters.Add(new ReportParameter("ToDate", toDate));

            _reportViewer.Visible = true;
            _reportViewer.ShowCredentialPrompts = false;
            _reportViewer.ShowParameterPrompts = false;
            _reportViewer.ServerReport.ReportServerUrl = new Uri("http://laptop-90g4ari6/ReportServer");
            _reportViewer.ServerReport.ReportPath = "/Reports/AttendanceReport";
            _reportViewer.ServerReport.SetParameters(parameters);
            _reportViewer.ServerReport.Refresh();
            _reportViewer.RefreshReport();
        }
    }
}
