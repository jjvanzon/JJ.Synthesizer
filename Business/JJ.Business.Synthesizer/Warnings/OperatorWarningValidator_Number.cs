using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Number : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Number(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            double number;
            if (Double.TryParse(Object.Data, out number))
            {
                if (number == 0)
                {
                    ValidationMessages.Add(() => Object.Data, MessageFormatter.NumberIs0WithName(Object.Name));
                }
            }
        }
    }
}
