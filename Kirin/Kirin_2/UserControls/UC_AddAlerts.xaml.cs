using Kirin_2.Models;
using Kirin_2.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_AddAlerts.xaml
    /// </summary>
    public partial class UC_AddAlerts : UserControl
    {
        string subjectId = string.Empty;
        public UC_AddAlerts()
        {
            InitializeComponent();
            
            if (App.Current != null)
            {
                subjectId = App.Current.Properties["SubjectId"].ToString();
            }

            FillStudentCombo(subjectId);
            FillAlertType();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void AddAlerts_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string studentId = cmbStudent.SelectedValue.ToString();
            string alertId = cmbalerts.SelectedValue.ToString();
            string description = txtdesc.Text;

            KIRINEntities1 kirinEntities = new KIRINEntities1();
            kirinEntities.SaveStudentAlerts(studentId, alertId, description);

            this.Visibility = Visibility.Collapsed;
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mw != null)
            {
                //SINGLE DAY VIEW
                mw.MainWindowFrame.Content = new ClassAttendance(SubjectId.Content.ToString(),DateTime.Now);
            }

            FillStudentCombo(subjectId);
            FillAlertType();
            txtdesc.Text = "";

            MessageBox.Show("Alert added successfully!");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

            FillStudentCombo(subjectId);
            FillAlertType();
            txtdesc.Text = "";
        }

        KIRINEntities1 kirinentities;
        private void FillStudentCombo(string subjectId)
        {
            kirinentities = new KIRINEntities1();
            List<GetStudentListbySubjectId_Result> lstStudent = kirinentities.GetStudentListbySubjectId(subjectId).ToList();

            cmbStudent.ItemsSource = lstStudent;
        }

        private void FillAlertType()
        {
            kirinentities = new KIRINEntities1();
            List<GetAlertType_Result> lstAlerts = kirinentities.GetAlertType().ToList();

            cmbalerts.ItemsSource = lstAlerts;
        }
    }
}
