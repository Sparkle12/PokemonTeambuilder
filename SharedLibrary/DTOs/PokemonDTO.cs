using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.DTOs
{
    public class PokemonDTO
    {
        public PokemonDTO(Pokemon poke) 
        {
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
        }
        public string Name { get; set; }
        public int Hp {  get; set; }
        public int Attack { get; set; }
        public int SpAttack { get; set; }
        public int Defence { get; set; }
        public int SpDefence { get; set; }
        public int Speed { get; set; }
        public List<MoveDTO> Learnable {  get; set; }

    }
}
