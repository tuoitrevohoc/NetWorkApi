using Microsoft.EntityFrameworkCore;

namespace NetWorkApi.Models
{

    /// <summary>
    /// Application database context
    /// </summary>
    public class AppDbContext: DbContext
    {

        public DbSet<AppUser> Users { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


    }
}
