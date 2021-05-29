using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal abstract class Tone_SideEffect_SetDefaults_Base : ISideEffect
    {
        protected readonly Tone _tone;
        protected readonly Tone _previousTone;

        /// <param name="previousTone">nullable</param>
        public Tone_SideEffect_SetDefaults_Base(Tone tone, Tone previousTone)
        {
            _tone = tone ?? throw new ArgumentNullException(nameof(tone));
            _previousTone = previousTone;
        }

        public abstract void Execute();
    }
}