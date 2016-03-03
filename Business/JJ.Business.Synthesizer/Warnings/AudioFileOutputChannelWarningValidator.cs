using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class AudioFileOutputChannelWarningValidator : FluentValidator<AudioFileOutputChannel>
    {
        public AudioFileOutputChannelWarningValidator(AudioFileOutputChannel obj)
            :base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Outlet, PropertyDisplayNames.Outlet).NotNull();
        }
    }
}
