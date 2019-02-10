using RestSharp;

namespace PokemonLookupAPI.DataGenerator.HTTP
{
    public interface IRestRequestFactory
    {
        IRestRequest Create();
        IRestRequest Create(string resource);
    }
}