using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System.Linq;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults : OperatorWarningValidator_Base
    {
        private readonly int _inletCount;

        public OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults(Operator obj, int inletCount)
            : base(obj, postponeExecute: true)
        {
            if (obj == null) throw new NullException(() => obj);
            if (inletCount < 0) throw new LessThanException(() => inletCount, 0);

            _inletCount = inletCount;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            int i = 0;
            foreach (Inlet inlet in Obj.Inlets.OrderBy(x => x.ListIndex).Take(_inletCount))
            {
                if (inlet.InputOutlet == null && !inlet.DefaultValue.HasValue)
                {
                    // ReSharper disable once AccessToModifiedClosure
                    ValidationMessages.Add(() => Obj.Inlets[i].InputOutlet, ResourceFormatter.InletNotSet(Obj.GetOperatorTypeEnum(), Obj.Name, inlet.Name));
                }
                i++;
            }
        }
    }
}
