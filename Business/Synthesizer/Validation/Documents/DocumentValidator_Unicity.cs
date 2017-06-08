using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Unicity : VersatileValidator<Document>
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentValidator_Unicity(Document obj, [NotNull] IDocumentRepository documentRepository)
            : base(obj, postponeExecute: true)
        {
            _documentRepository = documentRepository ?? throw new NullException(() => documentRepository);

            Execute();
        }
        protected override void Execute()
        {
            ValidateDocumentNameUnique();
            ValidateAudioFileOutputNamesUnique();
            ValidatePatchNamesUnique();
            ValidateSampleNamesUnique();
            ValidateScaleNamesUnique();
            ValidateDocumentReferencesUnique();
            ValidateDocumentReferenceAliasesUnique();
        }

        private void ValidateDocumentNameUnique()
        {
            bool isUnique = ValidationHelper.DocumentNameIsUnique(Obj, _documentRepository);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(nameof(Obj.Name), CommonResourceFormatter.Name, Obj.Name);
            }
        }

        private void ValidateAudioFileOutputNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateAudioFileOutputNames(Obj);
            
            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.AudioFileOutput + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                ValidationMessages.Add(nameof(Obj.AudioFileOutputs), messagePrefix + message);
            }
        }

        private void ValidatePatchNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicatePatchNames(Obj);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Patches + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                ValidationMessages.Add(PropertyNames.Patches, messagePrefix + message);
            }
        }

        private void ValidateSampleNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateSampleNames(Obj);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Samples + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                ValidationMessages.Add(nameof(Document.Samples), messagePrefix + message);
            }
        }

        private void ValidateScaleNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateScaleNames(Obj);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Scales + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                ValidationMessages.Add(nameof(Document.Scales), messagePrefix + message);
            }
        }

        private void ValidateDocumentReferencesUnique()
        {
            IList<DocumentReference> duplicates = ValidationHelper.GetDuplicateLowerDocumentReferences(Obj);

            // ReSharper disable once InvertIf
            if (duplicates.Count > 0)
            {
                IList<string> duplicateIdentifiers = duplicates.Select(x => ValidationHelper.GetUserFriendlyIdentifier_ForLowerDocumentReference(x)).ToArray();
                string message = ValidationResourceFormatter.NotUniquePlural(ResourceFormatter.Libraries, duplicateIdentifiers);
                ValidationMessages.Add(nameof(Document.LowerDocumentReferences), message);
            }
        }

        private void ValidateDocumentReferenceAliasesUnique()
        {
            IList<string> duplicates = ValidationHelper.GetDuplicateLowerDocumentReferenceAliases(Obj);

            // ReSharper disable once InvertIf
            if (duplicates.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Libraries + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(ResourceFormatter.Aliases, duplicates);
                ValidationMessages.Add(nameof(Document.LowerDocumentReferences), messagePrefix + message);
            }
        }
    }
}
