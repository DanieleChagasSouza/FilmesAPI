using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos;

public class CreateEnderecoDto
{
    [Required(ErrorMessage = "Logradoura é obrigatório!")]
    [MaxLength(100)]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Número é obrigatório!")]
    public int Numero { get; set; }
}
