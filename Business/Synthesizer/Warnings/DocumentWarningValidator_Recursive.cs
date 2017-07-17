using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class DocumentWarningValidator_Recursive : VersatileValidator
    {
        public DocumentWarningValidator_Recursive(
            Document document,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            HashSet<object> alreadyDone)
        {
            if (document == null) throw new NullException(() => document);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();
            if (curveRepository == null) throw new NullException(() => curveRepository);

            if (alreadyDone.Contains(document))
            {
                return;
            }
            alreadyDone.Add(document);

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(audioFileOutput);
                ExecuteValidator(new AudioFileOutputWarningValidator(audioFileOutput), messagePrefix);
            }

            if (document.AudioOutput != null)
            {
                ExecuteValidator(new AudioOutputWarningValidator(document.AudioOutput), ValidationHelper.GetMessagePrefix(document.AudioOutput));
            }

            // There are no Curve warnings.

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                ExecuteValidator(
                    new PatchWarningValidator_WithRelatedEntities(patch, sampleRepository, curveRepository, alreadyDone),
                    messagePrefix);
            }

            foreach (Sample sample in document.Samples)
            {
                byte[] bytes = sampleRepository.TryGetBytes(sample.ID);

                string messagePrefix = ValidationHelper.GetMessagePrefix(sample);
                ExecuteValidator(new SampleWarningValidator(sample, bytes, alreadyDone), messagePrefix);
            }

            // TODO:

            // DocumentWarningValidator_Basic?
            // DependentOnDocuments
            // DependentDocuments

            // TODO: Compare to validation in Circle code base.
        }
    }
}