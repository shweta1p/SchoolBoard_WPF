using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kirin_2.Models
{
    public class BudgetData : INotifyPropertyChanged
    {
        private string id;
        private string heatmap = "#FFC34444";
        private string parentId;
        private bool isRoot;
        private State isexpand;
        private string name;
        private double height;
        private double width;
        private string shape;
        private int level;
        private Visibility lblVisibility;
        private Visibility rlblVisibility;

        public BudgetData()
        {
            Models = new ObservableCollection<BudgetData>();
        }

        private string _mLabel;
        public string Label
        {
            get { return _mLabel; }
            set
            {
                if (_mLabel != value)
                {
                    _mLabel = value;
                    OnPropertyChanged("Label");
                }
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(("Id"));
                }
            }
        }

        public string ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                if (parentId != value)
                {
                    parentId = value;
                    OnPropertyChanged(("ParentId"));
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(("Name"));
                }
            }
        }
        public double _Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnPropertyChanged(("_Height"));
                }
            }
        }

        public double _Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    OnPropertyChanged(("_Width"));
                }
            }
        }

        public string _Shape
        {
            get
            {
                return shape;
            }
            set
            {
                if (shape != value)
                {
                    shape = value;
                    OnPropertyChanged(("_Shape"));
                }
            }
        }

        public string RatingColor
        {
            get
            {
                return heatmap;
            }
            set
            {
                if (heatmap != value)
                {
                    if (value != null)
                    {
                        heatmap = value;
                        OnPropertyChanged(("RatingColor"));
                    }
                }
            }
        }
        public bool IsRoot
        {
            get
            {
                return isRoot;
            }
            set
            {
                if (isRoot != value)
                {
                    isRoot = value;
                    OnPropertyChanged(("IsChild"));
                }
            }
        }
        public State IsExpand
        {
            get
            {
                return isexpand;
            }
            set
            {
                if (isexpand != value)
                {
                    isexpand = value;
                    OnPropertyChanged(("IsExpand"));
                }
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (level != value)
                {
                    level = value;
                    OnPropertyChanged(("Level"));
                }
            }
        }

        public Visibility LblVisibility
        {
            get
            {
                return lblVisibility;
            }
            set
            {
                if (lblVisibility != value)
                {
                    lblVisibility = value;
                    OnPropertyChanged(("LblVisibility"));
                }
            }
        }

        public Visibility RlblVisibility
        {
            get
            {
                return rlblVisibility;
            }
            set
            {
                if (rlblVisibility != value)
                {
                    rlblVisibility = value;
                    OnPropertyChanged(("RlblVisibility"));
                }
            }
        }
        private ObservableCollection<BudgetData> models;


        public ObservableCollection<BudgetData> Models
        {
            get { return models; }
            set
            {
                models = value;
                OnPropertyChanged(("Models"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public enum State
    {
        Expand,
        Collapse,
        None
    };

    public class BudgetDataList : ObservableCollection<BudgetData>
    {

    }

}
