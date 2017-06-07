using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation
{
    internal static partial class ValidationHelper
    {
        // AudioFileOutput

        public static bool AudioFileOutputNameIsUnique(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            bool isUnique = AudioFileOutputNameIsUnique(audioFileOutput.Document, audioFileOutput.Name);
            return isUnique;
        }

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

        public static bool DocumentReference_LowerDocument_IsUnique(Document higherDocument, [CanBeNull] Document lowerDocument)
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

        // Sample

        public static bool SampleNameIsUnique(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            bool isUnique = SampleNameIsUnique(sample.Document, sample.Name);

            return isUnique;
        }

        public static bool SampleNameIsUnique(Document document, string name)
        {
            if (document == null) throw new NullException(() => document);

            bool isUnique = NameIsUnique(document.Samples.Select(x => x.Name), name);

            return isUnique;
        }

        public static IList<string> GetDuplicateSampleNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = GetDuplicateNames(document.Samples.Select(x => x.Name));

            return duplicateNames;
        }

        // Scale

        public static bool ScaleNameIsUnique(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            bool isUnique = ScaleNameIsUnique(scale.Document, scale.Name);

            return isUnique;
        }

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
            IList<string> duplicateNames = nameEnumerable.GroupBy(x => NameHelper.ToCanonical(x))
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
