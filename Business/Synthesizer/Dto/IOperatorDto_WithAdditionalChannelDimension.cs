namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto_WithAdditionalChannelDimension : IOperatorDto
    {
        InputDto Channel { get; set; }
        int ChannelDimensionStackLevel { get; set; }
    }
}
