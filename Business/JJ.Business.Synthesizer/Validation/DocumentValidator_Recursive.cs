using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Validation;

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

            foreach (Curve curve in document.Curves)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Curve, curve.Name);
                Execute(new CurveValidator(curve, _alreadyDone), messagePrefix);
            }

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Patch, patch.Name);
                Execute(new PatchValidator_Recursive(patch, _repositoryWrapper.CurveRepository, _repositoryWrapper.SampleRepository, _alreadyDone), messagePrefix);
                Execute(new PatchValidator_InDocument(patch));
            }

            foreach (Sample sample in document.Samples)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Sample, sample.Name);
                Execute(new SampleValidator(sample, _alreadyDone), messagePrefix);
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.AudioFileOutput, audioFileOutput.Name);
                Execute(new AudioFileOutputValidator(audioFileOutput), messagePrefix);
            }

            foreach (Document childDocument in document.ChildDocuments)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.ChildDocument, childDocument.Name);
                Execute(new DocumentValidator_Recursive(childDocument, _repositoryWrapper, _alreadyDone));
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