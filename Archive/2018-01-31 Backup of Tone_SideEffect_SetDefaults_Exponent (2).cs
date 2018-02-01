//using System;
//using JJ.Data.Synthesizer.Entities;
//// ReSharper disable CompareOfFloatsByEqualityOperator

//namespace JJ.Business.Synthesizer.SideEffects
//{
//	/// <summary>
//	/// All the complexity in the implementation is because of floating point imprecision problems
//	/// trying to get the next tone value.
//	/// </summary>
//	internal class Tone_SideEffect_SetDefaults_Exponent : Tone_SideEffect_SetDefaults_Base
//	{
//		/// <inheritdoc />
//		public Tone_SideEffect_SetDefaults_Exponent(Tone tone, Tone previousTone)
//			: base(tone, previousTone) { }

//		public override void Execute()
//		{
//			if (_previousTone != null)
//			{
//				_tone.Value = GetNextToneValueOrDefault(_previousTone.Value);
//				_tone.Octave = _previousTone.Octave;

//				if (_tone.Value == default)
//				{
//					_tone.Value = _toneValues[0];
//					_tone.Octave++;
//				}
//			}
//			else
//			{
//				_tone.Value = _toneValues[0];
//				_tone.Octave = 1;
//			}
//		}

//		private double GetNextToneValueOrDefault(double previousToneValue)
//		{
//			for (int i = 0; i < _toneValues.Length; i++)
//			{
//				double toneValue = _toneValues[i];
//				if (Math.Round(toneValue, 2) > Math.Round(previousToneValue, 2))
//				{
//					return toneValue;
//				}
//			}

//			return default;
//		}

//		private static readonly double[] _toneValues =
//		{
//			0.0 / 12.0,
//			1.0 / 12.0,
//			2.0 / 12.0,
//			3.0 / 12.0,
//			4.0 / 12.0,
//			5.0 / 12.0,
//			6.0 / 12.0,
//			7.0 / 12.0,
//			8.0 / 12.0,
//			9.0 / 12.0,
//			10.0 / 12.0,
//			11.0 / 12.0
//		};
//	}
//}