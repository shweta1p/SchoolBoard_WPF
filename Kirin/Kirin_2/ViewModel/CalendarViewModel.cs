﻿using Kirin_2.Models;
using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Kirin_2.ViewModel
{
    public class CalendarViewModel : NotificationObject
    {
        KIRINEntities1 kirinentities;
        string subjectId = App.Current.Properties["SubjectId"].ToString();

        public CalendarViewModel()
        {
            kirinentities = new KIRINEntities1();
            Events = GenerateRandomAppointments();
            MinDate = DateTime.Now.Date.AddMonths(-3);
            MaxDate = DateTime.Now.AddMonths(3);
            DisplayDate = DateTime.Now.Date.AddHours(9);
        }
        private ScheduleAppointmentCollection events;

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public DateTime DisplayDate { get; set; }
        public ScheduleAppointmentCollection Events
        {
            get { return events; }
            set
            {
                events = value;
                RaisePropertyChanged("Events");
            }
        }

        /// <summary>
        /// Method to get foreground color based on background.
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        private Brush GetAppointmentForeground(Brush backgroundColor)
        {
            if (backgroundColor.ToString().Equals("#FF8551F2") || backgroundColor.ToString().Equals("#FF5363FA") || backgroundColor.ToString().Equals("#FF2D99FF"))
                return Brushes.White;
            else
                return new SolidColorBrush(Color.FromRgb(51, 51, 51));
        }

        private ScheduleAppointmentCollection GenerateRandomAppointments()
        {
            var WorkWeekDays = new ObservableCollection<DateTime>();
            var WorkWeekSubjects = new ObservableCollection<string>()
                                                           { "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
                                                             "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting" };

            var NonWorkingDays = new ObservableCollection<DateTime>();
            var NonWorkingSubjects = new ObservableCollection<string>()
                                                           { "Go to party", "Order Pizza", "Buy Gift", "Vacation" };
            var YearlyOccurrance = new ObservableCollection<DateTime>();
            var YearlyOccurranceSubjects = new ObservableCollection<string>() { "Wedding Anniversary", "Sam's Birthday", "Jenny's Birthday" };
            var MonthlyOccurrance = new ObservableCollection<DateTime>();
            var MonthlyOccurranceSubjects = new ObservableCollection<string>() { "Pay House Rent", "Car Service", "Medical Check Up" };
            var WeekEndOccurrance = new ObservableCollection<DateTime>();
            var WeekEndOccurranceSubjects = new ObservableCollection<string>() { "FootBall Match", "TV Show" };


            var brush = new ObservableCollection<SolidColorBrush>();
            brush.Add(new SolidColorBrush(Color.FromRgb(133, 81, 242)));
            brush.Add(new SolidColorBrush(Color.FromRgb(140, 245, 219)));
            brush.Add(new SolidColorBrush(Color.FromRgb(83, 99, 250)));
            brush.Add(new SolidColorBrush(Color.FromRgb(255, 222, 133)));
            brush.Add(new SolidColorBrush(Color.FromRgb(45, 153, 255)));
            brush.Add(new SolidColorBrush(Color.FromRgb(253, 183, 165)));
            brush.Add(new SolidColorBrush(Color.FromRgb(198, 237, 115)));
            brush.Add(new SolidColorBrush(Color.FromRgb(253, 185, 222)));
            brush.Add(new SolidColorBrush(Color.FromRgb(255, 222, 133)));
            
            Random ran = new Random();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }

            DateTime startMonth = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
            DateTime endMonth = new DateTime(today.Year, today.Month + 1, 30, 0, 0, 0);
            DateTime dt = new DateTime(today.Year, today.Month, 15, 0, 0, 0);
            int day = (int)startMonth.DayOfWeek;
            DateTime CurrentStart = startMonth.AddDays(-day);

            var appointments = new ScheduleAppointmentCollection();
            for (int i = 0; i < 90; i++)
            {
                if (i % 7 == 0 || i % 7 == 6)
                {
                    //add weekend appointments
                    NonWorkingDays.Add(CurrentStart.AddDays(i));
                }
                else
                {
                    //Add Workweek appointment
                    WorkWeekDays.Add(CurrentStart.AddDays(i));
                }
            }

            for (int i = 0; i < 50; i++)
            {
                DateTime date = WorkWeekDays[ran.Next(0, WorkWeekDays.Count)].AddHours(ran.Next(9, 17));
                appointments.Add(new ScheduleAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    AppointmentBackground = brush[i % brush.Count],
                    Foreground = GetAppointmentForeground(brush[i % brush.Count]),
                    Subject = WorkWeekSubjects[i % WorkWeekSubjects.Count],

                });
            }
            int j = 0;
            int k = 0;
            int l = 0;

            while (j < YearlyOccurranceSubjects.Count)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)];
                appointments.Add(new ScheduleAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    AppointmentBackground = brush[1],
                    Foreground = GetAppointmentForeground(brush[1]),
                    Subject = YearlyOccurranceSubjects[j]
                });
                j++;
            }

            while (k < MonthlyOccurranceSubjects.Count)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(9, 23));
                appointments.Add(new ScheduleAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    AppointmentBackground = brush[k],
                    Foreground = GetAppointmentForeground(brush[k]),
                    Subject = MonthlyOccurranceSubjects[k]
                });
                k++;
            }

            while (l < WeekEndOccurranceSubjects.Count)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(0, 23));
                appointments.Add(new ScheduleAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    AppointmentBackground = brush[l],
                    Foreground = GetAppointmentForeground(brush[1]),
                    Subject = WeekEndOccurranceSubjects[l]
                });
                l++;
            }

            var assignments = kirinentities.GetAssignmentforSchedule(subjectId).ToList();
            int a = 0;

            foreach (var assignment in assignments)
            {
                ObservableCollection<SchedulerReminder> RemList = new ObservableCollection<SchedulerReminder>(); 
                
                SchedulerReminder reminder = new SchedulerReminder();
                reminder.ReminderTimeInterval = TimeSpan.FromMinutes(10);

                RemList.Add(reminder);
                var color = (Color)ColorConverter.ConvertFromString("#B00000");
                appointments.Add(new ScheduleAppointment()
                {
                    StartTime = assignment.DATE_DUE,
                    EndTime = assignment.DATE_DUE.AddHours(1),
                    AppointmentBackground = new SolidColorBrush() { Opacity = 1, Color = color },
                    Foreground = Brushes.White,
                    Subject = assignment.NAME,
                    Reminders = RemList
                }) ;
                a++;
            }

            return appointments;
        }
    }
}
