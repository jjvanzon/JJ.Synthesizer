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
        private SynthesizerSqlExecutor _synthesizerSqlExecutor;

        public PatchRepository(IContext context)
            : base(context)
        {
            NHibernateContext nhibernateContext = (NHibernateContext)context;
            ISqlExecutor sqlExecutor = new NHibernateSqlExecutor(nhibernateContext.Session);
            _synthesizerSqlExecutor = new SynthesizerSqlExecutor(sqlExecutor);
        }

        public override IList<Patch> GetPage(int firstIndex, int count)
        {
            IList<Patch> list = new List<Patch>(count);

            IList<int> ids = _synthesizerSqlExecutor.Patch_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Patch entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            return _synthesizerSqlExecutor.Patch_Count();
        }
    }
}
