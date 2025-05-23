using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChallengeCSharp.Web.Models;

public class PacienteViewModel
{
    public int IdPaciente { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public int IdGenero { get; set; }
    
    [Required(ErrorMessage = "O endereço é obrigatório.")]
    public int IdEndereco { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [MaxLength(11)]
    public string CPF { get; set; }
    
    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [MaxLength(20)]
    public string DataNascimento { get; set; }
    
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