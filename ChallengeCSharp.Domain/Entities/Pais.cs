using System.ComponentModel.DataAnnotations;

namespace ChallengeCSharp.Domain.Entities
{
    public class Pais
    {
        [Key]
        public int COD_PAIS { get; set; }

        [Required]
        [MaxLength(30)]
        public string NOME { get; set; } = string.Empty;
        
        public ICollection<Estado>? Estados { get; set; }
    }
}