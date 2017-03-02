namespace JJ.Business.Synthesizer.Configuration
{
    public enum CalculationMethodEnum
    {
        Undefined,
        EntityToCalculatorDirectly,
        EntityThruDtoToCalculator,
        Roslyn,
        Roslyn_WithUninlining_WithRefParameters,
        Roslyn_WithUninlining_WithNormalAndOutParameters,
        HardCoded,
        ExampleGeneratedCode
    }
}