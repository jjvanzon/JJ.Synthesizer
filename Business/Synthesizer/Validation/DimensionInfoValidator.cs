﻿using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DimensionInfoValidator : VersatileValidator
    {
        public DimensionInfoValidator(bool hasDimension, Dimension standardDimension, string customDimensionName)
        {
            if (hasDimension)
            {
                bool dimensionIsFilledIn = standardDimension != null;
                bool customDimensionNameIsFilledIn = NameHelper.IsFilledIn(customDimensionName);

                if (dimensionIsFilledIn && customDimensionNameIsFilledIn)
                {
                    ValidationMessages.AddNotBothValidationMessage(
                        nameof(Dimension),
                        ResourceFormatter.StandardDimension,
                        ResourceFormatter.CustomDimensionName);
                }

                ExecuteValidator(new NameValidator(customDimensionName, ResourceFormatter.CustomDimensionName, required: false));
            }
            else
            {
                For(() => standardDimension, ResourceFormatter.StandardDimension).IsNull();
                For(() => customDimensionName, ResourceFormatter.CustomDimensionName).IsNullOrWhiteSpace();
            }
        }
    }
}