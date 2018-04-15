namespace Application
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Metrics;
    using Models;

    public class Program
    {       
        public static void Main(string[] args)
        {

            var calculationRequest = new CalculationRequest
            {
                BufferSize = 10 * 1024 * 1024,
                IgnoreArticels = true,
                StringComparer = StringComparer.CurrentCultureIgnoreCase,
                SourceFileName = @"D:\InputString3.txt"
            };

            var metrics = new List<IMetricCalculator>()
            {
                new MostPopularLetter(),
                new MostPopularWord(),
                new TenMostPopularWords(),
                new FindSpecificWords() { Words =  new[] { "computer", "football", "weather", "hockey" } }
            };          

            var fileProcessor = new FileProcessor()
            {                
                CalculationRequest = calculationRequest,
                Metrics = metrics
            };
           
            fileProcessor.Process();

            Console.ReadKey();
        }        
    }
}
