using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Value : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Value(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            double value;
            if (Double.TryParse(Object.Data, out value))
            {
                if (value == 0)
                {
                    ValidationMessages.Add(() => Object.Data, MessageFormatter.ValueIs0WithName(Object.Name));
                }
            }
        }
    }
}
