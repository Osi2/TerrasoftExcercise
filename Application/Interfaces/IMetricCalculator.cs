namespace Application.Interfaces
{
    using Models;
    using MapReduce;
    public interface IMetricCalculator
    {
        CalculationResponse Calculate(CalculationRequest calculationRequest, MapReduceWrapper mapReduceWrapper);
    }
}
