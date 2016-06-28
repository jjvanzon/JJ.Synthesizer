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

        protected override void RecalculateCollection()
        {
            base.RecalculateCollection();

            for (int i = 0; i < _sortedItems.Length; i++)
            {
                _sortedItems[i] = Log(_sortedItems[i]);
            }

            _min = Log(_min);
            _max = Log(_max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            // By pre-calculating logs and doing it base 2 I can do:
            // 2 logs, 1 div, 1 mul

            // If I would go base e, I also could have gone for:
            // 1 log e, 1 exp
            // Which might not be faster. Exp isn't just 1 instruction either.

            // If I would not pre-calculate logs, it would have been:
            // 6 logs, 2 array indexers.
            // (Each not-base-e log will be performed as 2 logs by .NET.
            //  Perhaps it is time to start writing my own code for this.)

            double input = _inputCalculator.Calculate();
            double logInput = Log(input);

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
                return Pow(logValueBefore);
            }
            else
            {
                return Pow(logValueAfter);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double Log(double value)
        {
            // Effectively performace 2 logs and a division.
            return Math.Log(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double Pow(double value)
        {
            return Math.Exp(value);
        }
    }
}
