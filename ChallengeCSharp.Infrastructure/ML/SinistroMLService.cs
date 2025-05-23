using ChallengeCSharp.Domain.ML;
using Microsoft.ML;

public class SinistroMLService
{
    private readonly string _modelPath;
    private readonly string _dataPath;
    private readonly MLContext _mlContext;
    private PredictionEngine<SinistroData, SinistroPrediction>? _predictionEngine;

    public SinistroMLService()
    {
        _mlContext = new MLContext();

        // Caminho do modelo dentro do projeto .Api
        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MLModels");
        _modelPath = Path.Combine(basePath, "sinistro-model.zip");
        _dataPath = Path.Combine(basePath, "Data/sinistros.csv");

        if (!File.Exists(_modelPath))
        {
            if (File.Exists(_dataPath))
            {
                TreinarModelo(_dataPath);
            }
            else
            {
                Console.WriteLine("Arquivo CSV para treinamento não encontrado.");
            }
        }
        
        LoadModel();
    }

    private void LoadModel()
    {
        if (File.Exists(_modelPath))
        {
            var model = _mlContext.Model.Load(_modelPath, out _);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SinistroData, SinistroPrediction>(model);
        }
    }

    public SinistroPrediction Prever(SinistroData input)
    {
        if (_predictionEngine == null)
            throw new InvalidOperationException("Modelo não carregado.");
        return _predictionEngine.Predict(input);
    }

    public void TreinarModelo(string caminhoCsv)
    {
        // Carrega os dados do arquivo CSV
        var dados = _mlContext.Data.LoadFromTextFile<SinistroData>(
            path: caminhoCsv,
            hasHeader: true,
            separatorChar: ','
        );

        // Pipeline de transformação + treino
        var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding(
                outputColumnName: "ProcedimentoEncoded",
                inputColumnName: nameof(SinistroData.Procedimento))
            .Append(_mlContext.Transforms.Concatenate("Features", "ProcedimentoEncoded", nameof(SinistroData.Valor)))
            .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                labelColumnName: nameof(SinistroData.HistoricoNegativo), 
                featureColumnName: "Features"));


        // Treina o modelo
        var modelo = pipeline.Fit(dados);
        
        // Salva o modelo treinado
        _mlContext.Model.Save(modelo, dados.Schema, _modelPath);

        // Recarrega o PredictionEngine com o novo modelo
        LoadModel();
    }
}
