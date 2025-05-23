using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class TratamentoViewModel
{
    public int IdTratamento { get; set; }

    [Required(ErrorMessage = "A consulta é obrigatória.")]
    public int IdConsulta { get; set; }
    
    [Required(ErrorMessage = "O custo é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O custo deve ser um valor positivo.")]
    public float Custo { get; set; }

    [Required(ErrorMessage = "A Descrição é obrigatória.")]
    [MaxLength(255)]
    public string Descricao { get; set; }
    
    [Required(ErrorMessage = "A data de início é obrigatória.")]
    [MaxLength(20)]
    public string DataInicio { get; set; }
    
    [Required(ErrorMessage = "A data de término é obrigatória.")]
    [MaxLength(20)]
    public string DataTermino { get; set; }
    
    [Required(ErrorMessage = "O tipo de tratamento é obrigatório.")]
    [MaxLength(50)]
    public string TipoTratamento { get; set; }

    public IEnumerable<SelectListItem>? Consultas { get; set; }
    
}