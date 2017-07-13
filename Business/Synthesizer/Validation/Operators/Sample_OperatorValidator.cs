using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sample_OperatorValidator : OperatorValidator_WithUnderlyingPatch
    {
        public Sample_OperatorValidator(Operator op)
            : base(
                op,
                expectedDataKeys: new[] { nameof(Sample_OperatorWrapper.SampleID) })
        { 
            if (!DataPropertyParser.DataIsWellFormed(op))
            {
                return;
            }

            string sampleIDString = DataPropertyParser.TryGetString(op, nameof(Sample_OperatorWrapper.SampleID));

            For(sampleIDString, CommonResourceFormatter.ID_WithName(ResourceFormatter.Sample)).IsInteger();

            if (!int.TryParse(sampleIDString, out int sampleID))
            {
                return;
            }

            // Check reference constraint of the Sample.
            // (We are quite tolerant here: we omit the check if it is not in a patch or document.)
            bool mustCheckReference = op.Patch?.Document != null;
            if (!mustCheckReference)
            {
                return;
            }

            IEnumerable<Sample> samples = op.Patch.Document.Samples;

            bool isInList = samples.Any(x => x.ID == sampleID);

            if (!isInList)
            {
                Messages.AddNotInListMessage(ResourceFormatter.Sample, sampleID);
            }
        }
    }
}