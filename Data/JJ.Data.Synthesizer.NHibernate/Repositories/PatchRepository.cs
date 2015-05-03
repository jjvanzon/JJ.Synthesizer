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
    public class PatchRepository : JJ.Data.Synthesizer.DefaultRepositories.PatchRepository
    {
        public PatchRepository(IContext context)
            : base(context)
        { }

        public override IList<Patch> GetPage(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            IList<Patch> list = new List<Patch>(count);

            IList<int> ids = sqlExecutor.Patch_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Patch entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            return sqlExecutor.Patch_Count();
        }
    }
}
