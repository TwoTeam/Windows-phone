﻿

#pragma checksum "C:\Users\Klemen\Desktop\EventHub\EventHub\StartPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "589DE2D8E835C87D97D1655FA0D1BDE7"
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
                #line 55 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Butn_register_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 33 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Butn_login_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 36 "..\..\StartPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Text_Forgot_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


