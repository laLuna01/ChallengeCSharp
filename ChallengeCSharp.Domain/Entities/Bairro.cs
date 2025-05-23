using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeCSharp.Domain.Entities;

public class Bairro
{
    [Key]
    public int COD_BAIRRO { get; set; }

    [Required]
    [MaxLength(30)]
    public string NOME { get; set; } = string.Empty;

    public int COD_CIDADE { get; set; }

    [ForeignKey("COD_CIDADE")]
    public Cidade? Cidade { get; set; }
    
    public ICollection<Endereco>? Enderecos { get; set; }
}