using Microsoft.EntityFrameworkCore;
using MisTareas.API.Data.Entities;

namespace MisTareas.API.Data
{
    public class MisTareasContext : DbContext
    {
        public DbSet<Column> Column { get; set; }
        public DbSet<Entities.Task> Task { get; set; }
        public DbSet<Board> Board { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=mistareas;user=root;password=Bavt_4903")
                .UseLazyLoadingProxies(false)
                .LogTo(Console.WriteLine);
        }
    }
}
