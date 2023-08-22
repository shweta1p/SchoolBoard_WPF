using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirin_2.ViewModel
{
    public class MindmapDataItem
    {
        public string Label { get; set; }
        public string ParentId { get; set; }
    }

    public class MindmapDataItems : ObservableCollection<MindmapDataItem>
    {
    }
}
