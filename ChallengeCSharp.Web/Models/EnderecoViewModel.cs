using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class EnderecoViewModel
{
    public int CodEndereco { get; set; }

    [Required(ErrorMessage = "O número é obrigatório.")]
    public int Numero { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    public string CEP { get; set; }

    [MaxLength(30)]
    public string? Referencia { get; set; }

    public int CodBairro { get; set; }

    [MaxLength(30)]
    public string? Logradouro { get; set; }

    // Lista de bairros para dropdown
    public IEnumerable<SelectListItem>? Bairros { get; set; }

    // Nome do bairro (para exibição no Index ou Detalhes)
    public string? NomeBairro { get; set; }
}