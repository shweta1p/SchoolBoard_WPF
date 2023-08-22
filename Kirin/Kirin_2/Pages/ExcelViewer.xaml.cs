using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.UI.Xaml.Spreadsheet;
using Syncfusion.UI.Xaml.Spreadsheet.Helpers;
using Syncfusion.Windows.Tools.Controls;
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

namespace Kirin_2.Pages
{
    /// <summary>
    /// Interaction logic for ExcelViewer.xaml
    /// </summary>
    public partial class ExcelViewer : RibbonWindow
    {
        public ExcelViewer(string filePath)
        {
            InitializeComponent();

            spSheet.FormulaBarVisibility = Visibility.Visible;
            spSheet.Open(filePath);
            
            //spSheet.WorkbookLoaded += Spreadsheet_WorkbookLoaded;
            //spSheet.WorkbookUnloaded += Spreadsheet_WorkbookUnloaded;
        }

        void Spreadsheet_WorkbookUnloaded(object sender, WorkbookUnloadedEventArgs args)
        {
            spSheet.ActiveGrid.CurrentCellBeginEdit -= ActiveGrid_CurrentCellBeginEdit;
        }
        void ActiveGrid_CurrentCellBeginEdit(object sender, CurrentCellBeginEditEventArgs args)
        {
            if (args.RowcolumnIndex.RowIndex == 5 && args.RowcolumnIndex.ColumnIndex == 5)
                args.Cancel = false;
        }
        void Spreadsheet_WorkbookLoaded(object sender, WorkbookLoadedEventArgs args)
        {
            spSheet.ActiveGrid.CurrentCellBeginEdit += ActiveGrid_CurrentCellBeginEdit;           
        }
    }
}
