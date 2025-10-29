using Microsoft.EntityFrameworkCore;
namespace OverTheGardenWallAPI.Models
{
    public class OverTheGardenWallDbContext : DbContext
    {
        public OverTheGardenWallDbContext(DbContextOptions<OverTheGardenWallDbContext> options) : base(options){}

        public DbSet<Character> Characters => Set<Character>();

    }
}
