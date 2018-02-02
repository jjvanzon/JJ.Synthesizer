namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal interface IOperatorDto_WithTargetChannelCount : IOperatorDto
	{
		int TargetChannelCount { get; set; }
	}
}