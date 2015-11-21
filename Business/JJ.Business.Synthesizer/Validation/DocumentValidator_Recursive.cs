using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_Recursive : FluentValidator<Document>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;
        private HashSet<object> _alreadyDone;

        public DocumentValidator_Recursive(
            Document document, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository,
            HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;
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

            bool isRootDocument = document.ParentDocument == null;
            if (isRootDocument)
            {
                Execute<DocumentValidator_RootDocument>();
                Execute<DocumentValidator_Unicity>();
            }
            else
            {
                Execute<DocumentValidator_ChildDocument>();
            }

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

                string messagePrefix = ValidationHelper.GetMessagePrefix(curve);
                Execute(new CurveValidator_WithoutNodes(curve), messagePrefix);
                Execute(new CurveValidator_Nodes(curve), messagePrefix);
            }

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(patch);
                Execute(new PatchValidator_Recursive(patch, _curveRepository, _sampleRepository, _documentRepository, _alreadyDone), messagePrefix);
                Execute(new PatchValidator_InDocument(patch), messagePrefix);
            }

            foreach (Scale scale in document.Scales)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(scale);
                Execute(new ScaleValidator_InDocument(scale), messagePrefix);
                Execute(new ScaleValidator_Versatile_WithoutTones(scale), messagePrefix);
                Execute(new ScaleValidator_Tones(scale), messagePrefix);
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
                Execute(new DocumentValidator_Recursive(childDocument, _curveRepository, _sampleRepository, _documentRepository, _alreadyDone), ValidationHelper.GetMessagePrefixForChildDocument(childDocument));
            }

            // TODO:

            // ParentDocument
            // DependentOnDocuments
            // DependentDocuments
            // MainPatch

            // TODO: Compare to validation in Circle code base.
            // TODO: Some collections / references must be filled in / empty depending on its being a root document or not.
        }
    }
}