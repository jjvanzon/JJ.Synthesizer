using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Curve_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Curve_OperatorValidator(Operator op)
            : base(
                  op,
                  OperatorTypeEnum.Curve,
                  expectedInletCount: 0,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.CurveID, PropertyNames.Dimension })
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
                        bool isRootDocument = op.Patch.Document.ParentDocument == null;

                        // If we're in a child document, we can reference the curves in both child document and root document,
                        // if we are in the root document, the possible curves are only the ones in the root document.
                        IEnumerable<Curve> curves;
                        if (isRootDocument)
                        {
                            curves = op.Patch.Document.Curves;
                        }
                        else
                        {
                            curves = op.Patch.Document.Curves.Union(op.Patch.Document.ParentDocument.Curves);
                        }

                        bool isInList = curves.Any(x => x.ID == curveID);

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