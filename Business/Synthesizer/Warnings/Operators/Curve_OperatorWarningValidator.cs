﻿using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Curve_OperatorWarningValidator : OperatorWarningValidator_Base
    {
        public Curve_OperatorWarningValidator(Operator op)
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