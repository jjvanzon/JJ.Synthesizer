using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Loop : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Loop(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<Inlet> sortedInlets = Object.Inlets.OrderBy(x => x.SortOrder).ToArray();

            if (sortedInlets.Count >= 2)
            {
                Inlet loopStartInlet = sortedInlets[1];

                if (loopStartInlet.InputOutlet == null)
                {
                    ValidationMessages.Add(() => loopStartInlet.InputOutlet, MessageFormatter.InletNotSet(Object.GetOperatorTypeEnum(), Object.Name, loopStartInlet.Name));
                }
            }

            if (sortedInlets.Count >= 3)
            {
                Inlet loopEndInlet = sortedInlets[2];

                if (loopEndInlet.InputOutlet == null)
                {
                    ValidationMessages.Add(() => loopEndInlet.InputOutlet, MessageFormatter.InletNotSet(Object.GetOperatorTypeEnum(), Object.Name, loopEndInlet.Name));
                }
            }
        }
    }
}
