using BibliotecaCleanArch.Core.Entities;
using BibliotecaCleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaCleanArch.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(Livro livro)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Livro>> GetAllAsync()
        => await _context.Livros.ToListAsync();

    public async Task<Livro> GetByIdAsync(int id)
    {
        return await _context.Livros
            .FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new KeyNotFoundException($"Livro com ID {id} não encontrado");
    }

    public Task UpdateAsync(Livro livro)
    {
        throw new NotImplementedException();
    }
}