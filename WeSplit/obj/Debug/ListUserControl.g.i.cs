﻿#pragma checksum "..\..\ListUserControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "36499542DCD47AF5DAF98A9D1644C549B00B2DA72FD3C064A77505091D8CBF26"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WeSplit;


namespace WeSplit {
    
    
    /// <summary>
    /// ListUserControl
    /// </summary>
    public partial class ListUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 8 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WeSplit.ListUserControl listUserControl;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border radioBorder;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton placeRadio;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton memberRadio;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button filterSearchButton;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchBox;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView dataListView1;
        
        #line default
        #line hidden
        
        
        #line 325 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView dataListView2;
        
        #line default
        #line hidden
        
        
        #line 466 "..\..\ListUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView dataListView3;
        
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
            System.Uri resourceLocater = new System.Uri("/WeSplit;component/listusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ListUserControl.xaml"
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
            this.listUserControl = ((WeSplit.ListUserControl)(target));
            
            #line 11 "..\..\ListUserControl.xaml"
            this.listUserControl.Loaded += new System.Windows.RoutedEventHandler(this.listUserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.radioBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.placeRadio = ((System.Windows.Controls.RadioButton)(target));
            
            #line 48 "..\..\ListUserControl.xaml"
            this.placeRadio.Checked += new System.Windows.RoutedEventHandler(this.placeRadio_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.memberRadio = ((System.Windows.Controls.RadioButton)(target));
            
            #line 53 "..\..\ListUserControl.xaml"
            this.memberRadio.Checked += new System.Windows.RoutedEventHandler(this.memberRadio_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.filterSearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\ListUserControl.xaml"
            this.filterSearchButton.Click += new System.Windows.RoutedEventHandler(this.filterSearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.searchBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.dataListView1 = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.dataListView2 = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            this.dataListView3 = ((System.Windows.Controls.ListView)(target));
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
            switch (connectionId)
            {
            case 8:
            
            #line 263 "..\..\ListUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.detailButton_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 404 "..\..\ListUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.detailButton_Click);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 545 "..\..\ListUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.detailButton_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
