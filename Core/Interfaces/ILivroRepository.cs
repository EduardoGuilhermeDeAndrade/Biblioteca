using BibliotecaCleanArch.Core.Entities;

public interface ILivroRepository
{
    Task<Livro> GetByIdAsync(int id);
    Task<List<Livro>> GetAllAsync();
    Task AddAsync(Livro livro);
    Task UpdateAsync(Livro livro);
    Task DeleteAsync(Livro livro);
}