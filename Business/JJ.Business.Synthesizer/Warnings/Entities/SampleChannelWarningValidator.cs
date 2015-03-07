using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Warnings.Entities
{
    public class SampleChannelWarningValidator : FluentValidator<SampleChannel>
    {
        public SampleChannelWarningValidator(SampleChannel obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            if (Object.RawBytes == null)
            {
                ValidationMessages.Add(() => Object.RawBytes, MessagesFormatter.SampleChannelNotLoaded(GetSampleName(), GetChannelName()));
            }
            else if (Object.RawBytes.Length == 0)
            {
                ValidationMessages.Add(() => Object.RawBytes.Length, MessagesFormatter.SampleCount0(GetSampleName(), GetChannelName()));
            }
        }

        // For warnings I need null-tollerance.

        private string GetSampleName()
        {
            string sampleName = Object.Sample != null ? Object.Sample.Name : null;
            return sampleName;
        }

        private string GetChannelName()
        {
            string channelName = Object.Channel != null ? Object.Channel.Name : null;
            return channelName;
        }
    }
}
