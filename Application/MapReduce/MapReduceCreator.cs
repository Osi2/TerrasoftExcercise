namespace Application.MapReduce
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class MapReduceCreator
    {
        private CalculationRequest _calculationRequest;

        private string[] separators = new[] { " " };
        private string[] specSymbols = new[] { " " , "+", "&", "&&", "|", "||", "!", "(", ")", "{", "}", "[", "]", "^", "~", "*", "?", ":", "\\", "\"" };
        private string[] articels = new[] { "to", "in", "for", "of", "the", "over", "from", "on", "out", "a", "at", "with", "after", "up", "as", "by", "and", "be", "is" };

        public MapReduceCreator(CalculationRequest calculationRequest)
        {
            _calculationRequest = calculationRequest;
        }     
       
        public ConcurrentDictionary<string, uint> CreateMapReduce(List<string> textInput, ConcurrentDictionary<string, uint> result)
        {
            try
            {
                Parallel.ForEach
                (
                    textInput,
                    new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                    (line, state, index) =>
                    {
                        foreach (var word in line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (_calculationRequest.IgnoreArticels && !IsValidWord(word)) continue;
                              
                            result.AddOrUpdate(word, 1, (key, oldVal) => oldVal + 1);
                        }
                    }
                );

                Console.WriteLine($"\nCreated MapResult with {result.Count} records\n");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in [CreateMapReduce]: " + ex.Message);
                return null;
            }
        }

        private bool IsValidWord(string word) => !articels.Contains(word);
    }
}
