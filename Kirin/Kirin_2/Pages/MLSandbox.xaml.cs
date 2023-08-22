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
using System.Collections.ObjectModel;
using Kirin_2.Models;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for MLSandbox.xaml
    /// </summary>
    public partial class MLSandbox : Page
    {
        public App app;
        MLViewModel mlvm;
        KIRINEntities1 kirinentities;
        string schoolId = string.Empty;
        string subjectId = string.Empty;
        public MLSandbox()
        {
            app = (App)Application.Current;
            subjectId = App.Current.Properties["SubjectId"].ToString();
            schoolId = App.Current.Properties["SchoolId"].ToString();

            mlvm = new MLViewModel();
            this.DataContext = mlvm;
            InitializeComponent();
            FillStudentCombo(subjectId);
        }

        private void HideAllOtherClasses()
        {
            mlvm.subject2.Visibility = System.Windows.Visibility.Hidden;
            mlvm.subject3.Visibility = System.Windows.Visibility.Hidden;
            mlvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        }

        private void StudentOption_SelectionChanged_Row3(object sender, SelectionChangedEventArgs e)
        {
            if (StudentParameterOption_Row3.SelectedValue != null)
            {
                string studentId = StudentParameterOption_Row3.SelectedValue.ToString();
                string toggleType = "Assignment";
                kirinentities = new KIRINEntities1();
                List<ObservableCollection<double>> subScore = new List<ObservableCollection<double>> { };
                List<string> subjectName = new List<string> { };

                var stdSubjectId = kirinentities.GetSubjectIdforStudent(studentId).ToList();

                if (stdSubjectId.Count() > 0)
                {
                    List<string> xlist = new List<string>();
                    foreach (GetSubjectIdforStudent_Result item in stdSubjectId)
                    {
                        string subId = item.ID.ToString();

                        var studentGrades = kirinentities.GetGradesbySubjectId(subId, studentId, toggleType).ToList();

                        if (studentGrades.Count() > 0)
                        {
                            subjectName.Add(item.COURSE_NAME);
                            ObservableCollection<double> stdGrades = new ObservableCollection<double>();

                            foreach (GetStudentGradebySubjectId_Result result in studentGrades)
                            {
                                stdGrades.Add(Convert.ToDouble(result.Percentage));

                                if (!xlist.Contains(result.MonthName))
                                {
                                    xlist.Add(result.MonthName);
                                }
                                else
                                {
                                    xlist.Add("");
                                }
                            }

                            if (studentGrades.Count() > 0)
                            {
                                subScore.Add(stdGrades);
                            }
                        }

                    }
                    string[] xAxis = xlist.ToArray();

                    mlvm.LineChartSampleRow3(subScore, subjectName, xAxis);
                }
                else
                {
                    string[] xAxis = new string[] { };
                    mlvm.LineChartSampleRow3(subScore, subjectName, xAxis);
                }
            }
            else
            {
                FillStudentCombo(subjectId);
            }
        }

        //private void StudentOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBoxItem selectedStudent = (ComboBoxItem)StudentParameterOption.SelectedItem;
        //    if (selectedStudent.Content == null)
        //    {
        //        mlvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 10, 20, 15, 18, 19, 20, 21, 18, 25, 19, 21, 22 };
        //        mlvm.PredictionSubject1 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 22, 23, 25, 26, 23 };
        //        mlvm.MLLineChart();
        //    }
        //    else
        //    {
        //        if (selectedStudent.Content.ToString() == "Student 1")
        //        {
        //            mlvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 10, 20, 15, 18, 19, 20, 21, 18, 25, 19, 21, 22 };
        //            mlvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 35, 33, 38, 40, 42, 39, 38, 37, 31, 32, 34, 35 };
        //            mlvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
        //            mlvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 45, 46, 48, 44, 50, 49, 48, 44, 47, 45, 46, 49 };
        //            mlvm.PredictionSubject1 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 22, 23, 25, 26, 23 };
        //            mlvm.PredictionSubject2 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 35, 36, 37, 38, 39 };
        //            mlvm.PredictionSubject3 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 63, 57, 59, 60, 61 };
        //            mlvm.PredictionSubject4 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 49, 46, 44, 48, 49 };
        //            mlvm.MLLineChart();
        //            mlvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict4.Visibility = System.Windows.Visibility.Hidden;
        //        }

        //        if (selectedStudent.Content.ToString() == "Student 2")
        //        {
        //            mlvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 34.23, 36.83, 30.63, 34.19, 36.74, 29.54, 36.38, 33.49, 29.31, 35.35, 39.55, 37.8 };
        //            mlvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 58.1, 50.71, 54.41, 55.7, 58.01, 53.68, 60.05, 59.46, 51.45, 55.95, 58.14, 59.8 };
        //            mlvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
        //            mlvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 70.2, 71.21, 66.93, 68.85, 67.58, 62.56, 68.95, 71.19, 68.71, 66.4, 67.26, 71.27 };
        //            mlvm.PredictionSubject1 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 37.8, 36.83, 30.63, 34.19, 36.74 };
        //            mlvm.PredictionSubject2 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 59.8, 50.71, 54.41, 55.7, 58.01 };
        //            mlvm.PredictionSubject3 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 55.69, 65.35, 55.3, 54.09, 72.45 };
        //            mlvm.PredictionSubject4 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 71.27, 71.21, 66.93, 68.85, 67.58 };
        //            mlvm.MLLineChart();
        //            mlvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict4.Visibility = System.Windows.Visibility.Hidden;
        //        }

        //        if (selectedStudent.Content.ToString() == "Student 3")
        //        {
        //            mlvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 97.93, 91.62, 97.15, 95.82, 94.48, 92.05, 93.87, 94.18, 96.93, 99.51, 94.58, 99.6 };
        //            mlvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 90.25, 88.71, 91.95, 90.31, 90.46, 91.89, 90.06, 88.86, 87.18, 89.87, 90.54, 88.13 };
        //            mlvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 85, 88, 85, 83, 90, 90.5, 89, 95, 96, 95, 96.5, 97 };
        //            mlvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 92, 93, 94, 95, 95, 94, 94, 95, 98, 93, 93, 94 };
        //            mlvm.PredictionSubject1 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 99.6, 94.18, 96.93, 99.51, 94.58 };
        //            mlvm.PredictionSubject2 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 88.13, 88.86, 87.18, 89.87, 90.54 };
        //            mlvm.PredictionSubject3 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 97, 95, 96, 95, 96.5 };
        //            mlvm.PredictionSubject4 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 94, 95, 98, 93, 93 };
        //            mlvm.MLLineChart();
        //            mlvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict4.Visibility = System.Windows.Visibility.Hidden;
        //        }
        //        if (selectedStudent.Content.ToString() == "Student 4")
        //        {
        //            mlvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 78.48, 74.69, 75.18, 73.84, 74.84, 76.62, 79.81, 65.07, 78.43, 83.01, 70.32, 84.26 };
        //            mlvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 76.16, 79.1, 75.98, 77.03, 85.8, 87.93, 88.86, 76.06, 79.9, 92.43, 78.17, 86.73 };
        //            mlvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 55, 59, 55, 56, 58, 60, 61, 56, 60, 58, 57, 63 };
        //            mlvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
        //            mlvm.PredictionSubject1 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 84.26, 65.07, 78.43, 83.01, 70.32 };
        //            mlvm.PredictionSubject2 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 86.73, 76.06, 79.9, 92.43, 78.17 };
        //            mlvm.PredictionSubject3 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 63, 56, 60, 58, 57 };
        //            mlvm.PredictionSubject4 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 55.69, 57.23, 67.04, 59.65, 54.31 };
        //            mlvm.MLLineChart();
        //            mlvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict4.Visibility = System.Windows.Visibility.Hidden;
        //        }
        //        if (selectedStudent.Content.ToString() == "Student 5")
        //        {
        //            mlvm.Subject1 = new System.Collections.ObjectModel.ObservableCollection<double> { 85, 88, 85, 83, 90, 90.5, 89, 95, 96, 95, 96.5, 97 };
        //            mlvm.Subject2 = new System.Collections.ObjectModel.ObservableCollection<double> { 78.48, 74.69, 75.18, 73.84, 74.84, 76.62, 79.81, 65.07, 78.43, 83.01, 70.32, 84.26 };
        //            mlvm.Subject3 = new System.Collections.ObjectModel.ObservableCollection<double> { 61.82, 65.35, 55.3, 54.09, 72.45, 52.08, 72.16, 57.23, 67.04, 59.65, 54.31, 55.69 };
        //            mlvm.Subject4 = new System.Collections.ObjectModel.ObservableCollection<double> { 90.25, 88.71, 91.95, 90.31, 90.46, 91.89, 90.06, 88.86, 87.18, 89.87, 90.54, 88.13 };
        //            mlvm.PredictionSubject1 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 97, 95, 96, 95, 96.5 };
        //            mlvm.PredictionSubject2 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 84.26, 65.07, 78.43, 83.01, 70.32 };
        //            mlvm.PredictionSubject3 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 55.69, 57.23, 67.04, 59.65, 54.31 };
        //            mlvm.PredictionSubject4 = new System.Collections.ObjectModel.ObservableCollection<double> { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, 88.13, 88.86, 87.18, 89.87, 90.54 };
        //            mlvm.MLLineChart();
        //            mlvm.subject2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.subject4.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict2.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict3.Visibility = System.Windows.Visibility.Hidden;
        //            mlvm.predict4.Visibility = System.Windows.Visibility.Hidden;
        //        }
        //    }
        //}

        public void ToggleSeries(object sender, System.EventArgs e)
        {
            var x = (CheckBox)sender;
            mlvm.subject2.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mlvm.subject3.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mlvm.subject4.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mlvm.predict2.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mlvm.predict3.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mlvm.predict4.Visibility = x.IsChecked == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        private void FillStudentCombo(string subjectId)
        {
            kirinentities = new KIRINEntities1();
            List<GetStudentListbySubjectId_Result> lstStudent = kirinentities.GetStudentListbySubjectId(subjectId).ToList();

            StudentParameterOption_Row3.ItemsSource = lstStudent;

            StudentParameterOption_Row3.SelectedValue = "2";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            GridRoot.Width = aw - 30;
            GridRoot.Height = ah - 120;
            BorderBox.Height = GridRoot.Height + 20;
            chartBox.Height = BorderBox.Height - 65;
            //MLLineChart.Width = GridRoot.Width - 50;
        }
    }
}
