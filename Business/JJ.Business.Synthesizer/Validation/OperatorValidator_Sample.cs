using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Sample : OperatorValidator_Base
    {
        public OperatorValidator_Sample(Operator op)
            : base(op, OperatorTypeEnum.Sample, 0, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            For(() => op.Data, PropertyDisplayNames.Data)
                .IsInteger();

            // TODO: Remove return statement when samples always come out of the document, and are not hacked into it in the PatchPlay action in the front-end.
            return;

            int sampleID;
            if (Int32.TryParse(op.Data, out sampleID))
            {
                // Check reference constraint of the Curve.
                // (We are quite tollerant here: we omit the check if it is not in a patch or document.)
                bool mustCheckReference = op.Patch != null && op.Patch.Document != null;
                if (mustCheckReference)
                {
                    bool isInList = op.Patch.Document.Samples.Any(x => x.ID == sampleID);
                    if (!isInList)
                    {
                        ValidationMessages.Add(PropertyNames.Sample, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.Sample, sampleID));
                    }
                }
            }
        }
    }
}