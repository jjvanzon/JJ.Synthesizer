using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.ResourceStrings;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_Delete : VersatileValidator
    {
        public DocumentReferenceValidator_Delete(DocumentReference obj, ICurveRepository curveRepository)
        {
            if (obj == null) throw new NullException(() => obj);

            DocumentReference documentReference = obj;

            string documentReferenceIdentifier = ResourceFormatter.Library + " " + ValidationHelper.GetUserFriendlyIdentifier_ForLowerDocumentReference(documentReference);

            if (documentReference.LowerDocument.IsSystemDocument())
            {
                string message = CommonResourceFormatter.CannotDelete_WithName(documentReferenceIdentifier);
                Messages.Add(message);
                return;
            }

            HashSet<int> lowerPatchIDHashSet = documentReference.LowerDocument.Patches.Select(x => x.ID).ToHashSet();

            IEnumerable<Operator> higherCustomOperators = documentReference.HigherDocument
                                                                           .Patches
                                                                           .SelectMany(x => x.Operators)
                                                                           .Where(x => lowerPatchIDHashSet.Contains(x.UnderlyingPatch?.ID ?? 0));

            foreach (Operator higherCustomOperator in higherCustomOperators)
            {
                string higherPatchPrefix = ValidationHelper.GetMessagePrefix(higherCustomOperator.Patch);
                string higherCustomOperatorIdentifier = ValidationHelper.GetUserFriendlyIdentifier(higherCustomOperator, curveRepository);

                string message = CommonResourceFormatter.CannotDelete_WithName_AndDependency(
                    documentReferenceIdentifier,
                    higherPatchPrefix + higherCustomOperatorIdentifier);

                Messages.Add(message);
            }
        }
    }
}
