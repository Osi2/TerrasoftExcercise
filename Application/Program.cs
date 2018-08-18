namespace Application
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Interfaces;
    using Metrics;
    using Models;

    public class Program
    {       
        public static void Main(string[] args)
        {
            string fileName = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide file name as first parameter");
                Console.ReadKey();
                return;
            }

            fileName = args[0];

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} doesn't exist");
                Console.ReadKey();
                return;
            }
            
            var calculationRequest = new CalculationRequest
            {
                BufferSize = 10 * 1024 * 1024,
                IgnoreArticels = true,
                StringComparer = StringComparer.CurrentCultureIgnoreCase,
                SourceFileName = fileName
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

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }        
    }
}
