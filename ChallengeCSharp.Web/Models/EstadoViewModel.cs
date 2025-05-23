using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class EstadoViewModel
{
    public int CodEstado { get; set; }

    [Required(ErrorMessage = "O nome do estado é obrigatório.")]
    public string NomeEstado { get; set; }

    [Required(ErrorMessage = "O país é obrigatório.")]
    public int CodPais { get; set; }

    // Para o dropdown dos países
    public IEnumerable<SelectListItem>? Paises { get; set; }

    // Opcional para exibir o nome do país na view de detalhes/index
    public string? NomePais { get; set; }
}