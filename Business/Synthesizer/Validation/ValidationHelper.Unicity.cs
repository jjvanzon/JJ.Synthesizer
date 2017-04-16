using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
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

        // Operator

        public static bool PatchInletNamesAreUniqueWithinPatch(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            IList<string> names = patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                       .Where(x => NameHelper.IsFilledIn(x.Name))
                                       .Select(x => NameHelper.ToCanonical(x.Name))
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
                                       .Where(x => NameHelper.IsFilledIn(x.Name))
                                       .Select(x => NameHelper.ToCanonical(x.Name))
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
