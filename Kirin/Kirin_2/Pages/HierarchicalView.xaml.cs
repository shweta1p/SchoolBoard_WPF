﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Kirin_2.Models;
using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System.Linq;
using System.Windows.Forms;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for HierarchicalView.xaml
    /// </summary>
    public partial class HierarchicalView : Page
    {
        public HierarchicalView()
        {
            InitializeComponent();

            (sfdiagram.Info as IGraphInfo).GetLayoutInfo += MainWindow_GetLayoutInfo;
            Globals.reset = 0;
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
