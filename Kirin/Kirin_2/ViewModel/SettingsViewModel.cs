using Kirin_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Kirin_2.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SettingsViewModel()
        {
            GetAdminLeftMenuBarData();

            GetFiltesData();

            GetAdminRightMenuBarData();

            GetDataStreamData("all");
        }

        private ObservableCollection<LeftMenuAdmin> _LeftMenuAdminList;
        public ObservableCollection<LeftMenuAdmin> LeftMenuAdminList
        {
            get { return _LeftMenuAdminList; }
            set
            {
                _LeftMenuAdminList = value;
                OnPropertyChanged(nameof(LeftMenuAdminList));
            }
        }

        public void GetAdminLeftMenuBarData()
        {
            LeftMenuAdminList = new ObservableCollection<LeftMenuAdmin>();

            LeftMenuAdminList.Add(new LeftMenuAdmin { ID = "1", Icon = "Building", Label = "Account Settings" });
            LeftMenuAdminList.Add(new LeftMenuAdmin { ID = "2", Icon = "PeopleGroup", Label = "Account Access Management" });
            LeftMenuAdminList.Add(new LeftMenuAdmin { ID = "3", Icon = "Filter", Label = "All Filters" });
            LeftMenuAdminList.Add(new LeftMenuAdmin { ID = "4", Icon = "Trash", Label = "Trash Can" });
        }

        private ObservableCollection<Filters> _FilterList;
        public ObservableCollection<Filters> FilterList
        {
            get { return _FilterList; }
            set
            {
                _FilterList = value;
                OnPropertyChanged(nameof(FilterList));
            }
        }

        public void GetFiltesData()
        {
            FilterList = new ObservableCollection<Filters>();

            FilterList.Add(new Filters { ID = "1", Name = "Exclude Product", Type = "Exclude", Views = "0" });
            FilterList.Add(new Filters { ID = "2", Name = "Include Hostname", Type = "Include", Views = "3" });
            FilterList.Add(new Filters { ID = "3", Name = "Renaame AdWords Campaigns", Type = "Search and Replace", Views = "3" });
        }

        private ObservableCollection<RightMenuAdmin> _RightMenuAdminList;
        public ObservableCollection<RightMenuAdmin> RightMenuAdminList
        {
            get { return _RightMenuAdminList; }
            set
            {
                _RightMenuAdminList = value;
                OnPropertyChanged(nameof(RightMenuAdminList));
            }
        }

        public void GetAdminRightMenuBarData()
        {
            RightMenuAdminList = new ObservableCollection<RightMenuAdmin>();

            List<RightSubMenuAdmin> subList = new List<RightSubMenuAdmin>();
            subList.Add(new RightSubMenuAdmin { ID = "1", Label = "Data Collection" });
            subList.Add(new RightSubMenuAdmin { ID = "2", Label = "Data Retention" });
            subList.Add(new RightSubMenuAdmin { ID = "3", Label = "Data Filters" });

            RightMenuAdminList.Add(new RightMenuAdmin { ID = "1", Icon = "ClipboardCheckOutline", Label = "Setup Assistant" });
            RightMenuAdminList.Add(new RightMenuAdmin { ID = "2", Icon = "Settings", Label = "Property Settings" });
            RightMenuAdminList.Add(new RightMenuAdmin { ID = "3", Icon = "Gamepad", Label = "Data Streams" });
            RightMenuAdminList.Add(new RightMenuAdmin { ID = "4", Icon = "Database", Label = "Data Settings", subMenuList= subList});
            RightMenuAdminList.Add(new RightMenuAdmin { ID = "5", Icon = "ArrowExpandUp", Label = "Data Import" });
            RightMenuAdminList.Add(new RightMenuAdmin { ID = "6", Icon = "AccountCash", Label = "Reporting Identity" });
        }

        private ObservableCollection<DataStream> _DataStreamList;
        public ObservableCollection<DataStream> DataStreamList
        {
            get { return _DataStreamList; }
            set
            {
                _DataStreamList = value;
                OnPropertyChanged(nameof(DataStreamList));
            }
        }

        public void GetDataStreamData(string Type)
        {
            DataStreamList = new ObservableCollection<DataStream>();

            if (Type.ToLower() == "all")
            {
                DataStreamList.Add(new DataStream { ID = "1", Icon = "Android", Name = "Flood-it! Android", Reference = "com.labpixis.flood", Label = "1051193346", Notes = "Receiving traffic in past 48 hours." });
                DataStreamList.Add(new DataStream { ID = "2", Icon = "AppleIos", Name = "Flood-it! iOS", Reference = "com.google.flood2", Label = "1051193347", Notes = "Receiving traffic in past 48 hours." });
                DataStreamList.Add(new DataStream { ID = "3", Icon = "Web", Name = "Flood-it! Web", Reference = "http://flood-it.app", Label = "2198273707", Notes = "Receiving traffic in past 48 hours." });
            }
            else if (Type.ToLower() == "ios")
            {
                DataStreamList.Add(new DataStream { ID = "2", Icon = "AppleIos", Name = "Flood-it! iOS", Reference = "com.google.flood2", Label = "1051193347", Notes = "Receiving traffic in past 48 hours." });
            }
            else if (Type.ToLower() == "android")
            {
                DataStreamList.Add(new DataStream { ID = "1", Icon = "Android", Name = "Flood-it! Android", Reference = "com.labpixis.flood", Label = "1051193346", Notes = "Receiving traffic in past 48 hours." });
            }
            else if (Type.ToLower() == "web")
            {
                DataStreamList.Add(new DataStream { ID = "3", Icon = "Web", Name = "Flood-it! Web", Reference = "http://flood-it.app", Label = "2198273707", Notes = "Receiving traffic in past 48 hours." });
            }
        }

    }

    public class LeftMenuAdmin
    {
        public string ID { get; set; }
        public string Icon { get; set; }
        public string Label { get; set; }
    }

    public class RightMenuAdmin
    {
        public string ID { get; set; }
        public string Icon { get; set; }
        public string Label { get; set; }
        public List<RightSubMenuAdmin> subMenuList { get; set; }
    }

    public class RightSubMenuAdmin
    {
        public string ID { get; set; }
        public string Icon { get; set; }
        public string Label { get; set; }
    }

    public class Filters
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Views { get; set; }
    }

    public class DataStream
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Icon { get; set; }
        public string Label { get; set; }
        public string Notes { get; set; }
    }
}
