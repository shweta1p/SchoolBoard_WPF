using Kirin_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for InsightFeedback.xaml
    /// </summary>
    public partial class InsightFeedback : UserControl
    {
        public InsightFeedback()
        {
            InitializeComponent();
            FillList();
        }

        private void FillList()
        {
            List<RadioButtonList> aList = new List<RadioButtonList>();
            aList.Add(new RadioButtonList() { Value = "5 minutes or less", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "5-30 minutes", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "30-60 minutes", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "1-3 hours", IsSelected = false });
            aList.Add(new RadioButtonList() { Value = "3 hourse or more", IsSelected = false });

            this.listBoxZone.ItemsSource = aList;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            myAnimation();
        }

        public void myAnimation()
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
            sb.Begin();
        }

    }

    public class RadioButtonList
    {
        public string Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
