using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = filme.Id }, filme);
    }

    /// <summary>
    /// Lista todos os filmes
    /// </summary>
    /// <param name="skip, take">Objeto com os campos necessários para lista todos os filmes</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperFimes([FromQuery] int skip = 0, [FromQuery] int take = 20)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
    }

    /// <summary>
    ///  mostra um filme
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para mostra um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int  id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualizar um filme
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para atualizar um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id); if (filme == null)
            return NotFound(); 
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualizar um filme parcial
    /// </summary>
    /// <param name="patch">Objeto com os campos necessários para atualizar um filme parcialmente</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id); if (filme == null)
            return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// delete um filme
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para delete um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id); 
        if(filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();

    }
}
