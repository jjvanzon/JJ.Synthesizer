using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_WithRelatedEntities : VersatileValidator<Patch>
    {
        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly HashSet<object> _alreadyDone;

        public PatchValidator_WithRelatedEntities(
            [NotNull] Patch obj,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] IPatchRepository patchRepository,
            [NotNull] HashSet<object> alreadyDone)
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

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            ExecuteValidator(new NameValidator(Obj.GroupName, ResourceFormatter.GroupName, required: false));

            ExecuteValidator(new PatchValidator_Name(Obj));
            ExecuteValidator(new PatchValidator_UniqueName(Obj));
            ExecuteValidator(new PatchValidator_UniqueInletNames(Obj));
            ExecuteValidator(new PatchValidator_UniqueInletListIndexes(Obj));
            ExecuteValidator(new PatchValidator_UniqueOutletNames(Obj));
            ExecuteValidator(new PatchValidator_UniqueOutletListIndexes(Obj));
            ExecuteValidator(new PatchValidator_IsOperatorsListComplete(Obj, _sampleRepository, _curveRepository, _patchRepository));

            foreach (Operator op in Obj.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, _sampleRepository, _curveRepository, _patchRepository);

                ExecuteValidator(new OperatorValidator_IsCircular(op, _patchRepository), messagePrefix);

                ExecuteValidator(
                    new OperatorValidator_WithUnderlyingEntities(
                        op,
                        _curveRepository,
                        _sampleRepository,
                        _patchRepository,
                        _alreadyDone),
                    messagePrefix);
            }
        }
    }
}