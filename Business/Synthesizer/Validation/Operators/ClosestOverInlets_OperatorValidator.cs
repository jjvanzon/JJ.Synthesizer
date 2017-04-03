using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Linq;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverInlets_OperatorValidator : OperatorValidator_Base
    {
        public ClosestOverInlets_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.ClosestOverInlets,
                Enumerable.Repeat(DimensionEnum.Undefined, obj.Inlets.Count).ToArray(),
                new[] { DimensionEnum.Undefined },
                // ReSharper disable once ArgumentsStyleOther
                expectedDataKeys: new string[0])
        { }
    }
}