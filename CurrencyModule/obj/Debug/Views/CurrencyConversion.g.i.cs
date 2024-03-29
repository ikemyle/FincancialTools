﻿#pragma checksum "..\..\..\Views\CurrencyConversion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BBFF716262AAE48E5365A0C4E3503C05E34DE70B5C65E869967AE06ED1FF55BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CurrencyConverter.Controls;
using CurrencyConverter.Converters;
using CurrencyConverter.Views;
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
using ToggleSwitch;


namespace CurrencyConverter.Views {
    
    
    /// <summary>
    /// CurrencyConversion
    /// </summary>
    public partial class CurrencyConversion : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\Views\CurrencyConversion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CurrencyConverter.Controls.CurrencySelector CurrencySelectorFrom;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\CurrencyConversion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CurrencyConverter.Controls.CurrencySelector CurrencySelectorTo;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\CurrencyConversion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CurrencyConverter.Controls.CurrencyInput InputFrom;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\CurrencyConversion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CurrencyConverter.Controls.CurrencyInput InputTo;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Views\CurrencyConversion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CurrencyConverter.Controls.CurrencyGrid MajorCurrencies;
        
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
            System.Uri resourceLocater = new System.Uri("/CurrencyConverter;component/views/currencyconversion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CurrencyConversion.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.CurrencySelectorFrom = ((CurrencyConverter.Controls.CurrencySelector)(target));
            return;
            case 2:
            this.CurrencySelectorTo = ((CurrencyConverter.Controls.CurrencySelector)(target));
            return;
            case 3:
            this.InputFrom = ((CurrencyConverter.Controls.CurrencyInput)(target));
            return;
            case 4:
            this.InputTo = ((CurrencyConverter.Controls.CurrencyInput)(target));
            return;
            case 5:
            this.MajorCurrencies = ((CurrencyConverter.Controls.CurrencyGrid)(target));
            return;
            case 6:
            
            #line 103 "..\..\..\Views\CurrencyConversion.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler(this.Hyperlink_RequestNavigate);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

