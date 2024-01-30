using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.DTOs
{
    public class TeamDTO
    {
        public TeamDTO(Team t) 
        {
            Id = t.Id;

            Pokemons = new List<PokemonDTO>();

            foreach(Pokemon p in t.Pokemons)
            {
                Console.WriteLine(p.Name);
                Pokemons.Add(new PokemonDTO(p));
            }
        }
        public int Id { get; set; }
        public List<PokemonDTO> Pokemons {  get; set; }
    }
}
