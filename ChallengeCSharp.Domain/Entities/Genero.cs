using System.ComponentModel.DataAnnotations;

namespace ChallengeCSharp.Domain.Entities;

public class Genero
{
    [Key]
    
    public int ID_GENERO { get; set; }
    
    public string DESCRICAO { get; set; }
    
    public ICollection<Dentista>? Dentistas { get; set; }
    
    public ICollection<Paciente>? Pacientes { get; set; }
}
