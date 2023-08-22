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
    public class SchoolViewModel : INotifyPropertyChanged
    {       
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SCHOOL _school;

        public SCHOOL School
        {
            get { return _school;  }
            set 
            {
                _school = value;
                OnPropertyChanged(nameof(School));
            }
        }

        private ObservableCollection<SCHOOL> _lstSchool;

        public ObservableCollection<SCHOOL> LstSchool
        {
            get { return _lstSchool; }
            set
            {
                _lstSchool = value;
                OnPropertyChanged(nameof(LstSchool));
            }
        }

        KIRINEntities1 schoolsEntities;
        public SchoolViewModel()
        {
            schoolsEntities = new KIRINEntities1();
            LoadSchool();
        }

        private void LoadSchool()
        {
            LstSchool = new ObservableCollection<SCHOOL>(schoolsEntities.SCHOOLs);
        }

    }
}
