using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Repository;
using SharedLibrary;
using System.Security.Claims;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TypeController : ControllerBase
    {

        private readonly TypeRepository _typeRep;
        private readonly RoleRepository _roleRep;

        public TypeController(TypeRepository type, RoleRepository role) { _typeRep = type; _roleRep = role; }

        [HttpGet("{id}")]

        public PType GetTypeById([FromRoute] int id) 
        {
            return _typeRep.FindById(id);
        }

        [HttpGet("name/{name}")]
        
        public PType GetTypeByName([FromRoute] string name) 
        {
            return _typeRep.FindByName(name);
        }

        [HttpPost("create")]

        public IActionResult CreateType(PType type)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if (_typeRep.FindByName(type.Name) != null) { return BadRequest("Type allready exists consider update instead"); }



            _typeRep.Create(type);
            _typeRep.Save();

            return Ok("Type has been created");
        }

        [HttpPost("update")]

        public IActionResult UpdateType(PType type) 
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if (_typeRep.FindByName(type.Name) == null) 
            {
                return CreateType(type);
            }

            _typeRep.Update(type);
            _typeRep.Save();

            return Ok("Type has been updated");
        }

        [HttpPost("delete/{id}")]
        public IActionResult DeleteTypeById([FromRoute] int id)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if(_typeRep.FindById(id) == null) { return BadRequest("Type dose not exist"); }

            PType type = _typeRep.FindById(id);
            _typeRep.Delete(type);
            _typeRep.Save();

            return Ok("Type deleted succesfully");
        }
    }
}
