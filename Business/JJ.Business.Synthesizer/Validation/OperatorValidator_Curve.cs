using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Curve : OperatorValidator_Base
    {
        public OperatorValidator_Curve(Operator op)
            : base(op, OperatorTypeEnum.Curve, 0, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            For(() => op.Data, PropertyDisplayNames.Data)
                .IsInteger();

            int curveID;
            if (Int32.TryParse(op.Data, out curveID))
            {
                // Check reference constraint of the Curve.
                // (We are quite tollerant here: we omit the check if it is not in a patch or document.)
                bool mustCheckReference = op.Patch != null && op.Patch.Document != null;
                if (mustCheckReference)
                {
                    bool isInList = op.Patch.Document.Curves.Any(x => x.ID == curveID);
                    if (!isInList)
                    {
                        ValidationMessages.Add(PropertyNames.Curve, MessageFormatter.NotFoundInList_WithItemName_AndID(PropertyDisplayNames.Curve, curveID));
                    }
                }
            }
        }
    }
}
