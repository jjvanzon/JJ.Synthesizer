using JJ.Framework.Data;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DocumentRepository : RepositoryBase<Document, int>, IDocumentRepository
    {
        public DocumentRepository(IContext context)
            : base(context)
        { }

        public virtual IList<Document> OrderByName()
        {
            return _context.Query<Document>()
                           .OrderBy(x => x.Name)
                           .ToArray();
        }

        public virtual Document TryGetComplete(int id)
        {
            // By default do it with lazy loading after all.
            return TryGet(id);
        }

        public Document GetComplete(int id)
        {
            Document document = TryGetComplete(id);
            if (document == null)
            {
                throw new NotFoundException<Document>(id);
            }
            return Get(id);
        }
    }
}
