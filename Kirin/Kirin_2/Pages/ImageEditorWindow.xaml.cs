using Kirin_2.UserControls;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for ImageEditorWindow.xaml
    /// </summary>
    public partial class ImageEditorWindow : Window
    {
        bool isDown, isDragging, isSelected;
        UIElement selectedElement = null;
        double originalLeft, originalTop;
        Point startPoint;
        AdornerLayer adornerLayer;
        public App app;

        public ImageEditorWindow(BitmapImage bitmapImage)
        {
            app = (App)Application.Current;
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            imageSource.Source = bitmapImage;
            ImageWin.Children.Add(app.imageMessage);

            app.imageMessage.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            app.imageMessage.Visibility = Visibility.Visible;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //registering mouse events
            this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
            this.MouseLeftButtonUp += MainWindow_MouseLeftButtonUp;
            this.MouseMove += MainWindow_MouseMove;
            this.MouseLeave += MainWindow_MouseLeave;

            myCanvas.PreviewMouseLeftButtonDown += MyCanvas_PreviewMouseLeftButtonDown;
            myCanvas.PreviewMouseLeftButtonUp += MyCanvas_PreviewMouseLeftButtonUp;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Height = myCanvas.RenderSize.Height;
            Width = myCanvas.RenderSize.Width;

            //Specification for target bitmap like width/height pixel etc.
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)Width, (int)Height, 96, 96, PixelFormats.Pbgra32);

            //creates Visual Brush of UIElement
            VisualBrush visualBrush = new VisualBrush(myCanvas);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                //draws image of element
                drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Point(Width, Height)));
            }

            //renders image
            renderTarget.Render(drawingVisual);

            //PNG encoder for creating PNG file
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));


            var bitmapImage = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                memoryStream.Position = 0;

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            var feedback = Window.GetWindow(new FeedbackWindow(bitmapImage));
            feedback.Show();

            this.Visibility = Visibility.Collapsed;

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mw.WindowState = WindowState.Normal;

            ImageWin.Children.Remove(app.imageMessage);
            ImageWin.Visibility = Visibility.Collapsed;
        }

        private void StopDragging()
        {
            if (isDown)
            {
                isDown = isDragging = false;
            }
        }

        private void MyCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopDragging();
            e.Handled = true;
        }

        private void MyCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //removing selected element
            if (isSelected)
            {
                isSelected = false;
                if (selectedElement != null)
                {
                    adornerLayer.Remove(adornerLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }

            // select element if any element is clicked other then canvas
            if (e.Source != myCanvas)
            {
                isDown = true;
                startPoint = e.GetPosition(myCanvas);

                selectedElement = e.Source as UIElement;

                originalLeft = Canvas.GetLeft(selectedElement);
                originalTop = Canvas.GetTop(selectedElement);

                //adding adorner on selected element
                adornerLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                adornerLayer.Add(new BorderAdorner(selectedElement));
                isSelected = true;
                e.Handled = true;
            }
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            //handling mouse move event and setting canvas top and left value based on mouse movement
            if (isDown)
            {
                if ((!isDragging) &&
                    ((Math.Abs(e.GetPosition(myCanvas).X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(myCanvas).Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                    isDragging = true;

                if (isDragging)
                {
                    Point position = Mouse.GetPosition(myCanvas);
                    Canvas.SetTop(selectedElement, position.Y - (startPoint.Y - originalTop));
                    Canvas.SetLeft(selectedElement, position.X - (startPoint.X - originalLeft));
                }
            }
        }

        private void MainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            //stop dragging on mouse leave
            StopDragging();
            e.Handled = true;
        }

        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //stop dragging on mouse left button up
            StopDragging();
            e.Handled = true;
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //remove selected element on mouse down
            if (isSelected)
            {
                isSelected = false;
                if (selectedElement != null)
                {
                    adornerLayer.Remove(adornerLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }
        }

        private void MainWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(myCanvas);
            HitTestResult result = VisualTreeHelper.HitTest(myCanvas, pt);

            if (result != null)
            {
                myCanvas.Children.Remove(result.VisualHit as UIElement);
            }
        }

        private void addBlackRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            // create new Rectangle
            System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();

            // assign properties
            rectangle.Width = 200;
            rectangle.Height = 200;
            rectangle.Fill = new SolidColorBrush(Colors.Black);

            // assign handlers
            rectangle.MouseDown += MainWindow_MouseLeftButtonDown;
            rectangle.MouseMove += MainWindow_MouseMove;
            rectangle.MouseUp += MainWindow_MouseLeftButtonUp;
            rectangle.MouseLeave += MainWindow_MouseLeave;
            rectangle.MouseRightButtonDown += MainWindow_MouseRightButtonDown;

            // set default position of rectangle to the canvas at (20,20)
            Canvas.SetLeft(rectangle, 100);
            Canvas.SetTop(rectangle, 100);

            // add it to canvas
            myCanvas.Children.Add(rectangle);
        }

        private void addYellowRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            // create new Rectangle
            System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();

            // assign properties
            rectangle.Width = 200;
            rectangle.Height = 200;
            rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            rectangle.Stroke = new SolidColorBrush(Colors.Yellow);
            rectangle.StrokeThickness = 5;

            // assign handlers
            rectangle.MouseDown += MainWindow_MouseLeftButtonDown;
            rectangle.MouseMove += MainWindow_MouseMove;
            rectangle.MouseUp += MainWindow_MouseLeftButtonUp;
            rectangle.MouseLeave += MainWindow_MouseLeave;
            rectangle.MouseRightButtonDown += MainWindow_MouseRightButtonDown;

            // set default position
            Canvas.SetLeft(rectangle, 100);
            Canvas.SetTop(rectangle, 100);

            // add it to canvas
            myCanvas.Children.Add(rectangle);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var bitmapImage = new BitmapImage();
            var feedback = Window.GetWindow(new FeedbackWindow(bitmapImage));
            feedback.Show();

            this.Visibility = Visibility.Collapsed;

            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mw.WindowState = WindowState.Normal;

            ImageWin.Children.Remove(app.imageMessage);
            ImageWin.Visibility = Visibility.Collapsed;
        }

        private void DefaultPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            //DefaultPopup.IsOpen = false;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }

    /*
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
    */
}
