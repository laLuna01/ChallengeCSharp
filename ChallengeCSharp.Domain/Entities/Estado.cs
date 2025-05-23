using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Estado
{
    [Key]
    public int COD_ESTADO { get; set; }

    [Required]
    [MaxLength(30)]
    public string NOME_ESTADO { get; set; } = string.Empty;

    public int COD_PAIS { get; set; }

    [ForeignKey("COD_PAIS")]
    public Pais? Pais { get; set; }
    
    public ICollection<Cidade>? Cidades { get; set; }
}