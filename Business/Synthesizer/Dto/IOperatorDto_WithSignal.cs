namespace JJ.Business.Synthesizer.Dto
{
    internal interface IOperatorDto_WithSignal : IOperatorDto
    {
        InputDto Signal { get; set; }
    }
}
