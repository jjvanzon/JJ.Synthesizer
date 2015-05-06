using JJ.Framework.Data.SqlClient;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.SqlClient
{
    public class SynthesizerSqlExecutor
    {
        private ISqlExecutor _sqlExecutor;

        public SynthesizerSqlExecutor(ISqlExecutor sqlExecutor)
        {
            if (sqlExecutor == null) throw new NullException(() => sqlExecutor);
            _sqlExecutor = sqlExecutor;
        }

        public int AudioFileOutput_Count()
        {
            return (int)_sqlExecutor.ExecuteScalar(SqlEnum.AudioFileOutput_Count);
        }

        public IList<int> AudioFileOutput_GetPageOfIDs(int firstIndex, int count)
        {
            return _sqlExecutor.ExecuteReader<int>(SqlEnum.AudioFileOutput_GetPageOfIDs, new { firstIndex, count, }).ToArray();
        }

        public int Curve_Count()
        {
            return (int)_sqlExecutor.ExecuteScalar(SqlEnum.Curve_Count);
        }

        public IList<int> Curve_GetPageOfIDs(int firstIndex, int count)
        {
            return _sqlExecutor.ExecuteReader<int>(SqlEnum.Curve_GetPageOfIDs, new { firstIndex, count, }).ToArray();
        }

        public int Document_CountRootDocuments()
        {
            return (int)_sqlExecutor.ExecuteScalar(SqlEnum.Document_CountRootDocuments);
        }

        public IList<int> Document_GetPageOfRootDocumentIDs(int firstIndex, int count)
        {
            return _sqlExecutor.ExecuteReader<int>(SqlEnum.Document_GetPageOfRootDocumentIDs, new { firstIndex, count, }).ToArray();
        }

        public int Patch_Count()
        {
            return (int)_sqlExecutor.ExecuteScalar(SqlEnum.Patch_Count);
        }

        public IList<int> Patch_GetPageOfIDs(int firstIndex, int count)
        {
            return _sqlExecutor.ExecuteReader<int>(SqlEnum.Patch_GetPageOfIDs, new { firstIndex, count, }).ToArray();
        }

        public int Sample_Count()
        {
            return (int)_sqlExecutor.ExecuteScalar(SqlEnum.Sample_Count);
        }

        public IList<int> Sample_GetPageOfIDs(int firstIndex, int count)
        {
            return _sqlExecutor.ExecuteReader<int>(SqlEnum.Sample_GetPageOfIDs, new { firstIndex, count, }).ToArray();
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
