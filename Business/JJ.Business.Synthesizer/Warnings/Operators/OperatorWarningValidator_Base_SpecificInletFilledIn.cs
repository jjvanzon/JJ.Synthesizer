using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Base_SpecificInletFilledIn : OperatorWarningValidator_Base
    {
        private readonly string _inletName;

        public OperatorWarningValidator_Base_SpecificInletFilledIn(Operator obj, string inletName)
            : base(obj, postponeExecute: true)
        {
            _inletName = inletName;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            Inlet inlet = OperatorHelper.TryGetInlet(op, _inletName);
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
