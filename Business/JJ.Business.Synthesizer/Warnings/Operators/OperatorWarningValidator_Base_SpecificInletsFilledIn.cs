using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Base_SpecificInletsFilledIn : OperatorWarningValidator_Base
    {
        private readonly IList<string> _inletNames;

        public OperatorWarningValidator_Base_SpecificInletsFilledIn(Operator obj, params string[] inletNames)
            : base(obj, postponeExecute: true)
        {
            if (inletNames == null) throw new NullException(() => inletNames);

            _inletNames = inletNames;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            foreach (string inletName in _inletNames)
            {
                Inlet inlet = OperatorHelper.TryGetInlet(op, inletName);
                if (inlet != null)
                {
                    if (inlet.InputOutlet == null)
                    {
                        ValidationMessages.Add(() => inlet.InputOutlet, MessageFormatter.InletNotSet(Object.GetOperatorTypeEnum(), Object.Name, inlet.Name));
                    }
                }
            }
        }
    }
}
