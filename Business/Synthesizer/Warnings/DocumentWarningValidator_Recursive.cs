using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class DocumentWarningValidator_Recursive : VersatileValidator<Document>
    {
        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly HashSet<object> _alreadyDone;

        public DocumentWarningValidator_Recursive(
            [NotNull] Document document,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] IPatchRepository patchRepository,
            [NotNull] HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _alreadyDone = alreadyDone ?? throw new AlreadyDoneIsNullException();
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }


        protected override void Execute()
        {
            Document document = Obj;

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

            // There are no Curve warnings.

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                ExecuteValidator(
                    new PatchWarningValidator_WithRelatedEntities(patch, _sampleRepository, _curveRepository, _patchRepository, _alreadyDone),
                    messagePrefix);
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