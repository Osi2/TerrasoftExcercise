namespace Application.Metrics
{
    using System;
    using MapReduce;
    using Models;
    using Interfaces;
    using System.Linq;

    public class TenMostPopularWords : IMetricCalculator
    {
        public CalculationResponse Calculate(CalculationRequest calculationRequest, MapReduceWrapper mapReduceWrapper)
        {
            var resList = mapReduceWrapper.mapReduce.ToList().OrderByDescending(kvp => kvp.Value).Take(10);

            var response = new CalculationResponse
            {
                MetricName = this.GetType().Name,
                ResultWords = resList.Select(kvp => kvp.Key).ToList(),
                ResultWordsCount  = resList.Select(kvp => kvp.Value).ToList()
            };

            return response;
        }
    }
}
