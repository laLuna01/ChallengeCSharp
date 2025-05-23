using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Endereco
{
    [Key]
    public int COD_ENDERECO { get; set; }
    
    public int NUMERO { get; set; }
    
    public int CEP { get; set; }
    
    [MaxLength(30)]
    public string? REFERENCIA { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(30)]
    public string LOGRADOURO { get; set; } = string.Empty;
    
    public int COD_BAIRRO { get; set; }
    
    [ForeignKey("COD_BAIRRO")]
    public Bairro? Bairro { get; set; }
    
    public ICollection<Dentista>? Dentistas { get; set; }
    
    public ICollection<Paciente>? Pacientes { get; set; }
}