using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
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

            int nameCount = document.EnumerateSelfAndParentAndTheirChildren()
                                    .SelectMany(x => x.AudioFileOutputs)
                                    .Where(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateAudioFileOutputNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndTheirChildren()
                                                   .SelectMany(x => x.AudioFileOutputs)
                                                   .GroupBy(x => x.Name?.ToLower())
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();
            return duplicateNames;
        }

        // TODO: Remove outcommented code.
        //// Curve

        //public static bool CurveNameIsUnique(Curve curve)
        //{
        //    if (curve == null) throw new NullException(() => curve);

        //    bool isUnique = CurveNameIsUnique(curve.Document, curve.Name);
        //    return isUnique;
        //}

        //public static bool CurveNameIsUnique(Document document, string name)
        //{
        //    if (document == null) throw new NullException(() => document);

        //    int nameCount = document.EnumerateSelfAndParentAndTheirChildren()
        //                            .SelectMany(x => x.Curves)
        //                            .Where(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
        //                            .Count();

        //    return nameCount <= 1;
        //}

        //public static IList<string> GetDuplicateCurveNames(Document document)
        //{
        //    if (document == null) throw new NullException(() => document);

        //    IList<string> duplicateNames = document.EnumerateSelfAndParentAndTheirChildren()
        //                                           .SelectMany(x => x.Curves)
        //                                           .GroupBy(x => x.Name?.ToLower())
        //                                           .Where(x => x.Count() > 1)
        //                                           .Select(x => x.Key)
        //                                           .ToArray();
        //    return duplicateNames;
        //}

        // Document

        public static bool DocumentNameIsUniqueWithinRootDocument(Document document)
        {
            if (document == null) throw new NullException(() => document);

            int nameCount = document.EnumerateSelfAndParentAndTheirChildren()
                                    .Where(x => String.Equals(x.Name, document.Name, StringComparison.OrdinalIgnoreCase))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateDocumentNamesWithinRootDocument(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndTheirChildren()
                                                   .GroupBy(x => x.Name?.ToLower())
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();
            return duplicateNames;
        }

        // Operator

        public static bool PatchInletNamesAreUniqueWithinPatch(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IList<string> names = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                       .Where(x => !String.IsNullOrEmpty(x.Name))
                                       .Select(x => x.Name.ToLower())
                                       .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            return namesAreUnique;
        }

        public static bool PatchInletListIndexesAreUniqueWithinPatch(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IList<int> listIndexes = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                          .Where(x => DataPropertyParser.DataIsWellFormed(x))
                                          .Select(x => DataPropertyParser.TryParseInt32(x, PropertyNames.ListIndex))
                                          .Where(x => x.HasValue)
                                          .Select(x => x.Value)
                                          .ToArray();

            bool listIndexesAreUnique = listIndexes.Distinct().Count() == listIndexes.Count;
            return listIndexesAreUnique;
        }

        public static bool PatchOutletListIndexesAreUniqueWithinPatch(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IList<int> listIndexes = patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                          .Where(x => DataPropertyParser.DataIsWellFormed(x))
                                          .Select(x => DataPropertyParser.TryParseInt32(x, PropertyNames.ListIndex))
                                          .Where(x => x.HasValue)
                                          .Select(x => x.Value)
                                          .ToArray();

            bool listIndexesAreUnique = listIndexes.Distinct().Count() == listIndexes.Count;
            return listIndexesAreUnique;
        }

        public static bool PatchOutletNamesAreUniqueWithinPatch(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IList<string> names = patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                       .Where(x => !String.IsNullOrEmpty(x.Name))
                                       .Select(x => x.Name.ToLower())
                                       .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            return namesAreUnique;
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

            int nameCount = document.EnumerateSelfAndParentAndTheirChildren()
                                    .SelectMany(x => x.Patches)
                                    .Where(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicatePatchNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndTheirChildren()
                                                   .SelectMany(x => x.Patches)
                                                   .GroupBy(x => x.Name?.ToLower())
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

            int nameCount = document.EnumerateSelfAndParentAndTheirChildren()
                                    .SelectMany(x => x.Samples)
                                    .Where(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateSampleNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndTheirChildren()
                                                   .SelectMany(x => x.Samples)
                                                   .GroupBy(x => x.Name?.ToLower())
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

            int nameCount = document.EnumerateSelfAndParentAndTheirChildren()
                                    .SelectMany(x => x.Scales)
                                    .Where(x => String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase))
                                    .Count();

            return nameCount <= 1;
        }

        public static IList<string> GetDuplicateScaleNames(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IList<string> duplicateNames = document.EnumerateSelfAndParentAndTheirChildren()
                                                   .SelectMany(x => x.Scales)
                                                   .GroupBy(x => x.Name?.ToLower())
                                                   .Where(x => x.Count() > 1)
                                                   .Select(x => x.Key)
                                                   .ToArray();
            return duplicateNames;
        }
    }
}
