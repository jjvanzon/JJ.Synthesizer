using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary>
    /// Base class for operator validators that do not have additional data in the Data property.
    /// Verifies that the Data property is null.
    /// </summary>
    public abstract class OperatorValidator_Base_WithoutData : OperatorValidator_Base
    {
        public OperatorValidator_Base_WithoutData(
            Operator obj,
            OperatorTypeEnum expectedOperatorTypeEnum,
            IList<DimensionEnum> expectedInletDimensionEnums,
            IList<DimensionEnum> expectedOutletDimensionEnums)
            : base(
                  obj,
                  expectedOperatorTypeEnum,
                  expectedInletDimensionEnums,
                  expectedOutletDimensionEnums,
                  expectedDataKeys: new string[0])
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Data, PropertyDisplayNames.Data).IsNullOrEmpty();
        }
    }
}
