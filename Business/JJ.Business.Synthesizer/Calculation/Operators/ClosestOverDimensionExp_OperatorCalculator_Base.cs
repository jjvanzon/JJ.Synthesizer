using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Copies.FromFramework;

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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double input = _inputCalculator.Calculate();

            double valueBefore;
            double valueAfter;

            CollectionHelper.BinarySearchInexact(
                _sortedItems,
                _halfCount,
                _min,
                _max,
                input,
                out valueBefore,
                out valueAfter);

            double inputLog = Math.Log(input);

            double distanceBefore = Geometry.AbsoluteDistance(inputLog, Math.Log(valueBefore));
            double distanceAfter = Geometry.AbsoluteDistance(inputLog, Math.Log(valueAfter));

            if (distanceBefore <= distanceAfter)
            {
                return valueBefore;
            }
            else
            {
                return valueAfter;
            }
        }
    }
}
