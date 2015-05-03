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
    public class AudioFileOutputRepository : JJ.Data.Synthesizer.DefaultRepositories.AudioFileOutputRepository
    {
        public AudioFileOutputRepository(IContext context)
            : base(context)
        { }

        public override IList<AudioFileOutput> GetPage(int firstIndex, int count)
        {
            IList<AudioFileOutput> list = new List<AudioFileOutput>(count);

            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            IList<int> ids = sqlExecutor.AudioFileOutput_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                AudioFileOutput entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            return sqlExecutor.AudioFileOutput_Count();
        }
    }
}
