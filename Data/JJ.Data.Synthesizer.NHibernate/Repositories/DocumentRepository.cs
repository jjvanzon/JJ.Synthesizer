using JJ.Data.Synthesizer.NHibernate.Helpers;
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
        public DocumentRepository(IContext context)
            : base(context)
        { }

        public override IList<Document> GetPage(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            IList<Document> list = new List<Document>(count);

            IList<int> ids = sqlExecutor.Document_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Document entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            SynthesizerSqlExecutor synthesizerSqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            return synthesizerSqlExecutor.Document_Count();
        }
    }
}
