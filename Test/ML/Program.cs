using System;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace ML
{
    class Program
    {
        public class IrisData
        {
            [Column("0")]
            public float SepalLength;

            [Column("1")]
            public float SepalWidth;

            [Column("2")]
            public float PetalLength;

            [Column("3")]
            public float PetalWidth;

            [Column("4")]
            [ColumnName("Label")]
            public string Label;
        }
        public class IrisPrediction
        {
            [ColumnName("PredictedLabel")]
            public string PredictedLabels;
        }

        static void Main(string[] args)
        {
            var pipeline = new LearningPipeline();
            string dataPath = "iris-data.txt";
            pipeline.Add(new TextLoader<IrisData>(dataPath, separator: ","));

            pipeline.Add(new Dictionarizer("Label"));

            pipeline.Add(new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"));
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });
            var model = pipeline.Train<IrisData, IrisPrediction>();

            var prediction = model.Predict(new IrisData()
            { //5.0,3.4,1.5,0.2
                SepalLength = 0.5f,
                SepalWidth = 0.3f,
                PetalLength = 0.1f,
                PetalWidth = 0.0f,
            });

            Console.WriteLine($"Predicted flower type is: {prediction.PredictedLabels}");

        }
    }
}
