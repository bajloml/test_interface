#pragma checksum "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E2425B42EB2F09EA3A9F09B06D1E20E70C0B3FA4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
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
using System.Windows.Interactivity;
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
using WPF_CrossComm_Client.MVVM_Pattern.Views;


namespace WPF_CrossComm_Client.MVVM_Pattern.Views {
    
    
    /// <summary>
    /// ReadWriteTab_View
    /// </summary>
    public partial class ReadWriteTab_View : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Tab_Grid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Write_Label;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Read_Label;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WriteVar;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WriteVarButton;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReadVarButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WPF_Client;component/mvvm_pattern/views/readwritetab_view.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM_pattern\Views\ReadWriteTab_View.xaml"
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
            this.Tab_Grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Write_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Read_Label = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.WriteVar = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.WriteVarButton = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.ReadVarButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

