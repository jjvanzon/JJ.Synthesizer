using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Round_VarStep_VarOffSet_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        private readonly OperatorCalculatorBase _offsetCalculator;

        public Round_VarStep_VarOffSet_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase offsetCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, stepCalculator, offsetCalculator })
        {
            // TODO: Enable code lines as soon as there are specialized calculators.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(stepCalculator);
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(offsetCalculator);

            _signalCalculator = signalCalculator;
            _stepCalculator = stepCalculator;
            _offsetCalculator = offsetCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double signal = _signalCalculator.Calculate(time, channelIndex);
            double step = _stepCalculator.Calculate(time, channelIndex);
            double offset = _offsetCalculator.Calculate(time, channelIndex);

            double result = Maths.RoundWithStep(signal, step, offset);
            return result;
        }
    }
}