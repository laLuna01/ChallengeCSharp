using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Paciente
{
    [Key]
    public int ID_PACIENTE { get; set; }
    
    [Required]
    [MaxLength(11)]
    public string CPF { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string DATA_NASCIMENTO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string NOME { get; set; } = string.Empty;
    
    public int GENERO_ID_GENERO { get; set; }
    
    [ForeignKey("GENERO_ID_GENERO")]
    public Genero? Genero { get; set; }
    
    public int ENDERECO_ID_ENDERECO { get; set; }
    
    [ForeignKey("ENDERECO_ID_ENDERECO")]
    public Endereco? Endereco { get; set; }
    
    public ICollection<Consulta>? Consultas { get; set; }
}