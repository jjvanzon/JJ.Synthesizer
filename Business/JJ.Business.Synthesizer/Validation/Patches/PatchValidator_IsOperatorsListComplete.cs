using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_IsOperatorsListComplete : FluentValidator<Patch>
    {
        public PatchValidator_IsOperatorsListComplete(Patch obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            Patch patch = Object;

            IList<Operator> operatorsInGraph = patch.GetOperatorsRecursive();
            IList<Operator> operatorsMissingInList = operatorsInGraph.Except(patch.Operators).ToArray();

            foreach (Operator operatorMissingInList in operatorsMissingInList)
            {
                string operatorIdentifier = ValidationHelper.GetOperatorIdentifier(operatorMissingInList);
                string message = MessageFormatter.OperatorIsInGraphButNotInList(operatorIdentifier);

                ValidationMessages.Add(PropertyNames.Operators, message);
            }
        }
    }
}
