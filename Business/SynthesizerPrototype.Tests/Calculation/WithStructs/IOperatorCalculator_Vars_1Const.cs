using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.SynthesizerPrototype.Tests.Calculation.WithStructs
{
    internal interface IOperatorCalculator_Vars_1Const : IOperatorCalculator
    {
        double ConstValue { get; set; }
        void SetVarCalculator(int i, IOperatorCalculator varOperatorCalculator);
    }
}
