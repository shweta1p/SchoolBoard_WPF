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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for UC_ContextMenu.xaml
    /// </summary>
    public partial class UC_ContextMenu : UserControl
    {
        public UC_ContextMenu()
        {
            InitializeComponent();
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
        //private void Print_Click(object sender, RoutedEventArgs e)
        //{

        //    ViewModel.PrintPreview.Print_WPF_Preview(MainWindowFrame);

        //}
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
    }
}
