using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
               new ProjectArtist { Name="Python" },
               new ProjectArtist { Name="C#" },
               new ProjectArtist { Name="Java" },
               new ProjectArtist { Name="Ruby" },
               new ProjectArtist { Name="API" },
               new ProjectArtist { Name="Performance" },
            };
            int i = 0;
            ProjectArtists.ForEach(x => x.ProjectArtistId = ++i);

            ProjectAlbums = new List<ProjectAlbum>
                {

                #region 创建Python类型模板
                new ProjectAlbum {
                        Title = "Python Web UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。"},

                    new ProjectAlbum {
                        Title = "Pyhton Windows UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Windows.png",
                        Describe ="Python Windows UI测试，集成PywinAuto、PyAutoIt等测试框架。"},

                    new ProjectAlbum {
                        Title = "Python 服务器功能测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Server.png",
                        Describe ="设计中..."},

                    new ProjectAlbum {
                        Title = "Pyhton 移动端功能测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Phone.png",
                        Describe ="Python 移动端测试，集成Appuim等测试框架。"},
                #endregion

                #region 创建C#类型模板
                new ProjectAlbum {
                        Title = "C# Web UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "C#"),
                        TemplateType = "C#",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="C# Web UI测试，集成Selenium等测试框架。"},

                    new ProjectAlbum {
                        Title = "C# Windows UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "C#"),
                        TemplateType = "C#",
                        ImageSource ="/Images/Template/Windows.png",
                        Describe ="C# Windows UI测试，集成AutoIt,UIAutomation等测试框架。"},
                #endregion

                #region 创建Java类型模板
                new ProjectAlbum {
                        Title = "Java Web UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Java"),
                        TemplateType = "Java",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Java Web UI测试，集成Selenium等测试框架。"},

                    new ProjectAlbum {
                        Title = "Java Windows UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Java"),
                        TemplateType = "Java",
                        ImageSource ="/Images/Template/Windows.png",
                        Describe ="Java Windows UI测试，设计中..."},
                #endregion

                #region 创建Ruby类型模板
                new ProjectAlbum {
                        Title = "Ruby Web UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Ruby"),
                        TemplateType = "Ruby",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Ruby Web UI测试，设计中..."},
                #endregion

                #region 创建API类型模板
                new ProjectAlbum {
                        Title = "Web API测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "API"),
                        TemplateType = "API",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Web API 测试，集成基本的API测试解决方案及简单的模板。"},
                #endregion

                #region 创建API类型模板
                new ProjectAlbum {
                        Title = "Web Performance测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Performance"),
                        TemplateType = "Performance",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Web Performance 测试，集成基本性能监测解决方案以及相关数据分析日志。"},
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
                        ImageSource ="/Images/Template/py.png",
                        Describe ="创建空白的Python文件。"},

                    new FileAlbum {
                        Title = "Java文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Java",
                        ImageSource ="/Images/Template/java.png",
                        Describe ="创建空白的Java文件。"},

                    new FileAlbum {
                        Title = "C Sharp文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "C#",
                        ImageSource ="/Images/Template/cs.png",
                        Describe ="创建空白的C Sharp文件。"},

                    new FileAlbum {
                        Title = "文本文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Txt",
                        ImageSource ="/Images/Template/txt.png",
                        Describe ="创建空白的文本文件。"},

                    new FileAlbum {
                        Title = "XML文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "XML",
                        ImageSource ="/Images/Template/xml.png",
                        Describe ="创建空白的XML文件。"},

                    new FileAlbum {
                        Title = "C++文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "C++",
                        ImageSource ="/Images/Template/cpp.png",
                        Describe ="创建空白的C++文件。"},

                    new FileAlbum {
                        Title = "C文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "C",
                        ImageSource ="/Images/Template/c.png",
                        Describe ="创建空白的C文件。"},

                    new FileAlbum {
                        Title = "Ruby文件",
                        FileArtist = FileArtists.First(a => a.Name == "Common"),
                        TemplateType = "Ruby",
                        ImageSource ="/Images/Template/rb.png",
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
                        ImageSource ="/Images/Template/bat.png",
                        Describe ="创建一个空的Batch文件。"},

                    new FileAlbum {
                        Title = "PowerShell文件",
                        FileArtist = FileArtists.First(a => a.Name == "Script"),
                        TemplateType = "PowerShell",
                        ImageSource ="/Images/Template/ps1.png",
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
