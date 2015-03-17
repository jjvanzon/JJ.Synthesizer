using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators.Entities
{
    internal class Zero_Calculator : OperatorCalculatorBase
    {
        public override double Calculate(double time, int channelIndex)
        {
            return 0;
        }
    }
}
