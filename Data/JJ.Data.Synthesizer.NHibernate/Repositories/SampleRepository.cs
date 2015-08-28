using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Data.Synthesizer.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.NHibernate.Helpers;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class SampleRepository : DefaultRepositories.SampleRepository
    {
        private new NHibernateContext _context;

        // Cache Bytes column, so we can get non-flushed bytes.
        private Dictionary<int, byte[]> _bytesDictionary = new Dictionary<int, byte[]>();
        private object _bytesDictionaryLock = new object();

        public SampleRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override byte[] TryGetBytes(int id)
        {
            lock (_bytesDictionary)
            {
                byte[] bytes;
                if (!_bytesDictionary.TryGetValue(id, out bytes))
                {
                    SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
                    bytes = sqlExecutor.Sample_TryGetBytes(id);
                    _bytesDictionary[id] = bytes;
                }

                return bytes;
            }
        }

        public override void SetBytes(int id, byte[] bytes)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            sqlExecutor.Sample_TrySetBytes(id, bytes);

            lock (_bytesDictionary)
            {
                _bytesDictionary[id] = bytes;
            }
        }
    }
}