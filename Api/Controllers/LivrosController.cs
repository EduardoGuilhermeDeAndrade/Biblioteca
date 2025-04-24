using BibliotecaCleanArch.Application.Services;
using BibliotecaCleanArch.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BibliotecaCleanArch.Api.Controllers;

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

    [HttpGet]
    [ProducesResponseType(typeof(List<Livro>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var livros = await _livroService.GetAllLivrosAsync();
        return Ok(livros);
    }

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