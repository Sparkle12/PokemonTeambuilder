using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary
{
    public class Pokemon : BaseClass
    {
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int SpAttack { get; set; }
        public int Defence { get; set; }
        public int SpDefence { get; set; }
        public int Speed { get; set; }
        
        public List<PType> Types { get; set; }
        public List<Move> Learnable {  get; set; }
        
        [NotMapped]
        public List<Move> learntMoves { get; set; }




    }
}
