﻿#pragma checksum "..\..\Prijava.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EC5C7F45D0A588B22961DB8C7BA6F813AECAEAEB91DAD4F216D6A2E28C2AAC59"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bolnica_aplikacija;
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


namespace Bolnica_aplikacija {
    
    
    /// <summary>
    /// Prijava
    /// </summary>
    public partial class Prijava : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Slika;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblKorisnickoIme;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtKorisnickoIme;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblLozinka;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtLozinka;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNeispravno;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNeispravno2;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrijava;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Prijava.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
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
            System.Uri resourceLocater = new System.Uri("/Bolnica_aplikacija;component/prijava.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Prijava.xaml"
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
            this.Slika = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lblKorisnickoIme = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.txtKorisnickoIme = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\Prijava.xaml"
            this.txtKorisnickoIme.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtKorisnickoIme_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblLozinka = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.txtLozinka = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 21 "..\..\Prijava.xaml"
            this.txtLozinka.PasswordChanged += new System.Windows.RoutedEventHandler(this.txtLozinka_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblNeispravno = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblNeispravno2 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnPrijava = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Prijava.xaml"
            this.btnPrijava.Click += new System.Windows.RoutedEventHandler(this.btnPrijava_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

