using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;

public class CreateFilmeDto
{

    [Required(ErrorMessage = "Titulo é obrigatório!")]
    [StringLength(30)]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Genêro é obrigatório!")]
    [StringLength(30)]
    public string Genero { get; set; }

    [Required]
    [Range(60, 300, ErrorMessage = "A duração deve ter de 60 a 300 minutos!")]

    public int Duracao { get; set; }
}
