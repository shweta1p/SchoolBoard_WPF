using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kirin_2.Models;
using Syncfusion.UI.Xaml.ImageEditor;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for FeedbackWindow.xaml
    /// </summary>
    public partial class FeedbackWindow : Window
    {

        public static BitmapSource bmapSource;
        KIRINEntities1 KirinEntities;
        public App app;
        public FeedbackWindow(BitmapImage bitmap)
        {
            app = (App)Application.Current;
            InitializeComponent();

            KirinEntities = new KIRINEntities1();

            //Set focus on feedback text box on loading
            feedbackTextBox.Focus();

            //// Send button desabled if no data entered
            //if (!string.IsNullOrEmpty(feedbackTextBox.Text) || McCheckBox.IsChecked == true)
            //{
            //    Color color = (Color)ColorConverter.ConvertFromString("#247BC0");
            //    btnSend.IsEnabled = true;
            //    btnSend.Foreground = new SolidColorBrush(color);
            //}
            //else
            //{
            //    Color color = (Color)ColorConverter.ConvertFromString("#000000");
            //    btnSend.IsEnabled = false;
            //    btnSend.Foreground = new SolidColorBrush(color);
            //}

            //Take screenshot of the current active mainwindow
            Window win = App.Current.MainWindow;
            UIElement element = win as UIElement;

            double Height, renderHeight, Width, renderWidth;

            Height = renderHeight = element.RenderSize.Height;
            Width = renderWidth = element.RenderSize.Width;

            //Specification for target bitmap like width/height pixel etc.
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            
            //creates Visual Brush of UIElement
            VisualBrush visualBrush = new VisualBrush(element);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                //draws image of element
                drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Point(Width, Height-54)));
            }

            //renders image
            renderTarget.Render(drawingVisual);

            //PNG encoder for creating PNG file
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));

            if (bitmap.StreamSource != null)
            {
                McCheckBox.IsChecked = true;
                editor.ImageSource = bitmap;
            }
            else
            {
                // Convert Bitmapsource screenshot to BitmapImage
                if (!(bmapSource is BitmapImage bitmapImage))
                {
                    bitmapImage = new BitmapImage();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        encoder.Save(memoryStream);
                        memoryStream.Position = 0;

                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                    }
                }

                //Set image source of canvas to the screenshot bitmapimage
                editor.ImageSource = bitmapImage;
            }
        }

        /*

        private void addBlackRectangleButton_Click(object sender, RoutedEventArgs e)
        {
             
                // create new Rectangle
                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
                // assign properties
                rectangle.Width = 200;
                rectangle.Height = 200;
                rectangle.Fill = new SolidColorBrush(Colors.Black);
                // assign handlers
                rectangle.MouseDown += rectangle_MouseDown;
                rectangle.MouseMove += rectangle_MouseMove;
                rectangle.MouseUp += rectangle_MouseUp;
                // set default position of rectangle to the canvas at (20,20)
                Canvas.SetLeft(rectangle, 20);
                Canvas.SetTop(rectangle, 20);
                // add it to canvas
                canvas.Children.Add(rectangle);
                
         

             
        }

        
        private void addYellowRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            
                // create new Rectangle
                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
                // assign properties
                rectangle.Width = 200;
                rectangle.Height = 200;
                rectangle.Stroke = new SolidColorBrush(Colors.Yellow);
                rectangle.StrokeThickness = 3;
                // assign handlers
                rectangle.MouseDown += rectangle_MouseDown;
                rectangle.MouseMove += rectangle_MouseMove;
                rectangle.MouseUp += rectangle_MouseUp;
                // set default position
                Canvas.SetLeft(rectangle, 0);
                Canvas.SetTop(rectangle, 0);
                // add it to canvas
                canvas.Children.Add(rectangle);
                
         

        }

        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // start dragging
            drag = true;
            // save start point of dragging
            startPoint = Mouse.GetPosition(canvas);
        }

        private void rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            // if dragging, then adjust rectangle position based on mouse movement
            if (drag)
            {
                System.Windows.Shapes.Rectangle draggedRectangle = sender as System.Windows.Shapes.Rectangle;
                System.Windows.Point newPoint = Mouse.GetPosition(canvas);
                double left = Canvas.GetLeft(draggedRectangle);
                double top = Canvas.GetTop(draggedRectangle);
                Canvas.SetLeft(draggedRectangle, left + (newPoint.X - startPoint.X));
                Canvas.SetTop(draggedRectangle, top + (newPoint.Y - startPoint.Y));

                startPoint = newPoint;
            }
        }

        private void rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // stop dragging
            drag = false;
        }

        */


        /*
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canvas);

            rect = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.Yellow,
                StrokeThickness = 4
            };
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.Y);
            canvas.Children.Add(rect);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || rect == null)
                return;

            var pos = e.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            rect.Width = w;
            rect.Height = h;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }
        */

        //   public static BitmapSource bmapSource = CopyScreen();
        // BitmapImage bImg = ConvertBitmapSourceToBitmapImage(bmapSource);
        // editor.ImageSource = bImg;

        private void McCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            editor.Visibility = Visibility.Visible;
            //rectView.Visibility = Visibility.Collapsed;
            //btnEditor.Visibility = Visibility.Visible;
            Height = 740;
            Top = 100;
        }

        private void McCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            editor.Visibility = Visibility.Collapsed;
            //rectView.Visibility = Visibility.Visible;
            //btnEditor.Visibility = Visibility.Collapsed;
        }

        private void Window_Close(object sender, RoutedEventArgs e)
        {
            this.Close();

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mw.feedbackBtn.IsEnabled = true;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(feedbackTextBox.Text) || McCheckBox.IsChecked == true)
            {
                string feedback = feedbackTextBox.Text.Trim().ToString();

                //-----Convert Image source to byte array-----------//
                BitmapImage bitmap = editor.ImageSource as BitmapImage;
                byte[] image = ImageToBytes(bitmap);

                string usename = App.Current.Properties["USERNAME"].ToString();

                var saveFeedback = KirinEntities.SaveFeedback(feedback, image, usename);

                if (saveFeedback == 1)
                {
                    MessageBox.Show("Your feedback has been successfully sent.");
                    this.Close();
                    MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    mw.feedbackBtn.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please enter feedback to send.");
            }
        }

        public static byte[] ImageToBytes(BitmapImage img)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void editor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mw.WindowState = WindowState.Minimized;

            BitmapImage bitmap = editor.ImageSource as BitmapImage;
            
            ImageEditorWindow imgWin = new ImageEditorWindow(bitmap);
            imgWin.WindowState = WindowState.Normal;
            imgWin.ShowDialog();
            
            //app.defaultPopUp.Visibility = Visibility.Visible;
        }

        //private void DefaultPopup_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    DefaultPopup.IsOpen = false;
        //}

        //private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    this.Visibility = Visibility.Collapsed;
        //}

    }

    //Adorner class that enables drag,resize capability for all the elements on the canvas.
    public class BorderAdorner : Adorner
    {
        //use thumb for resizing elements
        Thumb topLeft, topRight, bottomLeft, bottomRight;
        //visual child collection for adorner
        VisualCollection visualChilderns;

        public BorderAdorner(UIElement element) : base(element)
        {
            visualChilderns = new VisualCollection(this);

            //adding thumbs for drawing adorner rectangle and setting cursor
            BuildAdornerCorners(ref topLeft, Cursors.SizeNWSE);
            BuildAdornerCorners(ref topRight, Cursors.SizeNESW);
            BuildAdornerCorners(ref bottomLeft, Cursors.SizeNESW);
            BuildAdornerCorners(ref bottomRight, Cursors.SizeNWSE);

            //registering drag delta events for thumb drag movement
            topLeft.DragDelta += TopLeft_DragDelta;
            topRight.DragDelta += TopRight_DragDelta;
            bottomLeft.DragDelta += BottomLeft_DragDelta;
            bottomRight.DragDelta += BottomRight_DragDelta;
        }

        private void BottomRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb bottomRightCorner = sender as Thumb;
            //setting new height and width after drag
            if (adornedElement != null && bottomRightCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width + e.HorizontalChange, bottomRightCorner.DesiredSize.Width);
                double newHeight = Math.Max(e.VerticalChange + adornedElement.Height, bottomRightCorner.DesiredSize.Height);

                adornedElement.Width = newWidth;
                adornedElement.Height = newHeight;
            }
        }

        private void TopRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb topRightCorner = sender as Thumb;
            //setting new height, width and canvas top after drag
            if (adornedElement != null && topRightCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width + e.HorizontalChange, topRightCorner.DesiredSize.Width);
                double newHeight = Math.Max(adornedElement.Height - e.VerticalChange, topRightCorner.DesiredSize.Height);
                adornedElement.Width = newWidth;

                double oldTop = Canvas.GetTop(adornedElement);
                double newTop = oldTop - (newHeight - oldHeight);
                adornedElement.Height = newHeight;
                Canvas.SetTop(adornedElement, newTop);
            }
        }

        private void TopLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb topLeftCorner = sender as Thumb;
            //setting new height, width and canvas top, left after drag
            if (adornedElement != null && topLeftCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width - e.HorizontalChange, topLeftCorner.DesiredSize.Width);
                double newHeight = Math.Max(adornedElement.Height - e.VerticalChange, topLeftCorner.DesiredSize.Height);

                double oldLeft = Canvas.GetLeft(adornedElement);
                double newLeft = oldLeft - (newWidth - oldWidth);
                adornedElement.Width = newWidth;
                Canvas.SetLeft(adornedElement, newLeft);

                double oldTop = Canvas.GetTop(adornedElement);
                double newTop = oldTop - (newHeight - oldHeight);
                adornedElement.Height = newHeight;
                Canvas.SetTop(adornedElement, newTop);
            }
        }

        private void BottomLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb topRightCorner = sender as Thumb;
            //setting new height, width and canvas left after drag
            if (adornedElement != null && topRightCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width - e.HorizontalChange, topRightCorner.DesiredSize.Width);
                double newHeight = Math.Max(adornedElement.Height + e.VerticalChange, topRightCorner.DesiredSize.Height);

                double oldLeft = Canvas.GetLeft(adornedElement);
                double newLeft = oldLeft - (newWidth - oldWidth);
                adornedElement.Width = newWidth;
                Canvas.SetLeft(adornedElement, newLeft);

                adornedElement.Height = newHeight;
            }
        }

        public void BuildAdornerCorners(ref Thumb cornerThumb, Cursor customizedCursors)
        {
            //adding new thumbs for adorner to visual childern collection
            if (cornerThumb != null) return;
            cornerThumb = new Thumb() { Cursor = customizedCursors, Height = 10, Width = 10, Opacity = 0.5, Background = new SolidColorBrush(Colors.Red) };
            visualChilderns.Add(cornerThumb);
        }

        public void EnforceSize(FrameworkElement element)
        {
            if (element.Width.Equals(Double.NaN))
                element.Width = element.DesiredSize.Width;
            if (element.Height.Equals(Double.NaN))
                element.Height = element.DesiredSize.Height;

            //enforce size of element not exceeding to it's parent element size
            FrameworkElement parent = element.Parent as FrameworkElement;

            if (parent != null)
            {
                element.MaxHeight = parent.ActualHeight;
                element.MaxWidth = parent.ActualWidth;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            base.ArrangeOverride(finalSize);

            double desireWidth = AdornedElement.DesiredSize.Width;
            double desireHeight = AdornedElement.DesiredSize.Height;

            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;

            //arranging thumbs
            topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            topRight.Arrange(new Rect(desireWidth - adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            bottomLeft.Arrange(new Rect(-adornerWidth / 2, desireHeight - adornerHeight / 2, adornerWidth, adornerHeight));
            bottomRight.Arrange(new Rect(desireWidth - adornerWidth / 2, desireHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            return finalSize;
        }
        protected override int VisualChildrenCount { get { return visualChilderns.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChilderns[index]; }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }


    // Syncfusion image editor class where the header and footer elements have been removed.
    public class CustomEditor : SfImageEditor
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ToolbarSettings.IsToolbarVisiblity = false;
            var items = this.ToolbarSettings.ToolbarItems.Count;
            var border = this.Template.FindName("PART_HeaderToolbarPanel", this) as Border;
            var panel = (border.Child as Grid).Children[0] as StackPanel;
            var browseButton = panel.Children[0] as Button;
            var saveButton = panel.Children[1] as Button;
            var line = panel.Children[2] as System.Windows.Shapes.Rectangle;
            var undoButton = panel.Children[3] as Button;
            var redoButton = panel.Children[4] as Button;

            var grid = (border.Child as Grid).Children[1] as Grid;
            var stackPanel = grid.Children[0] as StackPanel;
            var SelectButton = stackPanel.Children[0] as Button;
            var panButton = stackPanel.Children[1] as Button;
            var line1 = stackPanel.Children[2] as System.Windows.Shapes.Rectangle;

            //var textButton = stackPanel.Children[3] as Button;
            //var shapeButton = stackPanel.Children[4] as Button;

            //var cropButton = stackPanel.Children[4] as Button;
            //var flipButton = stackPanel.Children[6] as Button;
            //var rotateButton = stackPanel.Children[7] as Button;
            //var penButton = stackPanel.Children[8] as Button;

            var resetButton = this.Template.FindName("PART_ResetIcon", this) as Button;

            var bottomToolbar = this.Template.FindName("PART_FooterToolbarPanel", this) as Border;
            var bottomPanel = bottomToolbar.Child as Grid;
            var resetZoomButton = bottomPanel.Children[0] as Button;
            var line2 = bottomPanel.Children[1] as System.Windows.Shapes.Rectangle;
            var decreaseZoomRect = bottomPanel.Children[1] as System.Windows.Shapes.Rectangle;
            var decreaseZoomButton = bottomPanel.Children[2] as Button;
            var slider = bottomPanel.Children[3] as Slider;
            var increaseZoomButton = bottomPanel.Children[4] as Button;
            var text = bottomPanel.Children[5] as TextBlock;

            bottomToolbar.Visibility = Visibility.Collapsed;
            bottomPanel.Visibility = Visibility.Collapsed;
            browseButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
            line.Visibility = Visibility.Collapsed;
            undoButton.Visibility = Visibility.Collapsed;
            redoButton.Visibility = Visibility.Collapsed;
            SelectButton.Visibility = Visibility.Collapsed;
            panButton.Visibility = Visibility.Collapsed;
            line1.Visibility = Visibility.Collapsed;
            browseButton.Visibility = Visibility.Collapsed;

            //textButton.Visibility = Visibility.Collapsed;
            //shapeButton.Visibility = Visibility.Collapsed;
            //cropButton.Visibility = Visibility.Collapsed;
            //flipButton.Visibility = Visibility.Collapsed;
            //rotateButton.Visibility = Visibility.Collapsed;
            //penButton.Visibility = Visibility.Collapsed;
            resetButton.Visibility = Visibility.Collapsed;

            resetZoomButton.Visibility = Visibility.Collapsed;
            line2.Visibility = Visibility.Collapsed;
            decreaseZoomButton.Visibility = Visibility.Collapsed;
            decreaseZoomButton.Visibility = Visibility.Collapsed;
            increaseZoomButton.Visibility = Visibility.Collapsed;
            slider.Visibility = Visibility.Collapsed;
            text.Visibility = Visibility.Collapsed;
        }

    }
}
