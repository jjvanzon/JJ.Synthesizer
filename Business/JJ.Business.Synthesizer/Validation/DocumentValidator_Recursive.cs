using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation
{
    public class DocumentValidator_Recursive : FluentValidator<Document>
    {
        private RepositoryWrapper _repositoryWrapper;
        private HashSet<object> _alreadyDone;

        // TODO: See if you can get away with passing less repositories than present in the RepositoryWrapper.
        public DocumentValidator_Recursive(Document document, RepositoryWrapper repositoryWrapper, HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _repositoryWrapper = repositoryWrapper;
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

            Execute<DocumentValidator_Basic>();
            Execute<DocumentValidator_Unique_ScaleNames>();

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                Execute(new AudioFileOutputValidator(audioFileOutput), ValidationHelper.GetMessagePrefix(audioFileOutput));
            }

            foreach (Curve curve in document.Curves)
            {
                if (_alreadyDone.Contains(curve))
                {
                    continue;
                }
                _alreadyDone.Add(curve);

                Execute(new CurveValidator(curve), ValidationHelper.GetMessagePrefix(curve));
            }

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                Execute(new PatchValidator_Recursive(patch, _repositoryWrapper.CurveRepository, _repositoryWrapper.SampleRepository, _repositoryWrapper.DocumentRepository, _alreadyDone), messagePrefix);
                Execute(new PatchValidator_InDocument(patch), messagePrefix);
            }

            foreach (Scale scale in document.Scales)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(scale);
                Execute(new ScaleValidator_InDocument(scale), messagePrefix);
                Execute(new ScaleValidator_Versatile(scale), messagePrefix);
            }

            foreach (Sample sample in document.Samples)
            {
                if (_alreadyDone.Contains(sample))
                {
                    continue;
                }
                _alreadyDone.Add(sample);

                Execute(new SampleValidator(sample), ValidationHelper.GetMessagePrefix(sample));
            }

            foreach (Document childDocument in document.ChildDocuments)
            {
                Execute(new DocumentValidator_Recursive(childDocument, _repositoryWrapper, _alreadyDone), ValidationHelper.GetMessagePrefixForChildDocument(childDocument));
            }

            // TODO:

            // ParentDocument
            // DependentOnDocuments
            // DependentDocuments
            // MainPatch

            // Name unicity within a root document.
            // TODO: Compare to validation in Circle code base.
            // TODO: Some collections / references must be filled in / empty depending on its being a root document or not.
        }
    }
}