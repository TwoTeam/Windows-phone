﻿

#pragma checksum "C:\Users\Klemen\Desktop\Eventhub - kopija\EventHub\StartPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4223890CE07619233763B99F2EC5FEC8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHub
{
    partial class PivotPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 29 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Pivot)(target)).SelectionChanged += this.pivot_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 40 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Reviews_Tapped;
                 #line default
                 #line hidden
                #line 40 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Holding += this.Reviews_Holding;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 43 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target)).Click += this.fav_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 44 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target)).Click += this.hid_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 117 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PinUnPinCommandButton_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 118 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.about_click;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 119 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.logout_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


