using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings.Entities
{
    public class AudioFileOutputChannelWarningValidator : FluentValidator<AudioFileOutputChannel>
    {
        public AudioFileOutputChannelWarningValidator(AudioFileOutputChannel obj)
            :base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Outlet, PropertyDisplayNames.Outlet)
                .NotNull();
        }
    }
}
