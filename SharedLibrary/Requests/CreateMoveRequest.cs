using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Requests
{
    public class CreateMoveRequest
    {
        public int type {  get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public bool AttackType { get; set; }
    }
}
