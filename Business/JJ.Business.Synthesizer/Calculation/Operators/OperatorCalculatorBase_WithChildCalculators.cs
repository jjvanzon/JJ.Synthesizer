using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Dispatch through a base class is faster than using an interface.

    internal abstract class OperatorCalculatorBase_WithChildCalculators : OperatorCalculatorBase 
    {
        private readonly OperatorCalculatorBase[] _childOperatorCalculators;

        public OperatorCalculatorBase_WithChildCalculators(IList<OperatorCalculatorBase> childOperatorCalculators)
        {
            if (childOperatorCalculators == null) throw new NullException(() => childOperatorCalculators);
            if (childOperatorCalculators.Count < 1) throw new LessThanException(() => childOperatorCalculators.Count, 1);
            if (childOperatorCalculators.Contains(null)) throw new HasNullsException(() => childOperatorCalculators);

            _childOperatorCalculators = childOperatorCalculators.ToArray();
        }

        /// <summary> Base implementation resets the state of the ChildOperatorCalculators. </summary>
        public override void Reset()
        {
            for (int i = 0; i < _childOperatorCalculators.Length; i++)
            {
                OperatorCalculatorBase childOperatorCalculator = _childOperatorCalculators[i];
                childOperatorCalculator.Reset();
            }
        }
    }
}
