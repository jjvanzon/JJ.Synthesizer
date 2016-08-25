using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Operators;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator : FluentValidator<Patch>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IPatchRepository _patchRepository;
        private HashSet<object> _alreadyDone;

        public PatchValidator(
            Patch obj,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _patchRepository = patchRepository;
            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            // TODO: Message prefix, or it will say 'Name is too long'.
            ExecuteValidator(new NameValidator(Object.GroupName, required: false));

            ExecuteValidator(new PatchValidator_Name(Object));
            ExecuteValidator(new PatchValidator_UniqueName(Object));
            ExecuteValidator(new PatchValidator_UniqueInletNames(Object));
            ExecuteValidator(new PatchValidator_UniqueInletListIndexes(Object));
            ExecuteValidator(new PatchValidator_UniqueOutletNames(Object));
            ExecuteValidator(new PatchValidator_UniqueOutletListIndexes(Object));
            ExecuteValidator(new PatchValidator_IsOperatorsListComplete(Object));

            foreach (Operator op in Object.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, _sampleRepository, _curveRepository, _patchRepository);

                ExecuteValidator(new OperatorValidator_IsCircular(op, _patchRepository), messagePrefix);

                // Message prefix not used here on purpose.
                // See Recursive_OperatorValidator.
                // This to prevent long message prefixes due to recursive processing.
                ExecuteValidator(new Recursive_OperatorValidator(
                    op,
                    _curveRepository,
                    _sampleRepository,
                    _patchRepository,
                    _alreadyDone));
            }
        }
   }
}