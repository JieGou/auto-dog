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

            FileArtists = new List<FileArtist>
            {
               new FileArtist {Name="Python" },
               new FileArtist { Name="C#" },
               new FileArtist { Name="Java" },
               new FileArtist { Name="JavaScript" },
               new FileArtist { Name="Ruby" },
               new FileArtist { Name="PowerShell" },
               new FileArtist { Name="Batch" },
               new FileArtist { Name="API" },
               new FileArtist { Name="Performance" },
            };

            int j = 0;
            FileArtists.ForEach(x => x.fileArtistId = ++j);

            ProjectAlbums = new List<ProjectAlbum>
                {
                    new ProjectAlbum {
                        Title = "Python Web UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new ProjectAlbum {
                        Title = "Pyhton Windows UI测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Windows.png",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new ProjectAlbum {
                        Title = "Python 服务器功能测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Server.png",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new ProjectAlbum {
                        Title = "Pyhton 移动端功能测试",
                        ProjectArtist = ProjectArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Phone.png",
                        DescripImageSource = "/Images/Template/Phone.png"},
                };

            var projectAlbumsGroupedByArtist = ProjectAlbums.GroupBy(a => a.ProjectArtist);
            foreach (var grouping in projectAlbumsGroupedByArtist)
            {
                grouping.Key.ProjectAlbums = grouping.ToList();
            }

            FileAlbums = new List<FileAlbum>
            {
                new FileAlbum {
                        Title = "Python Web UI测试",
                        FileArtist = FileArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new FileAlbum {
                        Title = "Pyhton Windows UI测试",
                        FileArtist = FileArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Windows.png",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new FileAlbum {
                        Title = "Python 服务器功能测试",
                        FileArtist = FileArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Server.png",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new FileAlbum {
                        Title = "Pyhton 移动端功能测试",
                        FileArtist = FileArtists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Phone.png",
                        DescripImageSource = "/Images/Template/Phone.png"},
            };

            var fileAlbumsGroupedByArtist = FileAlbums.GroupBy(a => a.FileArtist);
            foreach (var grouping in fileAlbumsGroupedByArtist)
            {
                grouping.Key.FileAlbums = grouping.ToList();
            }

        }
    }
}
