namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
	internal interface IOperatorCalculator_VarA_ConstB : IOperatorCalculator
	{
		IOperatorCalculator ACalculator { get; set; }
		double B { get; set; }
	}
}
