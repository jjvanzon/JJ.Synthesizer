//using System;
//using System.Linq;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//	internal class Tone_SideEffect_SetDefaults_Exponent : Tone_SideEffect_SetDefaults_Base
//	{
//		private static double[] _toneValues =
//		{
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
//			11.0 / 12.0,
//			12.0 / 12.0
//		};

//		/// <inheritdoc />
//		public Tone_SideEffect_SetDefaults_Exponent(Tone tone, Tone previousTone)
//			: base(tone, previousTone) { }

//		public override void Execute()
//		{
//			if (_previousTone != null)
//			{
//				_tone.Value = _previousTone.Value + 1.0 / 12.0;
//				_tone.Value = Math.Round(_tone.Value, 13, MidpointRounding.AwayFromZero);
//				_tone.Octave = _previousTone.Octave;

//				if (_tone.Value >= 1.0)
//				{
//					_tone.Value -= 1.0;
//					_tone.Octave++;
//				}
//			}
//			else
//			{
//				_tone.Value = 0.0;
//				_tone.Octave = 1;
//			}
//		}

//		private double GetNextToneValueOrDefault(double previousToneValue)
//		{
//			double nextToneValue = _toneValues.Where(x => x < previousToneValue).LastOrDefault();
//			return nextToneValue;
//		}
//	}
//}