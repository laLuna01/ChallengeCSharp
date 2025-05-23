using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class DentistaViewModel
{
    public int IdDentista { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public int IdGenero { get; set; }
    
    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public int IdEndereco { get; set; }

    [Required(ErrorMessage = "O CRO é obrigatório.")]
    [MaxLength(15)]
    public string CRO { get; set; }
    
    [Required(ErrorMessage = "A especialidade é obrigatória.")]
    [MaxLength(50)]
    public string Especialidade { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100)]
    public string Nome { get; set; }

    // Lista de bairros para dropdown
    public IEnumerable<SelectListItem>? Generos { get; set; }

    // Nome do bairro (para exibição no Index ou Detalhes)
    public string? NomeGenero { get; set; }
    
    // Lista de bairros para dropdown
    public IEnumerable<SelectListItem>? Enderecos { get; set; }

    // Nome do bairro (para exibição no Index ou Detalhes)
    public string? LogradouroEndereco { get; set; }
}