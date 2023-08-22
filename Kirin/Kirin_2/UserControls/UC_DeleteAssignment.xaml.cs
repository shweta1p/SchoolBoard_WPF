using Kirin_2.Models;
using Kirin_2.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_DeleteAssignment.xaml
    /// </summary>
    public partial class UC_DeleteAssignment : UserControl
    {
        public UC_DeleteAssignment()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void DeleteAssignmentModal_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            GradeBookViewModel gv = new GradeBookViewModel();
            MessageBoxResult result = MessageBox.Show("Proceed?",
                                          "Delete Assignment",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ObservableCollection<AssignmentList> assignmentList = (ObservableCollection<AssignmentList>)DGAssignment.ItemsSource;
                gv.DeleteAssignment(assignmentList);
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
