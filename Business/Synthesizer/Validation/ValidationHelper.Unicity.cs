﻿using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Validation
{
    public static partial class ValidationHelper
    {
        // AudioFileOutput

        public static bool AudioFileOutputNameIsUnique(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            bool isUnique = AudioFileOutputNameIsUnique(audioFileOutput.Document, audioFileOutput.Name);
            return isUnique;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool AudioFileOutputNameIsUnique(Document document, string name)
        {
            if (document == null) throw new NullException(() => document);

            bool isUnique = NameIsUnique(document.AudioFileOutputs.Select(x => x.Name), name);

            return isUnique;
        }

        public static IList<string> GetDuplicateAudioFileOutputNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = GetDuplicateNames(document.AudioFileOutputs.Select(x => x.Name));

            return duplicateNames;
        }

        // Document

        public static bool DocumentNameIsUnique(Document document, IDocumentRepository documentRepository)
        {
            bool isUnique = DocumentNameIsUnique(document, documentRepository, document.Name);

            return isUnique;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool DocumentNameIsUnique(Document document, IDocumentRepository documentRepository, string name)
        {
            if (document == null) throw new NullException(() => document);
            if (documentRepository == null) throw new NullException(() => documentRepository);

            IList<Document> documents = documentRepository.GetAll();

            string canonicalName = NameHelper.ToCanonical(name);

            bool alreadyExists = documents.Where(x => x.ID != document.ID)
                                          .Select(x => x.Name)
                                          .Any(x => NameHelper.AreEqual(x, canonicalName));

            bool isUnique = !alreadyExists;

            return isUnique;
        }

        // DocumentReference

        public static bool DocumentReference_LowerDocument_IsUnique(DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            if (documentReference.HigherDocument == null)
            {
                return true;
            }

            bool isUnique = DocumentReference_LowerDocument_IsUnique(documentReference.HigherDocument, documentReference.LowerDocument);

            return isUnique;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool DocumentReference_LowerDocument_IsUnique(Document higherDocument, Document lowerDocument)
        {
            if (higherDocument == null) throw new NullException(() => higherDocument);

            int count = higherDocument.LowerDocumentReferences
                                      .Where(x => x.LowerDocument?.ID == lowerDocument?.ID)
                                      .Take(2)
                                      .Count();

            bool isUnique = count <= 1;

            return isUnique;
        }

        public static IList<DocumentReference> GetDuplicateLowerDocumentReferences(Document higherDocument)
        {
            if (higherDocument == null) throw new NullException(() => higherDocument);

            IList<DocumentReference> duplicates = higherDocument.LowerDocumentReferences
                                                                .GroupBy(x => x.LowerDocument)
                                                                .Where(x => x.Count() > 1)
                                                                .Select(x => x.First())
                                                                .ToArray();
            return duplicates;
        }

        public static bool DocumentReferenceAliasIsUnique(DocumentReference documentReference)
        {
            if (documentReference == null) throw new NullException(() => documentReference);

            if (documentReference.HigherDocument == null)
            {
                return true;
            }

            bool isUnique = DocumentReferenceAliasIsUnique(documentReference.HigherDocument, documentReference.Alias);

            return isUnique;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool DocumentReferenceAliasIsUnique(Document document, string alias)
        {
            if (document == null) throw new NullException(() => document);

            bool isUnique = NameIsUnique(document.LowerDocumentReferences.GetFilledInAliases(), alias);

            return isUnique;
        }

        public static IList<string> GetDuplicateLowerDocumentReferenceAliases(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateAliases = GetDuplicateNames(document.LowerDocumentReferences.GetFilledInAliases());

            return duplicateAliases;
        }

        // Patch

        public static bool PatchNameIsUnique(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            bool isUnique = PatchNameIsUnique(patch.Document, patch.Name);

            return isUnique;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool PatchNameIsUnique(Document document, string name)
        {
            if (document == null) throw new NullException(() => document);

            bool isUnique = NameIsUnique(document.Patches.Select(x => x.Name), name);

            return isUnique;
        }

        public static IList<string> GetDuplicatePatchNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = GetDuplicateNames(document.Patches.Select(x => x.Name));

            return duplicateNames;
        }

        // Scale

        public static bool ScaleNameIsUnique(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            bool isUnique = ScaleNameIsUnique(scale.Document, scale.Name);

            return isUnique;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool ScaleNameIsUnique(Document document, string name)
        {
            if (document == null) throw new NullException(() => document);

            bool isUnique = NameIsUnique(document.Scales.Select(x => x.Name), name);

            return isUnique;
        }

        public static IList<string> GetDuplicateScaleNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = GetDuplicateNames(document.Scales.Select(x => x.Name));

            return duplicateNames;
        }

        // General

        private static IList<string> GetDuplicateNames(IEnumerable<string> nameEnumerable)
        {
            IList<string> duplicateNames = nameEnumerable.GroupBy(NameHelper.ToCanonical)
                                                         .Where(x => x.Count() > 1)
                                                         .Select(x => x.First())
                                                         .ToArray();
            return duplicateNames;
        }

        private static bool NameIsUnique(IEnumerable<string> nameEnumerable, string name)
        {
            string canonicalName = NameHelper.ToCanonical(name);

            int nameCount = nameEnumerable.Where(x => string.Equals(NameHelper.ToCanonical(x), canonicalName))
                                          .Take(2)
                                          .Count();
            return nameCount <= 1;
        }
    }
}
