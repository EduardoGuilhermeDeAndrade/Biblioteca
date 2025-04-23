using BibliotecaCleanArch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaCleanArch.Api.Controllers;

/// <summary>
/// Retorna todos os livros disponíveis.
/// </summary>
/// <response code="200">Livros encontrados</response>
[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly LivroService _livroService;
    public LivrosController(LivroService livroService) 
        => _livroService = livroService;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _livroService.GetAllLivrosAsync());
}