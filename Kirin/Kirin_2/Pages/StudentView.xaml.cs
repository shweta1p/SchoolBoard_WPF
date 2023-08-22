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
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Page
    {
        StudentViewModel svm;
        public StudentView()
        {
            svm = new StudentViewModel();
            svm.GetStudentCourseData("2", "2022");
            svm.GetStudentAssessmentData("2", "2022");

            this.DataContext = svm;
            InitializeComponent();

            ////-------Update Row background--------------//
            //foreach (StudentAssessmentData item in DGStudentScore.ItemsSource)
            //{
            //    if (item.ID == 1 || item.ID == 11)
            //    {
            //        var color = (Color)ColorConverter.ConvertFromString("#3d78a6");
            //        DGStudentScore.RowBackground = new SolidColorBrush() { Opacity = 1, Color = color };
            //    }
            //}
        }
    }

    public class SliderButton : System.Windows.Controls.Primitives.ToggleButton
    {
        public double ButtonWidth
        {
            get
            {
                return (double)GetValue(ButtonWidthProperty);
            }
            set
            {
                SetValue(ButtonWidthProperty, value);
            }
        }

        public static readonly System.Windows.DependencyProperty ButtonWidthProperty =
               System.Windows.DependencyProperty.Register("ButtonWidth", typeof(double),
               typeof(SliderButton), new System.Windows.PropertyMetadata(0.0));

        public string OnLabel
        {
            get
            {
                return (string)GetValue(OnLabelProperty);
            }
            set
            {
                SetValue(OnLabelProperty, value);
            }
        }

        public static readonly System.Windows.DependencyProperty
               OnLabelProperty = System.Windows.DependencyProperty.Register
               ("OnLabel", typeof(string), typeof(SliderButton),
               new System.Windows.PropertyMetadata(""));

        public string OffLabel
        {
            get
            {
                return (string)GetValue(OffLabelProperty);
            }
            set
            {
                SetValue(OffLabelProperty, value);
            }
        }

        public static readonly System.Windows.DependencyProperty OffLabelProperty =
               System.Windows.DependencyProperty.Register("OffLabel", typeof(string),
               typeof(SliderButton), new System.Windows.PropertyMetadata(""));
    }

}
