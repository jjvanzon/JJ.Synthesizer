using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull : ValidatorBase<Operator>
    {
        private readonly Patch _patch;

        public OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull([NotNull] Operator op, [NotNull] Patch patch)
            : base(op, postponeExecute: true)
        {
            _patch = patch ?? throw new NullException(() => patch);

            Execute();
        }

        protected sealed override void Execute()
        {
            if (Obj.Patch != null &&
                Obj.Patch != _patch)
            {
                ValidationMessages.Add(nameof(Obj.Patch), ResourceFormatter.OperatorPatchIsNotTheExpectedPatch(Obj.Name, _patch.Name));
            }

            foreach (Inlet inlet in Obj.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    ExecuteValidator(new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(inlet.InputOutlet.Operator, _patch));
                }
            }
        }
    }
}
