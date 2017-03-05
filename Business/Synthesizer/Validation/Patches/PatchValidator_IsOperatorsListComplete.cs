using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_IsOperatorsListComplete : ValidatorBase<Patch>
    {
        public PatchValidator_IsOperatorsListComplete([NotNull] Patch obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            Patch patch = Obj;

            IList<Operator> operatorsInGraph = patch.GetOperatorsRecursive();
            IList<Operator> operatorsMissingInList = operatorsInGraph.Except(patch.Operators).ToArray();

            foreach (Operator operatorMissingInList in operatorsMissingInList)
            {
                string operatorIdentifier = ValidationHelper.GetIdentifier(operatorMissingInList);
                string message = ResourceFormatter.OperatorIsInGraphButNotInList(operatorIdentifier);

                ValidationMessages.Add(PropertyNames.Operators, message);
            }
        }
    }
}
