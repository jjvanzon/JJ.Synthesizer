using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class SampleWarningValidator : VersatileValidator
    {
        /// <param name="bytes">nullable</param>
        public SampleWarningValidator(Sample sample, byte[] bytes, HashSet<object> alreadyDone)
        {
            if (sample == null) throw new NullException(() => sample);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();


            if (alreadyDone.Contains(sample))
            {
                return;
            }
            alreadyDone.Add(sample);

            For(sample.Amplifier, ResourceFormatter.Amplifier).IsNot(0.0);

            if (!sample.IsActive)
            {
                Messages.Add(ResourceFormatter.NotActive);
            }

            if (bytes == null)
            {
                Messages.Add(ResourceFormatter.NotLoaded);
            }
            else if (bytes.Length == 0)
            {
                Messages.Add(ValidationResourceFormatter.IsZero(CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Samples)));
            }
        }
    }
}
