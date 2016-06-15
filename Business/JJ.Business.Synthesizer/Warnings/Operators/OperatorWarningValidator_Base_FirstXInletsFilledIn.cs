using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Linq;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_FirstXInletsFilledIn : OperatorWarningValidator_Base
    {
        private int _inletCount;

        public OperatorWarningValidator_Base_FirstXInletsFilledIn(Operator obj, int inletCount)
            : base(obj, postponeExecute: true)
        {
            if (obj == null) throw new NullException(() => obj);
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);

            _inletCount = inletCount;

            Execute();
        }

        protected override void Execute()
        {
            int i = 0;
            foreach (Inlet inlet in Object.Inlets.OrderBy(x => x.ListIndex).Take(_inletCount))
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
