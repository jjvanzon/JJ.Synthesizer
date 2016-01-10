using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Linq;

namespace JJ.Business.Synthesizer.Warnings
{
    public abstract class OperatorWarningValidator_Base_AllInletsFilled : FluentValidator<Operator>
    {
        private int _inletCount;

        public OperatorWarningValidator_Base_AllInletsFilled(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute(new OperatorWarningValidator_Base_FirstXInletsFilledIn(Object, Object.Inlets.Count));
        }
    }
}
