using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieCup.Models;

namespace MovieCup.Services
{
    public class MockDataStore : IDataStore<Movie>
    {
        public List<Movie> movies;

        public MockDataStore()
        {
            movies = new List<Movie>();

            var mockItems = new List<Movie>
            {
                new Movie { Id = "tt3606756", Title = "Os Incríveis 2", Year=2018, Rating=8.5f },
                new Movie { Id = "tt4881806", Title = "Jurassic World: Reino Ameaçado", Year=2018, Rating=6.7f },
                new Movie { Id = "tt5164214", Title = "Oito Mulheres e um Segredo", Year=2018, Rating=6.3f },
                new Movie { Id = "tt7784604", Title = "Hereditário", Year=2018, Rating=7.8f },
                new Movie { Id = "tt4154756", Title = "Vingadores: Guerra Infinita", Year=2018, Rating=8.8f },
                new Movie { Id = "tt5463162", Title = "Deadpool 2", Year=2018, Rating=8.1f },
                new Movie { Id = "tt3778644", Title = "Han Solo: Uma História Star Wars", Year=2018, Rating=7.2f },
                new Movie { Id = "tt3501632", Title = "Thor: Ragnarok", Year=2017, Rating=7.9f },
                new Movie { Id = "tt2854926", Title = "Te Peguei!", Year=2018, Rating=7.1f },
                new Movie { Id = "tt0317705", Title = "Os Incríveis", Year=2004, Rating=8.0f },
                new Movie { Id = "tt3799232", Title = "A Barraca do Beijo", Year=2018, Rating=6.4f },
                new Movie { Id = "tt1365519", Title = "Tomb Raider: A Origem", Year=2018, Rating=6.5f },
                new Movie { Id = "tt1825683", Title = "Pantera Negra", Year=2018, Rating=7.5f },
                new Movie { Id = "tt5834262", Title = "Hotel Artemis", Year=2018, Rating=6.3f },
                new Movie { Id = "tt7690670", Title = "Superfly", Year=2018, Rating=5.1f },
                new Movie { Id = "tt6499752", Title = "Upgrade", Year=2018, Rating=7.8f },
            };

            foreach (var item in mockItems)
            {
                movies.Add(item);
            }
        }

        async Task<Movie> Verify(IEnumerable<Movie> tuple)
        {
            if (tuple.First().Rating == tuple.Last().Rating)
                return tuple.OrderBy(x => x.Title).First();
            return tuple.First().Rating > tuple.Last().Rating ? tuple.First() : tuple.Last();
        }

        public async Task<IEnumerable<Movie>> CheckOutWinnerAsync(List<Movie> items)
        {
            items.OrderBy(x => x.Title);
            var tempWinners = new List<Movie>();
            var tempWinners2 = new List<Movie>();
            for (int i = 0; i < 4; i++)
            {
                var temp = new List<Movie>() { items.First(), items.Last() };
                tempWinners.Add(await Verify(temp));
                items.Remove(items.First());
                items.Remove(items.Last());
            }
            for (int i = 0; i < 2; i++)
            {
                var temp = new List<Movie>() { tempWinners.First(), tempWinners.Last() };
                tempWinners2.Add(await Verify(temp));
                tempWinners.Remove(tempWinners.First());
                tempWinners.Remove(tempWinners.Last());
            }

            return tempWinners.First().Rating.Equals(tempWinners.Last().Rating) ? tempWinners.OrderBy(x => x.Title) : tempWinners.OrderBy(x => x.Rating);
        }

        async Task<IEnumerable<Movie>> IDataStore<Movie>.GetItemsAsync()
        {
            return await Task.FromResult(movies);
        }
    }
}