using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs
{
    internal interface IMultiply_OperatorDto_VarA_ConstB : IOperatorCalculator
    {
        IOperatorCalculator ACalculator { get; set; }
        double B { get; set; }
    }
}
