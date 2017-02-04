using System.Linq;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal abstract class OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults : VersatileValidator<Operator>
    {
        public OperatorWarningValidator_Base_AnyInletsFilledInOrHaveDefaults(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            bool anyInletsFilledIn = op.Inlets.Where(x => x.InputOutlet != null &&
                                                         !x.DefaultValue.HasValue)
                                              .Any();
            // ReSharper disable once InvertIf
            if (!anyInletsFilledIn)
            {
                string operatorIdentifier = ValidationHelper.GetIdentifier(op);
                ValidationMessages.Add(() => op.Inlets, MessageFormatter.OperatorHasNoInletsFilledIn_WithOperatorName(operatorIdentifier));
            }
        }
    }
}
