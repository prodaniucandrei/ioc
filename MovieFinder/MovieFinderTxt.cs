using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieFinder
{
    public class MovieFinderTxt : IMovieFinder
    {
        private readonly string _file;

        public MovieFinderTxt(string file)
        {
            _file = file;
        }

        public IEnumerable<IMovie> FindAll()
        {
            return GetMoviesFromTXT();
        }

        public IEnumerable<IMovie> FindByDirector(string director)
        {
            return GetMoviesFromTXT().Where(movie => movie.Director == director).ToList(); ;
        }

        private IEnumerable<IMovie> GetMoviesFromTXT()
        {
            var list = new List<IMovie>() {
                new Movie { Name = "Movie1-Txt", Director = "Director1", Length = 10},
                new Movie { Name = "Movie2-Txt", Director = "Director2", Length = 20},
                new Movie { Name = "Movie3-Txt", Director = "Director3", Length = 30},
            };

            /*using (var fs = new FileStream(_file, FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                var n = int.Parse(sr.ReadLine());
                for (var i = 0; i < n; i++)
                {
                    var movie = new Movie();
                    movie.Name = sr.ReadLine();
                    movie.Director = sr.ReadLine();
                    movie.Length = int.Parse(sr.ReadLine());
                    list.Add(movie);
                }
            }*/

            return list;
        }
    }
}

