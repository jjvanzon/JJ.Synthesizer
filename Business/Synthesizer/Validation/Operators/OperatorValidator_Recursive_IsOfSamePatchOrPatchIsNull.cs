using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull : ValidatorBase<Operator>
    {
        private readonly Patch _patch;
        private readonly ISampleRepository _sampleRepository;
        private readonly ICurveRepository _curveRepository;

        public OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(
            [NotNull] Operator op,
            [NotNull] Patch patch,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository)
            : base(op, postponeExecute: true)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
            _patch = patch ?? throw new NullException(() => patch);

            Execute();
        }

        protected sealed override void Execute()
        {
            if (Obj.Patch != null &&
                Obj.Patch != _patch)
            {
                string operatorIdentifier = ValidationHelper.GetUserFriendlyIdentifier(Obj, _sampleRepository, _curveRepository);
                string patchIdentifier = ValidationHelper.GetUserFriendlyIdentifier(_patch);

                ValidationMessages.Add(nameof(Obj.Patch), ResourceFormatter.OperatorPatchIsNotTheExpectedPatch(operatorIdentifier, patchIdentifier));
            }

            foreach (Inlet inlet in Obj.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    ExecuteValidator(new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(inlet.InputOutlet.Operator, _patch, _sampleRepository, _curveRepository));
                }
            }
        }
    }
}
