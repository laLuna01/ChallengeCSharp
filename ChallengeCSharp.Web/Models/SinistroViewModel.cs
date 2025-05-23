using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class SinistroViewModel
{
    public int IdSinistro { get; set; }

    [Required(ErrorMessage = "A consulta é obrigatória.")]
    public int IdConsulta { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MaxLength(255)]
    public string Descricao { get; set; }
    
    [Required(ErrorMessage = "O motivo é obrigatório.")]
    [MaxLength(255)]
    public string Motivo { get; set; }
    
    [Required(ErrorMessage = "A data de abertura é obrigatória.")]
    [MaxLength(20)]
    public string DataAbertura { get; set; }
    
    [Required(ErrorMessage = "O status é obrigatório.")]
    [MaxLength(20)]
    public string Status { get; set; }

    public IEnumerable<SelectListItem>? Consultas { get; set; }
    
}