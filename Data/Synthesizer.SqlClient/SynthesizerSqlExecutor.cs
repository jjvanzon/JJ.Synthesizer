using JJ.Framework.Data.SqlClient;
using JJ.Framework.Exceptions;
using System;

namespace JJ.Data.Synthesizer.SqlClient
{
    public class SynthesizerSqlExecutor
    {
        private readonly ISqlExecutor _sqlExecutor;

        public SynthesizerSqlExecutor(ISqlExecutor sqlExecutor)
        {
            if (sqlExecutor == null) throw new NullException(() => sqlExecutor);
            _sqlExecutor = sqlExecutor;
        }

        public int EntityPosition_DeleteOrphans()
        {
            return (int)_sqlExecutor.ExecuteScalar(SqlEnum.EntityPosition_DeleteOrphans);
        }

        public int GetID()
        {
            int id = (int)_sqlExecutor.ExecuteScalar(SqlEnum.GetID);
            return id;
        }

        /// <summary>
        /// Beware that you could get null returned, which either means the database
        /// field is null or that the database record does not exist.
        /// </summary>
        public byte[] Sample_TryGetBytes(int id)
        {
            object obj = _sqlExecutor.ExecuteScalar(SqlEnum.Sample_TryGetBytes, new { id }); ;
            if (obj == DBNull.Value)
            {
                return null;
            }

            byte[] bytes = (byte[])obj;
            return bytes;
        }
        
        /// <summary>
        /// Beware that if the record does not exist in the database,
        /// nothing is updated and you will not get an error message either.
        /// </summary>
        public void Sample_TrySetBytes(int id, byte[] bytes)
        {
            _sqlExecutor.ExecuteNonQuery(SqlEnum.Sample_TrySetBytes, new { id, bytes });
        }
    }
}
