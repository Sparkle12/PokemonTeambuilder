using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.DTOs
{
    public class PokemonDTO
    {
        public PokemonDTO() { }
        public PokemonDTO(Pokemon poke) 
        {
            Id = poke.Id;
            Name = poke.Name;
            Hp = poke.Hp;
            Attack = poke.Attack;
            SpAttack = poke.SpAttack;
            Defence = poke.Defence;
            SpDefence = poke.SpDefence;
            Speed = poke.Speed;

            Learnable = new List<MoveDTO>();

            foreach (Move m in poke.Learnable)
            {
                Learnable.Add(new MoveDTO(m));
            }

            Types = new List<string>();

            foreach(PType t in poke.Types) 
            {
                Types.Add(t.Name);
            }

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp {  get; set; }
        public int Attack { get; set; }
        public int SpAttack { get; set; }
        public int Defence { get; set; }
        public int SpDefence { get; set; }
        public int Speed { get; set; }
        public List<MoveDTO> Learnable {  get; set; }
        public List<string> Types { get; set; }

    }
}
