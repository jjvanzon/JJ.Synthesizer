using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class ClosestOverDimensionExp_OperatorCalculator_Base : ClosestOverDimension_OperatorCalculator_Base
    {
        public ClosestOverDimensionExp_OperatorCalculator_Base(
            OperatorCalculatorBase inputCalculator, 
            OperatorCalculatorBase collectionCalculator, 
            OperatorCalculatorBase fromCalculator, 
            OperatorCalculatorBase tillCalculator, 
            OperatorCalculatorBase stepCalculator, 
            DimensionStack dimensionStack) 
            : base(
                  inputCalculator, 
                  collectionCalculator, 
                  fromCalculator, 
                  tillCalculator, 
                  stepCalculator, 
                  dimensionStack)
        { }

        protected override void RecalculateCollection()
        {
            base.RecalculateCollection();

            for (int i = 0; i < _sortedItems.Length; i++)
            {
                _sortedItems[i] = Math.Log(_sortedItems[i]);
            }

            _min = Math.Log(_min);
            _max = Math.Log(_max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double input = _inputCalculator.Calculate();
            double logInput = Math.Log(input);

            double logValueBefore;
            double logValueAfter;

            // Fields are log'ed already.
            CollectionHelper.BinarySearchInexact(
                _sortedItems,
                _halfCount,
                _min,
                _max,
                logInput,
                out logValueBefore,
                out logValueAfter);

            double logDistanceBefore = Geometry.AbsoluteDistance(logInput, logValueBefore);
            double logDistanceAfter = Geometry.AbsoluteDistance(logInput, logValueAfter);

            if (logDistanceBefore <= logDistanceAfter)
            {
                return Math.Exp(logValueBefore);
            }
            else
            {
                return Math.Exp(logValueAfter);
            }
        }
    }
}
