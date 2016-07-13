using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull : ValidatorBase<Operator>
    {
        private Patch _patch;

        public OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(Operator op, Patch patch)
            : base(op, postponeExecute: true)
        {
            if (patch == null) throw new NullException(() => patch);

            _patch = patch;

            Execute();
        }

        protected override void Execute()
        {
            if (Object.Patch != null &&
                Object.Patch != _patch)
            {
                ValidationMessages.Add(PropertyNames.Patch, MessageFormatter.OperatorPatchIsNotTheExpectedPatch(Object.Name, _patch.Name));
            }

            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    ExecuteValidator(new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(inlet.InputOutlet.Operator, _patch));
                }
            }
        }
    }
}
