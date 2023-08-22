﻿using Kirin_2.ViewModel;
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
    /// Interaction logic for SchoolDB_StudentView.xaml
    /// </summary>
    public partial class SchoolDB_StudentView : Page
    {
        public SchoolDB_StudentView(string ID)
        {
            UserViewModel uvm = new UserViewModel();
            uvm.ViewStudentProfile(Int32.Parse(ID));
            DataContext = uvm;
            InitializeComponent();            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ah = ActualHeight;
            var aw = ActualWidth;
            var h = Height;
            var w = Width;

           // InfoGrid.Height = ActualHeight - 250;

        }

    }
}
