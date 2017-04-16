using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Curve_OperatorWarningValidator : OperatorWarningValidator_Base
    {
        public Curve_OperatorWarningValidator(Operator op)
            : base(op)
        { }

        protected override void Execute()
        {
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(Obj.Data))
            {
                string curveIDString = DataPropertyParser.TryGetString(Obj, PropertyNames.CurveID);

                For(() => curveIDString, ResourceFormatter.Curve)
                    .NotNullOrEmpty();
            }
        }
    }
}
