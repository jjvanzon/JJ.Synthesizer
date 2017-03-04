using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull : ValidatorBase<Operator>
    {
        private readonly Patch _patch;

        public OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(Operator op, Patch patch)
            : base(op, postponeExecute: true)
        {
            if (patch == null) throw new NullException(() => patch);

            _patch = patch;

            Execute();
        }

        protected sealed override void Execute()
        {
            if (Obj.Patch != null &&
                Obj.Patch != _patch)
            {
                ValidationMessages.Add(PropertyNames.Patch, ResourceFormatter.OperatorPatchIsNotTheExpectedPatch(Obj.Name, _patch.Name));
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
