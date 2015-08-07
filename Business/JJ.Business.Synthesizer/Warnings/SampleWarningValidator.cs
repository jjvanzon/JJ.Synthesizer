using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Warnings
{
    public class SampleWarningValidator : FluentValidator<Sample>
    {
        private HashSet<object> _alreadyDone;

        public SampleWarningValidator(Sample obj, HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Object))
            {
                return;
            }
            _alreadyDone.Add(Object);

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
