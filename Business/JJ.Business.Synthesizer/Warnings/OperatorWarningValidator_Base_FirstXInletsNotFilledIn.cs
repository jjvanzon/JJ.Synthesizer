using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
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
    public abstract class OperatorWarningValidator_Base_FirstXInletsNotFilledIn : OperatorWarningValidator_Base
    {
        private int _inletCount;

        public OperatorWarningValidator_Base_FirstXInletsNotFilledIn(Operator obj, int? inletCount = null)
            : base(obj, postponeExecute: true)
        {
            if (obj == null) throw new NullException(() => obj);

            _inletCount = inletCount ?? obj.Inlets.Count;

            if (_inletCount < 0) throw new LessThanException(() => _inletCount, 0);

            Execute();
        }

        protected override void Execute()
        {
            int i = 0;
            foreach (Inlet inlet in Object.Inlets.Take(_inletCount))
            {
                if (inlet.InputOutlet == null)
                {
                    ValidationMessages.Add(() => Object.Inlets[i].InputOutlet, MessageFormatter.InletNotSet(Object.GetOperatorTypeEnum(), Object.Name, inlet.Name));
                }
                i++;
            }
        }
    }
}
