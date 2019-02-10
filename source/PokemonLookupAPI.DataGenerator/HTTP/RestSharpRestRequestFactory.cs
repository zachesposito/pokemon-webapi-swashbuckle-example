using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonLookupAPI.DataGenerator.HTTP
{
    public class RestSharpRestRequestFactory : IRestRequestFactory
    {
        public IRestRequest Create()
        {
            return new RestSharp.Serializers.Newtonsoft.Json.RestRequest(); //default to JSON.NET for serialization
        }

        public IRestRequest Create(string resource)
        {
            return new RestSharp.Serializers.Newtonsoft.Json.RestRequest(resource);
        }
    }
}
