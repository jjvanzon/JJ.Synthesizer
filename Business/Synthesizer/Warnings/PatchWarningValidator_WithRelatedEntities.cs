using JJ.Framework.Validation;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class PatchWarningValidator_WithRelatedEntities : VersatileValidator<Patch>
    {
        public PatchWarningValidator_WithRelatedEntities(
            [NotNull] Patch obj,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] HashSet<object> alreadyDone)
            : base(obj)
        {
            foreach (Operator op in obj.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, sampleRepository, curveRepository);
                ExecuteValidator(new OperatorWarningValidator_WithUnderlyingEntities(op, sampleRepository, alreadyDone), messagePrefix);
            }
        }
    }
}