using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Loop : OperatorWarningValidator_Base
    {
        private static int[] indexesToCheck = new int[]
        {
            OperatorConstants.LOOP_SIGNAL_INDEX,
            OperatorConstants.LOOP_LOOP_START_INDEX,
            OperatorConstants.LOOP_LOOP_DURATION,
            OperatorConstants.LOOP_LOOP_END_INDEX
        };

        public OperatorWarningValidator_Loop(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<Inlet> sortedInlets = Object.Inlets.OrderBy(x => x.SortOrder).ToArray();

            foreach (int indexToCheck in indexesToCheck)
            {
                if (sortedInlets.Count > indexToCheck)
                {
                    Inlet loopStartInlet = sortedInlets[indexToCheck];

                    if (loopStartInlet.InputOutlet == null)
                    {
                        string message = MessageFormatter.InletNotSet(ResourceHelper.GetOperatorTypeDisplayName(Object), Object.Name, loopStartInlet.Name);
                        ValidationMessages.Add(() => loopStartInlet.InputOutlet, message);
                    }
                }
            }
        }
    }
}
