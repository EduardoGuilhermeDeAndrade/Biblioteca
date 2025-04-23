using BibliotecaCleanArch.Core.Entities;
using BibliotecaCleanArch.Core.Interfaces;
using BibliotecaCleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaCleanArch.Infrastructure.Repositories;
public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;
    public LivroRepository(AppDbContext context) 
        => _context = context;

    public Task AddAsync(Livro livro)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Livro>> GetAllAsync() 
        => await _context.Livros.ToListAsync();

    public Task<Livro> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}