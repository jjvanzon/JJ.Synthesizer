using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_CustomOperator : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_CustomOperator(Operator op)
            : base(op)
        { }

        protected override void Execute()
        {
            if (DataPropertyParser.DataIsWellFormed(Object.Data))
            {
                string underlyingPatchIDString = DataPropertyParser.TryGetString(Object, PropertyNames.UnderlyingPatchID);

                For(() => underlyingPatchIDString, PropertyDisplayNames.UnderlyingPatch)
                    .NotNullOrEmpty();
            }
        }
    }
}