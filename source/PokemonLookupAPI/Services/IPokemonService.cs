using System.Collections.Generic;
using PokemonLookupAPI.Models.Core;

namespace PokemonLookupAPI.Services
{
    public interface IPokemonService
    {
        IEnumerable<Pokemon> GetPokemon();
    }
}