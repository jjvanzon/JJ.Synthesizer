using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Reflection.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class DocumentRepository : DefaultRepositories.DocumentRepository
    {
        private new NHibernateContext _context;

        public DocumentRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Document> GetPageOfRootDocumentsOrderedByName(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            IList<int> ids = sqlExecutor.Document_GetPageOfRootDocumentIDsOrderedByName(firstIndex, count).ToArray();
            IList<Document> entities = GetManyByID(ids);
            return entities;
        }

        public override int CountRootDocuments()
        {
            int count = _context.Session.QueryOver<Document>()
                                        .Where(x => x.ParentDocument == null)
                                        .RowCount();
            return count;
        }

        private IList<Document> GetManyByID(IList<int> ids)
        {
            if (ids == null) throw new NullException(() => ids);

            IList<Document> list = new List<Document>(ids.Count);
            foreach (int id in ids)
            {
                Document entity = Get(id);
                list.Add(entity);
            }

            return list;
        }
    }
}
