using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class SampleChannelValidator : FluentValidator<SampleChannel>
    {
        public SampleChannelValidator(SampleChannel obj)
            : base(obj)
        { }
        
        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            SampleChannel sampleChannel = Object;

            For(() => sampleChannel.Sample, PropertyDisplayNames.Sample)
                .NotNull();

            For(() => sampleChannel.Channel, PropertyDisplayNames.Channel)
                .NotNull();

            if (sampleChannel.Sample != null &&
                sampleChannel.Sample.SpeakerSetup != null &&
                sampleChannel.Channel != null)
            {
                int[] allowedChannelIDs = sampleChannel.Sample.SpeakerSetup.SpeakerSetupChannels.Select(x => x.Channel.ID).ToArray();
                if (!allowedChannelIDs.Contains(sampleChannel.Channel.ID))
                {
                    string message = MessagesFormatter.ChannelNotAllowedForSpeakerSetup(sampleChannel.Channel.Name, sampleChannel.Sample.SpeakerSetup.Name);
                    ValidationMessages.Add(() => sampleChannel.Channel.ID, message);
                }
            };
        }
    }
}