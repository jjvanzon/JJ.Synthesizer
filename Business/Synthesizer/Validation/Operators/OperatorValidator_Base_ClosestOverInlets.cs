using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal abstract class OperatorValidator_Base_ClosestOverInlets : OperatorValidator_Base_WithOperatorType
    {
        private const int MINIMUM_INLET_COUNT = 3;

        public OperatorValidator_Base_ClosestOverInlets(Operator obj, OperatorTypeEnum operatorTypeEnum)
            : this(obj, obj?.Inlets.Count == 0 ? 0 : obj?.Inlets.Count - 1 ?? 0, operatorTypeEnum)
        { }

        private OperatorValidator_Base_ClosestOverInlets(Operator obj, int itemCount, OperatorTypeEnum operatorTypeEnum)
            : base(
                obj,
                operatorTypeEnum,
                DimensionEnum.Input.Concat(Enumerable.Repeat(DimensionEnum.Item, itemCount)).ToArray(),
                new[] { DimensionEnum.Number },
                expectedDataKeys: new string[0])
        {
            For(() => obj.Inlets.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets)).GreaterThanOrEqual(MINIMUM_INLET_COUNT);
        }
    }
}