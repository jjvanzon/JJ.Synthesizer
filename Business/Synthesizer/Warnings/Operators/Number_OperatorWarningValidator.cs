using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

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

                For(() => number, ResourceFormatter.Number).NotZero();
            }
        }
    }
}