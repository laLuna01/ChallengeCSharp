using Microsoft.ML.Data;

namespace ChallengeCSharp.Domain.ML
{
    public class SinistroData
    {
        [LoadColumn(0)]
        public float Valor { get; set; }
        
        [LoadColumn(1)]
        public string Procedimento { get; set; }
        
        [LoadColumn(2)]
        public bool HistoricoNegativo { get; set; }
    }
}
