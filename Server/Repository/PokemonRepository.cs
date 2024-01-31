using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server.Repository
{
    public class PokemonRepository : GenericRepository<Pokemon>
    {
        public PokemonRepository(GameDbContext context): base(context) { }

        public override Task<List<Pokemon>> GetAll()
        {
            return _table.Include(p => p.Types).Include("Learnable.Type").AsNoTracking().ToListAsync();
        }
        public override Pokemon FindById(int id)
        {
            return _table.Include(p => p.Types).Include("Learnable.Type").FirstOrDefault(p => p.Id == id);
        }

        public override async Task<Pokemon> FindByIdAsync(int id)
        {
            return await _table.Include(p => p.Types).Include("Learnable.Type").FirstOrDefaultAsync(p => p.Id == id);
        }

        public  Pokemon FindByName(string name)
        {
            return _table.Include(p => p.Types).Include("Learnable.Type").FirstOrDefault(p => p.Name == name);
        }

        public async Task<Pokemon> FindByNameAsync(string name)
        {
            return await _table.Include(p => p.Types).Include("Learnable.Type").FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
