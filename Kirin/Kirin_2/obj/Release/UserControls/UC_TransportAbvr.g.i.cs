﻿#pragma checksum "..\..\..\UserControls\UC_TransportAbvr.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E89C1C96A0EF641B4D3EFFC7CA8B639E1A46F899540B1E152363B0ABFB13D31B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Kirin_2.Properties;
using Kirin_2.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Kirin_2.UserControls {
    
    
    /// <summary>
    /// UC_TransportAbvr
    /// </summary>
    public partial class UC_TransportAbvr : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\UserControls\UC_TransportAbvr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Kirin_2.UserControls.UC_TransportAbvr StudentModel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\UserControls\UC_TransportAbvr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MenuClass;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\UC_TransportAbvr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform st;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\UserControls\UC_TransportAbvr.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DGAbvrData;
        
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
            System.Uri resourceLocater = new System.Uri("/Kirin_2;component/usercontrols/uc_transportabvr.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\UC_TransportAbvr.xaml"
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
            this.StudentModel = ((Kirin_2.UserControls.UC_TransportAbvr)(target));
            
            #line 11 "..\..\..\UserControls\UC_TransportAbvr.xaml"
            this.StudentModel.Loaded += new System.Windows.RoutedEventHandler(this.StudentView_Loaded);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\UserControls\UC_TransportAbvr.xaml"
            this.StudentModel.IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UserControl_IsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 21 "..\..\..\UserControls\UC_TransportAbvr.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MenuClass = ((System.Windows.Controls.Border)(target));
            
            #line 23 "..\..\..\UserControls\UC_TransportAbvr.xaml"
            this.MenuClass.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Rectangle_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.st = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 5:
            this.DGAbvrData = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

