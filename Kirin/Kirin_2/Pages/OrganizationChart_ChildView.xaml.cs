using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Kirin_2.Models;
using Kirin_2.ViewModel;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System.Linq;

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for OrganizationChart_ChildView.xaml
    /// </summary>
    public partial class OrganizationChart_ChildView : Page
    {

        KIRINEntities1 kirinentities;
        public OrganizationChart_ChildView(string parentId)
        {
            App.Current.Properties["TeacherId"] = parentId;
            InitializeComponent();
        }

        private void btnEye_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Node_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Content.ToString();
            string fname = string.Empty, lname = string.Empty;

            kirinentities = new KIRINEntities1();

            if (!string.IsNullOrEmpty(name))
            {
                if (name.Contains(' '))
                {
                    fname = name.Split(' ')[0].ToString();
                    lname = name.Split(' ')[1].ToString();
                }
            }

            int id = (from std in kirinentities.STUDENTs
                      where std.FIRST_NAME == fname
                      && std.LAST_NAME == lname && std.IsDeleted == false
                      select std.ID).FirstOrDefault();

            if (!string.IsNullOrEmpty(id.ToString()) && id != 0)
            {
                try
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            this.NavigationService.Navigate(new SchoolDB_StudentView(id.ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (string.IsNullOrEmpty(id.ToString()) || id == 0)
            {
                string courseCode = name;
                try
                {
                    ClassInfo classInfoWin = new ClassInfo(name);
                    classInfoWin.Show();
                }
                catch (Exception ex)
                {

                }
            }
        }

    }

}
