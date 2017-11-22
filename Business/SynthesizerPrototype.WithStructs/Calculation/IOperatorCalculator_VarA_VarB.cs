namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
	internal interface IOperatorCalculator_VarA_VarB : IOperatorCalculator
	{
		IOperatorCalculator ACalculator { get; set; }
		IOperatorCalculator BCalculator { get; set; }
	}
}
