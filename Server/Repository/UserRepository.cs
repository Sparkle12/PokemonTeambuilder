using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Reflection.Metadata.Ecma335;

namespace Server.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly RoleRepository _roleRep;

        public UserRepository (GameDbContext context, RoleRepository roleRep) : base (context) { _roleRep = roleRep; }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _table.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public User FindByUsername(string username)
        {
            
            return _table.Include(u => u.Role).FirstOrDefault(u => u.Username == username);
        }

        public override User FindById(int id) 
        {
            return _table.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }

        public override async Task<User> FindByIdAsync(int id)
        {
            return await _table.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public int UpdateRoleById(int roleId,int id) 
        {
            var user = FindById(id);

            if (user != null) 
            { 
                var role = _roleRep.FindById(roleId);
                if (role != null) 
                {
                    
                    user.Role = role;
                    Update(user);

                    return 0; ;
                }

                return 1;
                
            }

            return 2;
        }
    }
}
