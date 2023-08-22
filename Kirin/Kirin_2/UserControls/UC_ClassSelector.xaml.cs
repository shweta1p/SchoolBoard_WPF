using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for UC_ClassSelector.xaml
    /// </summary>
    public partial class UC_ClassSelector : UserControl
    {
        SubjectViewModel svm;
        public UC_ClassSelector()
        {
            //string userName = "dduke@sabc.on.ca";

            InitializeComponent();
            svm = new SubjectViewModel();
            // svm.GetClassListing("1");
            DataContext = svm;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            myAnimation(MenuClass);
            /*
            MenuClass.RenderTransform = new TranslateTransform();


            // Root is your root container. In this case - Grid
            // and you have to add it only in the case if it does not exists

            Storyboard sb = new Storyboard();

            DoubleAnimation slidey = new DoubleAnimation();
            slidey.To = 10;
            slidey.From = 0;
            slidey.Duration = new Duration(TimeSpan.FromMilliseconds(40));

            ScaleTransform scaleTransform = new ScaleTransform()
            {
                CenterX = 0,
                CenterY = 0,
                ScaleX = 400,
                ScaleY = 300
            };

            // Set the target of the animation
           Storyboard.SetTarget(slidey, MenuClass);
           Storyboard.SetTargetProperty(slidey, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
          // Storyboard.SetTarget(slidey, MenuClass);
            //Storyboard.SetTargetProperty(slidey, new PropertyPath("RenderTransform.(scaleTransform.ScaleY)"));

            // Kick the animation off
            sb.Children.Add(slidey);

            sb.Begin();
            */
        }

        public void myAnimation(Border myBorder)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation daScaleX = new DoubleAnimation();
            daScaleX.From = 0;
            daScaleX.To = 1;
            daScaleX.Duration = TimeSpan.FromMilliseconds(300);

            DoubleAnimation daScaleY = new DoubleAnimation();
            daScaleY.From = 0;
            daScaleY.To = 1;
            daScaleY.Duration = TimeSpan.FromMilliseconds(500);


            DoubleAnimation slidey = new DoubleAnimation();
            slidey.To = 10;
            slidey.From = 0;
            slidey.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            QuinticEase easing = new QuinticEase()
            {
                EasingMode = EasingMode.EaseOut
            };

            daScaleX.EasingFunction = easing;
            daScaleY.EasingFunction = easing;

            Storyboard.SetTargetProperty(daScaleX, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(daScaleX, MenuClass);
            Storyboard.SetTargetProperty(daScaleY, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(daScaleY, MenuClass);


            ColorAnimation coloranimation = new ColorAnimation();
            coloranimation.To = Colors.White;
            coloranimation.From = Colors.Transparent;
            Storyboard.SetTarget(slidey, MenuClass);
            Storyboard.SetTargetProperty(coloranimation, new PropertyPath(SolidColorBrush.ColorProperty));

            sb.Children.Add(daScaleX);
            sb.Children.Add(daScaleY);
            //  sb.Children.Add()
            sb.Begin();
        }

        private void School_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selectedSchool = (ListBox)sender;
            SchoolData selecteditemfromBox = (SchoolData)(SchoolListbox.SelectedItem);
            string teacherId = selecteditemfromBox.TeacherId; //((ListBoxItem)selecteditemfromBox).Tag.ToString();
            string schoolId = selecteditemfromBox.SchoolId;

            if (!string.IsNullOrWhiteSpace(teacherId) || !string.IsNullOrWhiteSpace(schoolId))
            {
                svm.FillSelectedStaffData(teacherId, schoolId);
            }
        }

        private void StarButtonSelected_Click(object sender, RoutedEventArgs e)
        {
            Button btnstar = sender as Button;
            btnstar.Foreground = Brushes.Goldenrod;
            //StarBtn.Foreground = Brushes.Goldenrod;
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }
    }

}
