using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using MovieCup.Models;

namespace MovieCup.Function
{
    public static class Function
    {
        [FunctionName("MovieList")]
        public static async Task<IActionResult> RunGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestMessage req)
        {
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://copadosfilmes.azurewebsites.net/api/filmes");
                result = await client.GetAsync("");
            }

            return result != null && result.IsSuccessStatusCode
                ? (ActionResult)new OkObjectResult(await result.Content.ReadAsStringAsync())
                : new BadRequestObjectResult("could not get the list of books!");
        }

        [FunctionName("Winner")]
        public static async Task<IActionResult> RunPost(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req)
        {
            var strList = await req.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(strList))
            {
                return new BadRequestObjectResult("no content");
            }
            
            var items = JsonConvert.DeserializeObject<List<Movie>>(strList);

            if (items == null || items.Count != 8)
            {
                return new BadRequestObjectResult("invalid quantity");
            }
            
            items = items.OrderBy(x => x.Title).ToList();
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

            IEnumerable<Movie> Winners = tempWinners2.First().Rating.Equals(tempWinners2.Last().Rating) ? tempWinners2.OrderBy(x => x.Title) : tempWinners2.OrderBy(x => x.Rating);

            return Winners != null && Winners.Count() == 2
                ? (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(Winners.Reverse()))
                : new BadRequestObjectResult("could not get the list of winner books");
        }

        async static Task<Movie> Verify(List<Movie> tuple)
        {
            if (tuple.First().Rating == tuple.Last().Rating)
                return tuple.OrderBy(x => x.Title).First();
            return tuple.First().Rating > tuple.Last().Rating ? tuple.First() : tuple.Last();
        }
    }
}
