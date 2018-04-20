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
        public static List<Artist> Artists { get; set; }
        //定义专辑列表
        public static List<Album> Albums { get; set; }
        public static void Seed()
        {
            Artists = new List<Artist>
            {
               new Artist { Name="Python" },
               new Artist { Name="C#" },
               new Artist { Name="Java" },
               new Artist { Name="JavaScript" },
               new Artist { Name="Ruby" },
               new Artist { Name="PowerShell" },
               new Artist { Name="Batch" },
               new Artist { Name="API" },
               new Artist { Name="Performance" },
            };

            int i = 0;
            Artists.ForEach(x => x.ArtistId = ++i);

            Albums = new List<Album>
                {
                    new Album {
                        Title = "Python Web UI测试",
                        Artist = Artists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Web.png",
                        Describe ="Python Web UI测试，集成Selenium等测试框架。",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new Album {
                        Title = "Pyhton Windows UI测试",
                        Artist = Artists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Windows.png",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new Album {
                        Title = "Python 服务器功能测试",
                        Artist = Artists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Server.png",
                        DescripImageSource = "/Images/Template/UI.png"},

                    new Album {
                        Title = "Pyhton 移动端功能测试",
                        Artist = Artists.First(a => a.Name == "Python"),
                        TemplateType = "Python",
                        ImageSource ="/Images/Template/Phone.png",
                        DescripImageSource = "/Images/Template/Phone.png"},
                };

            var albumsGroupedByArtist = Albums.GroupBy(a => a.Artist);
            foreach (var grouping in albumsGroupedByArtist)
            {
                grouping.Key.Albums = grouping.ToList();
            }
        }
    }
}
