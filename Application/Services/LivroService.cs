using BibliotecaCleanArch.Core.Entities;

namespace BibliotecaCleanArch.Application.Services;

public class LivroService
{
    private readonly ILivroRepository _livroRepository;

    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<Livro> GetLivroByIdAsync(int id)
    {
        var livro = await _livroRepository.GetByIdAsync(id);
        if (livro == null)
        {
            throw new NotFoundException($"Livro com ID {id} não encontrado");
        }
        return livro;
    }

    public async Task<List<Livro>> GetAllLivrosAsync()
    {
        return await _livroRepository.GetAllAsync();
    }

    public async Task<Livro> AddLivroAsync(Livro livro)
    {
        if (livro == null)
        {
            throw new ArgumentNullException(nameof(livro));
        }

        // Validações adicionais podem ser adicionadas aqui
        if (string.IsNullOrWhiteSpace(livro.Titulo))
        {
            throw new DomainException("O título do livro é obrigatório");
        }

        await _livroRepository.AddAsync(livro);
        return livro;
    }

    public async Task UpdateLivroAsync(Livro livro)
    {
        if (livro == null)
        {
            throw new ArgumentNullException(nameof(livro));
        }

        var existingLivro = await _livroRepository.GetByIdAsync(livro.Id);
        if (existingLivro == null)
        {
            throw new NotFoundException($"Livro com ID {livro.Id} não encontrado");
        }

        // Atualiza as propriedades
        existingLivro.Titulo = livro.Titulo;
        existingLivro.Autor = livro.Autor;
        existingLivro.Disponivel = livro.Disponivel;

        await _livroRepository.UpdateAsync(existingLivro);
    }

    public async Task DeleteLivroAsync(int id)
    {
        var livro = await _livroRepository.GetByIdAsync(id);
        if (livro == null)
        {
            throw new NotFoundException($"Livro com ID {id} não encontrado");
        }

        await _livroRepository.DeleteAsync(livro);
    }
}