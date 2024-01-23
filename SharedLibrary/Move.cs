using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public class Move : BaseClass
    {
       
        public PType Type { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }

        public bool AttackType { get; set; } // False physical , True special

        public List<Pokemon>? pokemons { get; set; }
    }
}
