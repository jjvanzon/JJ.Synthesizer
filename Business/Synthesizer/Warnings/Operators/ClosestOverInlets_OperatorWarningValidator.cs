using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ClosestOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public ClosestOverInlets_OperatorWarningValidator(Operator op)
            : base(op, inletCount: 1)
        { 
            bool anyItemsFilledIn = op.Inlets
                                      // Skip the signal inlet,
                                      // Only check the items. 
                                      .Skip(1)
                                      .Where(x => x.InputOutlet != null)
                                      .Any();

            // ReSharper disable once InvertIf
            if (!anyItemsFilledIn)
            {
                ValidationMessages.AddIsEmptyMessage(nameof(ClosestOverInlets_OperatorWrapper.Items), ResourceFormatter.ItemList);
            }
        }
    }
}
