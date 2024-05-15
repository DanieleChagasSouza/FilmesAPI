using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "Nome é obrigatório!")]
    [MaxLength(50)]
    public string Nome { get; set; }

    public int EnderecoId { get; set; }

}
