using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ClosestOverDimension_OperatorWarningValidator : OperatorWarningValidator_Base_SpecificInletsFilledInOrHaveDefaults
    {
        public ClosestOverDimension_OperatorWarningValidator(Operator obj)
            : base(
                  obj, 
                  OperatorConstants.CLOSEST_OVER_DIMENSION_INPUT_INDEX,
                  OperatorConstants.CLOSEST_OVER_DIMENSION_COLLECTION_INDEX,
                  OperatorConstants.CLOSEST_OVER_DIMENSION_TILL_INDEX)
        { }
    }
}