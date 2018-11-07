using System.Collections.Generic;

namespace MovieFinder
{
    public interface IMovieFinder
    {
        IEnumerable<IMovie> FindByDirector(string director);
        IEnumerable<IMovie> FindAll();
    }
}