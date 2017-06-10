using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Sample_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Sample_OperatorWarningValidator(Operator op)
            : base(op)
        { 
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string sampleIDString = DataPropertyParser.TryGetString(op, nameof(Sample_OperatorWrapper.SampleID));

                For(() => sampleIDString, ResourceFormatter.Sample)
                    .NotNullOrEmpty();
            }
        }
    }
}