using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Number_OperatorWarningValidator : OperatorWarningValidator_Base
    {
        public Number_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (DataPropertyParser.DataIsWellFormed(Object))
            {
                string numberString = DataPropertyParser.TryGetString(Object, PropertyNames.Number);
                double number;
                if (DoubleHelper.TryParse(numberString, DataPropertyParser.FormattingCulture, out number))
                {
                    if (number == 0.0)
                    {
                        ValidationMessages.Add(() => Object.Data, MessageFormatter.NumberIs0WithName(Object.Name));
                    }
                }
            }
        }
    }
}