using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Dto.Operators
{
	internal interface IOperatorDto_WithInterpolation_AndLookAheadOrLagBehind : IOperatorDto_WithInterpolation
	{
		LookAheadOrLagBehindEnum LookAheadOrLagBehindEnum { get; set; }
	}
}