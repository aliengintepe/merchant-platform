using merchantPlatform.ML.Models;
using Microsoft.ML;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace merchantPlatform.ML.Trainers
{
    public class DocumentClassificationTrainer
    {
        private readonly MLContext _mlContext;

        public DocumentClassificationTrainer(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        public ITransformer TrainFromCsv(string trainingFilePath)
        {
            var dataView = _mlContext.Data.LoadFromTextFile<DocumentInput>(
                path: trainingFilePath,
                hasHeader: true,
                separatorChar: ',',
                allowQuoting: true);

            var pipeline =
                _mlContext.Transforms.CustomMapping<DocumentInput, DocumentFeatures>(
                    (input, output) =>
                    {
                        var textLower = input.Text?.ToLower() ?? string.Empty;

             
                        output.Keyword_Kimlik = textLower.Contains("t.c. kimlik") || textLower.Contains("kimlik no") ? 1f : 0f;
                        output.Keyword_Vergi = textLower.Contains("vergi levhası") || textLower.Contains("iş yeri adresi")|| textLower.Contains("ticaret ünvanı")  || textLower.Contains("vergi dairesi") || textLower.Contains("faaliyet kodu") ? 1f : 0f;
                        output.Keyword_TicaretSicil = textLower.Contains("ticaret sicil") || textLower.Contains("sicil gazetesi") ? 1f : 0f;

                        output.HasTcKimlikNo = Regex.IsMatch(textLower, @"\b[1-9]{1}[0-9]{9}[02468]{1}\b") ? 1f : 0f;
                        output.CharLength = textLower.Length;
                        var digits = Regex.Matches(textLower, @"\d").Count;
                        output.DigitRatio = (output.CharLength > 0) ? (float)digits / output.CharLength : 0;
                        output.DatePatternCount = Regex.Matches(textLower, @"\b\d{2}\.\d{2}\.\d{4}\b").Count;
                        output.NumberPatternCount = Regex.Matches(textLower, @"\b\d{6,}\b").Count;
                        output.Text = input.Text;
                        output.Label = input.Label;
                    },
                    contractName: "CustomFeatures")

                .Append(_mlContext.Transforms.Text.FeaturizeText(
                    outputColumnName: "TextFeatures",
                    inputColumnName: nameof(DocumentFeatures.Text)))

                .Append(_mlContext.Transforms.Concatenate("Features",
                    "TextFeatures",
                    nameof(DocumentFeatures.Keyword_Kimlik),
                    nameof(DocumentFeatures.Keyword_Vergi),
                    nameof(DocumentFeatures.Keyword_TicaretSicil),
                    nameof(DocumentFeatures.HasTcKimlikNo),
                    nameof(DocumentFeatures.CharLength),
                    nameof(DocumentFeatures.DigitRatio),
                    nameof(DocumentFeatures.DatePatternCount),
                    nameof(DocumentFeatures.NumberPatternCount)))

                .Append(_mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "LabelKey",
                    inputColumnName: nameof(DocumentFeatures.Label)))

                .Append(_mlContext.MulticlassClassification.Trainers.LightGbm(
                    labelColumnName: "LabelKey",
                    featureColumnName: "Features"))

                .Append(_mlContext.Transforms.Conversion.MapKeyToValue(
                    outputColumnName: "PredictedLabel",
                    inputColumnName: "PredictedLabel"));

            var cvResults = _mlContext.MulticlassClassification.CrossValidate(
                data: dataView,
                estimator: pipeline,
                numberOfFolds: 5,
                labelColumnName: "LabelKey");

            foreach (var result in cvResults)
            {
                Console.WriteLine($"Fold Accuracy: {result.Metrics.MicroAccuracy:P2}, Macro: {result.Metrics.MacroAccuracy:P2}");
            }

            var model = pipeline.Fit(dataView);
            return model;
        }
    }
}