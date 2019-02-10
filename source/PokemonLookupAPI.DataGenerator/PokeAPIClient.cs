using PokemonLookupAPI.DataGenerator.HTTP;
using PokemonLookupAPI.DataGenerator.Models;
using RateLimiter;
using RestSharp;
using RestSharp.Serializers.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLookupAPI.DataGenerator
{
    public class PokeAPIClient
    {
        private string BaseURL { get; }
        private IRestClientFactory RestClientFactory { get; }
        private IRestRequestFactory RestRequestFactory { get; }
        private TimeLimiter Limiter { get; }

        public PokeAPIClient(string baseURL, IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory)
        {
            BaseURL = baseURL;
            RestClientFactory = restClientFactory;
            RestRequestFactory = restRequestFactory;
            Limiter = InitializeLimiter();
        }

        public PokeAPIClient(string baseURL)
        {
            BaseURL = baseURL;
            RestClientFactory = new RestSharpRestClientFactory();
            RestRequestFactory = new RestSharpRestRequestFactory();
            Limiter = InitializeLimiter();
        }

        private TimeLimiter InitializeLimiter()
        {
            return TimeLimiter.GetFromMaxCountByInterval(98, TimeSpan.FromMinutes(1));
        }

        public async Task CacheAllPokemon(string cachePath)
        {
            //get resource list
            var resourceList = await GetResourceList("pokemon");
            //retrieve all resources
            //Get all the responses as straight strings and combine into one big json array. Remember to rate limit
            var pokemon = await GetCombinedResourceResponses(resourceList.Results.Select(r => r.Url.AbsoluteUri.Replace(BaseURL, string.Empty)));
            Console.WriteLine($"** Got all pokemon data. Saving to cache.");
            Directory.CreateDirectory(cachePath);
            File.WriteAllText(Path.Combine(cachePath, "pokemon.json"), pokemon);
            Console.WriteLine($"** Pokemon data saved to cache.");
        }  

        private async Task<ResourceListResponse> GetResourceList(string resourceName)
        {
            var request = RestRequestFactory.Create(resourceName);
            //call twice to get full list
            var firstResponse = await Execute<ResourceListResponse>(request);
            var total = firstResponse.Count.ToString();
            Console.WriteLine($"** Getting {total} {resourceName} resources.");
            return await Execute<ResourceListResponse>(request.AddQueryParameter("limit", total));
        }

        private async Task<string> GetCombinedResourceResponses(IEnumerable<string> resources)
        {
            var complete = 0;
            var tasks = resources.Select(async r => {
                var json = await GetRawJSON(new RestSharp.RestRequest(r));
                complete++;
                Console.WriteLine($"** Got {complete}/{resources.Count()} resource responses.");
                return json;
            });
            var jsonResponses = await Task.WhenAll(tasks);
            
            return "[" + jsonResponses.Aggregate((built, next) => $"{built}, {next}") + "]";
        }

        private async Task<string> GetRawJSON(IRestRequest request)
        {
            var client = RestClientFactory.Create();
            client.BaseUrl = new Uri(BaseURL);
            var response = await Limiter.Perform(() => client.Execute(request));

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error during API request, see inner exception.", response.ErrorException);
            }

            return response.Content;
        }

        private async Task<T> Execute<T>(IRestRequest request) where T : new()
        {
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            var client = RestClientFactory.Create();
            client.BaseUrl = new Uri(BaseURL);
            var response = await Limiter.Perform(() => client.Execute<T>(request));

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Error during API request, see inner exception.", response.ErrorException);
            }

            return response.Data;
        }      
    }
}
