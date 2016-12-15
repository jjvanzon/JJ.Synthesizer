// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class SumOverDimension_OperatorCalculator_ByUnbundleAndAdd : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _addCalculator;

        public SumOverDimension_OperatorCalculator_ByUnbundleAndAdd(
            OperatorCalculatorBase signalCalculator,
            double till,
            DimensionStack dimensionStack)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            if (!ConversionHelper.CanCastToNonNegativeInt32(till))
            {
                throw new Exception(String.Format("_till '{0}' cannot be casted to non-negative Int32", till));
            }
            int tillInt = (int)till;

            // HACK in a piece of patch to unbundle and add.

            var unbundleCalculators = new List<OperatorCalculatorBase>(tillInt);
            for (int i = 0; i <= tillInt; i++)
            {
                var unbundleCalculator = new Unbundle_OperatorCalculator(signalCalculator, i, dimensionStack);
                unbundleCalculators.Add(unbundleCalculator);
            }

            _addCalculator = OperatorCalculatorFactory.CreateAddCalculator_Vars(unbundleCalculators);
        }

        public override double Calculate()
        {
            return _addCalculator.Calculate();
        }
    }
}