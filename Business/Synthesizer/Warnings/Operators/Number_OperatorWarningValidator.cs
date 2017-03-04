using JJ.Business.Synthesizer.Resources;
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
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(Obj))
            {
                double? number = DataPropertyParser.TryParseDouble(Obj, PropertyNames.Number);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (number == 0.0)
                {
                    ValidationMessages.Add(() => Obj.Data, MessageFormatter.NumberIs0WithName(Obj.Name));
                }
            }
        }
    }
}