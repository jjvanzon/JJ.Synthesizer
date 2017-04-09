using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Validation.Scales;
using JJ.Business.Synthesizer.Validation.Curves;
using JJ.Business.Synthesizer.Validation.Patches;
using JJ.Business.Synthesizer.Validation.Samples;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Recursive : VersatileValidator<Document>
    {
        private readonly ICurveRepository _curveRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly HashSet<object> _alreadyDone;

        public DocumentValidator_Recursive(
            Document document, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
            _alreadyDone = alreadyDone ?? throw new AlreadyDoneIsNullException();

            Execute();
        }

        protected sealed override void Execute()
        {
            Document document = Obj;

            if (_alreadyDone.Contains(document))
            {
                return;
            }
            _alreadyDone.Add(document);

            ExecuteValidator(new DocumentValidator_Basic(document));
            ExecuteValidator(new DocumentValidator_Unicity(document));

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                ExecuteValidator(new AudioFileOutputValidator(audioFileOutput), ValidationHelper.GetMessagePrefix(audioFileOutput));
            }

            if (document.AudioOutput != null)
            {
                ExecuteValidator(new AudioOutputValidator(document.AudioOutput), ValidationHelper.GetMessagePrefix(document.AudioOutput));
            }

            foreach (Curve curve in document.Curves)
            {
                if (_alreadyDone.Contains(curve))
                {
                    continue;
                }
                _alreadyDone.Add(curve);

                string messagePrefix = ValidationHelper.GetMessagePrefix(curve);
                ExecuteValidator(new CurveValidator_WithoutNodes(curve), messagePrefix);
                ExecuteValidator(new CurveValidator_Nodes(curve), messagePrefix);
            }

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                ExecuteValidator(new PatchValidator_WithRelatedEntities(patch, _curveRepository, _sampleRepository, _patchRepository, _alreadyDone), messagePrefix);
            }

            foreach (Scale scale in document.Scales)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(scale);
                ExecuteValidator(new ScaleValidator_InDocument(scale), messagePrefix);
                ExecuteValidator(new Versatile_ScaleValidator_WithoutTones(scale), messagePrefix);
                ExecuteValidator(new ScaleValidator_Tones(scale), messagePrefix);
            }

            foreach (Sample sample in document.Samples)
            {
                if (_alreadyDone.Contains(sample))
                {
                    continue;
                }
                _alreadyDone.Add(sample);

                ExecuteValidator(new SampleValidator(sample), ValidationHelper.GetMessagePrefix(sample));
            }

            foreach (DocumentReference lowerDocumentReference in document.LowerDocumentReferences)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix_ForLowerDocumentReference(lowerDocumentReference);
                ExecuteValidator(new DocumentReferenceValidator(lowerDocumentReference), messagePrefix);
            }
        }
    }
}