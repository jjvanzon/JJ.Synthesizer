using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DocumentRepository : RepositoryBase<Document, int>, IDocumentRepository
    {
        public DocumentRepository(IContext context)
            : base(context)
        { }

        public virtual IList<Document> GetPageOfRootDocuments(int firstIndex, int count)
        {
            return _context.Query<Document>()
                           .Skip(firstIndex)
                           .Take(count)
                           .ToArray();
        }

        public virtual int CountRootDocuments()
        {
            return _context.Query<Document>().Count();
        }

        public virtual IList<Document> GetInstruments(int documentID)
        {
            return _context.Query<Document>()
                           .Where(x => x.AsInstrumentInDocument.ID == documentID)
                           .ToArray();
        }

        public virtual IList<Document> GetEffects(int documentID)
        {
            return _context.Query<Document>()
                           .Where(x => x.AsEffectInDocument.ID == documentID)
                           .ToArray();
        }
    }
}
