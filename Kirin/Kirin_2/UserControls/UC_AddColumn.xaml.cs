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

namespace Kirin_2
{
    /// <summary>
    /// Interaction logic for UC_AddColumn.xaml
    /// </summary>
    public partial class UC_AddColumn : UserControl
    {
        public App app;
        public UC_AddColumn()
        {
            InitializeComponent();

            
               // DataContext = this.GradeBookViewModel;
            
            this.Loaded += SetCheckboxes;
         //   SetCheckboxes();
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;


        }
        //public List<string> chkBoxLst = new List<string>();
        

        public void SetCheckboxes(object sender, RoutedEventArgs e)
        {
            app = (App)Application.Current;
            // app = (App) Application.Current;
            if (app.chkBoxLst.Contains("Gender"))
            {
                GenderChkBox.IsChecked = true;
            }
            if (app.chkBoxLst.Contains("Language"))
            {
                LanguageChkBox.IsChecked = true;
            }
            if (app.chkBoxLst.Contains("Ethnicity"))
            {
                EthnicityChkBox.IsChecked = true;
            }
            if (app.chkBoxLst.Contains("Citizenship"))
            {
                CitizenshipChkBox.IsChecked = true;
            }
        }
        private void Update(object sender, RoutedEventArgs e)
        {


            var viewModel = (GradeBookViewModel)this.DataContext;
            if (viewModel.cDisplayColumns.CanExecute(app.chkBoxLst))
                viewModel.cDisplayColumns.Execute(app.chkBoxLst);

            this.Visibility = Visibility.Collapsed;
        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    this.VisualParent.
        //}

        private void CheckBox_Checked_Gender(object sender, RoutedEventArgs e)
        {
            if (GenderChkBox.IsChecked == true)
            {
                app.chkBoxLst.Add("Gender");
            }
            if (GenderChkBox.IsChecked == false)
            {
                app.chkBoxLst.Remove("Gender");

            }
        }
        private void CheckBox_Checked_Ethnicity(object sender, RoutedEventArgs e)
        {

            if (EthnicityChkBox.IsChecked == true)
            {
                app.chkBoxLst.Add("Ethnicity");
            }
            if (EthnicityChkBox.IsChecked == false)
            {
                app.chkBoxLst.Remove("Ethnicity");

            }
        }
        private void CheckBox_Checked_Language(object sender, RoutedEventArgs e)
        {
            if (LanguageChkBox.IsChecked == true)
            {
                app.chkBoxLst.Add("Language");
            }
            if (LanguageChkBox.IsChecked == false)
            {
                app.chkBoxLst.Remove("Language");

            }
        }
        private void CheckBox_Checked_Citizenship(object sender, RoutedEventArgs e)
        {

            if (CitizenshipChkBox.IsChecked == true)
            {
                app.chkBoxLst.Add("Citizenship");
            }
            if (CitizenshipChkBox.IsChecked == false)
            {
                app.chkBoxLst.Remove("Citizenship");

            }
        }




    }
}
