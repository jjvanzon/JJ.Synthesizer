using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class SampleWarningValidator : FluentValidator<Sample>
    {
        public SampleWarningValidator(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object.Amplifier == 0)
            {
                ValidationMessages.Add(() => Object.Amplifier, MessageFormatter.ObjectAmplifier0(PropertyDisplayNames.Sample, Object.Name));
            }

            if (!Object.IsActive)
            {
                ValidationMessages.Add(() => Object.Amplifier, MessageFormatter.SampleNotActive(Object.Name));
            }

            if (Object.Bytes == null)
            {
                ValidationMessages.Add(() => Object.Bytes, MessageFormatter.SampleNotLoaded(Object.Name));
            }
            else if (Object.Bytes.Length == 0)
            {
                ValidationMessages.Add(() => Object.Bytes.Length, MessageFormatter.SampleCount0(Object.Name));
            }
        }
    }
}
