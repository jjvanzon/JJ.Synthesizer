using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class AudioFileOutputWarningValidator : VersatileValidator<AudioFileOutput>
    {
        public AudioFileOutputWarningValidator(AudioFileOutput obj)
            : base(obj)
        { 
            AudioFileOutput audioFileOutput = obj;

            For(() => obj.Outlet, ResourceFormatter.Outlet).NotNull();
            For(() => audioFileOutput.Amplifier, ResourceFormatter.Amplifier).IsNot(0.0);
        }
    }
}