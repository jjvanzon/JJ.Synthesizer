using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithStructs
{
    internal interface IShift_OperatorCalculator_VarSignal_VarDistance : IOperatorCalculator
    {
        IOperatorCalculator SignalCalculator { get; set; }
        IOperatorCalculator DistanceCalculator { get; set; }
        DimensionStack DimensionStack { get; set; }
    }
}
