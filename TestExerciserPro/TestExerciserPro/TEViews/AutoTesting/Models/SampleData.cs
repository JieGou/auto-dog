using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExerciserPro.TEViews.AutoTesting.Models
{
    public static class SampleData
    {
        public static List<Genre> Genres { get; set; }
        public static List<Artist> Artists { get; set; }
        public static List<Album> Albums { get; set; }
        public static List<SolutionType> SolutionTypes { get; set; }
        public static List<LocationMap> LocationMaps { get; set; }
        public static void Seed()
        {
            if (Genres != null)
                return;

            SolutionTypes = new List<SolutionType>
            {
                new SolutionType {Title ="创建新解决方案"},
                new SolutionType {Title ="添加到解决方案"},
                new SolutionType {Title ="在新实例中创建"},
            };

            Genres = new List<Genre>
            {
                new Genre { Name = "Rock" },
                new Genre { Name = "Jazz" },
                new Genre { Name = "Metal" },
                new Genre { Name = "Alternative" },
                new Genre { Name = "Disco" },
                new Genre { Name = "Blues" },
                new Genre { Name = "Latin" },
                new Genre { Name = "Reggae" },
                new Genre { Name = "Pop" },
                new Genre { Name = "Classical" }
            };

            Artists = new List<Artist>
            {
                new Artist { Name = "Aaron Copland & London Symphony Orchestra" },
                new Artist { Name = "Aaron Goldberg" },
                new Artist { Name = "AC/DC" },
            };

            int i = 0;
            Artists.ForEach(x => x.ArtistId = ++i);

            Albums = new List<Album>
                {
                    new Album {Title = "Let There Be Rock", Genre = Genres.First(g => g.Name == "Rock"), Price = 8.99M, Artist = Artists.First(a => a.Name == "AC/DC"), AlbumArtUrl = "/Content/Images/placeholder.gif"},                
                };


            var r = new Random(Environment.TickCount);
            Albums.ForEach(x => x.Price = Convert.ToDecimal(r.NextDouble() * 20d));

            var albumsGroupedByArtist = Albums.GroupBy(a => a.Artist);
            foreach (var grouping in albumsGroupedByArtist)
            {
                grouping.Key.Albums = grouping.ToList();
            }
        }
    }
}
