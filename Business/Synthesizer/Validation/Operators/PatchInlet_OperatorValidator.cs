using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PatchInlet_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public PatchInlet_OperatorValidator(Operator op)
            : base(
                op,
                OperatorTypeEnum.PatchInlet,
                new[] { GetInletDimensionEnum(op) },
                new[] { DimensionEnum.Undefined })
        {
            For(() => op.Name, CommonResourceFormatter.Name).IsNullOrEmpty();
        }

        private static DimensionEnum GetInletDimensionEnum(Operator op)
        {
            return op?.Inlets.FirstOrDefault()?.GetDimensionEnum() ?? DimensionEnum.Undefined;
        }
    }
}
