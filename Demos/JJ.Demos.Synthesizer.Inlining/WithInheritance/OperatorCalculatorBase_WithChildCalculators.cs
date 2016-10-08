using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.WithInheritance
{
    //[Obsolete("I probably do not need this base class for my testing purposes. But is might be handy for copying code out of the business layer.")]
    internal abstract class OperatorCalculatorBase_WithChildCalculators : OperatorCalculatorBase
    {
        protected OperatorCalculatorBase[] _childOperatorCalculators;
        private readonly int _childOperatorCalculatorsCount;

        public OperatorCalculatorBase_WithChildCalculators(IList<OperatorCalculatorBase> childOperatorCalculators)
        {
            if (childOperatorCalculators == null) throw new NullException(() => childOperatorCalculators);
            if (childOperatorCalculators.Count < 1) throw new LessThanException(() => childOperatorCalculators.Count, 1);
            if (childOperatorCalculators.Contains(null)) throw new HasNullsException(() => childOperatorCalculators);

            _childOperatorCalculators = childOperatorCalculators.ToArray();
            _childOperatorCalculatorsCount = _childOperatorCalculators.Length;
        }
    }
}
