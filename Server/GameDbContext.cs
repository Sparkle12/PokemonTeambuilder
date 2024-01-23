using Microsoft.EntityFrameworkCore;
using SharedLibrary;

namespace Server
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PType> Types { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
     

    }
}
