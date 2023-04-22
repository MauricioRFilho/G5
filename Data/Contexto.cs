using Microsoft.EntityFrameworkCore;
using WebMvcMysql.Models;

namespace G5.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Logs> Log { get; set; }

        public DbSet<WebMvcMysql.Models.Users> Users { get; set; } = default!;
    }
}
