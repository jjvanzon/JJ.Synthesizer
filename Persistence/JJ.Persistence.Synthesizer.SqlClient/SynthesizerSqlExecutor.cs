using JJ.Framework.Persistence.SqlClient;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.SqlClient
{
    public class SynthesizerSqlExecutor
    {
        private ISqlExecutor _sqlExecutor;

        public SynthesizerSqlExecutor(ISqlExecutor sqlExecutor)
        {
            if (sqlExecutor == null) throw new NullException(() => sqlExecutor);
            _sqlExecutor = sqlExecutor;
        }

        /// <summary>
        /// Beware that you could get null returned, which either means the database
        /// field is null or that the database record does not exist.
        /// </summary>
        public byte[] Sample_TryGetBinary(int id)
        {
            byte[] binary = (byte[])_sqlExecutor.ExecuteScalar(SqlEnum.Sample_TryGetBinary, new { id });
            return binary;
        }

        /// <summary>
        /// Beware that if the record does not exist in the database,
        /// nothing is updated and you will not get an error message either.
        /// </summary>
        public void Sample_TrySetBinary(int id, byte[] binary)
        {
            _sqlExecutor.ExecuteNonQuery(SqlEnum.Sample_TrySetBinary, new { id, binary });
        }
    }
}
