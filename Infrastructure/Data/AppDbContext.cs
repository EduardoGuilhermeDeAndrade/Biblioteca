using BibliotecaCleanArch.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaCleanArch.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Livro> Livros { get; set; }
}