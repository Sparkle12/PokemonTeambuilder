using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Requests
{
    public class CreatePokemonRequest
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int SpAttack { get; set; }
        public int Defence { get; set; }
        public int SpDefence { get; set; }
        public int Speed { get; set; }
        public List<int> Types { get; set; }
        public List<int> Learnable {  get; set; }
    }
}
