using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_Delete : VersatileValidator
    {
        public PatchValidator_Delete([NotNull] Patch lowerPatch)
        {
            if (lowerPatch == null) throw new NullException(() => lowerPatch);

            string lowerPatchIdentifier = ResourceFormatter.Patch + " " + ValidationHelper.GetUserFriendlyIdentifier(lowerPatch);

            IEnumerable<Operator> derivedOperators = lowerPatch.EnumerateDerivedOperators();
            foreach (Operator op in derivedOperators)
            {
                Patch higherPatch = op.Patch;
                string higherDocumentPrefix = ValidationHelper.TryGetHigherDocumentPrefix(lowerPatch, higherPatch);
                string higherPatchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                string higherOperatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForCustomOperator(op);

                Messages.Add(
                    CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(
                        lowerPatchIdentifier,
                        higherDocumentPrefix + higherPatchPrefix + higherOperatorIdentifier));
            }
        }
    }
}