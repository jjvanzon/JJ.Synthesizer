using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class OperatorDtoToPatchCalculatorCSharpGeneratorResult
    {
        public string GeneratedCode { get; set; }
        public IList<CalculatorVariableInfo> CalculatorVariableInfos { get; set; }
    }
}
