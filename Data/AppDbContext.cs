using EscolaAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EscolaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Aluno>? Alunos { get; set; }
    }
}
