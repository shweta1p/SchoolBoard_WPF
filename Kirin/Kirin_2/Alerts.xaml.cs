using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for Alerts.xaml
    /// </summary>
    public partial class Alerts : Window
    {
        public Dictionary<string, string> alertIcons = new Dictionary<string, string>();


        public Alerts(string typeOfAlert, string TitleBar , string Label1, string Label2, string Label3)
        {
            /*
            alertIcons.Add("Birthday", "M12.5,2C10.84,2 9.5,5.34 9.5,7A3,3 0 0,0 12.5,10A3,3 0 0,0 15.5,7C15.5,5.34 14.16,2 12.5,2M12.5,6.5A1,1 0 0,1 13.5,7.5A1,1 0 0,1 12.5,8.5A1,1 0 0,1 11.5,7.5A1,1 0 0,1 12.5,6.5M10,11A1,1 0 0,0 9,12V20H7A1,1 0 0,1 6,19V18A1,1 0 0,0 5,17A1,1 0 0,0 4,18V19A3,3 0 0,0 7,22H19A1,1 0 0,0 20,21A1,1 0 0,0 19,20H16V12A1,1 0 0,0 15,11H10Z");
            alertIcons.Add("Medical", "M18 14H14V18H10V14H6V10H10V6H14V10H18");
            alertIcons.Add("SPED", "M12,17.27L18.18,21L16.54,13.97L22,9.24L14.81,8.62L12,2L9.19,8.62L2,9.24L7.45,13.97L5.82,21L12,17.27Z");
            alertIcons.Add("Other", "M13 14H11V9H13M13 18H11V16H13M1 21H23L12 2L1 21Z"); 
            */
            alertIcons.Add("Birthday", " ");
            alertIcons.Add("Medical", " ");
            alertIcons.Add("SPED", " ");
            alertIcons.Add("Other", " ");

            InitializeComponent();
            Lbl1.Content = Label1;
            Lbl2.Content = Label2;
            Lbl3.Content = Label3;

            Path path = new Path();

            if (typeOfAlert == "Birthday")
            {
                string sData = alertIcons.FirstOrDefault(x => x.Key == "Birthday").Value;
                var converter = TypeDescriptor.GetConverter(typeof(Geometry));
                path.Data = (Geometry)converter.ConvertFrom(sData);


                icon.Data = (Geometry)converter.ConvertFrom(sData);
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = (Color)ColorConverter.ConvertFromString("Blue");
                icon.Fill = mySolidColorBrush;
            }
            else if (typeOfAlert == "Medical")
            {
                string sData = alertIcons.FirstOrDefault(x => x.Key == "Medical").Value;
                var converter = TypeDescriptor.GetConverter(typeof(Geometry));
                path.Data = (Geometry)converter.ConvertFrom(sData);


                icon.Data = (Geometry)converter.ConvertFrom(sData);
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = (Color)ColorConverter.ConvertFromString("Red");
                icon.Fill = mySolidColorBrush;
            }
            else if (typeOfAlert == "SPED")
            {
                string sData = alertIcons.FirstOrDefault(x => x.Key == "SPED").Value;
                var converter = TypeDescriptor.GetConverter(typeof(Geometry));
                path.Data = (Geometry)converter.ConvertFrom(sData);


                icon.Data = (Geometry)converter.ConvertFrom(sData);
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = (Color)ColorConverter.ConvertFromString("Gold");
                icon.Fill = mySolidColorBrush;
            }
            else if (typeOfAlert == "Other")
            {
                string sData = alertIcons.FirstOrDefault(x => x.Key == "Other").Value;
                var converter = TypeDescriptor.GetConverter(typeof(Geometry));
                path.Data = (Geometry)converter.ConvertFrom(sData);

             
                icon.Data = (Geometry)converter.ConvertFrom(sData);
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = (Color)ColorConverter.ConvertFromString("Gold");
                icon.Fill = mySolidColorBrush;
            }

            this.Title = TitleBar;
        }
    }
}
