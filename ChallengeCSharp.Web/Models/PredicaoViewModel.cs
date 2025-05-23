using System.ComponentModel.DataAnnotations;

namespace ChallengeCSharp.Web.Models;

public class PredicaoViewModel
{
    [Required(ErrorMessage = "O valor é obrigatório.")]
    public float Valor { get; set; }
    
    [Required(ErrorMessage = "O procedimento é obrigatório.")]
    public string Procedimento { get; set; }
    
    [Required(ErrorMessage = "O histórico é obrigatório.")]
    public bool HistoricoNegativo { get; set; }
        
    // Resultado da predição
    public bool? Aprovado { get; set; }
    public float? Probabilidade { get; set; }
    public float? Score { get; set; }
}