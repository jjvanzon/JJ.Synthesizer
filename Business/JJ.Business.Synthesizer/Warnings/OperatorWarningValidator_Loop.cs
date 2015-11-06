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
            OperatorConstants.LOOP_START_INDEX,
            OperatorConstants.LOOP_SUSTAIN_INDEX,
            OperatorConstants.LOOP_END_INDEX
        };

        public OperatorWarningValidator_Loop(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<Inlet> sortedInlets = Object.Inlets.OrderBy(x => x.ListIndex).ToArray();

            foreach (int indexToCheck in indexesToCheck)
            {
                if (sortedInlets.Count > indexToCheck)
                {
                    Inlet loopStartInlet = sortedInlets[indexToCheck];

                    if (loopStartInlet.InputOutlet == null)
                    {
                        string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(Object);
                        string message = MessageFormatter.InletNotSet(operatorTypeDisplayName, Object.Name, loopStartInlet.Name);
                        ValidationMessages.Add(() => loopStartInlet.InputOutlet, message);
                    }
                }
            }
        }
    }
}
