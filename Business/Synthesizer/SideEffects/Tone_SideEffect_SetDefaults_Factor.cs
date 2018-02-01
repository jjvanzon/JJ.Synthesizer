using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Tone_SideEffect_SetDefaults_Factor : Tone_SideEffect_SetDefaults_BaseWithToneValueArray
	{
		/// <inheritdoc />
		public Tone_SideEffect_SetDefaults_Factor(Tone tone, Tone previousTone)
			: base(
				tone,
				previousTone,
				toneValues: new[]
				{
					1.0 / 1.0, // perfect unison - do
					16.0 / 15.0, // minor second
					//10.0 / 9.0, // (minor) major second - re
					9.0 / 8.0, // (major) major second - re
					6.0 / 5.0, // minor third
					5.0 / 4.0, // major third - mi
					4.0 / 3.0, // perfect fourth - fa
					3.0 / 2.0, // perfect fifth - so
					8.0 / 5.0, // minor sixth
					5.0 / 3.0, // major sixth - la
					// TODO: Minor seventh
					15.0 / 8.0 // major seventh - ti
				}) { }
	}
}