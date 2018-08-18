namespace Application.Metrics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using MapReduce;

    public class MostPopularLetter : IMetricCalculator
    {
        public CalculationResponse Calculate(CalculationRequest calculationRequest, MapReduceWrapper mapReduceWrapper)
        {
            var result = new Dictionary<char, long>();

            for (char c = 'A'; c <= 'z'; c++)
            {                
                var count = mapReduceWrapper.mapReduce.ToList().Sum(kvp => GetCharCount(kvp.Key, c) * kvp.Value);
                result.Add(c, count);
            }

            var resKVP = result.OrderByDescending(x => x.Value).First();         

            var response = new CalculationResponse()
            {
                MetricName = this.GetType().Name,
                ResultChar = resKVP.Key,
                ResultCharCount = resKVP.Value
            };
            
            return response;
        }

        private int GetCharCount(string s, char c) => s.ToCharArray().Where(x => x == c).Count();
    }
}
