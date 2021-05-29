using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    internal interface ISine_OperatorCalculator_VarFrequency : IOperatorCalculator
    {
        DimensionStack DimensionStack { get; set; }
        IOperatorCalculator FrequencyCalculator { get; set; }
    }
}
