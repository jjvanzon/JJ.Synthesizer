using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Collections;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentReferenceValidator_Delete : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_Delete([NotNull] DocumentReference obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            DocumentReference documentReference = Obj;

            HashSet<int> lowerPatchIDs = documentReference.LowerDocument.Patches.Select(x => x.ID).ToHashSet();

            IEnumerable<CustomOperator_OperatorWrapper> higherCustomOperatorWrappers =
                documentReference.HigherDocument.Patches
                                 .SelectMany(x => x.EnumerateOperatorWrappersOfType<CustomOperator_OperatorWrapper>())
                                 .Where(x => lowerPatchIDs.Contains(x.UnderlyingPatchID ?? 0))
                                 .ToArray();

            foreach (CustomOperator_OperatorWrapper higherCustomOperatorWrapper in higherCustomOperatorWrappers)
            {
                //string documentReferenceIdentifier = PropertyDisplayNames.DocumentReference + " " + ValidationHelper.GetIdentifier_ForLowerDocumentReference(documentReference);
                //string cannotDeleteMessagePrefix = CommonMessageFormatter.CannotDeleteObjectWithName( documentReferenceIdentifier) + ": ";
                //string message = $"{)}";
            }

            throw new NotImplementedException();
        }
    }
}
