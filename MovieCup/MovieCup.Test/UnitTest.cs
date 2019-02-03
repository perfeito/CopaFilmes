using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieCup.Models;
using MovieCup.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieCup.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task TestGetMovies()
        {
            var request = TestHelpers.CreateGetRequest();            
            var actionResult = await Function.Function.RunGet(request);
            var badRequestResult = actionResult as BadRequestResult;
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var serializedItem = JsonConvert.DeserializeObject<List<Movie>>(okObjectResult.Value.ToString());
            Assert.IsNotNull(serializedItem);
            Assert.AreEqual(serializedItem.Count, 16);
        }

        [TestMethod]
        public async Task TestWinner()
        {
            var items = new MockDataStore().movies;
            items.RemoveRange(8, 8);
            var request = TestHelpers.CreatePostRequest(items);
            var actionResult = await Function.Function.RunPost(request);
            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var serializedItem = JsonConvert.DeserializeObject<List<Movie>>(okObjectResult.Value.ToString());
            Assert.IsNotNull(serializedItem);
            Assert.AreEqual(serializedItem.First().Id, "tt4154756");
            Assert.AreEqual(serializedItem.Last().Id, "tt3606756");
        }
    }
}
