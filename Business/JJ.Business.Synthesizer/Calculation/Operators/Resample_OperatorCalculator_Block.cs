using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;

        private double _t0;
        protected double _x0;

        public Resample_OperatorCalculator_Block(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double samplingRate = _samplingRateCalculator.Calculate(dimensionStack);

            return Calculate(dimensionStack, samplingRate);
        }

        /// <summary> This extra overload prevents additional invokations of the _samplingRateCalculator in derived classes </summary>
        protected double Calculate(DimensionStack dimensionStack, double samplingRate)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double tOffset = time - _t0;
            double sampleCount = tOffset * samplingRate;
            sampleCount = Math.Truncate(sampleCount);

            if (sampleCount != 0.0)
            {
                _t0 += sampleCount / samplingRate;

                dimensionStack.Push(DimensionEnum.Time, _t0);

                _x0 = _signalCalculator.Calculate(dimensionStack);

                dimensionStack.Pop(DimensionEnum.Time);
            }

            return _x0;
        }
    }
}