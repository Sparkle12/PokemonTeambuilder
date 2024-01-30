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
    public class MoveController : ControllerBase
    {
        private readonly MoveRepository _moveRep;
        private readonly RoleRepository _roleRep;
        private readonly TypeRepository _typeRep;

        public MoveController(MoveRepository moveRepo, RoleRepository roleRepo, TypeRepository typeRep)
        {
            _moveRep = moveRepo; _roleRep = roleRepo; _typeRep = typeRep;
        }


        [HttpGet("{id}")]

        public async Task<MoveDTO> GetById([FromRoute] int id) 
        {
            return new MoveDTO(_moveRep.FindById(id));
        }

        [HttpGet("name/{name}")]

        public async Task<MoveDTO> GetByName([FromRoute] string name) 
        {
            return new MoveDTO(_moveRep.FindByName(name));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMove(CreateMoveRequest movereq) 
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if(_moveRep.FindByName(movereq.Name) != null) { return BadRequest("Move already exists"); }

            if(_typeRep.FindById(movereq.type) == null) { return BadRequest("Move type dose not exist");}

            PType type = _typeRep.FindById(movereq.type);
            Console.WriteLine(type.Name);

            Move move = new Move();
            move.Type = type;
            move.Name = movereq.Name;
            move.Power = movereq.Power;
            move.Accuracy = movereq.Accuracy;
            move.AttackType = movereq.AttackType;


            _moveRep.Create(move);
            _moveRep.Save();
            

            return Ok("Move has been created");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateMove(CreateMoveRequest movereq) 
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if(_moveRep.FindByName(movereq.Name) != null)
            {
                return await CreateMove(movereq);
            }

            if (_typeRep.FindById(movereq.type) == null) { return BadRequest("Move type dose not exist"); }

            PType type = _typeRep.FindById(movereq.type);

            
            Move move = _moveRep.FindByName(movereq.Name);
            move.Type = type;
            move.Name = movereq.Name;
            move.Power = movereq.Power;
            move.Accuracy = movereq.Accuracy;
            move.AttackType = movereq.AttackType;

            _moveRep.Update(move);
            _moveRep.Save();

            return Ok("Move has been updated");
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteMoveById([FromRoute] int id) 
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if(_moveRep.FindById(id) == null) { return BadRequest("Move dosen't exist"); }

            Move move = _moveRep.FindById(id);
            _moveRep.Delete(move);
            _moveRep.Save();

            return Ok("Move has been deleted");
        }
     }
}
