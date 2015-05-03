using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Data.SqlClient;
using JJ.Data.Synthesizer.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.NHibernate.Helpers;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class SampleRepository : JJ.Data.Synthesizer.DefaultRepositories.SampleRepository
    {
        public SampleRepository(IContext context)
            : base(context)
        { }

        public override byte[] GetBinary(int id)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            byte[] binary = sqlExecutor.Sample_TryGetBinary(id);
            if (binary == null)
            {
                throw new Exception(String.Format("Binary is null for Sample with id '{0}' or the Sample does not exist.", id));
            }
            return binary;
        }

        public override void SetBinary(int id, byte[] bytes)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            Sample sample = Get(id); // Force an exception when the entity does not exist.

            sqlExecutor.Sample_TrySetBinary(id, bytes);
        }

        public override IList<Sample> GetPage(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            IList<Sample> list = new List<Sample>(count);

            IList<int> ids = sqlExecutor.Sample_GetPageOfIDs(firstIndex, count).ToArray();
            foreach (int id in ids)
            {
                Sample entity = Get(id);
                list.Add(entity);
            }

            return list;
        }

        public override int Count()
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);

            return sqlExecutor.Sample_Count();
        }
    }
}
