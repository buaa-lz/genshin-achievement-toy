﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenshinAchievement.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GenshinAchievement.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 {&quot;心跳的记忆&quot;:[{&quot;id&quot;:&quot;84026&quot;,&quot;ver&quot;:&quot;1.4&quot;,&quot;name&quot;:&quot;美妙旅程：序&quot;,&quot;desc&quot;:&quot;完成「美妙旅程」，解锁全部结局。&quot;,&quot;reward&quot;:&quot;20&quot;},{&quot;id&quot;:&quot;84100&quot;,&quot;ver&quot;:&quot;1.4&quot;,&quot;name&quot;:&quot;霹雳闪雷真君&quot;,&quot;desc&quot;:&quot;见证班尼特足以影响天气的厄运。&quot;,&quot;reward&quot;:&quot;5&quot;},{&quot;id&quot;:&quot;84101&quot;,&quot;ver&quot;:&quot;1.4&quot;,&quot;name&quot;:&quot;运气即实力！&quot;,&quot;desc&quot;:&quot;在不失误的情况下解开机关，取得宝藏。&quot;,&quot;reward&quot;:&quot;5&quot;},{&quot;id&quot;:&quot;84104&quot;,&quot;ver&quot;:&quot;1.4&quot;,&quot;name&quot;:&quot;诸邪退散&quot;,&quot;desc&quot;:&quot;完成「寻妖觅邪记」，解锁全部结局。&quot;,&quot;reward&quot;:&quot;20&quot;},{&quot;id&quot;:&quot;84028&quot;,&quot;ver&quot;:&quot;1.4&quot;,&quot;name&quot;:&quot;慧眼识妖！&quot;,&quot;desc&quot;:&quot;正确鉴定所有情报。&quot;,&quot;reward&quot;:&quot;5&quot;},{&quot;id&quot;:&quot;84107&quot;,&quot;ver&quot;:&quot;1.4&quot;,&quot;name&quot;:&quot;辣椒英雄&quo... 的本地化字符串。
        /// </summary>
        internal static string AchievementJson {
            get {
                return ResourceManager.GetString("AchievementJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似于 (图标) 的 System.Drawing.Icon 类型的本地化资源。
        /// </summary>
        internal static System.Drawing.Icon kokomi {
            get {
                object obj = ResourceManager.GetObject("kokomi", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
    }
}
