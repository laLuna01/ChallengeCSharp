using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class CidadeViewModel
{
    public int CodCidade { get; set; }

    [Required(ErrorMessage = "O nome da cidade é obrigatório.")]
    public string NomeCidade { get; set; }

    [Required(ErrorMessage = "O estado é obrigatório.")]
    public int CodEstado { get; set; }

    // Para o dropdown dos países
    public IEnumerable<SelectListItem>? Estados { get; set; }

    // Opcional para exibir o nome do país na view de detalhes/index
    public string? NomeEstado { get; set; }
}