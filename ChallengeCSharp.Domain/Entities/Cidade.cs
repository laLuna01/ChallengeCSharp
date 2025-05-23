using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Cidade
{
    [Key]
    public int COD_CIDADE { get; set; }

    [Required]
    [MaxLength(30)]
    public string NOME { get; set; } = string.Empty;

    public int COD_ESTADO { get; set; }

    [ForeignKey("COD_ESTADO")]
    public Estado? Estado { get; set; }
    
    public ICollection<Bairro>? Bairros { get; set; }
}