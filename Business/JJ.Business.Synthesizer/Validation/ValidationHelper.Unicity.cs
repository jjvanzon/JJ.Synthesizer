using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

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

            int nameCount = document.EnumerateSelfAndParentAndChildren()
                                    .SelectMany(x => x.AudioFileOutputs)
                                    .Where(x => String.Equals(x.Name, name))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateAudioFileOutputNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndChildren()
                                                   .SelectMany(x => x.AudioFileOutputs)
                                                   .GroupBy(x => x.Name)
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();

            return duplicateNames;
        }

        // Curve

        public static bool CurveNameIsUnique(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            bool isUnique = CurveNameIsUnique(curve.Document, curve.Name);
            return isUnique;
        }

        public static bool CurveNameIsUnique(Document document, string name)
        {
            if (document == null) throw new NullException(() => document);

            int nameCount = document.EnumerateSelfAndParentAndChildren()
                                    .SelectMany(x => x.Curves)
                                    .Where(x => String.Equals(x.Name, name))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateCurveNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndChildren()
                                                   .SelectMany(x => x.Curves)
                                                   .GroupBy(x => x.Name)
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();

            return duplicateNames;
        }


        // Document

        public static bool DocumentNameIsUniqueWithinRootDocument(Document document)
        {
            if (document == null) throw new NullException(() => document);

            int nameCount = document.EnumerateSelfAndParentAndChildren()
                                    .Where(x => String.Equals(x.Name, document.Name))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateDocumentNamesWithinRootDocument(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndChildren()
                                                   .GroupBy(x => x.Name)
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();
            return duplicateNames;
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

            int nameCount = document.EnumerateSelfAndParentAndChildren()
                                    .SelectMany(x => x.Patches)
                                    .Where(x => String.Equals(x.Name, name))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicatePatchNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndChildren()
                                                   .SelectMany(x => x.Patches)
                                                   .GroupBy(x => x.Name)
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();

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

            int nameCount = document.EnumerateSelfAndParentAndChildren()
                                    .SelectMany(x => x.Samples)
                                    .Where(x => String.Equals(x.Name, name))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateSampleNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndChildren()
                                                   .SelectMany(x => x.Samples)
                                                   .GroupBy(x => x.Name)
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();

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

            int nameCount = document.EnumerateSelfAndParentAndChildren()
                                    .SelectMany(x => x.Scales)
                                    .Where(x => String.Equals(x.Name, name))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateScaleNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndChildren()
                                                   .SelectMany(x => x.Scales)
                                                   .GroupBy(x => x.Name)
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();

            return duplicateNames;
        }
    }
}
