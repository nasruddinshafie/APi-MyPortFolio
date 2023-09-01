using Microsoft.EntityFrameworkCore;
using MyPortFolio.Entities;

namespace MyPortFolio.Data
{
    public class DataContext: DbContext 
    {
        public DataContext(DbContextOptions options): base(options) 
        {

        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }


    }
}
