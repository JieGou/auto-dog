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
            ProjectArtists = new List<ProjectArtist>
            {
               new ProjectArtist { Name="Python" },
               new ProjectArtist { Name="C#" },
               new ProjectArtist { Name="Java" },
               new ProjectArtist { Name="JavaScript" },
               new ProjectArtist { Name="Ruby" },
               new ProjectArtist { Name="PowerShell" },
               new ProjectArtist { Name="Batch" },
               new ProjectArtist { Name="API" },
               new ProjectArtist { Name="Performance" },
            };
            int i = 0;
            ProjectArtists.ForEach(x => x.ProjectArtistId = ++i);

            ProjectAlbums = new List<ProjectAlbum>
                {
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
                        Describe ="Python Web UI测试，集成Selenium等测试框架。"},

                    new ProjectAlbum {
                        Title = "Python 服务器功能测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Server.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。"},

                    new ProjectAlbum {
                        Title = "Pyhton 移动端功能测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Phone.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。"},
                };

            var projectAlbumsGroupedByArtist = ProjectAlbums.GroupBy(a => a.ProjectArtist);
            foreach (var grouping in projectAlbumsGroupedByArtist)
            {
                grouping.Key.ProjectAlbums = grouping.ToList();
            }


            FileArtists = new List<FileArtist>
            {
               new FileArtist {Name="Common" },
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
            };

            var fileAlbumsGroupedByArtist = FileAlbums.GroupBy(a => a.FileArtist);
            foreach (var grouping in fileAlbumsGroupedByArtist)
            {
                grouping.Key.FileAlbums = grouping.ToList();
            }

        }
    }
}
