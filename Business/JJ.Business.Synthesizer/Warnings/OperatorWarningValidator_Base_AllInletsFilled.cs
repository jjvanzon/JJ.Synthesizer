using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Linq;

namespace JJ.Business.Synthesizer.Warnings
{
    internal abstract class OperatorWarningValidator_Base_AllInletsFilled : FluentValidator<Operator>
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
