using BoldReports.UI.Xaml;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Classroom.xaml
    /// </summary>
    public partial class Scoreesheet_Report : Page
    {
        public App app;
        string schoolId = string.Empty;
        string subjectId = string.Empty;

        public Scoreesheet_Report()
        {
            app = (App)Application.Current;
            subjectId = App.Current.Properties["SubjectId"].ToString();
            schoolId = App.Current.Properties["SchoolId"].ToString();

            InitializeComponent();

            ReportParameterCollection parameters = new ReportParameterCollection();
            parameters.Add(new ReportParameter("SubjectId", subjectId));
            parameters.Add(new ReportParameter("SchoolID", schoolId));

            _reportViewer.Visible = true;
            _reportViewer.ShowCredentialPrompts = false;
            _reportViewer.ShowParameterPrompts = false;
            _reportViewer.ServerReport.ReportServerUrl = new Uri("http://laptop-90g4ari6/ReportServer");
            _reportViewer.ServerReport.ReportPath = "/Reports/Scoresheet";
            _reportViewer.ServerReport.SetParameters(parameters);
            _reportViewer.ServerReport.Refresh();
            _reportViewer.RefreshReport();
            Globals.reset = 0;
        }
    }
}
