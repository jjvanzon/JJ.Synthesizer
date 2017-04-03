using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentReferenceValidator_Delete : VersatileValidator<DocumentReference>
    {
        private readonly IPatchRepository _patchRepository;

        public DocumentReferenceValidator_Delete([NotNull] DocumentReference obj, [NotNull] IPatchRepository patchRepository)
            : base(obj)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;
        }

        protected override void Execute()
        {
            DocumentReference documentReference = Obj;

            string documentReferenceIdentifier = ResourceFormatter.LowerDocument + " " + ValidationHelper.GetUserFriendlyIdentifier_ForLowerDocumentReference(documentReference);

            HashSet<int> lowerPatchIDHashSet = documentReference.LowerDocument.Patches.Select(x => x.ID).ToHashSet();

            IEnumerable<Operator> higherCustomOperators = documentReference.HigherDocument
                                                                           .Patches
                                                                           .SelectMany(x => x.EnumerateOperatorWrappersOfType<CustomOperator_OperatorWrapper>())
                                                                           .Where(x => lowerPatchIDHashSet.Contains(x.UnderlyingPatchID ?? 0))
                                                                           .Select(x => x.WrappedOperator);

            foreach (Operator higherCustomOperator in higherCustomOperators)
            {
                string higherPatchPrefix = ValidationHelper.GetMessagePrefix(higherCustomOperator.Patch);
                string higherCustomOperatorIdentifier = ValidationHelper.GetUserFriendlyIdentifier_ForCustomOperator(higherCustomOperator, _patchRepository);

                string message = CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(
                    documentReferenceIdentifier,
                    higherPatchPrefix + higherCustomOperatorIdentifier);

                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }
        }
    }
}
