using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Number_OperatorValidator : OperatorValidator_Base
    {
        public Number_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Number,
                new DimensionEnum[0],
                new[] { DimensionEnum.Number },
                expectedDataKeys: new[] { nameof(Number_OperatorWrapper.Number) })
        {
            string numberString = DataPropertyParser.TryGetString(obj, nameof(Number_OperatorWrapper.Number));
            For(() => numberString, ResourceFormatter.Number, DataPropertyParser.FormattingCulture)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();
        }
    }
}