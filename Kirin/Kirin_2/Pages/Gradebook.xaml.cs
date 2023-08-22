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
    /// Interaction logic for Gradebook.xaml
    /// </summary>
    public partial class Gradebook : Page
    {
        public Gradebook()
        {
            InitializeComponent();
            GradeBookViewModel gradebookvm = new GradeBookViewModel();
            
            gradebookvm.getSchoolList();
            //gradebookvm.getSemesterList();
            gradebookvm.getSubjectList();

            string subjectId = App.Current.Properties["SubjectId"].ToString();
            string schoolId = App.Current.Properties["SchoolId"].ToString();
            string userName = App.Current.Properties["USERNAME"].ToString();

            string semesterId = "8";

            gradebookvm.getStudentList(subjectId, schoolId, userName);
            //gradebookvm.getAssignmentList();
            this.DataContext= gradebookvm;

        }
    }
}
