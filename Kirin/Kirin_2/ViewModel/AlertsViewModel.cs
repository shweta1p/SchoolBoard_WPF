using Kirin_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kirin_2.ViewModel
{
    public class AlertsViewModel : INotifyPropertyChanged
    {
        public App app;
        string subjectId = App.Current.Properties["SubjectId"].ToString();
        string schoolId = App.Current.Properties["SchoolId"].ToString();
        KIRINEntities1 KirinEntities;
        public AlertsViewModel()
        {
            app = (App)Application.Current;
            KirinEntities = new KIRINEntities1();

            //GetStudentAlertsData();
        }

        private ObservableCollection<StudentAlertData> _StudentAlertList;
        public ObservableCollection<StudentAlertData> StudentAlertList
        {
            get { return _StudentAlertList; }
            set
            {
                _StudentAlertList = value;
                OnPropertyChanged("StudentAlertList");
            }
        }

        public void GetStudentAlertsData()
        {

            var data = KirinEntities.GetStudentAlertsData(subjectId, schoolId).ToList();

            StudentAlertList = new ObservableCollection<StudentAlertData>(from sa in data
                                                                          select new StudentAlertData
                                                                          {
                                                                              RNo = Convert.ToInt32(sa.RNo),
                                                                              Name = sa.LAST_NAME + ", " + sa.FIRST_NAME,
                                                                              Grade = sa.Grade,
                                                                              MedicalVisibility = !string.IsNullOrEmpty(sa.HEALTH_ALERT) ? Visibility.Visible : Visibility.Collapsed,
                                                                              GuardianVisibility = !string.IsNullOrEmpty(sa.GUARDIAN_ALERT) ? Visibility.Visible : Visibility.Collapsed,
                                                                              OtherVisibility = !string.IsNullOrEmpty(sa.OTHER_ALERT) ? Visibility.Visible : Visibility.Collapsed,
                                                                              SpecialVisibility = !string.IsNullOrEmpty(sa.SPECIAL_ALERT) ? Visibility.Visible : Visibility.Collapsed,
                                                                              MentalVisibility = !string.IsNullOrEmpty(sa.MENTAL_ALERT) ? Visibility.Visible : Visibility.Collapsed,
                                                                              Over18Visibility = !string.IsNullOrEmpty(sa.OVER18_ALERT) ? Visibility.Visible : Visibility.Collapsed,
                                                                              MentalSorting = !string.IsNullOrEmpty(sa.MENTAL_ALERT) ? 1 : 2,
                                                                              MedicalSorting = !string.IsNullOrEmpty(sa.HEALTH_ALERT) ? 1 : 2,
                                                                              GuardianSorting = !string.IsNullOrEmpty(sa.GUARDIAN_ALERT) ? 1 : 2,
                                                                              OtherSorting = !string.IsNullOrEmpty(sa.OTHER_ALERT) ? 1 : 2,
                                                                              SpecialSorting = !string.IsNullOrEmpty(sa.SPECIAL_ALERT) ? 1 : 2,
                                                                              Over18Sorting = !string.IsNullOrEmpty(sa.OVER18_ALERT) ? 1 : 2
                                                                          });
        }

        public bool canExecuteMethod(object param)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class StudentAlertData
    {
        public int RNo { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public string Discipline { get; set; }
        public string Guardian { get; set; }
        public string Medical { get; set; }
        public string Special { get; set; }
        public string Other { get; set; }
        public Visibility GuardianVisibility { get; set; }
        public Visibility MedicalVisibility { get; set; }
        public Visibility SpecialVisibility { get; set; }
        public Visibility OtherVisibility { get; set; }
        public Visibility MentalVisibility { get; set; }
        public Visibility Over18Visibility { get; set; }
        public int MentalSorting { get; set; }
        public int GuardianSorting { get; set; }
        public int SpecialSorting { get; set; }
        public int OtherSorting { get; set; }
        public int MedicalSorting { get; set; }
        public int Over18Sorting { get; set; }
    }

}
