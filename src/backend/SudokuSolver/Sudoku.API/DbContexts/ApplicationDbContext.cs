using Microsoft.EntityFrameworkCore;

namespace Sudoku.API.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Models.Sudoku> Sudokus { get; set; }
    }
}
