using ChallengeCSharp.Domain.ML;

public class SinistroIAService
{
    private readonly SinistroMLService _mlService;

    public SinistroIAService(SinistroMLService mlService)
    {
        _mlService = mlService;
    }

    public SinistroPrediction PreverAprovacao(SinistroData dados)
    {
        return _mlService.Prever(dados);
    }
}