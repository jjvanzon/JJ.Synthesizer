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
    internal class PatchValidator_IsOperatorsListComplete : ValidatorBase<Patch>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ICurveRepository _curveRepository;

        public PatchValidator_IsOperatorsListComplete(
            [NotNull] Patch obj,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] ICurveRepository curveRepository)
            : base(obj)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
        }

        protected override void Execute()
        {
            Patch patch = Obj;

            IList<Operator> operatorsInGraph = patch.GetOperatorsRecursive();
            IList<Operator> operatorsMissingInList = operatorsInGraph.Except(patch.Operators).ToArray();

            foreach (Operator operatorMissingInList in operatorsMissingInList)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(operatorMissingInList, _sampleRepository, _curveRepository);
                string message = ResourceFormatter.OperatorIsInGraphButNotInList;

                ValidationMessages.Add(nameof(patch.Operators), messagePrefix + message);
            }
        }
    }
}
