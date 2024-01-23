using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Xml.Linq;

namespace Server.Repository
{
    public class TypeRepository : GenericRepository<PType>
    {
        public TypeRepository(GameDbContext context) : base(context) { }

        public PType FindByName(string name)
        {
            return _table.FirstOrDefault(t => t.Name == name);
        }

        public async Task<PType> FindByNameAsync(string name)
        {
            return await _table.FirstOrDefaultAsync(t => t.Name == name);
        }

        public override PType FindById(int id)
        {
            return _table.FirstOrDefault(t => t.Id == id);
        }

        public override async Task<PType> FindByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(t => t.Id == id);
        }


    }
}
