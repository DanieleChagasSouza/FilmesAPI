using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona Endereço no banco de dados
    /// </summary>
    /// <param name="enderecoDto">Objeto com os campos necessários para criação de endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { Id = endereco.Id }, endereco);
    }

    //CreatedAtAction
    /// <summary>
    /// Lista todos os endereços
    /// </summary>
    /// <param>Objeto com os campos necessários para lista todos os endereços</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet]
    public IEnumerable<ReadEnderecoDto> RecuperEndereco()
    {
        return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.ToList());
    }

    /// <summary>
    ///  mostra um endereço por id
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para mostra um endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso de sucesso</response>
    [HttpGet("{id}")]
    public IActionResult RecuperaEnderecoPorId(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco != null)
        {
            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

            return Ok(enderecoDto);

        }
        return NotFound();
    }

    /// <summary>
    /// Atualizar endereço
    /// </summary>
    /// <param name="enderecoDto">Objeto com os campos necessários para atualizar endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpPut("{id}")]
    public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
    {
        var endereco = _context.Enderecos.FirstOrDefault(
            endereco => endereco.Id == id); if (endereco == null)
            return NotFound();
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// delete endereço
    /// </summary>
    /// <param name="id">Objeto com os campos necessários para delete endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso de sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteEndereco(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(
            endereco => endereco.Id == id);
        if (endereco == null) return NotFound();
        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();

    }
}
