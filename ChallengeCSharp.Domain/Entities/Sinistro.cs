using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Sinistro
{
    [Key]
    public int ID_SINISTRO { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string MOTIVO_SINISTRO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string DATA_ABERTURA { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string STATUS_SINISTRO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string DESCRICAO_SINISTRO { get; set; } = string.Empty;
    
    public int CONSULTA_ID_CONSULTA { get; set; }
    
    [ForeignKey("CONSULTA_ID_CONSULTA")]
    public Consulta? Consulta { get; set; }
}