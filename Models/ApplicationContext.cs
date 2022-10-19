using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public DbSet<User> User { get; set; }
    }
}
