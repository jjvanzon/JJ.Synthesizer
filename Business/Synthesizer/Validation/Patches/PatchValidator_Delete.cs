using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_Delete : VersatileValidator<Patch>
    {
        public PatchValidator_Delete([NotNull] Patch lowerPatch)
            : base(lowerPatch)
        {
            string lowerPatchIdentifier = ResourceFormatter.Patch + " " + ValidationHelper.GetUserFriendlyIdentifier(lowerPatch);

            IEnumerable<Operator> customOperators = lowerPatch.EnumerateDependentCustomOperators();
            foreach (Operator op in customOperators)
            {
                Patch higherPatch = op.Patch;
                string higherDocumentPrefix = ValidationHelper.TryGetHigherDocumentPrefix(lowerPatch, higherPatch);
                string higherPatchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                string higherOperatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForCustomOperator(op);

                ValidationMessages.Add(
                    nameof(Patch),
                    CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(lowerPatchIdentifier, higherDocumentPrefix + higherPatchPrefix + higherOperatorIdentifier));
            }
        }
    }
}