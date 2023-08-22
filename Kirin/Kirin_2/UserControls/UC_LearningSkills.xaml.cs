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

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_LearningSkills.xaml
    /// </summary>
    public partial class UC_LearningSkills : UserControl
    {
        public UC_LearningSkills()
        {
            InitializeComponent();
            DataContext = new GradeBookViewModel();
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            this.Visibility = Visibility.Collapsed;
        }
    }
}
