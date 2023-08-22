using Kirin_2.Models;
using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for Datasandbox.xaml
    /// </summary>
    public partial class Datasandbox : Page
    {
        public App app;
        SandboxViewModel sbvm;
        KIRINEntities1 kirinentities;
        string schoolId = string.Empty;
        string subjectId = string.Empty;
        public Datasandbox()
        {
            app = (App)Application.Current;
            subjectId = App.Current.Properties["SubjectId"].ToString();
            schoolId = App.Current.Properties["SchoolId"].ToString();

            sbvm = new SandboxViewModel();

            this.DataContext = sbvm;
            InitializeComponent();
            FillParameterCombo();
            FillParameterComboRow3();
            FillStudentCombo(subjectId);
        }

        private void StudentOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudent.SelectedValue != null)
            {
                string studentId = cmbStudent.SelectedValue.ToString();

                sbvm.GetSubjectGrades(studentId);
            }
        }

        private void Param1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBox param = (ComboBox)sender;
            //ComboBoxItem selectedItem = (ComboBoxItem)param.SelectedItem;
            string selectedValue = ParameterOption1.SelectedItem.ToString();
            ParameterOption2.Items.Clear();
            //GraphOption.Items.Clear();
            switch (selectedValue)
            {
                case "Grades":
                    {
                        ParameterOption2.Items.Add("Diversity");
                        ParameterOption2.Items.Add("Citizenship");
                        ParameterOption2.Items.Add("Languages");
                        //ParameterOption2.Items.Add("Engagement levels");
                        //ParameterOption2.Items.Add("Attendance");
                        //ParameterOption2.Items.Add("Months");
                        //ParameterOption2.Items.Add("Address");

                        break;
                    }
                default:
                    {
                        ParameterOption2.Items.Add("");
                        break;
                    }
            }

            ParameterOption2.SelectedItem = ParameterOption2.Items[0].ToString();
        }

        private void Param2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ParameterOption2.SelectedItem.ToString();
            GraphOption.Items.Clear();

            switch (selectedValue)
            {
                case "Diversity":
                    {
                        GraphOption.Items.Add("Bubble");
                        GraphOption.Items.Add("Radar");
                        GraphOption.Items.Add("Area");

                        break;
                    }
                case "Citizenship":
                    {
                        GraphOption.Items.Add("Bubble");
                        GraphOption.Items.Add("Radar");
                        GraphOption.Items.Add("Area");

                        break;
                    }
                case "Languages":
                    {
                        GraphOption.Items.Add("Bubble");
                        GraphOption.Items.Add("Radar");
                        GraphOption.Items.Add("Area");

                        break;
                    }
                //case "Attendance":
                //    {
                //        GraphOption.Items.Add("Bubble");
                //        GraphOption.Items.Add("Histogram");
                //        GraphOption.Items.Add("Area");

                //        break;
                //    }
                default:
                    {
                        GraphOption.Items.Add("");
                        break;
                    }
            }

            GraphOption.SelectedItem = GraphOption.Items[0].ToString();
        }

        private void GraphOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GraphOption.SelectedItem != null)
            {
                string selectedParameter2 = ParameterOption2.SelectedItem.ToString();
                string selectedGraphType = GraphOption.SelectedItem.ToString();

                if (selectedParameter2 == "Diversity")
                {
                    if (selectedGraphType == "Area")
                    {
                        diversityAreaChart.Visibility = Visibility.Visible;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Visible;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Bubble")
                    {
                        diversityBubble.Visibility = Visibility.Visible;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Visible;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Radar")
                    {
                        diversityRadarChart.Visibility = Visibility.Visible;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Visible;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                }
                if (selectedParameter2 == "Citizenship")
                {
                    if (selectedGraphType == "Area")
                    {
                        citizenshipAreaChart.Visibility = Visibility.Visible;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Visible;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Bubble")
                    {
                        citizenshipBubble.Visibility = Visibility.Visible;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Visible;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Radar")
                    {
                        citizenshipRadarChart.Visibility = Visibility.Visible;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Visible;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                }
                if (selectedParameter2 == "Languages")
                {
                    if (selectedGraphType == "Area")
                    {
                        languageAreaChart.Visibility = Visibility.Visible;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Visible;
                    }
                    else if (selectedGraphType == "Bubble")
                    {
                        languageBubble.Visibility = Visibility.Visible;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Visible;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Radar")
                    {
                        languageRadarChart.Visibility = Visibility.Visible;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Visible;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                }
                if (selectedParameter2 == "Attendance")
                {
                    if (selectedGraphType == "Area")
                    {
                        attendanceAreaChart.Visibility = Visibility.Visible;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Bubble")
                    {
                        attendanceBubble.Visibility = Visibility.Visible;
                        attandanceHistogram.Visibility = Visibility.Collapsed;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                    else if (selectedGraphType == "Histogram")
                    {
                        attandanceHistogram.Visibility = Visibility.Visible;
                        attendanceAreaChart.Visibility = Visibility.Collapsed;
                        attendanceBubble.Visibility = Visibility.Collapsed;
                        languageRadarChart.Visibility = Visibility.Collapsed;
                        languageBubble.Visibility = Visibility.Collapsed;
                        languageAreaChart.Visibility = Visibility.Collapsed;
                        citizenshipRadarChart.Visibility = Visibility.Collapsed;
                        citizenshipBubble.Visibility = Visibility.Collapsed;
                        citizenshipAreaChart.Visibility = Visibility.Collapsed;
                        diversityRadarChart.Visibility = Visibility.Collapsed;
                        diversityAreaChart.Visibility = Visibility.Collapsed;
                        diversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityBubble.Visibility = Visibility.Collapsed;
                        DGDiversityRadar.Visibility = Visibility.Collapsed;
                        DGDiversityArea.Visibility = Visibility.Collapsed;
                        DGCitizenshipBubble.Visibility = Visibility.Collapsed;
                        DGCitizenshipRadar.Visibility = Visibility.Collapsed;
                        DGCitizenshipArea.Visibility = Visibility.Collapsed;
                        DGLanguageBubble.Visibility = Visibility.Collapsed;
                        DGLanguageRadar.Visibility = Visibility.Collapsed;
                        DGLanguageArea.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void DGEthnicity_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {

            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            EthnicityStackedList ethnicity = (MainDataGrid.CurrentItem as EthnicityStackedList);
            if (ethnicity == null)
            {
                return;
            }

            List<ExtraCoList> sportsList = kirinentities.GetSportsfromEthnicity(schoolId, subjectId, ethnicity.Ethnicity)
                .Select(x => new ExtraCoList
                {
                    ExtraCo = x.EXTRA_CO,
                    //Color = (Color)ColorConverter.ConvertFromString(x.Color),
                    SportsCount = x.SportsCount.ToString(),
                    Progress = (int)x.SportsCount + 3,
                    Ethnicity = x.ETHNICITY
                }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = sportsList;
        }

        private void DGLanguage_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {

            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            EthnicityLangList ethnicity = (MainDataGrid.CurrentItem as EthnicityLangList);
            if (ethnicity == null)
            {
                return;
            }

            List<LanguageList> LangList = kirinentities.GetLanguagefromEthnicity(schoolId, subjectId, ethnicity.Ethnicity)
                .Select(x => new LanguageList
                {
                    Language = x.LANGUAGE,
                    //Color = (Color)ColorConverter.ConvertFromString(x.Color),
                    LangCount = x.LangCount.ToString(),
                    Progress = (int)x.LangCount + 3,
                    Ethnicity = x.ETHNICITY
                }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = LangList;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = System.Windows.Visibility.Visible;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = System.Windows.Visibility.Collapsed;
        }

        private void LangExpander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = System.Windows.Visibility.Visible;
        }

        private void LangExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = System.Windows.Visibility.Collapsed;
        }

        public T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

        private void ParamenterOptionRow3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ParamenterOptionRow3.SelectedItem.ToString();
            ParameterOption2Row3.Items.Clear();
            switch (selectedValue)
            {
                case "Ethnicity":
                    {
                        ParameterOption2Row3.Items.Add("Sports/Cocurricular");
                        ParameterOption2Row3.Items.Add("Languages");

                        break;
                    }
                default:
                    {
                        ParameterOption2Row3.Items.Add("");
                        break;
                    }
            }

            ParameterOption2Row3.SelectedItem = ParameterOption2Row3.Items[0].ToString();
        }

        private void ParameterOption2Row3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ParameterOption2Row3.SelectedItem.ToString();
            GraphOption3.Items.Clear();

            switch (selectedValue)
            {
                case "Sports/Cocurricular":
                    {
                        //GraphOption3.Items.Add("Bubble");
                        GraphOption3.Items.Add("Stacked Bar");

                        break;
                    }
                case "Languages":
                    {
                        //GraphOption3.Items.Add("Bubble");
                        GraphOption3.Items.Add("Stacked Bar");

                        break;
                    }
                default:
                    {
                        GraphOption3.Items.Add("");
                        break;
                    }
            }
            GraphOption3.SelectedItem = GraphOption3.Items[0].ToString();
        }

        private void GraphOption3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GraphOption3.SelectedItem != null)
            {
                string selectedParameter2 = ParameterOption2Row3.SelectedItem.ToString();
                string selectedGraphType = GraphOption3.SelectedItem.ToString();

                if (selectedParameter2 == "Sports/Cocurricular")
                {
                    //if (selectedGraphType == "Bubble")
                    //{
                    //    sportsBubble.Visibility = Visibility.Visible;
                    //    StackedBarEthnicity.Visibility = Visibility.Collapsed;
                    //    ethnicityLangBubble.Visibility = Visibility.Collapsed;
                    //    StackedBarLanguages.Visibility = Visibility.Collapsed;
                    //}
                    if (selectedGraphType == "Stacked Bar")
                    {
                        StackedBarEthnicity.Visibility = Visibility.Visible;
                        sportsBubble.Visibility = Visibility.Collapsed;
                        ethnicityLangBubble.Visibility = Visibility.Collapsed;
                        StackedBarLanguages.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Visible;
                        DGLanguages.Visibility = Visibility.Collapsed;
                    }

                }
                else if (selectedParameter2 == "Languages")
                {
                    //if (selectedGraphType == "Bubble")
                    //{
                    //    ethnicityLangBubble.Visibility = Visibility.Visible;
                    //    StackedBarLanguages.Visibility = Visibility.Collapsed;
                    //    sportsBubble.Visibility = Visibility.Collapsed;
                    //    StackedBarEthnicity.Visibility = Visibility.Collapsed;
                    //}
                    if (selectedGraphType == "Stacked Bar")
                    {
                        StackedBarLanguages.Visibility = Visibility.Visible;
                        ethnicityLangBubble.Visibility = Visibility.Collapsed;
                        sportsBubble.Visibility = Visibility.Collapsed;
                        StackedBarEthnicity.Visibility = Visibility.Collapsed;
                        DGEthnicity.Visibility = Visibility.Collapsed;
                        DGLanguages.Visibility = Visibility.Visible;
                    }
                }

                //if (selectedValue == "Bar")
                //{
                //    BarchartSampleSandBoxRow2.Visibility = Visibility.Visible;
                //    LineChartSampleRow2.Visibility = Visibility.Collapsed;
                //    PieChartSampleSandBoxRow2.Visibility = Visibility.Collapsed;
                //}
                //if (selectedValue == "Line")
                //{
                //    BarchartSampleSandBoxRow2.Visibility = Visibility.Collapsed;
                //    LineChartSampleRow2.Visibility = Visibility.Visible;
                //    PieChartSampleSandBoxRow2.Visibility = Visibility.Collapsed;
                //}
                //if (selectedValue == "Pie")
                //{
                //    BarchartSampleSandBoxRow2.Visibility = Visibility.Collapsed;
                //    LineChartSampleRow2.Visibility = Visibility.Collapsed;
                //    PieChartSampleSandBoxRow2.Visibility = Visibility.Visible;
                //}
            }
        }


        //private void DoubleBackButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int totalCount = cmbStudent.Items.Count;
            int selectedIndex = cmbStudent.SelectedIndex;

            //-----------Logic for Student Combobox Selection------------//
            if (selectedIndex < totalCount && selectedIndex != 0)
            {
                cmbStudent.SelectedItem = cmbStudent.Items[selectedIndex - 1];
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            int totalCount = cmbStudent.Items.Count;
            int selectedIndex = cmbStudent.SelectedIndex;

            //-----------Logic for Student Combobox Selection------------//
            if (selectedIndex < totalCount - 1)
            {
                cmbStudent.SelectedItem = cmbStudent.Items[selectedIndex + 1];
            }
        }

        //private void DoubleForwardButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void btnDBackwardRow2_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption2.Items.Count;
            int selectedIndex1 = ParameterOption2.SelectedIndex;

            //-----------Logic for 2nd Combobox Selection------------//
            if (selectedIndex1 < totalCount1 && selectedIndex1 != 0)
            {
                ParameterOption2.SelectedItem = ParameterOption2.Items[selectedIndex1 - 1];
            }
        }

        private void BackButtonRow2_Click(object sender, RoutedEventArgs e)
        {
            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            // -----------Logic for 3nd Combobox Selection------------//
            if (selectedIndex2 < totalCount2 && selectedIndex2 != 0)
            {
                GraphOption.SelectedItem = GraphOption.Items[selectedIndex2 - 1].ToString();
            }
        }

        private void ForwardButtonRow2_Click(object sender, RoutedEventArgs e)
        {
            int totalCount2 = GraphOption.Items.Count;
            int selectedIndex2 = GraphOption.SelectedIndex;

            //-----------Logic for 3nd Combobox Selection------------//
            if (selectedIndex2 < totalCount2 - 1)
            {
                GraphOption.SelectedItem = GraphOption.Items[selectedIndex2 + 1].ToString();
            }
        }

        private void DoubleForwardButtonRow2_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption2.Items.Count;
            int selectedIndex1 = ParameterOption2.SelectedIndex;

            //-----------Logic for 2nd Combobox Selection------------//
            if (selectedIndex1 < totalCount1 - 1)
            {
                ParameterOption2.SelectedItem = ParameterOption2.Items[selectedIndex1 + 1];
            }
        }

        private void DoubleBackButtonRow3_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption2Row3.Items.Count;
            int selectedIndex1 = ParameterOption2Row3.SelectedIndex;

            //-----------Logic for 2nd Combobox Selection------------//
            if (selectedIndex1 < totalCount1 && selectedIndex1 != 0)
            {
                ParameterOption2Row3.SelectedItem = ParameterOption2Row3.Items[selectedIndex1 - 1];
            }
        }

        private void BackButtonRow3_Click(object sender, RoutedEventArgs e)
        {
            int totalCount2 = GraphOption3.Items.Count;
            int selectedIndex2 = GraphOption3.SelectedIndex;

            // -----------Logic for 3nd Combobox Selection------------//
            if (selectedIndex2 < totalCount2 && selectedIndex2 != 0)
            {
                GraphOption3.SelectedItem = GraphOption3.Items[selectedIndex2 - 1].ToString();
            }
        }

        private void ForwardButtonRow3_Click(object sender, RoutedEventArgs e)
        {
            int totalCount2 = GraphOption3.Items.Count;
            int selectedIndex2 = GraphOption3.SelectedIndex;

            //-----------Logic for 3nd Combobox Selection------------//
            if (selectedIndex2 < totalCount2 - 1)
            {
                GraphOption3.SelectedItem = GraphOption3.Items[selectedIndex2 + 1].ToString();
            }
        }

        private void DoubleForwardButtonRow3_Click(object sender, RoutedEventArgs e)
        {
            int totalCount1 = ParameterOption2Row3.Items.Count;
            int selectedIndex1 = ParameterOption2Row3.SelectedIndex;

            //-----------Logic for 2nd Combobox Selection------------//
            if (selectedIndex1 < totalCount1 - 1)
            {
                ParameterOption2Row3.SelectedItem = ParameterOption2Row3.Items[selectedIndex1 + 1];
            }
        }

        //private void RadarChartSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var chart = sender as SfChart;


        //}

        private void FillParameterCombo()
        {
            ParameterOption1.Items.Add("Grades");
            //ParameterOption1.Items.Add("Ethnicity");

            ParameterOption1.SelectedItem = ParameterOption1.Items[0].ToString();
        }

        private void FillParameterComboRow3()
        {
            ParamenterOptionRow3.Items.Add("Ethnicity");

            ParamenterOptionRow3.SelectedItem = ParamenterOptionRow3.Items[0].ToString();
        }


        private void FillStudentCombo(string subjectId)
        {
            kirinentities = new KIRINEntities1();
            List<GetStudentListbySubjectId_Result> lstStudent = kirinentities.GetStudentListbySubjectId(subjectId).ToList();

            cmbStudent.ItemsSource = lstStudent;
            cmbStudent.SelectedValue = "2";
        }

        private void StackedBarEthnicity_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var ethnicity = sbvm.LabelsStackedBarEthnicity[xPoint];
            var extraCo = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyEthnicityandSports(schoolId, subjectId, ethnicity, extraCo);

            if (svm.StudentList != null)
            {
                app.sportsList.DataContext = svm;
                app.sportsList.DGStudentData.ItemsSource = svm.StudentList;
                app.sportsList.Visibility = Visibility.Visible;
            }
        }

        private void DG_Sports_SelectionChanged(object sender, EventArgs e)
        {
            ExtraCoList data = ((DataGrid)sender).SelectedItem as ExtraCoList;

            if (data != null)
            {
                var ethnicity = data.Ethnicity;
                var extraCo = data.ExtraCo;

                StudentViewModel svm = new StudentViewModel();
                svm.GetStudentDatabyEthnicityandSports(schoolId, subjectId, ethnicity, extraCo);

                if (svm.StudentList != null)
                {
                    app.sportsList.DataContext = svm;
                    app.sportsList.DGStudentData.ItemsSource = svm.StudentList;
                    app.sportsList.Visibility = Visibility.Visible;
                }
            }
        }

        private void StackedBarLanguage_Click(object sender, LiveCharts.ChartPoint chartpoint)
        {
            int xPoint = Convert.ToInt32(chartpoint.X);
            var ethnicity = sbvm.LabelsStackedBarLanguage[xPoint];
            var language = chartpoint.SeriesView.Title;

            StudentViewModel svm = new StudentViewModel();
            svm.GetStudentDatabyEthnicityandLanguage(schoolId, subjectId, ethnicity, language);

            if (svm.StudentList != null)
            {
                app.languageList.DataContext = svm;
                app.languageList.DGStudentData.ItemsSource = svm.StudentList;
                app.languageList.Visibility = Visibility.Visible;
            }
        }

        private void DG_Lang_SelectionChanged(object sender, EventArgs e)
        {
            LanguageList data = ((DataGrid)sender).SelectedItem as LanguageList;

            if (data != null)
            {
                var ethnicity = data.Ethnicity;
                var language = data.Language;

                StudentViewModel svm = new StudentViewModel();
                svm.GetStudentDatabyEthnicityandLanguage(schoolId, subjectId, ethnicity, language);

                if (svm.StudentList != null)
                {
                    app.languageList.DataContext = svm;
                    app.languageList.DGStudentData.ItemsSource = svm.StudentList;
                    app.languageList.Visibility = Visibility.Visible;
                }
            }
        }

        private void DGSubjects_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            string studentId = cmbStudent.SelectedValue.ToString();

            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            SubjectView subjectView = (MainDataGrid.CurrentItem as SubjectView);
            if (subjectView == null)
            {
                return;
            }

            List<SubjectTeacherView> childView = kirinentities.GetCourseInstructorData(schoolId, studentId, subjectView.Id.ToString()).
                                                                        Select(c => new SubjectTeacherView
                                                                        {
                                                                            CourseId = c.CourseID.ToString(),
                                                                            CourseName = c.COURSE_NAME,
                                                                            Name = c.TeacherName,
                                                                            label = c.TeacherName + " (" + c.COURSE_NAME + ")",
                                                                            subChildView = kirinentities.GetAssignmentDatabyCourseID(schoolId, studentId, c.CourseID.ToString()).
                                                                            Select(a => new SubAssignmentView
                                                                            {
                                                                                AssignmentId = a.ASSIGNMENT_ID,
                                                                                Assignment = a.Assignment,
                                                                                AssignmentDate = a.AssignmentDate,
                                                                                PointsPossible = a.POINTS_POSSIBLE.ToString(),
                                                                                PointsEarned = a.POINTS,
                                                                                Percentage = a.Percentage + "%"
                                                                            }).ToList()
                                                                        }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = childView;
        }

        private void DGChildView_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            string studentId = cmbStudent.SelectedValue.ToString();
            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            SubjectTeacherView childView = (MainDataGrid.CurrentItem as SubjectTeacherView);
            if (childView == null)
            {
                return;
            }

            List<SubAssignmentView> subChildView = kirinentities.GetAssignmentDatabyCourseID(schoolId, studentId, childView.CourseId.ToString()).
                                                                            Select(a => new SubAssignmentView
                                                                            {
                                                                                AssignmentId = a.ASSIGNMENT_ID,
                                                                                Assignment = a.Assignment,
                                                                                AssignmentDate = a.AssignmentDate,
                                                                                PointsPossible = a.POINTS_POSSIBLE.ToString(),
                                                                                PointsEarned = a.POINTS,
                                                                                Percentage = a.Percentage + "%"
                                                                            }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = subChildView;
        }

        private void ChildExpander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Visible;
        }

        private void ChildExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Collapsed;
        }

        private void DGDiversityBubble_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            DiversityData diversityData = (MainDataGrid.CurrentItem as DiversityData);
            if (diversityData == null)
            {
                return;
            }

            List<GradeStudentData> subChildView = kirinentities.GetGradesbyDiversity(schoolId, subjectId, diversityData.Diversity).
                                                                            Select(s => new GradeStudentData
                                                                            {
                                                                                Name = s.NAME,
                                                                                Score = s.Score,
                                                                                StudentId = s.STUDENT_ID.ToString()
                                                                            }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = subChildView;
        }

        private void DivExpander_Expanded(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Visible;
        }

        private void DivExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            DataGridRow row = FindVisualParent<DataGridRow>(sender as Expander);
            row.DetailsVisibility = Visibility.Collapsed;
        }

        private void DGCitizenshipBubble_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            CitizenshipBubbleData citizenshipBubble = (MainDataGrid.CurrentItem as CitizenshipBubbleData);
            if (citizenshipBubble == null)
            {
                return;
            }

            List<GradeStudentData> subChildView = kirinentities.GetGradesbyDiversity(schoolId, subjectId, citizenshipBubble.Citizenship).
                                                                            Select(s => new GradeStudentData
                                                                            {
                                                                                Name = s.NAME,
                                                                                Score = s.Score,
                                                                                StudentId = s.STUDENT_ID.ToString()
                                                                            }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = subChildView;
        }

        private void DGLanguageBubble_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            DataGrid MainDataGrid = sender as DataGrid;
            var cell = MainDataGrid.CurrentCell;

            LanguageBubbleData langBubble = (MainDataGrid.CurrentItem as LanguageBubbleData);
            if (langBubble == null)
            {
                return;
            }

            List<GradeStudentData> subChildView = kirinentities.GetGradesbyLanguage(schoolId, subjectId, langBubble.LangId).
                                                                            Select(s => new GradeStudentData
                                                                            {
                                                                                Name = s.NAME,
                                                                                Score = s.Score,
                                                                                StudentId = s.STUDENT_ID.ToString()
                                                                            }).ToList();

            DataGrid DetailsDataGrid = e.DetailsElement as DataGrid;

            DetailsDataGrid.ItemsSource = subChildView;
        }
        
    }
}
