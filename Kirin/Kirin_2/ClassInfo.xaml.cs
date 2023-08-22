﻿using Kirin_2.Models;
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
using System.Windows.Shapes;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for ClassInfo.xaml
    /// </summary>
    public partial class ClassInfo : Window
    {
        KIRINEntities1 kirinentities;
        public ClassInfo(string code)
        {
            InitializeComponent();
            kirinentities = new KIRINEntities1();

            var classInfo = kirinentities.GetClassInfo(code).ToList();

            //    List<classDetails> classInfo = new List<classDetails>()
            //    {
            //        new classDetails(){ code = "BDV4C", description = "BDDV4C - Entrepreneurship", code_detail = "BDV4C.1",  capacity="24/7", classroom = "Room 206" , schedule ="P1-P2(A) Q1" },

            //        new classDetails(){ code = "BDI3C", description = "BDI3C - Entrepreneurial Studies", code_detail = "BDI3C 2",  capacity="14/27", classroom = "Room 119" , schedule ="P1-P2(A) Q3" },
            //        new classDetails(){ code = "BBI1O", description = "BBI10 - Intro to Business", code_detail = "BDI3C 2",  capacity="14/27", classroom = "Room 119" , schedule ="P1-P2(A) Q3" },
            //        new classDetails(){ code = "BBI20", description = "BBI20 - Intro to Business", code_detail = "BDI20 4",  capacity="14/15", classroom = "Room 226" , schedule ="P1-P2(A) Q4" },
            //        new classDetails(){ code = "BDI3C", description = "BDI3C - Entrepreneurial Studies", code_detail = "BDV4C.1",  capacity="24/27", classroom = "Room 206" , schedule ="P1-P2(A) Q1" },
            //        new classDetails(){ code = "BDI1O", description = "BDI1O - Introduction to Business", code_detail = "BBI1O.2",  capacity="13/13", classroom = "Room 209" , schedule ="P3(A) P5-P6(A) Q2" },
            //                     new classDetails(){ code = "BDI2O", description = "BDI2O - Introduction to Business", code_detail = "BBI2O.2",  capacity="15/15", classroom = "Room 209" , schedule ="P3(A) P5-P6(A) Q2" },
            //                     new classDetails(){ code = "BDI1O", description = "BDI1O - Introduction to Business", code_detail = "BBI1O.3",  capacity="13/13", classroom = "Room 119" , schedule ="P3-P4(A) P6(A) Q3" },
            //                     new classDetails(){ code = "BDI2O", description = "BDI2O - Introduction to Business", code_detail = "BBI2O.3",  capacity="15/15", classroom = "Room 119" , schedule ="P3-P4(A) P6(A) Q3" },
            //                     new classDetails(){ code = "Prep", description = "PREP.15", code_detail = "PREP.15",  capacity="0/1", classroom = "Room P/L" , schedule ="P3-P4(A) P6(A) Q4" },
            //                     new classDetails(){ code = "Lunch", description = "Lunch", code_detail = "Lunch 5",  capacity="0/1", classroom = "Room P/L" , schedule ="P3-P5(A) Q1" },
            //                     new classDetails(){ code = "BDI3C.1", description = "Entreepreneurial Studies", code_detail = "BDI3C.1",  capacity="20/27", classroom = "Room 207" , schedule ="P3-P4(A) P6(A) Q1" },
            //                     new classDetails(){ code = "BDI1O.2", description = "BBI1O - Introduction to Business", code_detail = "BBI1O.2",  capacity="13/13", classroom = "Room 209" , schedule ="P3(A) P5-P6(A) Q2" },
            //                     new classDetails(){ code = "BDI2O.2", description = "BBI2O - Introduction to Business", code_detail = "BBI2O.2",  capacity="15/15", classroom = "Room 209" , schedule ="P3(A) P5-P6(A) Q2" },
            //                         new classDetails(){ code = "BDI3O", description = "BBI1O - Introduction to Business", code_detail = "BBI1O.3",  capacity="13/13", classroom = "Room 119" , schedule ="P3-P4(A) P6(A) Q3" },



            //};


            var uriSource = new Uri("/Images/classInfoLogo.jpg", UriKind.Relative);
            ClassInfoLogo.Source = new BitmapImage(uriSource);

            if (classInfo.Count() > 0) {
                classDetails selectedClass = new classDetails();

                selectedClass = (from cl in classInfo
                                select new classDetails
                                {
                                    code = cl.COURSE_CODE,
                                    capacity = "24/27",
                                    classroom = "Room 119",
                                    code_detail = cl.COURSE_NAME,
                                    description = cl.DESCRIPTION,
                                    schedule = "P3-P4(A) P6(A) Q1"
                                }).First();

                coursename.Text = selectedClass.code_detail;
                term.Text = selectedClass.schedule;
                coursenumber.Text = selectedClass.code;
                periodstart.Text = "11/11/2021 Mon";
                periodEnd.Text = "10/12/2021 Fri";
                periodDays.Text = selectedClass.schedule;
                gradescale.Text = "Numeric";
                room.Text = selectedClass.classroom;
                customDisplayName.Text = selectedClass.code;
                sectionnumber.Text = "1";
            }

            //selectedClass = (from cl in classInfo
            //                 where cl.code == tag
            //                 select new classDetails
            //                 { 
            //                     code = cl.code,
            //                     capacity = cl.capacity,
            //                     classroom = cl.classroom,
            //                     code_detail = cl.code_detail,
            //                      description = cl.description,
            //                       schedule = cl.schedule
            //                 }).First();
                       
        }
    }

    public class classDetails
    {
        public string code;
        public string description;
        public string code_detail;
        public string classroom;
        public string schedule;
        public string capacity;
    }
}
