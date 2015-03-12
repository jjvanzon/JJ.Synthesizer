using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.
    internal abstract class OperatorCalculatorBase
    {
        protected OperatorCalculatorBase[] _operands;

        //public abstract double Calculate(double time, int channelIndex);

        // Make virtual now to program some of the Visitor code before programming
        // all the Calculator implementation code.
        public virtual double Calculate(double time, int channelIndex)
        {
            throw new NotImplementedException();
        }
    }
}
