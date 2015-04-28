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
    public class AudioFileOutputRepository : JJ.Data.Synthesizer.DefaultRepositories.AudioFileOutputRepository
    {
        private SynthesizerSqlExecutor _synthesizerSqlExecutor;

        public AudioFileOutputRepository(IContext context)
            : base(context)
        {
            NHibernateContext nhibernateContext = (NHibernateContext)context;
            ISqlExecutor sqlExecutor = new NHibernateSqlExecutor(nhibernateContext.Session);
            _synthesizerSqlExecutor = new SynthesizerSqlExecutor(sqlExecutor);
        }

        public override IList<AudioFileOutput> GetPage(int firstIndex, int count)
        {
            IList<AudioFileOutput> list = new List<AudioFileOutput>(count);

            IList<int> ids = _synthesizerSqlExecutor.AudioFileOutput_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                AudioFileOutput entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            return _synthesizerSqlExecutor.AudioFileOutput_Count();
        }
    }
}
