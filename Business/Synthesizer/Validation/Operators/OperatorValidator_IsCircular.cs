using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_IsCircular : VersatileValidator<Operator>
    {
        public OperatorValidator_IsCircular(Operator op)
            : base(op, postponeExecute: true)
        {
            Execute();
        }

        protected sealed override void Execute()
        {
            Operator op = Obj;

            if (op.IsCircular())
            {
                ValidationMessages.Add(() => op, ResourceFormatter.CircularReference);
            }

            // TODO: Enable the UnderlyingPatchIsCircular check again, when it is corrected, so it works.
            return;

            // ReSharper disable once HeuristicUnreachableCode
            // ReSharper disable once InvertIf
            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
            {
                if (op.HasCircularUnderlyingPatch())
                {
                    ValidationMessages.Add(() => op, ResourceFormatter.UnderlyingPatchIsCircular);
                }
            }
        }
    }
}
