using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class DocumentWarningValidator_Recursive : FluentValidator<Document>
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

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                Execute(new PatchWarningValidator_Recursive(patch, _sampleRepository, _alreadyDone), messagePrefix);
            }

            foreach (Sample sample in document.Samples)
            {
                byte[] bytes = _sampleRepository.TryGetBytes(sample.ID);

                Execute(new SampleWarningValidator(sample, bytes, _alreadyDone), ValidationHelper.GetMessagePrefix(sample));
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                Execute(new AudioFileOutputWarningValidator(audioFileOutput), ValidationHelper.GetMessagePrefix(audioFileOutput));
            }

            foreach (Document childDocument in document.ChildDocuments)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefixForChildDocument(childDocument);
                Execute(new DocumentWarningValidator_Recursive(childDocument, _sampleRepository, _alreadyDone));
            }

            // TODO:

            // DocumentWarningValidator_Basic?
            // Curves?
            // ParentDocument
            // DependentOnDocuments
            // DependentDocuments

            // TODO: Compare to validation in Circle code base.
        }
    }
}