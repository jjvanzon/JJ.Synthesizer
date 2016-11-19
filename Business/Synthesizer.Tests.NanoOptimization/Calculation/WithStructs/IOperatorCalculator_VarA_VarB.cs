using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithStructs
{
    internal interface IOperatorCalculator_VarA_VarB : IOperatorCalculator
    {
        IOperatorCalculator ACalculator { get; set; }
        IOperatorCalculator BCalculator { get; set; }
    }
}
