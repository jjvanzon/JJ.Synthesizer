using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Linq;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class DimensionToOutlets_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public DimensionToOutlets_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.DimensionToOutlets,
                new DimensionEnum[] { DimensionEnum.Undefined },
                Enumerable.Repeat(DimensionEnum.Undefined, obj?.Outlets.Count ?? 0).ToArray())
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Outlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets)).GreaterThan(0);
        }
    }
}