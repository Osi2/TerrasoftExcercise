namespace Application
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Interfaces;
    using MapReduce;
    using Models;
    using System.Collections.Concurrent;

    public class FileProcessor
    {
        public CalculationRequest CalculationRequest { get; set; }
        public List<IMetricCalculator> Metrics { get; set; }
       
        public void Process()
        {
            long count = 0;
            var inputText = new List<string>();
            var mapReduceDict = new ConcurrentDictionary<string, uint>(CalculationRequest.StringComparer);

            var metricsCalculator = new MetricsCalculator(Metrics);
            var mapReduceCreator = new MapReduceCreator(CalculationRequest);

            using (FileStream fs = File.Open(CalculationRequest.SourceFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {               
                using (BufferedStream bs = new BufferedStream(fs, CalculationRequest.BufferSize))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            inputText.Add(line);
                            count++;

                            if (count % 10000 == 0)
                                Console.WriteLine($"Read {count} lines");

                            if (count % 100000 == 0)
                            {
                                mapReduceDict = mapReduceCreator.CreateMapReduce(inputText, mapReduceDict);
                                inputText = new List<string>();
                            }
                        }

                        mapReduceDict = mapReduceCreator.CreateMapReduce(inputText, mapReduceDict);

                        var mapReduceWrapper = new MapReduceWrapper { mapReduce = mapReduceDict };

                        metricsCalculator.RunAllMetrics(CalculationRequest, mapReduceWrapper);
                    }
                }
            }
        }
    }
}
