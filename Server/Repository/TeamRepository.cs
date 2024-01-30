using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server.Repository
{
    public class TeamRepository : GenericRepository<Team>
    {
        public TeamRepository(GameDbContext db) : base(db) { }

        public override Team FindById(int id)
        {
            return _table.Include("Pokemons.Types").Include("Pokemons.Learnable.Type").FirstOrDefault(t => t.Id == id);
        }

        public override async Task<Team> FindByIdAsync(int id)
        {
            return await _table.Include("Pokemons.Types").Include("Pokemons.Learnable.Type").FirstOrDefaultAsync(t => t.Id == id);
        }

        public Team FindByPokemons(List<Pokemon> pokes)
        {
            return _table.Include("Pokemons.Types").Include("Pokemons.Learnable.Type").FirstOrDefault(t => t.Pokemons.All(p => pokes.Contains(p)));

        }
    }
}
