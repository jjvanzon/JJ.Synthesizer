using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class CalculatorVariableInfo
    {
        public string NameCamelCase { get; set; }
        public string TypeName { get; set; }
        public ArrayCalculatorBase Calculator { get; set; }
    }
}
