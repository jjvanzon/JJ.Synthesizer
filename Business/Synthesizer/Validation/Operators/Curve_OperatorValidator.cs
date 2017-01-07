using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Curve_OperatorValidator : OperatorValidator_Base
    {
        public Curve_OperatorValidator(Operator op)
            : base(
                  op,
                OperatorTypeEnum.Curve,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined },
                expectedDataKeys: new string[] { PropertyNames.CurveID })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string curveIDString = DataPropertyParser.TryGetString(op, PropertyNames.CurveID);
                For(() => curveIDString, PropertyDisplayNames.CurveID).IsInteger();

                int curveID;
                if (Int32.TryParse(curveIDString, out curveID))
                {
                    // Check reference constraint of the Curve.
                    // (We are quite tollerant here: we omit the check if it is not in a patch or document.)
                    bool mustCheckReference = op.Patch != null && op.Patch.Document != null;
                    if (mustCheckReference)
                    {
                        bool isInList = op.Patch.Document.Curves.Any(x => x.ID == curveID);

                        if (!isInList)
                        {
                            ValidationMessages.AddNotInListMessage(PropertyNames.Curve, PropertyDisplayNames.Curve, curveID);
                        }
                    }
                }
            }
        }
    }
}