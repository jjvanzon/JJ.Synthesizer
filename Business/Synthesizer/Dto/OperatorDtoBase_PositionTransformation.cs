namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_PositionTransformation
        : OperatorDtoBase_PositionReader, IOperatorDto_PositionTransformation
    {
        public InputDto Signal { get; set; }
    }
}