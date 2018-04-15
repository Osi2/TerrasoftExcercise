namespace Application.Models
{
    using System.Collections.Generic;
    public class CalculationResponse
    {
        public string MetricName { get; set; }
        public string ResultWord { get; set; }
        public List<string> ResultWords { get; set; }
        public List<uint> ResultWordsCount { get; set; }
        public char ResultChar { get; set; }
        public long ResultCharCount { get; set; }
        public long ResultWordCount { get; set; }
    }
}
