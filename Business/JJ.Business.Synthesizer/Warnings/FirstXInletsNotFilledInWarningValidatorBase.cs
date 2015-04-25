using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public abstract class FirstXInletsNotFilledInWarningValidatorBase : OperatorWarningValidatorBase
    {
        private int _inletCount;

        public FirstXInletsNotFilledInWarningValidatorBase(Operator obj, int? inletCount = null)
            : base(obj, postponeExecute: true)
        {
            if (obj == null) throw new NullException(() => obj);
            if (inletCount < 0) throw new Exception("inletCount cannot be less than 0.");

            _inletCount = inletCount ?? obj.Inlets.Count;

            Execute();
        }

        protected override void Execute()
        {
            int i = 0;
            foreach (Inlet inlet in Object.Inlets.Take(_inletCount))
            {
                if (inlet.InputOutlet == null)
                {
                    ValidationMessages.Add(() => Object.Inlets[i].InputOutlet, MessageFormatter.InletNotSet(Object.OperatorTypeName, Object.Name, inlet.Name));
                }
                i++;
            }
        }
    }
}
