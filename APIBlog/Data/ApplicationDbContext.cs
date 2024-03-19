using APIBlog.Modelos;
using Microsoft.EntityFrameworkCore;

namespace APIBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Agregar Modelos
        public DbSet<Post> Post { get; set; }
    }
}
