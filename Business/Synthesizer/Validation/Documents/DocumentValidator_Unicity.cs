using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Unicity : VersatileValidator<Document>
    {
        public DocumentValidator_Unicity(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ValidateAudioFileOutputNamesUnique();
            ValidatePatchNamesUnique();
            ValidateSampleNamesUnique();
            ValidateScaleNamesUnique();
        }

        private void ValidateAudioFileOutputNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateAudioFileOutputNames(Object);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string message = MessageFormatter.NamesNotUnique_WithEntityTypeNameAndNames(PropertyDisplayNames.AudioFileOutput, duplicateNames);
                ValidationMessages.Add(PropertyNames.AudioFileOutputs, message);
            }
        }

        private void ValidatePatchNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicatePatchNames(Object);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string message = MessageFormatter.NamesNotUnique_WithEntityTypeNameAndNames(PropertyDisplayNames.Patch, duplicateNames);
                ValidationMessages.Add(PropertyNames.Patches, message);
            }
        }

        private void ValidateSampleNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateSampleNames(Object);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string message = MessageFormatter.NamesNotUnique_WithEntityTypeNameAndNames(PropertyDisplayNames.Sample, duplicateNames);
                ValidationMessages.Add(PropertyNames.Samples, message);
            }
        }

        private void ValidateScaleNamesUnique()
        {
            IList<string> duplicateNames = ValidationHelper.GetDuplicateScaleNames(Object);

            // ReSharper disable once InvertIf
            if (duplicateNames.Count > 0)
            {
                string message = MessageFormatter.NamesNotUnique_WithEntityTypeNameAndNames(PropertyDisplayNames.Scale, duplicateNames);
                ValidationMessages.Add(PropertyNames.Scales, message);
            }
        }
    }
}
