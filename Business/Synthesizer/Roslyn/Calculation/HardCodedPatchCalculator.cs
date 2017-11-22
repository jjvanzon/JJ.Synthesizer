using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Roslyn.Calculation
{
	internal class HardCodedPatchCalculator : PatchCalculatorBase
	{
		// Fields

		private double _input0;
		private double _standardDimensionFrequency1;
		private double _customDimensionPrettiness1;
		private double _phase0;
		private double _prevPos0;
		private double _phase1;
		private double _prevPos1;
		private double _phase2;
		private double _prevPos2;
		private double _phase3;
		private double _prevPos3;
		private double _phase4;
		private double _prevPos4;
		private double _phase5;
		private double _prevPos5;
		private double _phase6;
		private double _prevPos6;
		private double _phase7;
		private double _prevPos7;

		private readonly ArrayCalculator_MinPositionZero_Line _curveCalculator32415_1;

		// Constructor

		public HardCodedPatchCalculator(
			int samplingRate, 
			int channelCount, 
			int channelIndex,
			Dictionary<string, double[]> curveArrays,
			Dictionary<string, double> curveRates)
			: base(samplingRate, channelCount, channelIndex)
		{
			_curveCalculator32415_1 = new ArrayCalculator_MinPositionZero_Line(curveArrays["curveCalculator32415_1"], curveRates["curveCalculator32415_1"], valueBefore: 0.0, valueAfter: 0.0);

			Reset(time: 0.0);

			// TODO: Copy defaults from fields to value dictionaries in the base, like SingleChannelPatchCalculator's constructor.
		}

		// Calculate

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Calculate(float[] buffer, int frameCount, double startTime)
		{
			double frameDuration = _frameDuration;
			int channelCount = _channelCount;
			int channelIndex = _channelIndex;
			int valueCount = frameCount * channelCount;

			double input0 = _input0;
			double phase0 = _phase0;
			double prevPos0 = _prevPos0;
			double phase1 = _phase1;
			double prevPos1 = _prevPos1;
			double phase2 = _phase2;
			double prevPos2 = _prevPos2;
			double phase3 = _phase3;
			double prevPos3 = _prevPos3;
			double phase4 = _phase4;
			double prevPos4 = _prevPos4;
			double phase5 = _phase5;
			double prevPos5 = _prevPos5;
			double phase6 = _phase6;
			double prevPos6 = _prevPos6;
			double phase7 = _phase7;
			double prevPos7 = _prevPos7;

			var curveCalculator32415_1 = _curveCalculator32415_1;

			double t0 = startTime;
			double t1;

			// Writes values in an interleaved way to the buffer.
			for (int i = channelIndex; i < valueCount; i += channelCount)
			{
				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase0 += (t1 - prevPos0) * input0;
				prevPos0 = t1;
				double sine1 = SineCalculator.Sin(phase0);

				// Multiply
				double multiply1 = 10 * sine1;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase1 += (t1 - prevPos1) * input0;
				prevPos1 = t1;
				double sine2 = SineCalculator.Sin(phase1);

				// Multiply
				double multiply2 = 10 * sine2;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase2 += (t1 - prevPos2) * input0;
				prevPos2 = t1;
				double sine3 = SineCalculator.Sin(phase2);

				// Multiply
				double multiply3 = 10 * sine3;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase3 += (t1 - prevPos3) * input0;
				prevPos3 = t1;
				double sine4 = SineCalculator.Sin(phase3);

				// Multiply
				double multiply4 = 10 * sine4;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase4 += (t1 - prevPos4) * input0;
				prevPos4 = t1;
				double sine5 = SineCalculator.Sin(phase4);

				// Multiply
				double multiply5 = 10 * sine5;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase5 += (t1 - prevPos5) * input0;
				prevPos5 = t1;
				double sine6 = SineCalculator.Sin(phase5);

				// Multiply
				double multiply6 = 10 * sine6;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase6 += (t1 - prevPos6) * input0;
				prevPos6 = t1;
				double sine7 = SineCalculator.Sin(phase6);

				// Multiply
				double multiply7 = 10 * sine7;

				// Shift
				t1 = t0 + 0.25;

				// Sine
				phase7 += (t1 - prevPos7) * input0;
				prevPos7 = t1;
				double sine8 = SineCalculator.Sin(phase7);

				// Multiply
				double multiply8 = 10 * sine8;

				// Add
				double add1 = multiply8 + multiply7 + multiply6 + multiply5 + multiply4 + multiply3 + multiply2 + multiply1;

				// Curve
				double curve1 = curveCalculator32415_1.Calculate(t1);

				// Multiply
				double multiply9 = add1 * curve1;

				// Accumulate
				double value = multiply9;

				if (double.IsNaN(value)) // winmm will trip over NaN.
				{
					value = 0;
				}

				float floatValue = (float)value; // TODO: This seems unsafe. What happens if the cast is invalid?

				PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

				t0 += frameDuration;
			}

			_input0 = input0;
			_phase0 = phase0;
			_prevPos0 = prevPos0;
			_phase1 = phase1;
			_prevPos1 = prevPos1;
			_phase2 = phase2;
			_prevPos2 = prevPos2;
			_phase3 = phase3;
			_prevPos3 = prevPos3;
			_phase4 = phase4;
			_prevPos4 = prevPos4;
			_phase5 = phase5;
			_prevPos5 = prevPos5;
			_phase6 = phase6;
			_prevPos6 = prevPos6;
			_phase7 = phase7;
			_prevPos7 = prevPos7;
		}

		// Values

		public override void SetValue(int position, double value)
		{
			base.SetValue(position, value);

			switch (position)
			{
				case 0:
					_input0 = value;
					break;
			}
		}

		public override void SetValue(DimensionEnum dimensionEnum, double value)
		{
			base.SetValue(dimensionEnum, value);

			switch (dimensionEnum)
			{
				case DimensionEnum.Frequency:
					_standardDimensionFrequency1 = value;
					break;
			}

			switch (dimensionEnum)
			{
				case DimensionEnum.Frequency:
					_input0 = value;
					break;
			}
		}

		public override void SetValue(string name, double value)
		{
			base.SetValue(name, value);

			string canonicalName = NameHelper.ToCanonical(name);

			if (string.Equals(name, "prettiness", StringComparison.Ordinal))
			{
				_customDimensionPrettiness1 = value;
			}

			if (string.Equals(name, "prettiness", StringComparison.Ordinal))
			{
				_input0 = value;
			}
		}

		public override void SetValue(DimensionEnum dimensionEnum, int position, double value)
		{
			base.SetValue(dimensionEnum, position, value);

			switch (dimensionEnum)
			{
				case DimensionEnum.Frequency:
					_standardDimensionFrequency1 = value;
					break;
			}

			if (dimensionEnum == DimensionEnum.Frequency && position == 0)
			{
				_standardDimensionFrequency1 = value;
			}
		}

		public override void SetValue(string name, int position, double value)
		{
			base.SetValue(name, position, value);

			string canonicalName = NameHelper.ToCanonical(name);

			if (string.Equals(name, "prettiness", StringComparison.Ordinal) && position == 0)
			{
				_customDimensionPrettiness1 = value;
			}

			if (string.Equals(name, "prettiness", StringComparison.Ordinal) && position == 0)
			{
				_input0 = value;
			}
		}

		// Reset

		public override void Reset(double time)
		{
			// TODO: Use time?
			// TODO: Set dimension variables?

			_phase0 = 0.0;
			_prevPos0 = 0.0;
			_phase1 = 0.0;
			_prevPos1 = 0.0;
			_phase2 = 0.0;
			_prevPos2 = 0.0;
			_phase3 = 0.0;
			_prevPos3 = 0.0;
			_phase4 = 0.0;
			_prevPos4 = 0.0;
			_phase5 = 0.0;
			_prevPos5 = 0.0;
			_phase6 = 0.0;
			_prevPos6 = 0.0;
			_phase7 = 0.0;
			_prevPos7 = 0.0;
		}
	}
}