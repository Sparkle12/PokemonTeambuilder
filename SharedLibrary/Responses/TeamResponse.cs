using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Responses
{
    public class TeamResponse
    {
        public TeamResponse(Team team,string msg) 
        {
            Team = new TeamDTO(team);
            Message = msg;
        }
        public TeamDTO Team { get; set; }
        public string Message { get; set; }
    }
}
