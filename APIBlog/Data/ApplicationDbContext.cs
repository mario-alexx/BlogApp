using Microsoft.EntityFrameworkCore;

namespace APIBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Agregar Modelos
    }
}
