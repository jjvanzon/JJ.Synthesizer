using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
	/// <summary>
	/// All the complexity in the implementation is because of floating point imprecision problems
	/// trying to get the next tone value.
	/// </summary>
	internal class Tone_SideEffect_SetDefaults_Exponent : Tone_SideEffect_SetDefaults_BaseWithToneValueArray
	{
		/// <inheritdoc />
		public Tone_SideEffect_SetDefaults_Exponent(Tone tone, Tone previousTone)
			: base(
				tone,
				previousTone,
				toneValues: new[]
				{
					0.0 / 12.0,
					1.0 / 12.0,
					2.0 / 12.0,
					3.0 / 12.0,
					4.0 / 12.0,
					5.0 / 12.0,
					6.0 / 12.0,
					7.0 / 12.0,
					8.0 / 12.0,
					9.0 / 12.0,
					10.0 / 12.0,
					11.0 / 12.0
				}) { }
	}
}