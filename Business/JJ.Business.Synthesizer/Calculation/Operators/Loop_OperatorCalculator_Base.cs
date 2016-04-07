using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class Loop_OperatorCalculator_Base : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        protected double _origin;

        public Loop_OperatorCalculator_Base(
            OperatorCalculatorBase signalCalculator,
            IList<OperatorCalculatorBase> childOperatorCalculators)
            : base(childOperatorCalculators)
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
        }

        protected abstract double? TransformTime(double outputTime, int channelIndex);

        public override void Reset(double time, int channelIndex)
        {
            _origin = time;

            double? tranformedTime = TransformTime(time, channelIndex);
            if (!tranformedTime.HasValue)
            {
                // TODO: There is no meaningful value to fall back to.
                // What to do?
                tranformedTime = time;
            }

            base.Reset(tranformedTime.Value, channelIndex);
        }

        public override double Calculate(double time, int channelIndex)
        {
            double? transformedTime = TransformTime(time, channelIndex);
            if (!transformedTime.HasValue)
            {
                return 0;
            }

            double value = _signalCalculator.Calculate(transformedTime.Value, channelIndex);
            return value;
        }
    }
}