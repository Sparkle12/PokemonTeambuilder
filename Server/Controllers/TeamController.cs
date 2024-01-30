using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using SharedLibrary;
using SharedLibrary.DTOs;
using SharedLibrary.Requests;
using SharedLibrary.Responses;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamRepository _teamRep;
        private readonly RoleRepository _roleRep;
        private readonly PokemonRepository _pokemonRep;
        public TeamController(TeamRepository teamRepo,RoleRepository roleRep, PokemonRepository pokeRep) 
        {
            _teamRep = teamRepo;
            _roleRep = roleRep;
            _pokemonRep = pokeRep;
        }

        [HttpGet("{id}")]
        public async Task<TeamDTO> GetById(int id)
        {
            return new TeamDTO(_teamRep.FindById(id));
        }

        [HttpGet]
        public async Task<TeamResponse> GetByPokemons([FromBody] TeamRequest req)
        {
            req.Pokemons.Sort();
            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (int index in req.Pokemons)
            {
                var poke = _pokemonRep.FindById(index);
                if (poke == null)
                    return new TeamResponse(new Team(),"Pokemon dose not exist");
                pokemons.Add(poke);
            }

            var team = _teamRep.FindByPokemons(pokemons);
            if (team == null)
                return new TeamResponse(new Team(), "Team dose not exist");
            return new TeamResponse(team,"Succes");
        }

        [HttpPost("create")]
        public async  Task<IActionResult> CreateTeam(TeamRequest teamreq) 
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var userroleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(userroleId)) { return BadRequest("You dont have permission for that"); }


            }
            teamreq.Pokemons.Sort();
            List<Pokemon> pokemons = new List<Pokemon>();
            
            foreach(int index in teamreq.Pokemons)
            {
                var poke = _pokemonRep.FindById(index);
                if(poke == null)
                    return BadRequest("Pokemon dose not exist");
                pokemons.Add(poke);
            }

            if (_teamRep.FindByPokemons(pokemons) != null)
                return BadRequest("Team already exists");

            _teamRep.Create(new Team(pokemons));
            _teamRep.Save();

            return Ok("Team has been created");
        }
    }
}
