﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Loop_OperatorWarningValidator : OperatorWarningValidator_Base
    {
        private static int[] indexesToCheck = new int[]
        {
            OperatorConstants.LOOP_SIGNAL_INDEX,
            OperatorConstants.LOOP_LOOP_START_MARKER_INDEX,
            OperatorConstants.LOOP_LOOP_END_MARKER_INDEX,
            OperatorConstants.LOOP_NOTE_DURATION_INDEX
        };

        public Loop_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<Inlet> sortedInlets = Object.Inlets.OrderBy(x => x.ListIndex).ToArray();

            foreach (int indexToCheck in indexesToCheck)
            {
                bool isValidIndex = sortedInlets.Count > indexToCheck;
                if (isValidIndex)
                {
                    Inlet inlet = sortedInlets[indexToCheck];

                    if (inlet.InputOutlet == null)
                    {
                        string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(Object);
                        string message = MessageFormatter.InletNotSet(operatorTypeDisplayName, Object.Name, inlet.Name);
                        ValidationMessages.Add(() => inlet.InputOutlet, message);
                    }
                }
            }
        }
    }
}