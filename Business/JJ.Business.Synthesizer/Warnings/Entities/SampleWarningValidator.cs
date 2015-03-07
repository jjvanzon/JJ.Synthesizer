using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings.Entities
{
    public class SampleWarningValidator : FluentValidator<Sample>
    {
        public SampleWarningValidator(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            if (Object.Amplifier == 0)
            {
                ValidationMessages.Add(() => Object.Amplifier, MessagesFormatter.ObjectAmplifier0(PropertyDisplayNames.Sample, Object.Name));
            }

            if (!Object.IsActive)
            {
                ValidationMessages.Add(() => Object.Amplifier, MessagesFormatter.SampleNotActive(Object.Name));
            }

            int i = 1;
            foreach (SampleChannel sampleChannel in Object.SampleChannels)
            {
                string messageHeading = String.Format("{0} {1}: ", PropertyDisplayNames.SampleChannel, i);
                Execute(new SampleChannelWarningValidator(sampleChannel), messageHeading);

                i++;
            }   
        }
    }
}
