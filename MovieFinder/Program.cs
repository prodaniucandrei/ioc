using CustomIoCContainer;
using System;

namespace MovieFinder
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = ResourceProvider.GetConfig("config");
            IoCContainer.Config(config, typeof(Program).Assembly);

            var obj = IoCContainer.Resolve<MovieFinder.IMovieLister>();

            obj.ListMoviesFromDirector("Director1");

            Console.ReadKey();
        }
    }
}
