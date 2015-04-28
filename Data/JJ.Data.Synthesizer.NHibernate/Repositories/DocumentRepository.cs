using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class DocumentRepository : JJ.Data.Synthesizer.DefaultRepositories.DocumentRepository
    {
        private SynthesizerSqlExecutor _synthesizerSqlExecutor;

        public DocumentRepository(IContext context)
            : base(context)
        {
            NHibernateContext nhibernateContext = (NHibernateContext)context;
            ISqlExecutor sqlExecutor = new NHibernateSqlExecutor(nhibernateContext.Session);
            _synthesizerSqlExecutor = new SynthesizerSqlExecutor(sqlExecutor);
        }

        public override IList<Document> GetPage(int firstIndex, int count)
        {
            IList<Document> list = new List<Document>(count);

            IList<int> ids = _synthesizerSqlExecutor.Document_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Document entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            return _synthesizerSqlExecutor.Document_Count();
        }
    }
}
