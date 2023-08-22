using Kirin_2.Models;
using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for AssignmentMaker.xaml
    /// </summary>
    public partial class AssignmentMaker : Page
    {
        public App app = (App)Application.Current;
        GradeBookViewModel gradebookvm = new GradeBookViewModel();
        string subjectId = App.Current.Properties["SubjectId"].ToString();
        string schoolId = App.Current.Properties["SchoolId"].ToString();

        public AssignmentMaker()
        {
            InitializeComponent();
            FillSemesterCombo();

            //string userName = App.Current.Properties["USERNAME"].ToString();
            //string semesterId = cmbSemester.SelectedValue.ToString();
            string semesterId = "27";
            gradebookvm.getAssignmentList(subjectId, schoolId, semesterId);
            gradebookvm.getAssignmentCategory();
            this.DataContext = gradebookvm;
            Globals.reset = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            app.addAssignment.Refresh();
            app.addAssignment.Visibility = Visibility.Visible;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string semesterId = "27";
            gradebookvm.getAssignmentData(subjectId, schoolId, semesterId);

            if (gradebookvm.Assignment_List != null)
            {
                app.deleteAssignment.DataContext = gradebookvm;
                app.deleteAssignment.DGAssignment.ItemsSource = gradebookvm.AssignmentList;
                app.deleteAssignment.Visibility = Visibility.Visible;
            }
        }
        
        private void cmbSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var semester = "27"; //cmbSemester.SelectedValue;

            if (semester != null)
            {
                gradebookvm.getAssignmentList(subjectId, schoolId, semester.ToString());
            }
            else
            {
                FillSemesterCombo();
            }
        }

        private void FillSemesterCombo()
        {
            gradebookvm.getSemesterList();
            List<GetSemesterList_Result> lstSemesterList = gradebookvm.Semester_List.ToList();

            //cmbSemester.ItemsSource = lstSemesterList;
            //cmbSemester.SelectedValue = 27; //lstSemesterList[lstSemesterList.Count() - 1].ID;
        }
    }
}
