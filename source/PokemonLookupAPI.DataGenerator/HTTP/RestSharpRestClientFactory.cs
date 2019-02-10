using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonLookupAPI.DataGenerator.HTTP
{
    public class RestSharpRestClientFactory : IRestClientFactory
    {
        public IRestClient Create()
        {
            return new RestClient();
        }
    }
}
