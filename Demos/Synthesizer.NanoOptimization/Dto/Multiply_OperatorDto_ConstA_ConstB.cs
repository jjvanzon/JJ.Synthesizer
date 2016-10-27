using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto_ConstA_ConstB : OperatorDto_ConstA_ConstB
    {
        public override string OperatorName => OperatorNames.Multiply;

        public Multiply_OperatorDto_ConstA_ConstB(double a, double b)
            : base(a, b)
        { }
    }
}
