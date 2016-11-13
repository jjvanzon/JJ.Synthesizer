using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;

namespace JJ.Demos.Synthesizer.NanoOptimization.Dto
{
    internal class Multiply_OperatorDto_ConstA_ConstB : OperatorDtoBase_ConstA_ConstB
    {
        public override string OperatorTypeName => OperatorNames.Multiply;

        public Multiply_OperatorDto_ConstA_ConstB(double a, double b)
            : base(a, b)
        { }
    }
}
