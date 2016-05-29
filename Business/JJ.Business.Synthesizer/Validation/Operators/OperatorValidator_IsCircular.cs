using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_IsCircular : FluentValidator<Operator>
    {
        private IPatchRepository _patchRepository;

        public OperatorValidator_IsCircular(Operator op, IPatchRepository patchRepository)
            : base(op, postponeExecute: true)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            if (op.IsCircular())
            {
                ValidationMessages.Add(() => op, MessageFormatter.OperatorIsCircularWithName(op.Name));
            }

            // TODO: Enable the UnderlyingPatchIsCircular check again, when it is corrected, so it works.
            return;

            if (op.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
            {
                if (op.HasCircularUnderlyingPatch(_patchRepository))
                {
                    ValidationMessages.Add(() => op, Messages.UnderlyingPatchIsCircular);
                }
            }
        }
    }
}
