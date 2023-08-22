using Kirin_2.Model;
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
    /// Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindowVM _context;
      
        MainWindow _mainWindow;
        public UserControlMenuItem(ItemMenu itemMenu, MainWindowVM context)
        {
            _context = context;
            _mainWindow = (MainWindow)Application.Current.MainWindow;

            InitializeComponent();

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext =  itemMenu;

        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mainWindow.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Page);

        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mainWindow.SwitchScreen(((MaterialDesignThemes.Wpf.PackIcon)sender).Tag.ToString());
        }

        private void HandleExpanderExpanded(object sender, RoutedEventArgs e)
        {
            
            //((Expander)sender).IsExpanded = false;
          //  ExpandExculsively(sender as Expander);
        }

        private void expandexculsively(Expander expander)
        {
            //foreach (var child in ListViewItemMenu.Children)
            //{
            //    if (child is expander && child != expander)
            //        ((expander)child).isexpanded = false;
            //}
        }
    }
}
