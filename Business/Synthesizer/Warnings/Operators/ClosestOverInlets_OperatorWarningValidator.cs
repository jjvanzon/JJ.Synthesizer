using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class ClosestOverInlets_OperatorWarningValidator : OperatorWarningValidator_Base_FirstXInletsFilledInOrHaveDefaults
    {
        public ClosestOverInlets_OperatorWarningValidator(Operator obj)
            : base(obj, inletCount: 1)
        { }

        protected override void Execute()
        {
            Operator op = Obj;

            bool anyItemsFilledIn = op.Inlets
                                      // Skip the signal inlet,
                                      // Only check the items. 
                                      .Skip(1)
                                      .Where(x => x.InputOutlet != null)
                                      .Any();

            // ReSharper disable once InvertIf
            if (!anyItemsFilledIn)
            {
                // TODO: Lower priority: This message slightly lies, because the signal inlet may very wel be filled in.
                ValidationMessages.AddIsEmptyMessage(PropertyNames.Inlets, ResourceFormatter.Inlets);
            }
        }
    }
}
