using merchantPlatform.ML.Models;
using merchantPlatform.ML.Trainers;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;

namespace merchantPlatform.Services
{
    public class DocumentClassificationService
    {
        private readonly MLContext _mlContext;
        private readonly DocumentClassificationTrainer _trainer;
        private ITransformer? _model;
        private PredictionEngine<DocumentInput, DocumentPrediction>? _predictionEngine;

        public DocumentClassificationService(MLContext mlContext, DocumentClassificationTrainer trainer)
        {
            _mlContext = mlContext;
            _trainer = trainer;

            var trainingFilePath = Path.Combine(AppContext.BaseDirectory, "ML", "Data", "training.csv");
            if (File.Exists(trainingFilePath))
            {
                var model = _trainer.TrainFromCsv(trainingFilePath);
                SetModel(model);
            }
        }

        public void SetModel(ITransformer model)
        {
            _model = model;
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<DocumentInput, DocumentPrediction>(_model);
        }

        public DocumentPrediction Predict(string text)
        {
            if (_predictionEngine == null)
                throw new InvalidOperationException("Model henüz yüklenmedi. Lütfen modeli önce eğitin.");

            var input = new DocumentInput { Text = text };
            return _predictionEngine.Predict(input);
        }
    }
}
