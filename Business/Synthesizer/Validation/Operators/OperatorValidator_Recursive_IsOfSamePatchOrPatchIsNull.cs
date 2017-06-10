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
        public OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(
            [NotNull] Operator op,
            [NotNull] Patch patch,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository)
            : base(op)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patch == null) throw new NullException(() => patch);

            if (op.Patch != null &&
                op.Patch != patch)
            {
                string operatorIdentifier = ValidationHelper.GetUserFriendlyIdentifier(op, sampleRepository, curveRepository);
                string patchIdentifier = ValidationHelper.GetUserFriendlyIdentifier(patch);

                ValidationMessages.Add(nameof(op.Patch), ResourceFormatter.OperatorPatchIsNotTheExpectedPatch(operatorIdentifier, patchIdentifier));
            }

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    ExecuteValidator(new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(inlet.InputOutlet.Operator, patch, sampleRepository, curveRepository));
                }
            }
        }
    }
}
