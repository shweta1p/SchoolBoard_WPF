using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kirin_2.Models
{
    public class StaffData : INotifyPropertyChanged
    {
        private string name;
        private string imageurl;
        private string heatmap = "#FFC34444";
        private string reportingPerson;
        private int level;
        private int id;
        private string email;
        private string phoneNo;
        private string lblDesignation;
        private string homeRoom;
        private Visibility visibility;
        private Visibility roomvisibility;
        private string tbDesignation;
        private Visibility imgvisibility;
        private State isexpand;
        private bool isRoot;
        private Visibility desiVisibility;
        private string parentId;
        private HorizontalAlignment alignment;

        public string _Shape { get; set; }
        public double _Width { get; set; }
        public double _Height { get; set; }
        public double FontSize { get; set; }

        public StaffData()
        {
            Models = new ObservableCollection<StaffData>();
        }

        private string _mDesignation;
        public string Designation
        {
            get { return _mDesignation; }
            set
            {
                if (_mDesignation != value)
                {
                    _mDesignation = value;
                    OnPropertyChanged("Designation");
                }
            }
        }

        public HorizontalAlignment Alignment
        {
            get { return alignment; }
            set
            {
                if (alignment != value)
                {
                    alignment = value;
                    OnPropertyChanged("Alignment");
                }
            }
        }
        

        public string TbDesignation
        {
            get { return tbDesignation; }
            set
            {
                if (tbDesignation != value)
                {
                    tbDesignation = value;
                    OnPropertyChanged("TbDesignation");
                }
            }
        }

        private SolidColorBrush ColorConverter(string hexaColor)
        {
            if (hexaColor != null)
            {
                byte r = Convert.ToByte(hexaColor.Substring(1, 2), 16);
                byte g = Convert.ToByte(hexaColor.Substring(3, 2), 16);
                byte b = Convert.ToByte(hexaColor.Substring(5, 2), 16);
                byte a = Convert.ToByte(hexaColor.Substring(7, 2), 16);
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(r, g, b, a));
                return myBrush;
            }
            return null;
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

        public string ReportingPerson
        {
            get
            {
                return reportingPerson;
            }
            set
            {
                if (reportingPerson != value)
                {
                    reportingPerson = value;
                    OnPropertyChanged(("ReportingPerson"));
                }
            }
        }

        public string ImageUrl
        {
            get
            {
                return imageurl;
            }
            set
            {
                if (imageurl != value)
                {
                    if (value != null)
                    {
                        imageurl = value;
                        OnPropertyChanged(("ImageUrl"));
                    }
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
                    if (value != null)
                    {
                        level = value;
                        OnPropertyChanged(("Level"));
                    }
                }
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    if (value != null)
                    {
                        id = value;
                        OnPropertyChanged(("Id"));
                    }
                }
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(("Email"));
                }
            }
        }

        public string PhoneNo
        {
            get
            {
                return phoneNo;
            }
            set
            {
                if (phoneNo != value)
                {
                    phoneNo = value;
                    OnPropertyChanged(("PhoneNo"));
                }
            }
        }

        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                if (visibility != value)
                {
                    visibility = value;
                    OnPropertyChanged(("Visibility"));
                }
            }
        }

        public Visibility DesiVisibility
        {
            get
            {
                return desiVisibility;
            }
            set
            {
                if (desiVisibility != value)
                {
                    desiVisibility = value;
                    OnPropertyChanged(("DesiVisibility"));
                }
            }
        }

        public string LblDesignation
        {
            get
            {
                return lblDesignation;
            }
            set
            {
                if (lblDesignation != value)
                {
                    lblDesignation = value;
                    OnPropertyChanged(("LblDesignation"));
                }
            }
        }

        public string HomeRoom
        {
            get
            {
                return homeRoom;
            }
            set
            {
                if (homeRoom != value)
                {
                    homeRoom = value;
                    OnPropertyChanged(("HomeRoom"));
                }
            }
        }

        public Visibility Roomvisibility
        {
            get
            {
                return roomvisibility;
            }
            set
            {
                if (roomvisibility != value)
                {
                    roomvisibility = value;
                    OnPropertyChanged(("Roomvisibility"));
                }
            }
        }

        public Visibility Imgvisibility
        {
            get
            {
                return imgvisibility;
            }
            set
            {
                if (imgvisibility != value)
                {
                    imgvisibility = value;
                    OnPropertyChanged(("Imgvisibility"));
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
                    OnPropertyChanged(("IsRoot"));
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

        private ObservableCollection<StaffData> models;


        public ObservableCollection<StaffData> Models
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

    public class StaffDataList : ObservableCollection<StaffData>
    {

    }
}
