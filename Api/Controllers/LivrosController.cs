using BibliotecaCleanArch.Application.Services;
using BibliotecaCleanArch.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BibliotecaCleanArch.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de livros
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class LivrosController : ControllerBase
{
    private readonly LivroService _livroService;

    public LivrosController(LivroService livroService)
    {
        _livroService = livroService;
    }

    /// <summary>
    /// Obtém todos os livros
    /// </summary>
    /// <response code="200">Retorna a lista de livros</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Livro>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var livros = await _livroService.GetAllLivrosAsync();
        return Ok(livros);
    }

    /// <summary>
    /// Obtém um livro específico pelo ID
    /// </summary>
    /// <param name="id">ID do livro</param>
    /// <response code="200">Livro encontrado</response>
    /// <response code="404">Livro não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var livro = await _livroService.GetLivroByIdAsync(id);
            return Ok(livro);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Adiciona um novo livro
    /// </summary>
    /// <param name="livro">Dados do livro</param>
    /// <response code="201">Livro criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] Livro livro)
    {
        try
        {
            var createdLivro = await _livroService.AddLivroAsync(livro);
            return CreatedAtAction(nameof(GetById), new { id = createdLivro.Id }, createdLivro);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException)
        {
            return BadRequest("Dados do livro não podem ser nulos");
        }
    }

    /// <summary>
    /// Atualiza um livro existente
    /// </summary>
    /// <param name="id">ID do livro</param>
    /// <param name="livro">Dados atualizados do livro</param>
    /// <response code="204">Livro atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Livro não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] Livro livro)
    {
        if (id != livro.Id)
        {
            return BadRequest("ID do livro não corresponde ao ID na URL");
        }

        try
        {
            await _livroService.UpdateLivroAsync(livro);
            return NoContent();
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Remove um livro
    /// </summary>
    /// <param name="id">ID do livro</param>
    /// <response code="204">Livro removido com sucesso</response>
    /// <response code="404">Livro não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _livroService.DeleteLivroAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}