using Microsoft.EntityFrameworkCore;

namespace WebTeploobmenApp_Suv.Data
{
    public class TeploobmenContext :DbContext
    {
        public DbSet<Variant> Variants { get; set; }
        public DbSet<User> Users { get; set; }

        public TeploobmenContext(DbContextOptions<TeploobmenContext> options) :base(options) 
        { 
        }
    }
}
