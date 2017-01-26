using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Linq;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverInlets_OperatorValidator : OperatorValidator_Base
    {
        private const int MINIMUM_INLET_COUNT = 3;

        public ClosestOverInlets_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.ClosestOverInlets,
                Enumerable.Repeat(DimensionEnum.Undefined, obj.Inlets.Count).ToArray(),
                new[] { DimensionEnum.Undefined },
                expectedDataKeys: new string[0])
        { }
    }
}