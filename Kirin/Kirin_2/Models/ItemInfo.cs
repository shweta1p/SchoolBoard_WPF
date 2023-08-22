using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirin_2.Models
{
    public class ItemInfo
    {
        public ItemInfo()
        {

        }
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Label { get; set; }
        public List<string> ParentId { get; set; }
        public string _Shape { get; set; }
        public double _Width { get; set; }
        public double _Height { get; set; }
        public string _Color { get; set; }
        public bool IsRoot { get; set; }
        public int Level { get; set; }
          
    }

    public class DataItems : ObservableCollection<ItemInfo>
    {

    }
}
