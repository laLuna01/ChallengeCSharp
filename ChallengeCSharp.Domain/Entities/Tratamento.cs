using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Tratamento
{
    [Key]
    public int ID_TRATAMENTO { get; set; }
    
    [Required]
    public float CUSTO { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string DESCRICAO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string DATA_INICIO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string DATA_TERMINO { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string TIPO_TRATAMENTO { get; set; } = string.Empty;
    
    public int CONSULTA_ID_CONSULTA { get; set; }
    
    [ForeignKey("CONSULTA_ID_CONSULTA")]
    public Consulta? Consulta { get; set; }
}