﻿using Kirin_2.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for ScoreSheet_StudentView.xaml
    /// </summary>
    public partial class ScoreSheet_StudentView : Page
    {
        GradeBookViewModel gbvm = new GradeBookViewModel();
        string subjectId = App.Current.Properties["SubjectId"].ToString();
        string schoolId = App.Current.Properties["SchoolId"].ToString();
        string userName = App.Current.Properties["USERNAME"].ToString();
        string semId = string.Empty;
        public ScoreSheet_StudentView(string ID, string semesterId)
        {
            InitializeComponent();
            //FillSemesterCombo();
            int semester = Convert.ToInt32(semesterId);
            semId = semesterId;

            gbvm.getSemesterList();
            string sem = gbvm.Semester_List.Where(x => x.ID == semester).Select(x => x.SEMESTER).FirstOrDefault();

            lblSem.Content = sem;
            gbvm.ViewStudentScoreSheet(Int32.Parse(ID), semesterId);
            gbvm.getStudentList(subjectId, schoolId, userName); //semesterId

            DataContext = gbvm;
        }

        //private void Footer_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (Footer != null)
        //    {
        //        IEnumerable list = Footer.ItemsSource as IEnumerable;
        //        List<string> lstFile = new List<string>();

        //        foreach (DataGridItem DemoGridItem in Footer.Items)
        //        {
        //            TextBlock myCheckbox = (TextBlock)DemoGridItem.Cells[0].Controls[1];
                   
        //        }
        //}


        //private void cmbSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var semester = cmbSemesterList.SelectedValue;

        //    if (semester != null)
        //    {
        //        //gbvm.ViewStudentScoreSheet(Int32.Parse(ID), semester);
        //    }
        //    else
        //    {
        //        FillSemesterCombo();
        //    }
        //}

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (lblID.Content != null) {
                int currentStdID = Convert.ToInt32(lblID.Content.ToString());
                int stdId = gbvm.STUDENTLIST.Where(s => s.ID < currentStdID).Select(s => s.ID).FirstOrDefault();

                gbvm.ViewStudentScoreSheet(stdId, semId);
            }            
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (lblID.Content != null)
            {
                int currentStdID = Convert.ToInt32(lblID.Content.ToString());
                int stdId = gbvm.STUDENTLIST.Where(s => s.ID > currentStdID).OrderBy(s => s.ID).Select(s => s.ID).FirstOrDefault();
                 
                gbvm.ViewStudentScoreSheet(stdId, semId);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

            MainGrid.Height = ((ah /4) *3);
        }

        //private void FillSemesterCombo()
        //{
        //    gbvm.getSemesterList();
        //    List<GetSemesterList_Result> lstSemesterList = gbvm.Semester_List.ToList();

        //    cmbSemesterList.ItemsSource = lstSemesterList;

        //    cmbSemesterList.SelectedValue = lstSemesterList[lstSemesterList.Count() - 1].ID;
        //}
    }
}
