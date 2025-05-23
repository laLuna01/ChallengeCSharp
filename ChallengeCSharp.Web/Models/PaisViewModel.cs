using System.ComponentModel.DataAnnotations;

namespace ChallengeCSharp.Web.Models;

public class PaisViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do país é obrigatório.")]
    public string Nome { get; set; }
}