using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs
{
    internal interface IOperatorCalculator_VarA_VarB : IOperatorCalculator
    {
        IOperatorCalculator ACalculator { get; set; }
        IOperatorCalculator BCalculator { get; set; }
    }
}
