using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs
{
    internal interface IShift_OperatorCalculator_VarSignal_ConstDistance : IOperatorCalculator
    {
        IOperatorCalculator SignalCalculator { get; set; }
        double Distance { get; set; }
        DimensionStack DimensionStack { get; set; }
    }
}
