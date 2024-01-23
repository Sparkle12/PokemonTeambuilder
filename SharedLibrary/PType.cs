using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public class PType : BaseClass
    {
        public string Name { get; set; }
        public List<Pokemon>? Pokes { get; set; } 
    }
}
