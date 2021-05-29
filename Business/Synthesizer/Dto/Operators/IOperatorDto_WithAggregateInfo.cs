namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_WithAggregateInfo : IOperatorDto
    {
        AggregateInfo GetAggregateInfo();
    }
}
