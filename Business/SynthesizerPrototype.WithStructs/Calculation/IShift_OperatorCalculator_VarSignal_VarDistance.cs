using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
	internal interface IShift_OperatorCalculator_VarSignal_VarDistance : IOperatorCalculator
	{
		IOperatorCalculator SignalCalculator { get; set; }
		IOperatorCalculator DistanceCalculator { get; set; }
		DimensionStack DimensionStack { get; set; }
	}
}
