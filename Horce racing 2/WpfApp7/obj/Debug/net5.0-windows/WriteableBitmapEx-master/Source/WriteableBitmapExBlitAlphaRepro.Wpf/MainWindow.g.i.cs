﻿#pragma checksum "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DE5CBC61EBF741DE5CEC9390ACB18186F392318A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace WriteableBitmapExBlitAlphaRepro.Wpf {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgOrg;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgOrgOverlay;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgMod;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgModPrgba;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp7;V1.0.0.0;component/writeablebitmapex-master/source/writeablebitmapexblit" +
                    "alpharepro.wpf/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\..\..\..\WriteableBitmapEx-master\Source\WriteableBitmapExBlitAlphaRepro.Wpf\MainWindow.xaml"
            ((WriteableBitmapExBlitAlphaRepro.Wpf.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ImgOrg = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.ImgOrgOverlay = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.ImgMod = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.ImgModPrgba = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

