using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Dimension : VersatileValidator<Operator>
    {
        public OperatorValidator_Dimension(Operator op)
            : base(op)
        { 
            if (op.OperatorType == null)
            {
                return;
            }

            if (op.OperatorType.HasDimension)
            {
                bool dimensionIsFilledIn = op.GetStandardDimensionEnum() != DimensionEnum.Undefined;
                bool customDimensionNameIsFilledIn = NameHelper.IsFilledIn(op.CustomDimensionName);

                if (dimensionIsFilledIn && customDimensionNameIsFilledIn)
                {
                    ValidationMessages.AddNotBothValidationMessage(
                        nameof(Dimension),
                        ResourceFormatter.StandardDimension,
                        ResourceFormatter.CustomDimensionName);
                }

                ExecuteValidator(new NameValidator(op.CustomDimensionName, ResourceFormatter.CustomDimensionName, required: false));
            }
            else
            {
                For(() => op.StandardDimension, ResourceFormatter.StandardDimension).IsNull();
                For(() => op.CustomDimensionName, ResourceFormatter.CustomDimensionName).IsNullOrEmpty();
            }
        }
    }
}