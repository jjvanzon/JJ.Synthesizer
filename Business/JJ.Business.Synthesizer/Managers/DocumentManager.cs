using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Helpers;

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
        }
    }
}
