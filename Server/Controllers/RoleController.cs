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
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRep;

        public RoleController(RoleRepository roleRep) 
        {
            _roleRep = roleRep;
        }

        [HttpGet("{id}")]
        public async Task<Role> GetRole([FromRoute] int id)
        {
            return _roleRep.FindById(id);
        }

        [HttpGet("can_ban/{id}")]
        public bool CanBan([FromRoute] int id)
        {
            return _roleRep.CanBan(id);
        }

        [HttpGet("premium/{id}")]
        public bool IsPremium(int id) 
        {
            return _roleRep.IsPremiumMember(id);
        }

        [HttpPost("create")]
        public  async Task<IActionResult> CreateRole(Role role)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if (_roleRep.FindByName(role.Name) != null) { return BadRequest("Role already exists"); }

            _roleRep.Create(role);
            _roleRep.Save();

            return Ok("Role has been created");
        }

        [HttpPost("update")]

        public async Task<IActionResult> UpdateRole(Role role) 
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var roleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(roleId)) { return BadRequest("You dont have permission for that"); }
            }

            if (_roleRep.FindById(role.Id) == null)
            {
                return await CreateRole(role);
            }
            else
            {
                _roleRep.Update(role);
                _roleRep.Save();
                return Ok("Role has been updated");
            }

        }
    }
}
