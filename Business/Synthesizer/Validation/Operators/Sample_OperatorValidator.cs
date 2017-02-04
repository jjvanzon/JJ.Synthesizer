using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sample_OperatorValidator : OperatorValidator_Base
    {
        public Sample_OperatorValidator(Operator op)
            : base(
                op,
                OperatorTypeEnum.Sample,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new[] { PropertyNames.SampleID })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (!DataPropertyParser.DataIsWellFormed(op))
            {
                return;
            }

            string sampleIDString = DataPropertyParser.TryGetString(op, PropertyNames.SampleID);

            For(() => sampleIDString, PropertyDisplayNames.SampleID).IsInteger();

            int sampleID;
            if (!int.TryParse(sampleIDString, out sampleID))
            {
                return;
            }

            // Check reference constraint of the Sample.
            // (We are quite tollerant here: we omit the check if it is not in a patch or document.)
            bool mustCheckReference = op.Patch?.Document != null;
            if (!mustCheckReference)
            {
                return;
            }

            IEnumerable<Sample> samples = op.Patch.Document.Samples;

            bool isInList = samples.Any(x => x.ID == sampleID);

            if (!isInList)
            {
                ValidationMessages.AddNotInListMessage(PropertyNames.Sample, PropertyDisplayNames.Sample, sampleID);
            }
        }
    }
}