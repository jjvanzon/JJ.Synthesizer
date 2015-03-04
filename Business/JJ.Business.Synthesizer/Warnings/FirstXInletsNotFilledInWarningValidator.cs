using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class FirstXInletsNotFilledInWarningValidator: ValidatorBase<Operator>
    {
        private int _inletCount;

        public FirstXInletsNotFilledInWarningValidator(Operator obj, int? inletCount = null)
            : base(obj, postponeExecute: true)
        {
            if (obj == null) throw new NullException(() => obj);
            if (inletCount < 0) throw new Exception("inletCount cannot be less than 0.");

            _inletCount = inletCount ?? obj.Inlets.Count;

            Execute();
        }

        protected override void Execute()
        {
            foreach (Inlet inlet in Object.Inlets.Take(_inletCount))
            {
                if (inlet.Input == null)
                {
                    // TODO: Use a better propertyKey. with an expression?
                    ValidationMessages.Add("OperandNotSet", MessagesFormatter.InletNotSet(Object.OperatorTypeName, Object.Name, inlet.Name));
                }
            }
        }
    }
}
