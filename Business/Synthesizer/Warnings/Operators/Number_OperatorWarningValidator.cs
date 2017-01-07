using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
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
                double? number = DataPropertyParser.TryParseDouble(Object, PropertyNames.Number);
                if (number.HasValue)
                {
                    if (number.Value == 0.0)
                    {
                        ValidationMessages.Add(() => Object.Data, MessageFormatter.NumberIs0WithName(Object.Name));
                    }
                }
            }
        }
    }
}