using BibliotecaCleanArch.Core.Entities;

namespace BibliotecaCleanArch.Core.Interfaces;
public interface ILivroRepository
{
    Task<Livro> GetByIdAsync(int id);
    Task<List<Livro>> GetAllAsync();
    Task AddAsync(Livro livro);
}