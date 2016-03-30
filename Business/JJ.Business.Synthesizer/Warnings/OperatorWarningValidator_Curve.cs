using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_Curve : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Curve(Operator op)
            : base(op)
        { }

        protected override void Execute()
        {
            if (DataPropertyParser.DataIsWellFormed(Object.Data))
            {
                string curveIDString = DataPropertyParser.TryGetString(Object, PropertyNames.CurveID);

                For(() => curveIDString, PropertyDisplayNames.Curve)
                    .NotNullOrEmpty();
            }
        }
    }
}
