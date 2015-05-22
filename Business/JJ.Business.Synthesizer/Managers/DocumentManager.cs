using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.Managers
{
    public class DocumentManager
    {
        private RepositoryWrapper _repositoryWrapper;

        public DocumentManager(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositoryWrapper = repositoryWrapper;
        }                                                                       

        public VoidResult CanDelete(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new DocumentValidator_Delete(document);

            if (!validator.IsValid)
            {
                var result = new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
                return result;
            }
            else
            {
                var result = new VoidResult
                {
                    Successful = true,
                    Messages = new Message[0]
                };
                return result;
            }
        }

        public void DeleteWithRelatedEntities(int documentID)
        {
            Document document = _repositoryWrapper.DocumentRepository.Get(documentID);
            DeleteWithRelatedEntities(document);
        }

        public void DeleteWithRelatedEntities(Document document)
        {
            if (document == null) throw new NullException(() => document);

            document.DeleteRelatedEntities(_repositoryWrapper);

            document.UnlinkRelatedEntities();

            _repositoryWrapper.DocumentRepository.Delete(document);

            _repositoryWrapper.Commit();
        }

        public Document CreateInstrument(int parentDocumentID)
        {
            Document parentDocument = _repositoryWrapper.DocumentRepository.Get(parentDocumentID);
            Document instrument = CreateInstrument(parentDocument);
            return instrument;
        }

        public Document CreateInstrument(Document parentDocument)
        {
            if (parentDocument == null) throw new NullException(() => parentDocument);

            Document instrument = _repositoryWrapper.DocumentRepository.Create();
            instrument.LinkInstrumentToDocument(parentDocument);

            ISideEffect sideEffect = new Instrument_SideEffect_GenerateName(instrument);
            sideEffect.Execute();

            return instrument;
        }
    }
}
