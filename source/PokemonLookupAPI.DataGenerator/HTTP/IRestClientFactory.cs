using RestSharp;

namespace PokemonLookupAPI.DataGenerator.HTTP
{
    public interface IRestClientFactory
    {
        IRestClient Create();
    }
}