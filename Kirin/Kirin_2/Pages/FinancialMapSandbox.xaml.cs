using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Theming;
using Syncfusion.UI.Xaml.DiagramRibbon;
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
    /// Interaction logic for FinancialMapSandbox.xaml
    /// </summary>
    public partial class FinancialMapSandbox : Page
    {
        public FinancialMapSandbox()
        {
            InitializeComponent();
            SfSkinManager.SetTheme(this, new Syncfusion.SfSkinManager.Theme() { ThemeName = "Office2019Colorful" });
            SfDiagramRibbon sfDiagramRibbon = new SfDiagramRibbon() { DataContext = Sfdiagram };

            Sfdiagram.SetValue(Grid.RowProperty, 1);
            sfDiagramRibbon.SetValue(Grid.RowProperty, 0);

            Root_Grid.Children.Add(Sfdiagram);
            Root_Grid.Children.Add(sfDiagramRibbon);
            Globals.reset = 0;
        }

        SfDiagram Sfdiagram = new SfDiagram()
        {
            Theme = new OfficeTheme(),
            Nodes = new NodeCollection(),
            Connectors = new ConnectorCollection(),
            Groups = new GroupCollection(),
            Constraints = GraphConstraints.Default | GraphConstraints.Undoable,
            SnapSettings = new SnapSettings() { SnapConstraints = SnapConstraints.All },
            HorizontalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Horizontal },
            VerticalRuler = new Syncfusion.UI.Xaml.Diagram.Controls.Ruler() { Orientation = Orientation.Vertical },
        };

        
    }
}
