using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Tone_SideEffect_SetDefaults_Factor : Tone_SideEffect_SetDefaults_BaseWithToneValueArray
	{
		private static readonly double[] _toneValues =
		{
			// perfect unison - do
			1.0 / 1.0,
			// minor second
			16.0 / 15.0,
			// (minor) major second - re
			10.0 / 9.0,
			// (major) major second - re 
			//9.0 / 8.0, // Major second sounded better to me.
			// minor third
			6.0 / 5.0,
			// major third - mi
			5.0 / 4.0,
			// perfect fourth - fa
			4.0 / 3.0,
			// tritone/ augmented fourth / diminished fifth
			MathHelper.SQRT_2, // No simple rational number for the tritone.
			// perfect fifth - so
			3.0 / 2.0,
			// minor sixth
			8.0 / 5.0,
			// major sixth - la
			5.0 / 3.0,
			// minor seventh
			16.0 / 9.0,
			// major seventh - ti
			15.0 / 8.0
		};

		/// <inheritdoc />
		public Tone_SideEffect_SetDefaults_Factor(Tone tone, Tone previousTone)
			: base(tone, previousTone, _toneValues) { }
	}
}