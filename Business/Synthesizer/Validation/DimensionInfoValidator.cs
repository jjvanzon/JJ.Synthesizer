using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DimensionInfoValidator : VersatileValidator<(bool, Dimension, string)>
    {
        public DimensionInfoValidator(
            (bool hasDimension, Dimension standardDimension, string customDimensionName) tuple)
            : base(tuple)
        {
            if (tuple.hasDimension)
            {
                bool dimensionIsFilledIn = tuple.standardDimension != null;
                bool customDimensionNameIsFilledIn = NameHelper.IsFilledIn(tuple.customDimensionName);

                if (dimensionIsFilledIn && customDimensionNameIsFilledIn)
                {
                    ValidationMessages.AddNotBothValidationMessage(
                        nameof(Dimension),
                        ResourceFormatter.StandardDimension,
                        ResourceFormatter.CustomDimensionName);
                }

                ExecuteValidator(new NameValidator(tuple.customDimensionName, ResourceFormatter.CustomDimensionName, required: false));

            }
            else
            {
                For(() => tuple.standardDimension, ResourceFormatter.StandardDimension).IsNull();
                For(() => tuple.customDimensionName, ResourceFormatter.CustomDimensionName).IsNullOrWhiteSpace();
            }
        }
    }
}