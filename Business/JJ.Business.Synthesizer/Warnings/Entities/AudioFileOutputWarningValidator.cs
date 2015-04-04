using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings.Entities
{
    public class AudioFileOutputWarningValidator : FluentValidator<AudioFileOutput>
    {
        public AudioFileOutputWarningValidator(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            AudioFileOutput audioFileOutput = Object;

            if (audioFileOutput.Amplifier == 0)
            {
                ValidationMessages.Add(() => audioFileOutput.Amplifier, MessageFormatter.ObjectAmplifier0(PropertyDisplayNames.AudioFileOutput, audioFileOutput.Name));
            }

            int i = 1;
            foreach (AudioFileOutputChannel audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                string messageHeader = String.Format("{0} {1}: ", PropertyDisplayNames.Channel, i);
                Execute(new AudioFileOutputChannelWarningValidator(audioFileOutputChannel), messageHeader);

                i++;
            }
        }
    }
}