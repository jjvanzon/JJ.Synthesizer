using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
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
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal },
                expectedDataKeys: new string[] { PropertyNames.SampleID })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string sampleIDString = DataPropertyParser.TryGetString(op, PropertyNames.SampleID);

                For(() => sampleIDString, PropertyDisplayNames.SampleID).IsInteger();

                int sampleID;
                if (Int32.TryParse(sampleIDString, out sampleID))
                {
                    // Check reference constraint of the Sample.
                    // (We are quite tollerant here: we omit the check if it is not in a patch or document.)
                    bool mustCheckReference = op.Patch != null && op.Patch.Document != null;
                    if (mustCheckReference)
                    {
                        IEnumerable<Sample> samples = op.Patch.Document.Samples;

                        bool isInList = samples.Any(x => x.ID == sampleID);

                        if (!isInList)
                        {
                            ValidationMessages.AddNotInListMessage(PropertyNames.Sample, PropertyDisplayNames.Sample, sampleID);
                        }
                    }
                }
            }
        }
    }
}