using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Titulo é obrigatório!")]
    [MaxLength(30)]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Genêro é obrigatório!")]
    [MaxLength(30)]
    public string Genero { get; set;}

    [Required]
    [Range(60, 300, ErrorMessage = "A duração deve ter de 60 a 300 minutos!")]
    public int Duracao { get; set; }

    public virtual ICollection<Sessao> Sessoes { get; set; }
}
