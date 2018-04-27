using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoDog.Logics;

namespace AutoDog.Models
{
    public static class SampleData
    {
        //定义艺术家列表
        public static List<ProjectArtist> ProjectArtists { get; set; }
        //定义专辑列表
        public static List<ProjectAlbum> ProjectAlbums { get; set; }

        public static List<FileArtist> FileArtists { get; set; }

        public static List<FileAlbum> FileAlbums { get; set; }
        public static void Seed()
        {
            #region 新建工程/项目模板
            ProjectArtists = new List<ProjectArtist>
            {
               new ProjectArtist { Name=Common.ProjectType.Python.ToString() },
               new ProjectArtist { Name=Common.ProjectType.CSharp.ToString() },
               new ProjectArtist { Name=Common.ProjectType.Java.ToString() },
               new ProjectArtist { Name=Common.ProjectType.Ruby.ToString() },
               new ProjectArtist { Name=Common.ProjectType.API.ToString() },
               new ProjectArtist { Name=Common.ProjectType.Performance.ToString() },
            };
            //int i = 0;
            //ProjectArtists.ForEach(x => x.ProjectArtistId = ++i);

            ProjectAlbums = new List<ProjectAlbum>
                {

                #region 创建Python类型模板
                new ProjectAlbum {
                        Title = "Python Web UI测试",
                        TemplateType = Common.ProjectType.Python.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Python.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/web_py.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。",
                        ProjectIcon= "pack://application:,,/Resources/project_py.png",
                        IncludeFileExtension =".py",
                },

                    new ProjectAlbum {
                        Title = "Pyhton Windows UI测试",
                        TemplateType = Common.ProjectType.Python.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Python.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Windows_py.png",
                        Describe ="Python Windows UI测试，集成PywinAuto、PyAutoIt等测试框架。",
                        ProjectIcon= "pack://application:,,/Resources/project_py.png",
                        IncludeFileExtension =".py",
                    },

                    new ProjectAlbum {
                        Title = "Python 服务器功能测试",
                        TemplateType = Common.ProjectType.Python.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Python.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Server_py.png",
                        Describe ="设计中...",
                        ProjectIcon= "pack://application:,,/Resources/project_py.png",
                        IncludeFileExtension =".py",
                    },

                    new ProjectAlbum {
                        Title = "Pyhton 移动端功能测试",
                        TemplateType = Common.ProjectType.Python.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Python.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Phone_py.png",
                        Describe ="Python 移动端测试，集成Appuim等测试框架。",
                        ProjectIcon= "pack://application:,,/Resources/project_py.png",
                        IncludeFileExtension =".py",
                    },
                #endregion

                #region 创建C#类型模板
                new ProjectAlbum {
                        Title = "C# Web UI测试",
                        TemplateType = Common.ProjectType.CSharp.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.CSharp.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Web_csharp.png",
                        Describe ="C# Web UI测试，集成Selenium等测试框架。",
                        ProjectIcon= "pack://application:,,/Resources/project_csharp.png",
                        IncludeFileExtension =".cs",
                },

                    new ProjectAlbum {
                        Title = "C# Windows UI测试",
                        TemplateType = Common.ProjectType.CSharp.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.CSharp.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Windows_csharp.png",
                        Describe ="C# Windows UI测试，集成AutoIt,UIAutomation等测试框架。",
                        ProjectIcon= "pack://application:,,/Resources/project_csharp.png",
                        IncludeFileExtension =".cs",
                    },
                #endregion

                #region 创建Java类型模板
                new ProjectAlbum {
                        Title = "Java Web UI测试",
                        TemplateType = Common.ProjectType.Java.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Java.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Web_java.png",
                        Describe ="Java Web UI测试，集成Selenium等测试框架。",
                        ProjectIcon= "pack://application:,,/Resources/project_java.png",
                        IncludeFileExtension =".java",
                },

                    new ProjectAlbum {
                        Title = "Java Windows UI测试",
                        TemplateType = Common.ProjectType.Java.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Java.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Windows_java.png",
                        Describe ="Java Windows UI测试，设计中...",
                        ProjectIcon= "pack://application:,,/Resources/project_java.png",
                        IncludeFileExtension =".java",
                    },
                #endregion

                #region 创建Ruby类型模板
                new ProjectAlbum {
                        Title = "Ruby Web UI测试",
                        TemplateType = Common.ProjectType.Ruby.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Ruby.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Web_ruby.png",
                        Describe ="Ruby Web UI测试，设计中...",
                        ProjectIcon= "pack://application:,,/Resources/project_ruby.png",
                        IncludeFileExtension =".rb",
                },
                #endregion

                #region 创建API类型模板
                new ProjectAlbum {
                        Title = "Web API测试",
                        TemplateType = Common.ProjectType.API.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.API.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/Web_api.png",
                        Describe ="Web API 测试，集成基本的API测试解决方案及简单的模板。",
                        ProjectIcon= "pack://application:,,/Resources/project_api.png",
                        IncludeFileExtension =".xml",
                },
                #endregion

                #region 创建性能测试类型模板
                new ProjectAlbum {
                        Title = "Web Performance测试",
                        TemplateType = Common.ProjectType.Performance.ToString(),
                        ProjectArtist = ProjectArtists.First(a => a.Name == Common.ProjectType.Performance.ToString()),
                        ImageSource ="pack://application:,,/Images/Template/web_performance.png",
                        Describe ="Web Performance 测试，集成基本性能监测解决方案以及相关数据分析日志。",
                        ProjectIcon= "pack://application:,,/Resources/project_proformance.png",
                        IncludeFileExtension =".xml",
                       
                },
                #endregion
                
                };

            var projectAlbumsGroupedByArtist = ProjectAlbums.GroupBy(a => a.ProjectArtist);
            foreach (var grouping in projectAlbumsGroupedByArtist)
            {
                grouping.Key.ProjectAlbums = grouping.ToList();
            }
            #endregion


            #region  新建文件类型模板
            FileArtists = new List<FileArtist>
            {
               new FileArtist { Name="Common" },
               new FileArtist { Name="API" },
               new FileArtist { Name="Performance" },
               new FileArtist { Name="Script" },
            };

            int j = 0;
            FileArtists.ForEach(x => x.fileArtistId = ++j);

            FileAlbums = new List<FileAlbum>
            {
                new FileAlbum {
                        Title = "Python文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/file_py.png",
                        Describe ="创建空白的Python文件。"},

                    new FileAlbum {
                        Title = "Java文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Java",
                        ImageSource ="/Images/Template/file_java.png",
                        Describe ="创建空白的Java文件。"},

                    new FileAlbum {
                        Title = "C Sharp文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "C#",
                        ImageSource ="/Images/Template/file_csharp.png",
                        Describe ="创建空白的C Sharp文件。"},

                    new FileAlbum {
                        Title = "文本文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Txt",
                        ImageSource ="/Images/Template/file_txt.png",
                        Describe ="创建空白的文本文件。"},

                    new FileAlbum {
                        Title = "XML文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "XML",
                        ImageSource ="/Images/Template/file_xml.png",
                        Describe ="创建空白的XML文件。"},

                    new FileAlbum {
                        Title = "C++文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "C++",
                        ImageSource ="/Images/Template/file_cpp.png",
                        Describe ="创建空白的C++文件。"},

                    new FileAlbum {
                        Title = "C文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "C",
                        ImageSource ="/Images/Template/file_c.png",
                        Describe ="创建空白的C文件。"},

                    new FileAlbum {
                        Title = "Ruby文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Ruby",
                        ImageSource ="/Images/Template/file_rb.png",
                        Describe ="创建空白的C++文件。"},

                    new FileAlbum {
                        Title = "API测试文件",
                        FileArtist = FileArtists.First(a => a.Name == "API"),
                        TemplateType = "API",
                        ImageSource ="/Images/Template/api.png",
                        Describe ="创建一个API测试模板文件。"},

                    new FileAlbum {
                        Title = "Web Performance测试文件",
                        FileArtist = FileArtists.First(a => a.Name == "Performance"),
                        TemplateType = "Performance",
                        ImageSource ="/Images/Template/performance.png",
                        Describe ="创建一个Web 性能测试模板文件。"},

                    new FileAlbum {
                        Title = "Batch文件",
                        FileArtist = FileArtists.First(a => a.Name == "Script"),
                        TemplateType = "Batch",
                        ImageSource ="/Images/Template/file_bat.png",
                        Describe ="创建一个空的Batch文件。"},

                    new FileAlbum {
                        Title = "PowerShell文件",
                        FileArtist = FileArtists.First(a => a.Name == "Script"),
                        TemplateType = "PowerShell",
                        ImageSource ="/Images/Template/file_ps1.png",
                        Describe ="创建一个空的PowerShell文件。"},
            };

            var fileAlbumsGroupedByArtist = FileAlbums.GroupBy(a => a.FileArtist);
            foreach (var grouping in fileAlbumsGroupedByArtist)
            {
                grouping.Key.FileAlbums = grouping.ToList();
            }

            #endregion

        }
    }
}
