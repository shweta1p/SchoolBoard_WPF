using Kirin_2.Models;
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

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for UC_ViewBulletin.xaml
    /// </summary>
    public partial class UC_ViewBulletin : UserControl
    {
        public KIRINEntities1 kirinEntities;
        string schoolId = "1";
        public UC_ViewBulletin()
        {
            InitializeComponent();
            
            LoadDailyBulletin(schoolId);
        }

      
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public void LoadDailyBulletin(string schoolId)
        {
            kirinEntities = new KIRINEntities1();
            var dailybulletin = kirinEntities.GetDailyBulletinData(Convert.ToInt32(schoolId)).ToList();

            if (dailybulletin != null)
            {
                AnnouncementHeader.Text = dailybulletin[0].SCHOOL_NAME + " Bulletin for " + dailybulletin[0].UPLOADED_DATE;
                AnnouncementContent.Text = dailybulletin[0].MESSAGE;
            }

            //AnnouncementHeader.Text = "Saint Andre Bessette Catholic Secondary School Bulletin for " + DateTime.UtcNow;
            //AnnouncementContent.Text = "  Schools are closed. Buses are cancelled.";
        }

        private void dpicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime datepicker = (DateTime) dpicker.SelectedDate;

            kirinEntities = new KIRINEntities1();
            var dailybulletin = kirinEntities.GetDailyBulletinDatabyDate(Convert.ToInt32(schoolId),datepicker).ToList();

            if (dailybulletin.Count != 0)
            {
                AnnouncementHeader.Text = dailybulletin[0].SCHOOL_NAME + " Bulletin for " + dailybulletin[0].UPLOADED_DATE;
                AnnouncementContent.Text = dailybulletin[0].MESSAGE;
            }
            else
            {
                AnnouncementHeader.Text =  "Saint Andre Bessette Catholic Secondary School Bulletin for " + datepicker;
                AnnouncementContent.Text = "No Details available";
            }

        }

    }
}
