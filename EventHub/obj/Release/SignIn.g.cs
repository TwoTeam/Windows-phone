﻿

#pragma checksum "C:\Users\Klemen\Desktop\Eventhub - kopija\EventHub\SignIn.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DA50361D2D70317D5F4526988FD33027"
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
    partial class SignIn : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 39 "..\..\SignIn.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.forr_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 40 "..\..\SignIn.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.sign_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 46 "..\..\SignIn.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Login_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


