using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Validation.Scales;
using JJ.Business.Synthesizer.Validation.Curves;
using JJ.Business.Synthesizer.Validation.DocumentReferences;
using JJ.Business.Synthesizer.Validation.Patches;
using JJ.Business.Synthesizer.Validation.Samples;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Recursive : VersatileValidator<Document>
    {
        private readonly HashSet<object> _alreadyDone;
        private readonly RepositoryWrapper _repositories;

        public DocumentValidator_Recursive(
            Document document,
            [NotNull] RepositoryWrapper repositories,
            HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
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
            ExecuteValidator(new DocumentValidator_Unicity(document, _repositories.DocumentRepository));
            ExecuteValidator(new DocumentValidator_DoesNotReferenceItself(document));
            ExecuteValidator(new DocumentValidator_SystemDocumentReferenceMustExist(document, _repositories));

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
                ExecuteValidator(
                    new PatchValidator_WithRelatedEntities(
                        patch,
                        _repositories.CurveRepository,
                        _repositories.SampleRepository,
                        _alreadyDone),
                    messagePrefix);
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
                ExecuteValidator(new DocumentReferenceValidator_Basic(lowerDocumentReference), messagePrefix);
            }
        }
    }
}