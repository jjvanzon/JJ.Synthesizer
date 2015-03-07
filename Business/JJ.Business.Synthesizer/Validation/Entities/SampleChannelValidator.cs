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

            For(() => sampleChannel.ChannelType, PropertyDisplayNames.ChannelType)
                .NotNull();

            if (sampleChannel.Sample != null &&
                sampleChannel.Sample.ChannelSetup != null &&
                sampleChannel.ChannelType != null)
            {
                int[] allowedChannelTypeIDs = sampleChannel.Sample.ChannelSetup.ChannelSetupChannelTypes.Select(x => x.ChannelType.ID).ToArray();
                if (!allowedChannelTypeIDs.Contains(sampleChannel.ChannelType.ID))
                {
                    string message = MessagesFormatter.ChannelTypeNotAllowedForChannelSetup(sampleChannel.ChannelType.Name, sampleChannel.Sample.ChannelSetup.Name);
                    ValidationMessages.Add(() => sampleChannel.ChannelType.ID, message);
                }
            };
        }
    }
}