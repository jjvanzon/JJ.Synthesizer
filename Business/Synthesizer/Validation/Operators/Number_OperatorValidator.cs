using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Number_OperatorValidator : OperatorValidator_Basic
    {
        public Number_OperatorValidator(Operator obj)
            : base(
                obj,
                expectedDataKeys: new[] { nameof(Number_OperatorWrapper.Number) })
        {
            string numberString = DataPropertyParser.TryGetString(obj, nameof(Number_OperatorWrapper.Number));
            For(numberString, ResourceFormatter.Number, DataPropertyParser.FormattingCulture)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();
        }
    }
}