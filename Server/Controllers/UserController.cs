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
        private readonly UserRepository _userRepo;
        private readonly RoleRepository _roleRep;

        public UserController(UserRepository userRepo, RoleRepository roleRep) { _userRepo = userRepo; _roleRep = roleRep; }


        [HttpGet("{id}")]
        public UserDTO GetUserById([FromRoute] int id) 
        {
            var user = _userRepo.FindById(id);
            if (user == null) { return null; }
            var dto = new UserDTO(user);
            return dto;
        }

        [HttpGet("username/{username}")]
        public UserDTO GetUserByUsername([FromRoute] string username)
        {
            var user =  _userRepo.FindByUsername(username);
            if(user == null) { return null; }
            var dto = new UserDTO(user);
            return dto;
        }

        [HttpPost("update")]
        public IActionResult UpdateUserRoleById(GiveRoleRequest req)
        {
            var currentUser = HttpContext.User;
            var currentUserRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(currentUserRole))
            {
                var userroleId = int.Parse(currentUserRole);
                if (!_roleRep.CanCreateRole(userroleId)) { return BadRequest("You dont have permission for that"); }


            }

            if (_roleRep.FindById(req.roleId) == null) { return BadRequest("Role dosen't exist"); }

            if (_userRepo.FindById(req.userId) == null) { return BadRequest("User dosen't exist"); }

            var retCode = _userRepo.UpdateRoleById(req.roleId, req.userId);
            _userRepo.Save();

            return Ok();
        }
    }
}
