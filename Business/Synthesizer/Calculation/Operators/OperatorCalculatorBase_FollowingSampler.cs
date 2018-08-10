using System;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class OperatorCalculatorBase_FollowingSampler : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		protected internal readonly OperatorCalculatorBase _signalCalculator;
	    protected readonly OperatorCalculatorBase _samplingRateCalculator;
		protected internal readonly OperatorCalculatorBase _positionCalculator;

	    private double _previousX;
	    private double _passedDx;

        public OperatorCalculatorBase_FollowingSampler(
			OperatorCalculatorBase signalCalculator,
			OperatorCalculatorBase samplingRateCalculator,
			OperatorCalculatorBase positionCalculator,
            params OperatorCalculatorBase[] additionalChildCalculators)
            : base(new[] { signalCalculator, samplingRateCalculator, positionCalculator }.Union(additionalChildCalculators).ToArray())
        {
			_signalCalculator = signalCalculator;
			_samplingRateCalculator = samplingRateCalculator;
			_positionCalculator = positionCalculator;
		}

		public sealed override double Calculate()
		{
			double x = _positionCalculator.Calculate();

            double dx = x - _previousX;
		    _passedDx += dx;

		    double largeDx = GetLargeDx();

            if (_passedDx >= largeDx)
		    {
				ShiftForward();
				SetNextSample();
				Precalculate();

		        _passedDx = 0.0;
			}
            else if (_passedDx <= -largeDx)
			{
				ShiftBackward();
				SetPreviousSample();
				Precalculate();

			    _passedDx = 0.0;
			}

		    _previousX = x;

            return Calculate(x);
		}

		protected abstract void ShiftForward();
		protected abstract void SetNextSample();
		protected abstract void ShiftBackward();
		protected abstract void SetPreviousSample();
		protected abstract void Precalculate();
		protected abstract double Calculate(double x);

		public override void Reset()
		{
			base.Reset();
			ResetNonRecursive();
		}

		protected virtual void ResetNonRecursive()
        {
            double position = _positionCalculator.Calculate();
            _previousX = position;
            _passedDx = 0.0;
        }

        /// <summary> Gets the sampling rate, converts it to an absolute number, ensures a minimum value and returns dx. </summary>
        protected internal double GetLargeDx()
		{
			double samplingRate = _samplingRateCalculator.Calculate();

			samplingRate = Math.Abs(samplingRate);

			if (samplingRate < MINIMUM_SAMPLING_RATE)
			{
				samplingRate = MINIMUM_SAMPLING_RATE;
			}

			return 1.0 / samplingRate;
		}
	}
}
