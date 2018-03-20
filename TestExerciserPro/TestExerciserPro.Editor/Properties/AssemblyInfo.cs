#region Using directives
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
#endregion

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("TestExerciserPro.Editor")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("TestExerciserPro.Editor")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: NeutralResourcesLanguage("en-US")]


//将 ComVisible 设置为 false 将使此程序集中的类型
//对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("250d858c-a761-44c1-89ba-4ba2b2d9ea85")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: :
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//补充
[assembly: XmlnsPrefix("https://github.com/devdiv/TestExecutePro/tree/master/TestExerciserPro/TestExerciserPro.Editor", "demoEdit")]

[assembly: XmlnsDefinition("https://github.com/devdiv/TestExecutePro/tree/master/TestExerciserPro/TestExerciserPro.Editor", "TestExerciserPro.Editor")]
[assembly: XmlnsDefinition("https://github.com/devdiv/TestExecutePro/tree/master/TestExerciserPro/TestExerciserPro.Editor", "TestExerciserPro.Editor.Editing")]
[assembly: XmlnsDefinition("https://github.com/devdiv/TestExecutePro/tree/master/TestExerciserPro/TestExerciserPro.Editor", "TestExerciserPro.Editor.Rendering")]
[assembly: XmlnsDefinition("https://github.com/devdiv/TestExecutePro/tree/master/TestExerciserPro/TestExerciserPro.Editor", "TestExerciserPro.Editor.Highlighting")]
[assembly: XmlnsDefinition("https://github.com/devdiv/TestExecutePro/tree/master/TestExerciserPro/TestExerciserPro.Editor", "TestExerciserPro.Editor.Search")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2243:AttributeStringLiteralsShouldParseCorrectly",
    Justification = "AssemblyInformational Version does not need to be a parsable version")]
