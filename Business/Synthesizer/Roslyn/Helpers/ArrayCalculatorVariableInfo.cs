using System;
using JJ.Business.Synthesizer.Calculation.Arrays;

namespace JJ.Business.Synthesizer.Roslyn.Helpers
{
    internal class ArrayCalculatorVariableInfo
    {
        public string NameCamelCase { get; set; }
        [Obsolete("Check if this is used anymore. If not: remove member.")]
        public string TypeName { get; set; }
        public ArrayCalculatorBase Calculator { get; set; }
    }
}
