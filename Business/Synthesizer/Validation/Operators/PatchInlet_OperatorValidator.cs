using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PatchInlet_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public PatchInlet_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.PatchInlet,
                new[] { GetInletDimensionEnum(obj) },
                new[] { DimensionEnum.Undefined })
        { }

        private static DimensionEnum GetInletDimensionEnum(Operator obj)
        {
            return obj?.Inlets.FirstOrDefault()?.GetDimensionEnum() ?? DimensionEnum.Undefined;
        }
    }
}
