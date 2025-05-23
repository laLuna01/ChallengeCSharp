using System.ComponentModel.DataAnnotations;

namespace ChallengeCSharp.Web.Models;

public class GeneroViewModel
{
    public int Id { get; set; } // ADICIONE ISSO
    
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Descricao { get; set; }
}