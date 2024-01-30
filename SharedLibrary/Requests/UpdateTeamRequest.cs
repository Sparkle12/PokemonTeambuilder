using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Requests
{
    public class UpdateTeamRequest
    {
        public int UserId { get; set; }
        public List<int> PokeId { get; set; }
    }
}
