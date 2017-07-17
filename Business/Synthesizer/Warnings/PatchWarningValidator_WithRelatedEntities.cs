using JJ.Framework.Validation;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class PatchWarningValidator_WithRelatedEntities : VersatileValidator
    {
        public PatchWarningValidator_WithRelatedEntities(
            Patch obj,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            HashSet<object> alreadyDone)
        {
            if (obj == null) throw new NullException(() => obj);

            foreach (Operator op in obj.Operators)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(op, sampleRepository, curveRepository);
                ExecuteValidator(new OperatorWarningValidator_VersatileWithUnderlyingEntities(op, sampleRepository, alreadyDone), messagePrefix);
            }
        }
    }
}