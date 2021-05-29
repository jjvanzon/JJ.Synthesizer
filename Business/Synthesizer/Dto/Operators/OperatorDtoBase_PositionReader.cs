namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal abstract class OperatorDtoBase_PositionReader
        : OperatorDtoBase_WithDimension, IOperatorDto_PositionReader
    {
        /// <inheritdoc />
        public InputDto Position { get; set; }
    }
}