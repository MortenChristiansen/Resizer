﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18213
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.CSharp {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class CSharpSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static CSharpSettings defaultInstance = ((CSharpSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CSharpSettings())));
        
        public static CSharpSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SaveMetadata {
            get {
                return ((bool)(this["SaveMetadata"]));
            }
            set {
                this["SaveMetadata"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("90")]
        public int EncodingQuality {
            get {
                return ((int)(this["EncodingQuality"]));
            }
            set {
                this["EncodingQuality"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1000")]
        public int MaxDimension {
            get {
                return ((int)(this["MaxDimension"]));
            }
            set {
                this["MaxDimension"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int ResizingApproach {
            get {
                return ((int)(this["ResizingApproach"]));
            }
            set {
                this["ResizingApproach"] = value;
            }
        }
    }
}
