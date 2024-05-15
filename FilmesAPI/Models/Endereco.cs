using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Logradoura é obrigatório!")]
    [MaxLength(100)]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Número é obrigatório!")]
    public int Numero { get; set; }

    public virtual Cinema Cinema { get; set; }
}
