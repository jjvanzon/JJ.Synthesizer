using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Tone_SideEffect_SetDefaults_LiteralFrequency : Tone_SideEffect_SetDefaults_Base
	{
		/// <inheritdoc />
		public Tone_SideEffect_SetDefaults_LiteralFrequency(Tone tone, Tone previousTone)
			: base(tone, previousTone) { }

		public override void Execute()
		{
			if (_previousTone != null)
			{
				_tone.Value = _previousTone.Value * Math.Pow(2.0, 1.0 / 12.0);
				_tone.Octave = _tone.GetCalculatedOctave();
			}
			else
			{
				// Start at Octave 1, not 0, by multiplying base frequency times 2.
				_tone.Value = _tone.Scale.BaseFrequency * 2;
				_tone.Octave = _tone.GetCalculatedOctave();
			}
		}
	}
}