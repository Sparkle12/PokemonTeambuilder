using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.DTOs
{
    public class MoveDTO
    {
        public MoveDTO() { }
        public MoveDTO(Move move)
        {
            Type = move.Type.Name;
            Name = move.Name;
            Power = move.Power;
            Accuracy = move.Accuracy;
            AttackType = move.AttackType;
        }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public bool AttackType { get; set; }
    }
}
