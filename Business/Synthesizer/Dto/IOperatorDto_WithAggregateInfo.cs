namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto_WithAggregateInfo : IOperatorDto
    {
        AggregateInfo AggregateInfo { get; set; }
    }
}
