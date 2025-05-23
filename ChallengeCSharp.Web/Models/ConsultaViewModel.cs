using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class ConsultaViewModel
{
    public int IdConsulta { get; set; }

    [Required(ErrorMessage = "O paciente é obrigatório.")]
    public int IdPaciente { get; set; }
    
    [Required(ErrorMessage = "O dentista é obrigatório.")]
    public int IdDentista { get; set; }
    
    [Required(ErrorMessage = "O custo é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O custo deve ser um valor positivo.")]
    public float Custo { get; set; }

    [Required(ErrorMessage = "O status do sinistro é obrigatório.")]
    [MaxLength(20)]
    public string StatusSinistro { get; set; }
    
    [Required(ErrorMessage = "A data da consulta é obrigatória.")]
    [MaxLength(20)]
    public string DataConsulta { get; set; }
    
    [Required(ErrorMessage = "O tipo da consulta é obrigatório.")]
    [MaxLength(100)]
    public string TipoConsulta { get; set; }

    // Lista de bairros para dropdown
    public IEnumerable<SelectListItem>? Pacientes { get; set; }

    // Nome do bairro (para exibição no Index ou Detalhes)
    public string? NomePaciente { get; set; }
    
    // Lista de bairros para dropdown
    public IEnumerable<SelectListItem>? Dentistas { get; set; }

    // Nome do bairro (para exibição no Index ou Detalhes)
    public string? NomeDentista { get; set; }
}