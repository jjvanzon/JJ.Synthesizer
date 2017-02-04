using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class DimensionToOutlets_OperatorWarningValidator : VersatileValidator<Operator>
    {
        public DimensionToOutlets_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            // ReSharper disable once InvertIf
            if (op.Inlets.Count >= 1)
            {
                // ReSharper disable once InvertIf
                if (op.Inlets[0].InputOutlet == null)
                {
                    string operatorIdentifier = ValidationHelper.GetIdentifier(op);
                    string message = MessageFormatter.OperatorHasNoInletsFilledIn_WithOperatorName(operatorIdentifier);

                    ValidationMessages.Add(() => op.Inlets[0].InputOutlet, message);
                }
            }
        }
    }
}
