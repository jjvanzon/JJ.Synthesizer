using System;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
	internal abstract class Interpolate_OperatorCalculator_Base_1stAttempt
        : OperatorCalculatorBase_WithChildCalculators
	{
		private const double MINIMUM_SAMPLING_RATE = 1.0 / 60.0; // Once a minute

		protected internal readonly OperatorCalculatorBase _signalCalculator;
		protected readonly OperatorCalculatorBase _samplingRateCalculator;
		protected internal readonly OperatorCalculatorBase _positionCalculator;

	    private double _previousX;
	    private double _passedDx;

        public Interpolate_OperatorCalculator_Base_1stAttempt(
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

		public /*sealed*/ override double Calculate()
		{
			double x = _positionCalculator.Calculate();

			// TODO: What if _x0 or _x1 are way off? How will it correct itself?
			if (MustShiftForward(x))
			{
				ShiftForward();
				SetNextSample();
				Precalculate();
			}
			else if (MustShiftBackward(x))
			{
				ShiftBackward();
				SetPreviousSample();
				Precalculate();
			}

			return Calculate(x);
		}

		protected virtual bool MustShiftForward(double x)
        {
            double dx = x - _previousX;
            if (dx >= 0)
            {
                _passedDx += dx;
            }
            else
            {
                // Substitute for Math.Abs().
                // This makes it work for changes that go in reverse and even position changes that quickly goes back and forth.
                _passedDx -= dx;
            }

            return _passedDx >= SlowDx();
        }

	    protected abstract void ShiftForward();

	    protected virtual void SetNextSample() => _passedDx = 0.0;

        protected virtual bool MustShiftBackward(double x)
        {
            double dx = x - _previousX;
            if (dx >= 0)
            {
                _passedDx += dx;
            }
            else
            {
                // Substitute for Math.Abs().
                // This makes it work for changes that go in reverse and even position changes that quickly goes back and forth.
                _passedDx -= dx;
            }

            return _passedDx >= SlowDx();
        }

        protected abstract void ShiftBackward();
		protected abstract void SetPreviousSample();

		protected abstract void Precalculate();

        /// <summary>
        /// Base only sets some internal variables.
        /// You are going to have to calculate in a derived class.
        /// </summary>
	    protected virtual double Calculate(double x)
        {
            // Phase tracking
            _previousX = x;

            return default;
        }

        public override void Reset()
		{
			base.Reset();
			ResetNonRecursive();
		}

		protected abstract void ResetNonRecursive();
        
		/// <summary> Gets the sampling rate, converts it to an absolute number, ensures a minimum value and returns dx. </summary>
		protected internal double SlowDx()
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
