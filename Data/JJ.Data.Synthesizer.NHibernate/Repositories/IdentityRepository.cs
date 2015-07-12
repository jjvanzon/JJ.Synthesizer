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
    public class IdentityRepository : JJ.Data.Synthesizer.DefaultRepositories.IdentityRepository
    {
        private string _connectionString;

        public IdentityRepository(IContext context)
            : base(context)
        {
            _connectionString = context.Location;
        }

        public override int GenerateID()
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor_InSeparateConnection(_connectionString);
            int id = sqlExecutor.GenerateID();
            return id;
        }
    }
}
