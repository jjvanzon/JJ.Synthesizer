using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Number_OperatorWarningValidator : OperatorWarningValidator_Base
    {
        public Number_OperatorWarningValidator(Operator op)
            : base(op)
        { 
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(op))
            {
                double? number = DataPropertyParser.TryParseDouble(op, nameof(Number_OperatorWrapper.Number));

                For(() => number, ResourceFormatter.Number).NotZero();
            }
        }
    }
}