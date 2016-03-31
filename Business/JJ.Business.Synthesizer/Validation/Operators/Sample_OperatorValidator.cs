using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using System.Globalization;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sample_OperatorValidator : OperatorValidator_Base
    {
        public Sample_OperatorValidator(Operator op)
            : base(
                  op,
                  OperatorTypeEnum.Sample,
                  expectedInletCount: 1,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.SampleID })
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
                        bool isRootDocument = op.Patch.Document.ParentDocument == null;

                        // If we're in a child document, we can reference the samples in both child document and root document,
                        // if we are in the root document, the possible samples are only the ones in the root document.
                        IEnumerable<Sample> samples;
                        if (isRootDocument)
                        {
                            samples = op.Patch.Document.Samples;
                        }
                        else
                        {
                            samples = op.Patch.Document.Samples.Union(op.Patch.Document.ParentDocument.Samples);
                        }

                        bool isInList = samples.Any(x => x.ID == sampleID);

                        if (!isInList)
                        {
                            ValidationMessages.Add(PropertyNames.Sample, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.Sample, sampleID));
                        }
                    }
                }
            }
        }
    }
}