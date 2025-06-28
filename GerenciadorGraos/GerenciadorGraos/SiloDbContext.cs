using Microsoft.EntityFrameworkCore;
using GerenciadorGraos.Models;

namespace GerenciadorGraos
{
    public class SiloDbContext : DbContext
    {
        public DbSet<Silo> Silos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GerenciadorGraos.db");
        }
    }
}