using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieFinder
{
    public class MovieFinderJson : IMovieFinder
    {
        private readonly string _file;

        public MovieFinderJson()
        {
        }

        public IEnumerable<IMovie> FindAll()
        {
            return GetMoviesFromJSON();
        }

        public IEnumerable<IMovie> FindByDirector(string director)
        {
            return GetMoviesFromJSON().Where(movie => movie.Director == director).ToList(); ;
        }

        private IEnumerable<IMovie> GetMoviesFromJSON()
        {
            var list = new List<IMovie>() {
                new Movie { Name = "Movie1-Json", Director = "Director1", Length = 10},
                new Movie { Name = "Movie2-Json", Director = "Director2", Length = 20},
                new Movie { Name = "Movie3-Json", Director = "Director3", Length = 30},
            };

            return list;
            /*using (var fs = new FileStream(_file, FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                var content = sr.ReadToEnd();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
                return movies;
            }*/
        }
    }
}
