namespace JJ.Business.Synthesizer.Dto
{
	internal interface IOperatorDto_WithSound : IOperatorDto
	{
		InputDto Sound { get; set; }
	}
}
