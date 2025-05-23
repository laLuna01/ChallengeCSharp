using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Dentista
{
    [Key]
    public int ID_DENTISTA { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string CRO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string ESPECIALIDADE { get; set; } = string.Empty;
    
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