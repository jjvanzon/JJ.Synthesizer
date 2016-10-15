using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal interface IShift_OperatorCalculator_VarSignal_ConstDifference : IOperatorCalculator
    {
        IOperatorCalculator SignalCalculator { get; set; }
        double Distance { get; set; }
        DimensionStack DimensionStack { get; set; }
    }
}
