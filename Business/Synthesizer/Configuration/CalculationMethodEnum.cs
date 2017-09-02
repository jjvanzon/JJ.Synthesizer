namespace JJ.Business.Synthesizer.Configuration
{
    public enum CalculationMethodEnum
    {
        Undefined,
        CalculatorClasses,
        Roslyn,
        Roslyn_WithUninlining_WithRefParameters,
        Roslyn_WithUninlining_WithNormalAndOutParameters,
        HardCoded,
        ExampleGeneratedCode
    }
}