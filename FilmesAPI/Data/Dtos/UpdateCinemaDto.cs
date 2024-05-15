using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "Nome é obrigatório!")]
    [MaxLength(50)]
    public string Nome { get; set; }
}
