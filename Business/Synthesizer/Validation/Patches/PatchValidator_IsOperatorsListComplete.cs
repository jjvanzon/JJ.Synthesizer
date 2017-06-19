using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_IsOperatorsListComplete : ValidatorBase
    {
        public PatchValidator_IsOperatorsListComplete(
            [NotNull] Patch patch,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository)
        {
            if (patch == null) throw new NullException(() => patch);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            IList<Operator> operatorsInGraph = patch.GetOperatorsRecursive();
            IList<Operator> operatorsMissingInList = operatorsInGraph.Except(patch.Operators).ToArray();

            foreach (Operator operatorMissingInList in operatorsMissingInList)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(operatorMissingInList, sampleRepository, curveRepository);
                string message = ResourceFormatter.OperatorIsInGraphButNotInList;

                ValidationMessages.Add(nameof(patch.Operators), messagePrefix + message);
            }
        }
    }
}
