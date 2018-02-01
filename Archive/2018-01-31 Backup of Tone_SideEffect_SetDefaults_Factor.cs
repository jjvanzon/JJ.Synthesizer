//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//	internal class Tone_SideEffect_SetDefaults_Factor : Tone_SideEffect_SetDefaults_Base
//	{
//		/// <inheritdoc />
//		public Tone_SideEffect_SetDefaults_Factor(Tone tone, Tone previousTone)
//			: base(tone, previousTone) { }

//		public override void Execute()
//		{
//			if (_previousTone != null)
//			{
//				_tone.Value = _previousTone.Value;
//				_tone.Octave = _previousTone.Octave;
//			}
//			else
//			{
//				_tone.Value = 1.0;
//				_tone.Octave = 1;
//			}
//		}
//	}
//}