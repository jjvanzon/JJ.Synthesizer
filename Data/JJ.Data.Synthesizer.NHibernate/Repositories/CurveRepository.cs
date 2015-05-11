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
    public class CurveRepository : JJ.Data.Synthesizer.DefaultRepositories.CurveRepository
    {
        public CurveRepository(IContext context)
            : base(context)
        { }

        public override IList<Curve> GetPage(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            IList<Curve> list = new List<Curve>(count);

            IList<int> ids = sqlExecutor.Curve_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Curve entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            return sqlExecutor.Curve_Count();
        }
    }
}
