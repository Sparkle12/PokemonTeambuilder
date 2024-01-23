using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server.Repository
{
    public class RoleRepository : GenericRepository<Role>
    {

        public RoleRepository(GameDbContext context) : base(context)
        { 
        }
        

        public async Task<Role> FindByName (string name)
        {
            return await _table.FirstOrDefaultAsync(r => r.Name == name);
        }

        public bool CanBan(int id)
        {
            return FindById(id).BanPermission;
        }

        public bool CanCreateRole(int id)
        {
            return FindById(id).CreateRolePermission;
        }

        public bool IsPremiumMember(int id) 
        {
            return FindById(id).PremiumPermission;
        }


    }
}
