namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_WithAdditionalChannelDimension : IOperatorDto
    {
        InputDto Channel { get; set; }
    }
}
