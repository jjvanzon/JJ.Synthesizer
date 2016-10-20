using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{
    internal interface ISine_OperatorCalculator_VarFrequency : IOperatorCalculator
    {
        DimensionStack DimensionStack { get; set; }
        IOperatorCalculator FrequencyCalculator { get; set; }
    }
}
