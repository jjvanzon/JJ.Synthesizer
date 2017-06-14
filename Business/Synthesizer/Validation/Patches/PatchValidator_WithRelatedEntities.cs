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
        public PatchValidator_WithRelatedEntities(
            [NotNull] Patch patch,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] HashSet<object> alreadyDone)
            : base(patch)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            ExecuteValidator(new NameValidator(patch.GroupName, ResourceFormatter.GroupName, required: false));

            ExecuteValidator(new PatchValidator_Name(patch));
            ExecuteValidator(new PatchValidator_UniqueName(patch));
            ExecuteValidator(new PatchValidator_IsOperatorsListComplete(patch, sampleRepository, curveRepository));
            ExecuteValidator(new PatchValidator_HiddenButInUse(patch));
            ExecuteValidator(new DimensionInfoValidator((patch.HasDimension, patch.DefaultStandardDimension, patch.DefaultCustomDimensionName)));

            foreach (Operator op in patch.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, sampleRepository, curveRepository);

                ExecuteValidator(new OperatorValidator_IsCircular(op), messagePrefix);

                ExecuteValidator(
                    new OperatorValidator_VersatileWithUnderlyingEntities(
                        op,
                        curveRepository,
                        sampleRepository,
                        alreadyDone),
                    messagePrefix);
            }
        }
    }
}