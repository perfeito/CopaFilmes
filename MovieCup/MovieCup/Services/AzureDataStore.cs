using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using MovieCup.Models;

namespace MovieCup.Services
{
    public class AzureDataStore : IDataStore<Movie>
    {
        HttpClient client;
        IEnumerable<Movie> items;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Movie>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Movie>> GetItemsAsync()
        {
            if (IsConnected)
            {
                var response = await client.GetAsync($"api/movielist");                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Movie>>(await response.Content.ReadAsStringAsync());
                }
                return new List<Movie>();
            }

            return items;
        }

        public async Task<IEnumerable<Movie>> CheckOutWinnerAsync(List<Movie> items)
        {
            if (IsConnected)
            {
                var serializedItem = JsonConvert.SerializeObject(items);

                var response = await client.PostAsync($"api/winner", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Movie>>(await response.Content.ReadAsStringAsync());
                }
                return new List<Movie>();
            }

            return items;
        }
    }
}
