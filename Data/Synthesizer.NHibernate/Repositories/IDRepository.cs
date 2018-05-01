using JetBrains.Annotations;
using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
	[UsedImplicitly]
	public class IDRepository : DefaultRepositories.IDRepository
	{
		private readonly string _connectionString;

		public IDRepository(IContext context) : base(context) => _connectionString = context.Location;

		public override int GetID()
		{
			SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor_InSeparateConnection(_connectionString);
			int id = sqlExecutor.GetID();
			return id;
		}
	}
}