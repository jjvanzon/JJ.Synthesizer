using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class FirstXOperandsNotFilledInWarningValidator: ValidatorBase<Operator>
    {
        private int _operandCount;

        public FirstXOperandsNotFilledInWarningValidator(Operator obj, int operandCount)
            : base(obj, postponeExecute: true)
        {
            if (operandCount < 0) throw new Exception("operandCount cannot be less than 0.");
            _operandCount = operandCount;

            Execute();
        }

        protected override void Execute()
        {
            foreach (Inlet inlet in Object.Inlets.Take(_operandCount))
            {
                if (inlet.Input == null)
                {
                    // TODO: Use a better propertyKey. with an expression?
                    ValidationMessages.Add("OperandNotSet", MessagesFormatter.OperandNotSet(Object.OperatorTypeName, Object.Name, inlet.Name));
                }
            }
        }
    }
}
