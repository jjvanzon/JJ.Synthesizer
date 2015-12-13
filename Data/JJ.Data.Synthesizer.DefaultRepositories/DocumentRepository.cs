using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using JJ.Framework.Common;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DocumentRepository : RepositoryBase<Document, int>, IDocumentRepository
    {
        public DocumentRepository(IContext context)
            : base(context)
        { }

        public virtual IList<Document> GetPageOfRootDocumentsOrderedByName(int firstIndex, int pageSize)
        {
            return _context.Query<Document>()
                           .Where(x => x.ParentDocument == null)
                           .OrderBy(x => x.Name)
                           .Skip(firstIndex)
                           .Take(pageSize)
                           .ToArray();
        }

        public virtual int CountRootDocuments()
        {
            return _context.Query<Document>()
                           .Where(x => x.ParentDocument == null)
                           .Count();
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
                throw new EntityNotFoundException<Document>(id);
            }
            return Get(id);
        }
    }
}
