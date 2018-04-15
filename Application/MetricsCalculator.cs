namespace Application
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using MapReduce;
    using Models;
    public class MetricsCalculator
    {
        public List<IMetricCalculator> _metrics = null;

        public MetricsCalculator(List<IMetricCalculator> metrics)
        {
            _metrics = metrics;
        }

        public void RunAllMetrics(CalculationRequest calculationRequest, MapReduceWrapper mapReduceWrapper)
        {
            foreach (var metric in _metrics)
            {
                var response = metric.Calculate(calculationRequest, mapReduceWrapper);

                PrintResponse(response);
            }
        }

        private static void PrintResponse(CalculationResponse calculationResponse)
        {
            Console.WriteLine();
            Console.WriteLine($"Metric Name: {calculationResponse.MetricName}");

            if (calculationResponse.ResultChar != 0)
                Console.WriteLine($"Result Char: '{calculationResponse.ResultChar}' count: {calculationResponse.ResultCharCount}");
            else if (calculationResponse.ResultWord != null)
                Console.WriteLine($"Result Word: '{calculationResponse.ResultWord}' count: {calculationResponse.ResultWordCount}");
            else if (calculationResponse.ResultWords != null)
            {
                Console.WriteLine($"Result Words (in desc order): ");
                for (int i = 0; i < calculationResponse.ResultWords.Count; i++)
                {
                    if (i != 0) Console.Write(", ");
                    Console.Write($"{calculationResponse.ResultWords[i]}({calculationResponse.ResultWordsCount[i]})");
                }
            }

            Console.WriteLine();
        }
    }
}
