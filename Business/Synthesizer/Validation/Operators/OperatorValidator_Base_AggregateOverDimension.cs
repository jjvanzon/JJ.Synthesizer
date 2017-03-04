using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_AggregateOverDimension : OperatorValidator_Base
    {
        /// <param name="outletDimensionEnum">
        /// Inlets have dimensions Signal, Undefined, Undefined and Undefined,
        /// Outlet usually has dimension Undefined, but in an exceptional case it might have a different dimension, like Signal.
        /// </param>
        public OperatorValidator_Base_AggregateOverDimension(Operator obj, OperatorTypeEnum operatorTypeEnum, DimensionEnum outletDimensionEnum = DimensionEnum.Undefined)
            : base(
                obj,
                operatorTypeEnum,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { outletDimensionEnum },
                expectedDataKeys: new[] { PropertyNames.CollectionRecalculation })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(Obj.Data));
        }
    }
}