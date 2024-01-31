using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Responses
{

    public class UpdateTeamResponse
    {
        public UpdateTeamResponse() { }
        public UpdateTeamResponse(int t,string m) 
        {
            TeamId = t;
            Message = m;
        }
        public int TeamId {  get; set; }
        public string Message { get; set; }
    }
}
