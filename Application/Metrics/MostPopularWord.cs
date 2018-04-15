namespace Application.Metrics
{
    using System;
    using System.Linq;
    using MapReduce;
    using Models;
    using Interfaces;
    public class MostPopularWord : IMetricCalculator
    {
        public CalculationResponse Calculate(CalculationRequest calculationRequest, MapReduceWrapper mapReduceWrapper)
        {
            Console.WriteLine("Started metric MostPopularWord");

            var res = mapReduceWrapper.mapReduce.ToList().OrderByDescending(kvp => kvp.Value).First();

            var response = new CalculationResponse
            {
                MetricName = this.GetType().Name,
                ResultWord = res.Key,
                ResultWordCount = res.Value
            };

            Console.WriteLine("Completed metric MostPopularWord");

            return response;
        }
    }
}
