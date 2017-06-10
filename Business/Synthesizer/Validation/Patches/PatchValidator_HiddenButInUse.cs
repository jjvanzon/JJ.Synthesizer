using System.Collections.Generic;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_HiddenButInUse : VersatileValidator<Patch>
    {
        public PatchValidator_HiddenButInUse(Patch lowerPatch)
            : base(lowerPatch)
        { 
            // ReSharper disable once InvertIf
            if (lowerPatch.Hidden)
            {
                string lowerPatchIdentifier = ResourceFormatter.Patch + " " + ValidationHelper.GetUserFriendlyIdentifier(lowerPatch);

                IEnumerable<Operator> customOperators = lowerPatch.EnumerateDependentCustomOperators();
                foreach (Operator op in customOperators)
                {
                    bool isExternal = op.Patch.Document != lowerPatch.Document;
                    if (!isExternal)
                    {
                        continue;
                    }

                    Patch higherPatch = op.Patch;
                    string higherDocumentPrefix = ValidationHelper.TryGetHigherDocumentPrefix(lowerPatch, higherPatch);
                    string higherPatchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                    string higherOperatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForCustomOperator(op);

                    ValidationMessages.Add(
                        nameof(Patch),
                        ResourceFormatter.CannotHide_WithName_AndDependentItem(lowerPatchIdentifier, higherDocumentPrefix + higherPatchPrefix + higherOperatorIdentifier));
                }
            }
        }
    }
}