namespace JJ.Business.Synthesizer.Dto.Operators
{
    internal interface IOperatorDto_WithNumber : IOperatorDto
    {
        InputDto Number { get; set; }
    }
}