using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ClosestOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public ClosestOverInlets_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            bool anyItemsFilledIn = op.Inlets.Skip(1).Where(x => x.InputOutlet != null).Any();
            // ReSharper disable once InvertIf
            if (!anyItemsFilledIn)
            {
                string operatorIdentifier = ValidationHelper.GetIdentifier(op);
                ValidationMessages.Add(() => op.Inlets, MessageFormatter.OperatorHasNoInletsFilledIn_WithOperatorName(operatorIdentifier));
            }
        }
    }
}
