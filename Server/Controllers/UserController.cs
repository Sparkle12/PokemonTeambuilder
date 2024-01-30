using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using SharedLibrary;
using SharedLibrary.Models.DTOs;
using SharedLibrary.Requests;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRep;
        private readonly RoleRepository _roleRep;
        private readonly TeamRepository _teamRep;
        private readonly PokemonRepository _pokemonRep;

        public UserController(UserRepository userRep, RoleRepository roleRep,TeamRepository teamRep, PokemonRepository pokeRep) 
        { 
            _userRep = userRep; 
            _roleRep = roleRep;
            _teamRep = teamRep;
            _pokemonRep = pokeRep;
        }


        [HttpGet("{id}")]
        public async Task<UserDTO> GetUserById([FromRoute] int id) 
        {
            var user = _userRep.FindById(id);
            if (user == null) { return null; }
            return new UserDTO(user);
        }

        [HttpGet("username/{username}")]
        public async Task<UserDTO> GetUserByUsername([FromRoute] string username)
        {
            var user =  _userRep.FindByUsername(username);
            if(user == null) { return null; }
            return new UserDTO(user);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUserRoleById(GiveRoleRequest req)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var userroleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(userroleId)) { return BadRequest("You dont have permission for that"); }


            }

            if (_roleRep.FindById(req.roleId) == null) { return BadRequest("Role dosen't exist"); }

            if (_userRep.FindById(req.userId) == null) { return BadRequest("User dosen't exist"); }

            var retCode = _userRep.UpdateRoleById(req.roleId, req.userId);
            _userRep.Save();

            return Ok();
        }

        [HttpPost("update/team")]
        public async Task<IActionResult> UpdateTeamId([FromBody] UpdateTeamRequest req)
        {
            var user = _userRep.FindById(req.UserId);
            if(user == null) { return BadRequest("User dose not exist"); }

            req.PokeId.Sort();
            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (int index in req.PokeId)
            {
                var poke = _pokemonRep.FindById(index);
                if (poke == null)
                    return BadRequest("Pokemon dose not exist");
                pokemons.Add(poke);
            }

            var team = _teamRep.FindByPokemons(pokemons);

            if (team == null) { return BadRequest("Team dose not exist"); }

            user.TeamId = team.Id;

            _userRep.Update(user);
            _userRep.Save();

            return Ok("Team id has been updated");

        }
    }
}
