using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DocumentReferenceRepository : RepositoryBase<DocumentReference, int>, IDocumentReferenceRepository
    {
        public DocumentReferenceRepository(IContext context)
            : base(context)
        { }
    }
}
