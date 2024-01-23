using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server.Repository
{
    public class MoveRepository : GenericRepository<Move>
    {
        public MoveRepository(GameDbContext context):base(context) { }

        public override Move FindById(int id)
        {
            return _table.Include(m => m.Type).FirstOrDefault(m => m.Id == id);
        }

        public override async Task<Move> FindByIdAsync(int id)
        {
            return await _table.Include(m =>m.Type).FirstOrDefaultAsync(m => m.Id == id);
        }

        public Move FindByName(string name) 
        {
            return _table.Include(m => m.Type).FirstOrDefault(m => m.Name == name);
        }

        public async Task<Move> FindByNameAsync(string name) 
        {
            return await _table.Include(m => m.Type).FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
