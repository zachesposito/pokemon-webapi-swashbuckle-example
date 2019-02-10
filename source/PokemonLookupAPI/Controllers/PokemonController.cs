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

        [HttpGet]
        public ActionResult<List<Pokemon>> GetTopTenPokemon()
        {
            return PokemonService.GetPokemon().Take(10).ToList();
        }

        [HttpGet("{name}")]
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