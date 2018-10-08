using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Comparative;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public class PatchVarConstReplacer
    {
        private readonly SystemFacade _systemFacade;
        private readonly PatchFacade _patchFacade;

        public PatchVarConstReplacer(SystemFacade systemFacade, PatchFacade patchFacade)
        {
            _systemFacade = systemFacade ?? throw new ArgumentNullException(nameof(systemFacade));
            _patchFacade = patchFacade ?? throw new ArgumentNullException(nameof(patchFacade));
        }

        public void ReplaceVarsWithConstsIfNeeded(Patch patch, IList<double?> constsToReplaceVariables)
        {
            IList<Operator> patchInlets = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                               .Select(x => new PatchInletOrOutlet_OperatorWrapper(x))
                                               .OrderBy(x => x.Inlet.Position)
                                               .Select(x => x.WrappedOperator)
                                               .ToArray();

            if (constsToReplaceVariables.Count > patchInlets.Count)
            {
                throw new GreaterThanException(() => constsToReplaceVariables.Count, () => patchInlets.Count);
            }

            for (var i = 0; i < constsToReplaceVariables.Count; i++)
            {
                double? constToReplaceVariable = constsToReplaceVariables[i];

                if (!constToReplaceVariable.HasValue)
                {
                    continue;
                }

                Operator op = patchInlets[i];

                Patch numberPatch = _systemFacade.GetSystemPatch(OperatorTypeEnum.Number);
                op.LinkToUnderlyingPatch(numberPatch);
                new Number_OperatorWrapper(op) { Number = constToReplaceVariable.Value };
                IResult result = _patchFacade.SaveOperator(op);
                result.Assert();
            }
        }
    }
}
