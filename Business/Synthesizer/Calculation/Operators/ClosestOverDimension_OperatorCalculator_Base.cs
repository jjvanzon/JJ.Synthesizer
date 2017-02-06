using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Collections;
using CollectionHelper = JJ.Business.Synthesizer.CopiedCode.FromFramework.CollectionHelper;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class ClosestOverDimension_OperatorCalculator_Base
        : SortOverDimension_OperatorCalculator_Base
    {
        protected readonly OperatorCalculatorBase _inputCalculator;

        protected double _min;
        protected double _max;
        protected int _halfCount;

        public ClosestOverDimension_OperatorCalculator_Base(
            OperatorCalculatorBase inputCalculator,
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(collectionCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        {
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _inputCalculator = inputCalculator;

            _childOperatorCalculators = _childOperatorCalculators.Union(inputCalculator).ToArray();

            // ReSharper disable once VirtualMemberCallInConstructor
            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double input = _inputCalculator.Calculate();

            double valueBefore;
            double valueAfter;

            CollectionHelper.BinarySearchInexact(
                _samples,
                _halfCount,
                _min,
                _max,
                input,
                out valueBefore,
                out valueAfter);

            double distanceBefore = Geometry.AbsoluteDistance(input, valueBefore);
            double distanceAfter = Geometry.AbsoluteDistance(input, valueAfter);

            return distanceBefore <= distanceAfter ? valueBefore : valueAfter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void RecalculateCollection()
        {
            base.RecalculateCollection();

            if (_count != 0)
            {
                _min = _samples[0];
                _max = _samples[_count - 1];
                _halfCount = _count >> 1;
            }
            else
            {
                _min = 0;
                _max = 0;
                _halfCount = 0;
            }
        }
    }
}