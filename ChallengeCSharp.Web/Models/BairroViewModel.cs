using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class BairroViewModel
{
    public int CodBairro { get; set; }

    [Required(ErrorMessage = "O nome do bairro é obrigatório.")]
    public string NomeBairro { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public int CodCidade { get; set; }

    // Para o dropdown dos países
    public IEnumerable<SelectListItem>? Cidades { get; set; }

    // Opcional para exibir o nome do país na view de detalhes/index
    public string? NomeCidade { get; set; }
}