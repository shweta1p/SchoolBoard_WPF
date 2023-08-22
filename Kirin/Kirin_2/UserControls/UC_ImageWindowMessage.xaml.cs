using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_ImageWindowMessage.xaml
    /// </summary>
    public partial class UC_ImageWindowMessage : UserControl
    {
        public UC_ImageWindowMessage()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
