using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonLookupAPI.Services;
using PokemonLookupAPI.Models.Core;

namespace PokemonLookupAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private IPokemonService PokemonService { get; }

        public PokemonController(IPokemonService pokemonService)
        {
            PokemonService = pokemonService;
        }
        /// <summary>
        /// Get all pokemon names.
        /// </summary>
        [HttpGet]
        public ActionResult<List<string>> GetPokemonIndex()
        {
            return PokemonService.GetPokemon().OrderBy(p => p.Order).Select(p => p.Name).ToList();
        }

        /// <summary>
        /// Get all pokemon with given type.
        /// </summary>
        /// <param name="type">The type name e.g. Fighting</param>
        [HttpGet("ByType/{type}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(List<Pokemon>), 200)]
        public ActionResult<List<Pokemon>> GetPokemonByType(string type)
        {
            var matches = PokemonService.GetPokemon().Where(p => p.Types.Select(t => t.Type.Name.ToLower()).Contains(type.ToLower()));
            if (matches.Any())
            {
                return matches.OrderBy(p => p.Order).ToList();
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a pokemon by name.
        /// </summary>
        /// <param name="name">The pokemon's name e.g. Pikachu</param>
        [HttpGet("{name}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Pokemon), 200)]
        public ActionResult<Pokemon> GetSpecificPokemon(string name)
        {
            var matches = PokemonService.GetPokemon().Where(p => p.Name.ToLower() == name.ToLower());
            if (matches.Any())
            {
                return matches.Single();
            }
            else
            {
                return NotFound();
            }
        }
    }
}