using Kirin_2.Pages;
using Kirin_2.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_TransportAbvr.xaml
    /// </summary>
    public partial class UC_TransportAbvr : UserControl
    {
        public UC_TransportAbvr()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void StudentView_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            myAnimation(MenuClass);          
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
            sb.Begin();
        }

    }
}
