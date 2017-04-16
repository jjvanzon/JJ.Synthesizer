using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class PatchWarningValidator_WithRelatedEntities : VersatileValidator<Patch>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;

        private readonly HashSet<object> _alreadyDone;

        public PatchWarningValidator_WithRelatedEntities(
            [NotNull] Patch obj,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] IPatchRepository patchRepository,
            [NotNull] HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _alreadyDone = alreadyDone ?? throw new AlreadyDoneIsNullException();
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            foreach (Operator op in Obj.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, _sampleRepository, _curveRepository, _patchRepository);
                ExecuteValidator(new OperatorWarningValidator_WithUnderlyingEntities(op, _sampleRepository, _alreadyDone), messagePrefix);
            }
        }
    }
}