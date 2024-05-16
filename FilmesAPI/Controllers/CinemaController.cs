using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <summary>
    /// Adiciona cinema ao banco de dados
    /// </summary>
    /// <param name="cinemaDto">Objeto com os campos necessários para criação de cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemaPorId), new { Id = cinema.Id }, cinemaDto);
    }
    /// <summary>
    /// Lista todos os cinemas
    /// </summary>
    /// <param name="enderecoId">Objeto com os campos necessários para lista todos os cinemas: ex:/cinema?enderecoId=2</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet]
    public IEnumerable<ReadCinemaDto> RecuperCinema([FromQuery] int? enderecoId = null)
    { 
        if(enderecoId == null)
        {
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
            
        }
        return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw
            ($"SELECT Id, Nome, EnderecoId FROM cinemas where cinemas.EnderecoId = {enderecoId}").ToList());
    }

    /// <summary>
    ///  mostra um cinema por id
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para mostra um cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet("{id}")]
    public IActionResult RecuperaCinemaPorId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();
        var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
        return Ok(cinemaDto);
    }

    /// <summary>
    /// Atualizar cinema
    /// </summary>
    /// <param name="cinemaDto">Objeto com os campos necessários para atualizar cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpPut("{id}")]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        var cinema = _context.Cinemas.FirstOrDefault(
            cinema => cinema.Id == id); if (cinema == null)
            return NotFound();
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// delete cinema
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para delete cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(
            cinema => cinema.Id == id);
        if (cinema == null) return NotFound();
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();

    }

}
