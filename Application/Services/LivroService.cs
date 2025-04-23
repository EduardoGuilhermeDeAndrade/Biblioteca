using BibliotecaCleanArch.Core.Entities;
using BibliotecaCleanArch.Core.Interfaces;

namespace BibliotecaCleanArch.Application.Services;
public class LivroService
{
    private readonly ILivroRepository _livroRepository;
    public LivroService(ILivroRepository livroRepository) 
        => _livroRepository = livroRepository;

    public async Task<List<Livro>> GetAllLivrosAsync() 
        => await _livroRepository.GetAllAsync();
}