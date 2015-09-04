using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Data.SqlClient;

namespace JJ.Data.Synthesizer.NHibernate.Helpers
{
    internal static class SqlExecutorHelper
    {
        /// <summary>
        /// You need to recreate the SqlExecutor every time you use one,
        /// because even though the IContext is long-lived,
        /// the underlying NHibernate Session is not; it is recreated every time you end a transaction
        /// with either Rollback, Commit or Dispose, requiring you to recreate the SqlExecutor.
        /// </summary>
        public static SynthesizerSqlExecutor CreateSynthesizerSqlExecutor(IContext context)
        {
            NHibernateContext nHibernateContext = (NHibernateContext)context;
            ISqlExecutor sqlExecutor = new NHibernateSqlExecutor(nHibernateContext.Session);
            SynthesizerSqlExecutor synthesizerSqlExecutor = new SynthesizerSqlExecutor(sqlExecutor);
            return synthesizerSqlExecutor;
        }

        public static SynthesizerSqlExecutor CreateSynthesizerSqlExecutor_InSeparateConnection(string connectionString)
        {
            ISqlExecutor sqlExecutor = new SqlExecutor(connectionString);
            SynthesizerSqlExecutor synthesizerSqlExecutor = new SynthesizerSqlExecutor(sqlExecutor);
            return synthesizerSqlExecutor;
        }
    }
}
