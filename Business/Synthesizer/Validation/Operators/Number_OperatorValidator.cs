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
                new[] { DimensionEnum.Result },
                expectedDataKeys: new[] { nameof(Number_OperatorWrapper.Number) })
        { }

        protected override void Execute()
        {
            string numberString = DataPropertyParser.TryGetString(Obj, nameof(Number_OperatorWrapper.Number));
            For(() => numberString, ResourceFormatter.Number, DataPropertyParser.FormattingCulture)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();

            base.Execute();
        }
    }
}