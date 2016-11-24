using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class DocumentWarningValidator_Recursive : VersatileValidator<Document>
    {
        private ISampleRepository _sampleRepository;
        private HashSet<object> _alreadyDone;

        public DocumentWarningValidator_Recursive(Document document, ISampleRepository sampleRepository, HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            Document document = Object;

            if (_alreadyDone.Contains(document))
            {
                return;
            }
            _alreadyDone.Add(document);

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(audioFileOutput);
                ExecuteValidator(new AudioFileOutputWarningValidator(audioFileOutput), messagePrefix);
            }

            if (document.AudioOutput != null)
            {
                ExecuteValidator(new AudioOutputWarningValidator(document.AudioOutput), ValidationHelper.GetMessagePrefix(document.AudioOutput));
            }

            foreach (Curve curve in document.Curves)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(curve);
                ExecuteValidator(new CurveWarningValidator(curve), messagePrefix);
            }

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                ExecuteValidator(new PatchWarningValidator_Recursive(patch, _sampleRepository, _alreadyDone), messagePrefix);
            }

            foreach (Sample sample in document.Samples)
            {
                byte[] bytes = _sampleRepository.TryGetBytes(sample.ID);

                string messagePrefix = ValidationHelper.GetMessagePrefix(sample);
                ExecuteValidator(new SampleWarningValidator(sample, bytes, _alreadyDone), messagePrefix);
            }

            // TODO:

            // DocumentWarningValidator_Basic?
            // DependentOnDocuments
            // DependentDocuments

            // TODO: Compare to validation in Circle code base.
        }
    }
}