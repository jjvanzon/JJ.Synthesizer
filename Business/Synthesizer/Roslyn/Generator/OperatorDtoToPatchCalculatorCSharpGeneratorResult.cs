using System.Collections.Generic;
using JJ.Business.Synthesizer.Roslyn.Helpers;

namespace JJ.Business.Synthesizer.Roslyn.Generator
{
    internal class OperatorDtoToPatchCalculatorCSharpGeneratorResult
    {
        public string GeneratedCode { get; set; }
        public IList<CurveCalculatorVariableInfo> CurveCalculatorVariableInfos { get; set; }
    }
}
