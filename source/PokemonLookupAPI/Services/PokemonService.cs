using PokemonLookupAPI.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonLookupAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private IEnumerable<Pokemon> Pokemon { get; set; }

        public PokemonService()
        {
            Pokemon = LoadPokemon();
        }

        private IEnumerable<Pokemon> LoadPokemon()
        {
            var json = System.IO.File.ReadAllText("pokemon.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Pokemon>>(json);
        }

        public IEnumerable<Pokemon> GetPokemon() => Pokemon;
    }
}
