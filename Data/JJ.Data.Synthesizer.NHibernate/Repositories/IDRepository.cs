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
    public class IDRepository : JJ.Data.Synthesizer.DefaultRepositories.IDRepository
    {
        private string _connectionString;

        public IDRepository(IContext context)
            : base(context)
        {
            _connectionString = context.Location;
        }

        public override int GetID()
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor_InSeparateConnection(_connectionString);
            int id = sqlExecutor.GetID();
            return id;
        }
    }
}
