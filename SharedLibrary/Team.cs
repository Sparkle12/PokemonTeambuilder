using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary
{
    public class Team : BaseClass
    {
        public Team() { }
        public Team(List<Pokemon> pokemons) 
        {
            Pokemons = pokemons;
        }
        public List<Pokemon> Pokemons {  get; set; }
        [NotMapped]
        public int PokemonOut {  get; set; }
    }
}
