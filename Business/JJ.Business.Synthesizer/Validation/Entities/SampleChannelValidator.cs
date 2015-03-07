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
        private Channel _expectedChannel;

        public SampleChannelValidator(SampleChannel obj, Channel expectedChannel)
            : base(obj, postponeExecute: true)
        {
            _expectedChannel = expectedChannel;

            Execute();
        }
        
        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            SampleChannel sampleChannel = Object;

            For(() => sampleChannel.Sample, PropertyDisplayNames.Sample)
                .NotNull();

            For(() => sampleChannel.Channel, PropertyDisplayNames.Channel)
                .NotNull();

            // Check if the channel matches the expected channel.
            if (sampleChannel.Sample != null &&
                sampleChannel.Channel != null &&
                sampleChannel.Sample.SpeakerSetup != null)
            {
                if (sampleChannel.Channel.ID != _expectedChannel.ID)
                {
                    string message = MessagesFormatter.ChannelMustBeXForSpeakerSetupY(_expectedChannel.Name, sampleChannel.Sample.SpeakerSetup.Name);
                    ValidationMessages.Add(() => sampleChannel.Channel.ID, message);
                }
            };
        }
    }
}