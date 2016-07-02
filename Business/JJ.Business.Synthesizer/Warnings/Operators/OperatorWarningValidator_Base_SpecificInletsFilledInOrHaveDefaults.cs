using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Base_SpecificInletsFilledInOrHaveDefaults : OperatorWarningValidator_Base
    {
        private readonly IList<int> _inletListIndexes;

        public OperatorWarningValidator_Base_SpecificInletsFilledInOrHaveDefaults(Operator obj, params int[] inletListIndexes)
            : base(obj, postponeExecute: true)
        {
            if (inletListIndexes == null) throw new NullException(() => inletListIndexes);

            _inletListIndexes = inletListIndexes;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            foreach (int inletIndex in _inletListIndexes)
            {
                Inlet inlet = OperatorHelper.TryGetInlet(op, inletIndex);
                if (inlet != null)
                {
                    if (inlet.InputOutlet == null && !inlet.DefaultValue.HasValue)
                    {
                        string inletIdentifier = ValidationHelper.GetInletIdentifier(inlet);
                        ValidationMessages.Add(() => inlet.InputOutlet, MessageFormatter.InletNotSet(Object.GetOperatorTypeEnum(), Object.Name, inletIdentifier));
                    }
                }
            }
        }
    }
}
