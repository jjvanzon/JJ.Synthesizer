
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal interface IOperatorCalculator_VarA_ConstB : IOperatorCalculator
    {
        IOperatorCalculator ACalculator { get; set; }
        double B { get; set; }
    }
}
