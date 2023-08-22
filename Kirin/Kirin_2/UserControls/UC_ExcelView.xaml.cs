using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.UI.Xaml.Spreadsheet.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Kirin_2.UserControls
{
    /// <summary>
    /// Interaction logic for UC_ExcelView.xaml
    /// </summary>
    public partial class UC_ExcelView : UserControl
    {
        public UC_ExcelView(string filePath)
        {
            InitializeComponent();

            spSheet.FormulaBarVisibility = Visibility.Visible;
            spSheet.Open(filePath);

            spSheet.WorkbookLoaded += spSheet_WorkbookLoaded;
            spSheet.WorkbookUnloaded += spSheet_WorkbookUnloaded;
        }

        void spSheet_WorkbookLoaded(object sender, WorkbookLoadedEventArgs args)
        {
            spSheet.ActiveGrid.CurrentCellBeginEdit += ActiveGrid_CurrentCellBeginEdit;
            //spSheet.ActiveGrid.AllowEditing = false;
        }

        void spSheet_WorkbookUnloaded(object sender, WorkbookUnloadedEventArgs args)
        {
            spSheet.ActiveGrid.CurrentCellBeginEdit -= ActiveGrid_CurrentCellBeginEdit;
        }

        void ActiveGrid_CurrentCellBeginEdit(object sender, CurrentCellBeginEditEventArgs args)
        {
            args.Cancel = true;
        }
    }
}

