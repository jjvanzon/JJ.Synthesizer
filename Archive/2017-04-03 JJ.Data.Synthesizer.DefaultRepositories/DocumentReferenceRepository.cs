using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DocumentReferenceRepository : RepositoryBase<DocumentReference, int>, IDocumentReferenceRepository
    {
        public DocumentReferenceRepository(IContext context)
            : base(context)
        { }
    }
}
