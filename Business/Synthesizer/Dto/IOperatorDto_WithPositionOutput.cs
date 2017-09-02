namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto_WithPositionOutput
        : IOperatorDto_PositionReader,
          IOperatorDto_WithSignal_WithDimension
    { }
}