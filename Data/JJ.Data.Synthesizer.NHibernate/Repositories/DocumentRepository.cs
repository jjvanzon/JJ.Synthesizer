using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Data.SqlClient;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class DocumentRepository : JJ.Data.Synthesizer.DefaultRepositories.DocumentRepository
    {
        private new NHibernateContext _context;

        public DocumentRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Document> GetPageOfRootDocuments(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            IList<int> ids = sqlExecutor.Document_GetPageOfRootDocumentIDs(firstIndex, count).ToArray();
            IList<Document> entities = GetManyByID(ids);
            return entities;
        }

        public override int CountRootDocuments()
        {
            int count = _context.Session.QueryOver<Document>()
                                        .Where(x => x.AsEffectInDocument == null)
                                        .Where(x => x.AsInstrumentInDocument == null)
                                        .RowCount();

            return count;
        }

        public override IList<Document> GetInstruments(int documentID)
        {
            IList<Document> entities = _context.Session.QueryOver<Document>()
                                                       .Where(x => x.AsInstrumentInDocument.ID == documentID)
                                                       .List();
            return entities;
        }

        public override IList<Document> GetEffects(int documentID)
        {
            IList<Document> entities = _context.Session.QueryOver<Document>()
                                                       .Where(x => x.AsEffectInDocument.ID == documentID)
                                                       .List();
            return entities;
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
