using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace GeneratedCSharp
{
	public class GeneratedPatchCalculator2 : PatchCalculatorBase
	{
		// Fields

		private double _origin_2;
		private double _origin_9;
		private double _origin_12;
		private double _channel_a_0;

		// Constructor

		public GeneratedPatchCalculator2(
			int samplingRate,
			int channelCount,
			int channelIndex,
			Dictionary<string, ArrayDto> arrayDtoDictionary)
			: base(samplingRate, channelCount, channelIndex)
		    => Reset(time: 0.0);

	    // Calculate

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Calculate(float[] buffer, int frameCount, double startTime)
		{
			double frameDuration = _frameDuration;

			double origin_2 = _origin_2;
			double origin_9 = _origin_9;
			double origin_12 = _origin_12;
			double channel_a_0 = _channel_a_0;

			double time_b_0 = 0.0;

			int valueCount = frameCount * 2;
			time_b_0 = startTime;

			for (int i = 0; i < valueCount; i += 2)
			{
				// Sine (ConstFrequency_WithOriginShifting)
				double phase_3 = (time_b_0 - origin_2) * 440.0;
				double sine_4 = SineCalculator.Sin(phase_3);

				// GreaterThanOrEqual (VarA_ConstB)
				bool greaterthanorequal_5 = sine_4 >= 0.0;

				// If (VarCondition_VarThen_VarElse) (begin)

				// Sine (ConstFrequency_WithOriginShifting)
				double phase_10 = (time_b_0 - origin_9) * 440.0;
				double sine_11 = SineCalculator.Sin(phase_10);

				// If (VarCondition_VarThen_VarElse) (then)
				double then_6 = sine_11;

				// Sine (ConstFrequency_WithOriginShifting)
				double phase_13 = (time_b_0 - origin_12) * 440.0;
				double sine_14 = SineCalculator.Sin(phase_13);

				// Negative (VarNumber)
				double negative_15 = -sine_14;

				// If (VarCondition_VarThen_VarElse) (else)
				double else_7 = negative_15;

				// If (VarCondition_VarThen_VarElse) (end)
				double if_8 = greaterthanorequal_5 ? then_6 : else_7;

				// Accumulate
				double value = if_8;

				if (double.IsNaN(value))
				{
					value = 0;
				}

				float floatValue = (float)value;

				PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);

				time_b_0 += frameDuration;
			}

			_origin_2 = origin_2;
			_origin_9 = origin_9;
			_origin_12 = origin_12;
			_channel_a_0 = channel_a_0;
		}

		// Reset

		public override void Reset(double time)
		{
			double frameDuration = _frameDuration;

			double origin_2 = _origin_2;
			double origin_9 = _origin_9;
			double origin_12 = _origin_12;
			double channel_a_0 = _channel_a_0;

			double time_b_0 = 0.0;

			time_b_0 = time;

			// Initialize Dimensions Values
			channel_a_0 = 0;

			// Sine (ConstFrequency_WithOriginShifting)
			origin_2 = time_b_0;
			double phase_3 = (time_b_0 - origin_2) * 440.0;
			double sine_4 = SineCalculator.Sin(phase_3);

			// GreaterThanOrEqual (VarA_ConstB)
			bool greaterthanorequal_5 = sine_4 >= 0.0;

			// If (VarCondition_VarThen_VarElse) (begin)

			// Sine (ConstFrequency_WithOriginShifting)
			origin_9 = time_b_0;
			double phase_10 = (time_b_0 - origin_9) * 440.0;
			double sine_11 = SineCalculator.Sin(phase_10);

			// If (VarCondition_VarThen_VarElse) (then)
			double then_6 = sine_11;

			// Sine (ConstFrequency_WithOriginShifting)
			origin_12 = time_b_0;
			double phase_13 = (time_b_0 - origin_12) * 440.0;
			double sine_14 = SineCalculator.Sin(phase_13);

			// Negative (VarNumber)
			double negative_15 = -sine_14;

			// If (VarCondition_VarThen_VarElse) (else)
			double else_7 = negative_15;

			// If (VarCondition_VarThen_VarElse) (end)
			double if_8 = greaterthanorequal_5 ? then_6 : else_7;

			_origin_2 = origin_2;
			_origin_9 = origin_9;
			_origin_12 = origin_12;
			_channel_a_0 = channel_a_0;
		}

		// Values

		public override void SetValue(int position, double value) => base.SetValue(position, value);

	    public override void SetValue(DimensionEnum dimensionEnum, double value)
		{
			base.SetValue(dimensionEnum, value);

			switch (dimensionEnum)
			{
				case DimensionEnum.Channel:
					_channel_a_0 = value;
					break;

			}
		}

		public override void SetValue(string name, double value)
		{
			base.SetValue(name, value);

			string canonicalName = NameHelper.ToCanonical(name);

		}

		public override void SetValue(DimensionEnum dimensionEnum, int position, double value)
		{
			base.SetValue(dimensionEnum, position, value);

			switch (dimensionEnum)
			{
				case DimensionEnum.Channel:
					_channel_a_0 = value;
					break;

			}

		}

		public override void SetValue(string name, int position, double value)
		{
			base.SetValue(name, position, value);

			string canonicalName = NameHelper.ToCanonical(name);


		}
	}
}
