using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioOutputValidator : FluentValidator<AudioOutput>
    {
        public AudioOutputValidator(AudioOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(Object.GetSpeakerSetupEnum(), PropertyNames.SpeakerSetup, PropertyDisplayNames.SpeakerSetup)
                .IsEnum<SpeakerSetupEnum>()
                .IsNot(SpeakerSetupEnum.Undefined);

            For(() => Object.SamplingRate, PropertyDisplayNames.SamplingRate).GreaterThan(0);
            For(() => Object.SpeedFactor, PropertyDisplayNames.SpeedFactor).IsNot(0);
            For(() => Object.MaxConcurrentNotes, PropertyDisplayNames.MaxConcurrentNotes).GreaterThan(0);
            For(() => Object.BufferDuration, PropertyDisplayNames.MaxConcurrentNotes).GreaterThan(0);
        }
    }
}