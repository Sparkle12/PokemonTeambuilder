using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using SharedLibrary;
using SharedLibrary.DTOs;
using SharedLibrary.Requests;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PokemonController: ControllerBase
    {
        private readonly RoleRepository _roleRep;
        private readonly PokemonRepository _pokemonRep;
        private readonly TypeRepository _typeRep;
        private readonly MoveRepository _moveRep;

        public PokemonController(RoleRepository roleRep, PokemonRepository pokemonRep, TypeRepository typeRep, MoveRepository moveRep)
        {
            _roleRep = roleRep;
            _pokemonRep = pokemonRep;
            _typeRep = typeRep;
            _moveRep = moveRep;
        }
        [HttpGet("all")]
        public async Task<List<PokemonDTO>> GetAll()
        {
            var all = await _pokemonRep.GetAll();

            List<PokemonDTO> pokes = new List<PokemonDTO>();

            foreach (var pokemon in all) 
            {
                pokes.Add(new PokemonDTO(pokemon));
            }

            return pokes;
        }
        [HttpGet("{id}")]
        public async Task<PokemonDTO> GetById([FromRoute] int id) 
        {
            return new PokemonDTO(_pokemonRep.FindById(id));
        }

        [HttpGet("name/{name}")]
        public async Task<PokemonDTO> GetByName([FromRoute] string name) 
        {
            return new PokemonDTO(_pokemonRep.FindByName(name));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePokemon([FromBody] CreatePokemonRequest pokereq)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if(_pokemonRep.FindByName(pokereq.Name) != null) { return BadRequest("Pokemon already exists"); }

            var types = new List<PType>();
            foreach (int index in pokereq.Types)
            {
                var type = _typeRep.FindById(index);
                if (type == null) { return BadRequest("Type dosen't exist"); }
                else
                    types.Add(type);
            }

            var moves = new List<Move>();
            foreach (int index in pokereq.Learnable)
            {
                var move = _moveRep.FindById(index);
                if (move == null) { Console.WriteLine(index); return BadRequest("Move dosen't exist"); }
                else
                    moves.Add(move);
            }

            var pokemon = new Pokemon();
            pokemon.Types = types;
            pokemon.Learnable = moves;
            pokemon.Name = pokereq.Name;
            pokemon.Hp = (int)Math.Floor(0.02 * pokereq.Hp * 100 + 110);
            pokemon.Attack = (int)Math.Floor(0.02 * pokereq.Attack * 100 + 5);
            pokemon.SpAttack = (int)Math.Floor(0.02 * pokereq.SpAttack * 100 + 5);
            pokemon.Defence = (int)Math.Floor(0.02 * pokereq.Defence * 100 + 5);
            pokemon.SpDefence = (int)Math.Floor(0.02 * pokereq.SpDefence * 100 + 5);
            pokemon.Speed = (int)Math.Floor(0.02 * pokereq.Speed * 100 + 5);

            _pokemonRep.Create(pokemon);
            _pokemonRep.Save();

            return Ok("Pokemon has been created");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePokemon([FromBody] CreatePokemonRequest pokereq)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if (_pokemonRep.FindByName(pokereq.Name) == null) { return await CreatePokemon(pokereq); }

            var types = new List<PType>();
            foreach (int index in pokereq.Types)
            {
                var type = _typeRep.FindById(index);
                if (type == null) { return BadRequest("Type dosen't exist"); }
                else
                    types.Add(type);
            }

            var moves = new List<Move>();
            foreach (int index in pokereq.Learnable)
            {
                var move = _moveRep.FindById(index);
                if (move == null) { return BadRequest("Move dosen't exist"); }
                else
                    moves.Add(move);
            }

            var pokemon = _pokemonRep.FindByName(pokereq.Name);
            pokemon.Types = types;
            pokemon.Learnable = moves;
            pokemon.Name = pokereq.Name;
            pokemon.Hp = (int)Math.Floor(0.02 * pokereq.Hp*100+110);
            pokemon.Attack = (int)Math.Floor(0.02 * pokereq.Attack * 100 + 5);
            pokemon.SpAttack = (int)Math.Floor(0.02 * pokereq.SpAttack * 100 + 5);
            pokemon.Defence = (int)Math.Floor(0.02 * pokereq.Defence * 100 + 5);
            pokemon.SpDefence = (int)Math.Floor(0.02 * pokereq.SpDefence * 100 + 5);
            pokemon.Speed = (int)Math.Floor(0.02 * pokereq.Speed * 100 + 5);

            _pokemonRep.Update(pokemon);
            _pokemonRep.Save();

            return Ok("Pokemon has been updated");
        }
    }
}
