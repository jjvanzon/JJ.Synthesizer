using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverInletsExp_OperatorValidator : OperatorValidator_Base
    {
        private const int MINIMUM_INLET_COUNT = 3;

        public ClosestOverInletsExp_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.ClosestOverInletsExp,
                Enumerable.Repeat(DimensionEnum.Undefined, obj.Inlets.Count).ToArray(),
                new[] { DimensionEnum.Undefined },
                expectedDataKeys: new string[0])
        { }

        protected override void Execute()
        {
            For(() => Obj.Inlets.Count, CommonResourceFormatter.ObjectCount(ResourceFormatter.Inlets)).GreaterThanOrEqual(MINIMUM_INLET_COUNT);

            base.Execute();
        }
    }
}