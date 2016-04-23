using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class AudioFileOutputWarningValidator : FluentValidator<AudioFileOutput>
    {
        public AudioFileOutputWarningValidator(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            AudioFileOutput audioFileOutput = Object;

            For(() => Object.Outlet, PropertyDisplayNames.Outlet).NotNull();
            For(() => audioFileOutput.Amplifier, PropertyDisplayNames.Amplifier).IsNot(0.0);
        }
    }
}