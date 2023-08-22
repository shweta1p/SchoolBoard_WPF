﻿using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirin_2.Behaviors
{
    public class BlackoutDatesBehavior : Behavior<SfScheduler>
    {
        ObservableCollection<DateTime> blackOutDates = new ObservableCollection<DateTime>();
        protected override void OnAttached()
        {
            this.AssociatedObject.ViewChanged += AssociatedObject_ViewChanged;
        }

        private void AssociatedObject_ViewChanged(object sender, ViewChangedEventArgs e)
        {
            blackOutDates.Clear();
            for (int i = -4; i < 3; i++)
            {
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(-2));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(2));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(6));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(10));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(14));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(18));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(22));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(26));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(30));
                blackOutDates.Add(DateTime.Now.Date.AddMonths(i).AddDays(34));
            }

            this.AssociatedObject.BlackoutDates = blackOutDates;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.ViewChanged -= AssociatedObject_ViewChanged;
            if (blackOutDates != null)
                blackOutDates = null;
        }
    }
}
