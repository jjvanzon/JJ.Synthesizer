namespace JJ.Business.Synthesizer.Dto
{
	internal abstract class OperatorDtoBase_WithSound : OperatorDtoBase, IOperatorDto_WithSound
	{
		public InputDto Sound { get; set; }
	}
}
