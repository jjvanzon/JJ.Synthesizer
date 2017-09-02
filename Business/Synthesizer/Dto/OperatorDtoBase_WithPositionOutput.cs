namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithPositionOutput
        : OperatorDtoBase_PositionReader, IOperatorDto_WithPositionOutput
    {
        public InputDto Signal { get; set; }
    }
}