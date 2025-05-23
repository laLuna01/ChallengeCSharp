using Microsoft.ML.Data;

namespace ChallengeCSharp.Domain.ML;

public class SinistroPrediction
{
    [ColumnName("PredictedLabel")]
    public bool Aprovado { get; set; }

    public float Probability { get; set; }
    public float Score { get; set; }
}