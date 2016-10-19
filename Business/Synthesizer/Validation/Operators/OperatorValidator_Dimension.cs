using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Dimension : FluentValidator<Operator>
    {
        public OperatorValidator_Dimension(Operator op)
            : base(op)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            if (op.OperatorType == null)
            {
                return;
            }

            if (op.OperatorType.HasDimension == true)
            {
                bool dimensionIsFilledIn = op.GetStandardDimensionEnum() != DimensionEnum.Undefined;
                bool customDimensionNameIsFilledIn = !String.IsNullOrWhiteSpace(op.CustomDimensionName);

                if (dimensionIsFilledIn && customDimensionNameIsFilledIn)
                {
                    ValidationMessages.AddNotBothValidationMessage(
                        PropertyNames.Dimension,
                        PropertyDisplayNames.StandardDimension,
                        PropertyDisplayNames.CustomDimensionName);
                }

                ExecuteValidator(new NameValidator(op.CustomDimensionName, PropertyDisplayNames.CustomDimensionName, required: false));
            }
            else
            {
                For(() => Object.StandardDimension, PropertyDisplayNames.StandardDimension).IsNull();
                For(() => Object.CustomDimensionName, PropertyDisplayNames.CustomDimensionName).IsNullOrEmpty();
            }
        }
    }
}