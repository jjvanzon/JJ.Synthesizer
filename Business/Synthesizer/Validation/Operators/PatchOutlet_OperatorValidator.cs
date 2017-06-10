using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PatchOutlet_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public PatchOutlet_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.PatchOutlet,
                new[] { DimensionEnum.Undefined },
                new[] { GetOutletDimensionEnum(obj) })
        { }

        private static DimensionEnum GetOutletDimensionEnum(Operator obj)
        {
            return obj?.Outlets.FirstOrDefault()?.GetDimensionEnum() ?? DimensionEnum.Undefined;
        }
    }
}
