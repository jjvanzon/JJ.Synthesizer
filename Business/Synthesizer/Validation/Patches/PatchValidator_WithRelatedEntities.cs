using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_WithRelatedEntities : VersatileValidator<Patch>
    {
        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly HashSet<object> _alreadyDone;

        public PatchValidator_WithRelatedEntities(
            [NotNull] Patch obj,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _alreadyDone = alreadyDone ?? throw new AlreadyDoneIsNullException();

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            ExecuteValidator(new NameValidator(Obj.GroupName, ResourceFormatter.GroupName, required: false));

            ExecuteValidator(new PatchValidator_Name(Obj));
            ExecuteValidator(new PatchValidator_UniqueName(Obj));
            ExecuteValidator(new PatchValidator_IsOperatorsListComplete(Obj, _sampleRepository, _curveRepository));
            ExecuteValidator(new PatchValidator_HiddenButInUse(Obj));

            foreach (Operator op in Obj.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, _sampleRepository, _curveRepository);

                ExecuteValidator(new OperatorValidator_IsCircular(op), messagePrefix);

                ExecuteValidator(
                    new OperatorValidator_WithUnderlyingEntities(
                        op,
                        _curveRepository,
                        _sampleRepository,
                        _alreadyDone),
                    messagePrefix);
            }
        }
    }
}