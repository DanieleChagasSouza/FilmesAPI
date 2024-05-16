using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona sessão no banco de dados
    /// </summary>
    /// <param name="sessaoDto">Objeto com os campos necessários para criação de sessão</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarSessao([FromBody] CreateSessaoDto sessaoDto)
    {
        Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaSessaoPorId), 
            new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
    }

    //CreatedAtAction
    /// <summary>
    /// Lista todos os sessão
    /// </summary>
    /// <param>Objeto com os campos necessários para lista todos os sessão</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet]
    public IEnumerable<ReadSessaoDto> RecuperSessao()
    {
        return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
    }

    /// <summary>
    ///  mostra um sessão por id
    /// </summary>
    /// <param name="filmeId" name="cinemaId">Objeto com os campos necessários para mostra um sessão</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult RecuperaSessaoPorId(int filmeId, int cinemaId)
    {
        var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId 
        && sessao.CinemaId == cinemaId);
        if (sessao != null)
        {
            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

            return Ok(sessaoDto);

        }
        return NotFound();
    }

}
