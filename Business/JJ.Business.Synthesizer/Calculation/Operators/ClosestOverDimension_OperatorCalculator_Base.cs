using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Copies.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class ClosestOverDimension_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _inputCalculator;
        private readonly OperatorCalculatorBase _collectionCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        protected double _closestItem;

        public ClosestOverDimension_OperatorCalculator_Base(
            OperatorCalculatorBase inputCalculator,
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                inputCalculator,
                collectionCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator
            })
        {
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _inputCalculator = inputCalculator;
            _collectionCalculator = collectionCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        /// <summary> Just returns _aggregate. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _closestItem;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        /// <summary> does nothing </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ResetNonRecursive()
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void RecalculateAggregate()
        {
            double input = _inputCalculator.Calculate();
            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            double step = _stepCalculator.Calculate();

            double length = till - from;
            bool isForward = length > 0.0;

            double position = from;

#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
#if !USE_INVAR_INDICES
            _dimensionStack.Set(position);
#else
            _dimensionStack.Set(_dimensionStackIndex, position);
#endif

            double firstItem = _collectionCalculator.Calculate();

            double smallestDistance = Geometry.AbsoluteDistance(input, firstItem);
            double closestItem = firstItem;

            position += step;

            if (isForward)
            {
                while (position <= till)
                {

#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    double item = _collectionCalculator.Calculate();

                    double distance = Geometry.AbsoluteDistance(input, item);
                    if (smallestDistance > distance)
                    {
                        smallestDistance = distance;
                        closestItem = item;
                    }

                    position += step;
                }
            }
            else
            {
                // Is backwards
                while (position >= till)
                {

#if !USE_INVAR_INDICES
                    _dimensionStack.Set(position);
#else
                    _dimensionStack.Set(_dimensionStackIndex, position);
#endif
                    double item = _collectionCalculator.Calculate();

                    double distance = Geometry.AbsoluteDistance(input, item);
                    if (smallestDistance > distance)
                    {
                        smallestDistance = distance;
                        closestItem = item;
                    }

                    position += step;
                }
            }

            _closestItem = closestItem;
        }
    }
}
