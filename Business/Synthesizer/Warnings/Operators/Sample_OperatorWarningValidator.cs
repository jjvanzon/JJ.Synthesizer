using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Sample_OperatorWarningValidator : VersatileValidator
    {
        public Sample_OperatorWarningValidator([NotNull] Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string sampleIDString = DataPropertyParser.TryGetString(op, nameof(Sample_OperatorWrapper.SampleID));

                For(sampleIDString, ResourceFormatter.Sample)
                    .NotNullOrEmpty();
            }
        }
    }
}