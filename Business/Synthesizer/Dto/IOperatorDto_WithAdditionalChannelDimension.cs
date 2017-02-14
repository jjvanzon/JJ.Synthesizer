namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto_WithAdditionalChannelDimension : IOperatorDto
    {
        int ChannelDimensionStackLevel { get; set; }
    }
}
