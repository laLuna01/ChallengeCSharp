using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Consulta
{
    [Key]
    public int ID_CONSULTA { get; set; }
    
    [Required]
    public float CUSTO { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string STATUS_SINISTRO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string DATA_CONSULTA { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string TIPO_CONSULTA { get; set; } = string.Empty;
    
    public int PACIENTE_ID_PACIENTE { get; set; }
    
    [ForeignKey("PACIENTE_ID_PACIENTE")]
    public Paciente? Paciente { get; set; }
    
    public int DENTISTA_ID_DENTISTA { get; set; }
    
    [ForeignKey("DENTISTA_ID_DENTISTA")]
    public Dentista? Dentista { get; set; }
    
    public ICollection<Tratamento>? Tratamentos { get; set; }
    
    public ICollection<Sinistro>? Sinistros { get; set; }
}