using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PokemonLookupAPI.Models.Core
{
    public partial class Pokemon
    {
        //[JsonProperty("abilities")]
        //public IEnumerable<Ability> Abilities { get; set; }

        //[JsonProperty("base_experience")]
        //public long BaseExperience { get; set; }

        //[JsonProperty("forms")]
        //public ResourceReference[] Forms { get; set; }

        //[JsonProperty("game_indices")]
        //public IEnumerable<GameIndex> GameIndices { get; set; }

        //[JsonProperty("height")]
        //public long Height { get; set; }

        //[JsonProperty("held_items")]
        //public object[] HeldItems { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        //[JsonProperty("is_default")]
        //public bool IsDefault { get; set; }

        //[JsonProperty("location_area_encounters")]
        //public Uri LocationAreaEncounters { get; set; }

        //[JsonProperty("moves")]
        //public IEnumerable<Move> Moves { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("species")]
        public ResourceReference Species { get; set; }

        //[JsonProperty("sprites")]
        //public Sprites Sprites { get; set; }

        [JsonProperty("stats")]
        public IEnumerable<Stat> Stats { get; set; }

        [JsonProperty("types")]
        public IEnumerable<PokemonType> Types { get; set; }

        //[JsonProperty("weight")]
        //public long Weight { get; set; }
    }

    public partial class Ability
    {
        [JsonProperty("ability")]
        public ResourceReference AbilityReference { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("slot")]
        public long Slot { get; set; }
    }

    public partial class ResourceReference
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("url")]
        //public Uri Url { get; set; }
    }

    public partial class GameIndex
    {
        [JsonProperty("game_index")]
        public long Index { get; set; }

        [JsonProperty("version")]
        public ResourceReference Version { get; set; }
    }

    public partial class Move
    {
        [JsonProperty("move")]
        public ResourceReference MoveReference { get; set; }

        [JsonProperty("version_group_details")]
        public VersionGroupDetail[] VersionGroupDetails { get; set; }
    }

    public partial class VersionGroupDetail
    {
        [JsonProperty("level_learned_at")]
        public long LevelLearnedAt { get; set; }

        [JsonProperty("move_learn_method")]
        public ResourceReference MoveLearnMethod { get; set; }

        [JsonProperty("version_group")]
        public ResourceReference VersionGroup { get; set; }
    }

    public partial class Sprites
    {
        [JsonProperty("back_default")]
        public Uri BackDefault { get; set; }

        [JsonProperty("back_female")]
        public Uri BackFemale { get; set; }

        [JsonProperty("back_shiny")]
        public Uri BackShiny { get; set; }

        [JsonProperty("back_shiny_female")]
        public Uri BackShinyFemale { get; set; }

        [JsonProperty("front_default")]
        public Uri FrontDefault { get; set; }

        [JsonProperty("front_female")]
        public Uri FrontFemale { get; set; }

        [JsonProperty("front_shiny")]
        public Uri FrontShiny { get; set; }

        [JsonProperty("front_shiny_female")]
        public Uri FrontShinyFemale { get; set; }
    }

    public partial class Stat
    {
        [JsonProperty("base_stat")]
        public long BaseStat { get; set; }

        [JsonProperty("effort")]
        public long Effort { get; set; }

        [JsonProperty("stat")]
        public ResourceReference StatReference { get; set; }
    }

    public partial class PokemonType
    {
        [JsonProperty("slot")]
        public long Slot { get; set; }

        [JsonProperty("type")]
        public ResourceReference Type { get; set; }
    }

    public partial class Pokemon
    {
        public static Pokemon FromJson(string json) => JsonConvert.DeserializeObject<Pokemon>(json, JSON.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Pokemon self) => JsonConvert.SerializeObject(self, JSON.Converter.Settings);
    }
    
}
