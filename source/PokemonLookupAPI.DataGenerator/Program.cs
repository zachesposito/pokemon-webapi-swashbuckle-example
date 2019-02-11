using System;
using System.IO;
using System.Linq;

namespace PokemonLookupAPI.DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var cachePath = @"C:\Users\zesposito\source\repos\learning\pokemon-lookup-api\data\pokeapi-cache";

            Console.WriteLine("** Starting data generator.");
            //if pokeapi cache doesn't exist, get it and cache
            if (Directory.Exists(cachePath) && Directory.EnumerateFiles(cachePath, "*.json").Any())
            {
                Console.WriteLine("** Data already exists.");
            }
            else
            {
                new PokeAPIClient("https://pokeapi.co/api/v2/").CacheAllPokemon(cachePath).GetAwaiter().GetResult();
            }

            Console.WriteLine("** Done generating data.");
            Console.ReadLine();
        }
    }
}
