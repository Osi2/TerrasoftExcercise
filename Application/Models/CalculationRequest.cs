namespace Application.Models
{
    using System;

    public class CalculationRequest
    {
        public int BufferSize =  1024 * 1024;
        public StringComparer StringComparer { get; set; }
        public string SourceFileName { get; set; }
        public bool IgnoreArticels { get; set; }
    }
}
