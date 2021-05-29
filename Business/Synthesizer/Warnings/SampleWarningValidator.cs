using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.ResourceStrings;
using JJ.Framework.Validation;
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

            if (!alreadyDone.Add(sample))
            {
                return;
            }

            For(sample.Amplifier, ResourceFormatter.Amplifier).NotZero();


            if (bytes?.Length == 0)
            {
                Messages.Add(ValidationResourceFormatter.IsZero(CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Samples)));
            }
        }
    }
}
