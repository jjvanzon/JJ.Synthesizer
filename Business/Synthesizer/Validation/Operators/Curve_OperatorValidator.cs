using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Curve_OperatorValidator : OperatorValidator_Basic
    {
        public Curve_OperatorValidator(Operator op)
            : base(
                op,
                expectedDataKeys: new[] { nameof(Curve_OperatorWrapper.CurveID) })
        { 
            if (!DataPropertyParser.DataIsWellFormed(op))
            {
                return;
            }

            string curveIDString = DataPropertyParser.TryGetString(op, nameof(Curve_OperatorWrapper.CurveID));
            For(curveIDString, CommonResourceFormatter.ID_WithName(ResourceFormatter.Curve)).IsInteger();

            if (!int.TryParse(curveIDString, out int curveID))
            {
                return;
            }

            // Check reference constraint of the Curve.
            // (We are quite tolerant here: we omit the check if it is not in a patch or document.)
            bool mustCheckReference = op.Patch?.Document != null;
            if (!mustCheckReference)
            {
                return;
            }

            bool isInList = op.Patch.Document.Curves.Any(x => x.ID == curveID);
            if (isInList)
            {
                return;
            }

            Messages.AddNotInListMessage(ResourceFormatter.Curve, curveID);
        }
    }
}