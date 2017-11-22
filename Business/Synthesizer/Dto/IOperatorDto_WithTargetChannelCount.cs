namespace JJ.Business.Synthesizer.Dto
{
	internal interface IOperatorDto_WithTargetChannelCount : IOperatorDto
	{
		int TargetChannelCount { get; set; }
	}
}