namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_WithSignal : IOperatorDto
    {
        InputDto Signal { get; set; }
    }
}
