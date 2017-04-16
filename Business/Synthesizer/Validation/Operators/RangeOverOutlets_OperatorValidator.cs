using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class RangeOverOutlets_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public RangeOverOutlets_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.RangeOverOutlets,
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                Enumerable.Repeat(DimensionEnum.Undefined, obj?.Outlets.Count ?? 0).ToArray())
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Obj.Outlets.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets)).GreaterThan(0);
        }
    }
}