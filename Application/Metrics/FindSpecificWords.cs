namespace Application.Metrics
{
    using System;
    using System.Linq;
    using Application.MapReduce;
    using Application.Models;
    using Interfaces;
    public class FindSpecificWords : IMetricCalculator
    {
        public string[] Words { get; set; }
        public CalculationResponse Calculate(CalculationRequest calculationRequest, MapReduceWrapper mapReduceWrapper)
        {
            var resList = mapReduceWrapper.mapReduce.ToList().OrderByDescending(kvp => kvp.Value).Where(kvp => Words.Contains(kvp.Key));

            var response = new CalculationResponse
            {
                MetricName = this.GetType().Name,
                ResultWords = resList.Select(kvp => kvp.Key).ToList(),
                ResultWordsCount = resList.Select(kvp => kvp.Value).ToList()
            };
            
            return response;
        }
    }
}
