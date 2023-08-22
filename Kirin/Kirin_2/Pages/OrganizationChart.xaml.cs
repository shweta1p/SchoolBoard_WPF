﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Kirin_2.Models;
using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System.Linq;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for OrganizationChart.xaml
    /// </summary>
    public partial class OrganizationChart : Page
    {

        KIRINEntities1 kirinentities;
        public OrganizationChart()
        {
            InitializeComponent();

            //(sfdiagram.Info as IGraphInfo).ItemTappedEvent += DiagramPage_ItemTappedEvent;
            (sfdiagram.Info as IGraphInfo).GetLayoutInfo += MainWindow_GetLayoutInfo;
            Globals.reset = 0;
        }


        private void Sfdiagram_Loaded(object sender, RoutedEventArgs e)
        {
            //(sfdiagram.DataContext as OrganizationVM).prevbutton = orgCompactAlternate;
        }

        private void Node_Click(object sender, RoutedEventArgs e)
        {
            //int row, col;
            Button btn = sender as Button;
            string reportingPerson = btn.Content.ToString();
            string fname = string.Empty, lname = string.Empty;

            kirinentities = new KIRINEntities1();

            if (!string.IsNullOrEmpty(reportingPerson))
            {
                fname = reportingPerson.Split(' ')[0].ToString();
                lname = reportingPerson.Split(' ')[1].ToString();
            }

            int id = (from staff in kirinentities.STAFF_DIRECTORY
                      where staff.FIRST_NAME == fname
                      && staff.LAST_NAME == lname && (staff.ROLEID == 1 || staff.ROLEID == 13)
                      select staff.ID).FirstOrDefault();

            if (!string.IsNullOrEmpty(id.ToString()) && id != 0)
            {
                kirinentities = new KIRINEntities1();
                var studentData = kirinentities.GetStudentDatafromTeacherID(Convert.ToInt32(id)).ToList();

                if (studentData.Count() > 1)
                {
                    try
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                this.NavigationService.Navigate(new OrganizationChart_ChildView(id.ToString()));
                            }
                        }
                    }
                    catch (Exception ee)
                    {

                    }
                }
            }
        }

        private void MainWindow_GetLayoutInfo(object sender, LayoutInfoArgs args)
        {
            if (sfdiagram.LayoutManager.Layout is DirectedTreeLayout)
            {
                if ((sfdiagram.LayoutManager.Layout as DirectedTreeLayout).Type == LayoutType.Organization)
                {
                    if (args.Item is INode)
                    {
                        if (((args.Item as INode).Content as StaffData).Designation.ToString() == "DIRECTOR")
                        {
                            args.Assistants.Add(args.Children[0]);
                            args.Children.Remove(args.Children[0]);
                        }

                        if (((args.Item as INode).Content as StaffData).Designation.ToString() == "PRINCIPAL")
                        {
                            args.Assistants.Add(args.Children[0]);
                            args.Children.Remove(args.Children[0]);
                        }
                    }
                }
            }
        }
    }

}
