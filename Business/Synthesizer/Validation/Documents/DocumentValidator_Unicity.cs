using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Unicity : VersatileValidator
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentValidator_Unicity(Document document, IDocumentRepository documentRepository)
        {
            if (document == null) throw new NullException(() => document);

            _documentRepository = documentRepository ?? throw new NullException(() => documentRepository);

            ValidateDocumentNameUnique(document);
            ValidateAudioFileOutputNamesUnique(document);
            ValidatePatchNamesUnique(document);
            ValidateScaleNamesUnique(document);
            ValidateDocumentReferencesUnique(document);
            ValidateDocumentReferenceAliasesUnique(document);
        }

        private void ValidateDocumentNameUnique(Document document)
        {
            bool isUnique = ValidationHelper.DocumentNameIsUnique(document, _documentRepository);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                Messages.AddNotUniqueMessageSingular(CommonResourceFormatter.Name, document.Name);
            }
        }

        private void ValidateAudioFileOutputNamesUnique(Document document)
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateAudioFileOutputNames(document);
            
            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.AudioFileOutput + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                Messages.Add(messagePrefix + message);
            }
        }

        private void ValidatePatchNamesUnique(Document document)
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicatePatchNames(document);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Patches + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                Messages.Add(messagePrefix + message);
            }
        }

        private void ValidateScaleNamesUnique(Document document)
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateScaleNames(document);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Scales + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names, duplicateNames);
                Messages.Add(messagePrefix + message);
            }
        }

        private void ValidateDocumentReferencesUnique(Document document)
        {
            IList<DocumentReference> duplicates = ValidationHelper.GetDuplicateLowerDocumentReferences(document);

            // ReSharper disable once InvertIf
            if (duplicates.Count > 0)
            {
                IList<string> duplicateIdentifiers = duplicates.Select(x => ValidationHelper.GetUserFriendlyIdentifier_ForLowerDocumentReference(x)).ToArray();
                string message = ValidationResourceFormatter.NotUniquePlural(ResourceFormatter.Libraries, duplicateIdentifiers);
                Messages.Add(message);
            }
        }

        private void ValidateDocumentReferenceAliasesUnique(Document document)
        {
            IList<string> duplicates = ValidationHelper.GetDuplicateLowerDocumentReferenceAliases(document);

            // ReSharper disable once InvertIf
            if (duplicates.Count > 0)
            {
                string messagePrefix = ResourceFormatter.Libraries + ": ";
                string message = ValidationResourceFormatter.NotUniquePlural(ResourceFormatter.Aliases, duplicates);
                Messages.Add(messagePrefix + message);
            }
        }
    }
}
