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
        private SynthesizerSqlExecutor _synthesizerSqlExecutor;

        public CurveRepository(IContext context)
            : base(context)
        {
            NHibernateContext nhibernateContext = (NHibernateContext)context;
            ISqlExecutor sqlExecutor = new NHibernateSqlExecutor(nhibernateContext.Session);
            _synthesizerSqlExecutor = new SynthesizerSqlExecutor(sqlExecutor);
        }

        public override IList<Curve> GetPage(int firstIndex, int count)
        {
            IList<Curve> list = new List<Curve>(count);

            IList<int> ids = _synthesizerSqlExecutor.Curve_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Curve entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            return _synthesizerSqlExecutor.Curve_Count();
        }
    }
}
