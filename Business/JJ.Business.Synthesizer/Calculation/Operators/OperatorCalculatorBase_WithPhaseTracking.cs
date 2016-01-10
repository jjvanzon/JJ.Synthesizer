using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.
    internal abstract class OperatorCalculatorBase_WithPhaseTracking : OperatorCalculatorBase
    {
        protected double _phase;
        protected double _previousTime;

        public sealed override void ResetPhase()
        {
            _phase = 0.0;
            _previousTime = 0.0;

            base.ResetPhase();
        }
    }
}
