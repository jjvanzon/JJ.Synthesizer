using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // You could imagine many more optimized calculations, such as first item is const and several,
    // that omit the loop, but future optimizations will just make that work obsolete again.

    internal class AverageOverInlets_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _firstItemCalculator;
        private readonly OperatorCalculatorBase[] _remainingItemCalculators;
        private readonly double _remainingItemCalculatorsCount;
        private readonly double _count;

        public AverageOverInlets_OperatorCalculator(IList<OperatorCalculatorBase> itemCalculators)
            : base(itemCalculators)
        {
            if (itemCalculators == null) throw new NullException(() => itemCalculators);
            if (itemCalculators.Count == 0) throw new CollectionEmptyException(() => itemCalculators);

            _firstItemCalculator = itemCalculators.First();
            _remainingItemCalculators = itemCalculators.Skip(1).ToArray();
            _remainingItemCalculatorsCount = _remainingItemCalculators.Length;
            _count = itemCalculators.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double sum = _firstItemCalculator.Calculate();

            for (int i = 0; i < _remainingItemCalculatorsCount; i++)
            {
                double value = _remainingItemCalculators[i].Calculate();
                sum += value;
            }

            return sum / _count;
        }
    }
}
