﻿#pragma checksum "..\..\..\Pages\ClassMultiDayAttendance_old.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BD5E28EB88E0606D77EE31B9B39FAEB45C0FDADCEA34C2A5370CA1315290B2DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Kirin_2.Pages;
using Kirin_2.Properties;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Kirin_2.Pages {
    
    
    /// <summary>
    /// ClassMultiDayAttendance_old
    /// </summary>
    public partial class ClassMultiDayAttendance_old : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 249 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ClassTitle;
        
        #line default
        #line hidden
        
        
        #line 264 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateRange;
        
        #line default
        #line hidden
        
        
        #line 276 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TabButtonFirst;
        
        #line default
        #line hidden
        
        
        #line 277 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TabButtonLast;
        
        #line default
        #line hidden
        
        
        #line 306 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 328 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid AttendanceList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Kirin_2;component/pages/classmultidayattendance_old.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ClassTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.DateRange = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.TabButtonFirst = ((System.Windows.Controls.Button)(target));
            
            #line 276 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
            this.TabButtonFirst.Click += new System.Windows.RoutedEventHandler(this.SwitchToSingleDayView);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TabButtonLast = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 305 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
            this.SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 312 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SubmitAttendance);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AttendanceList = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 8:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseRightButtonDownEvent;
            
            #line 426 "..\..\..\Pages\ClassMultiDayAttendance_old.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.AddAlertComment);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

