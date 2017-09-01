namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_WithPositionOutput
        : OperatorDtoBase_PositionReader, IOperatorDto_PositionWriter
    {
        public InputDto Signal { get; set; }
    }
}