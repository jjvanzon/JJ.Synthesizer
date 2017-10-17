using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data.NHibernate;
using System.Collections.Generic;
using System.Reflection;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.NHibernate
{
    /// <summary>
    /// Specialized IContext that derives from NHibernateContext,
    /// but adds to it the management of byte arrays.
    /// 
    /// It is a bad plan to map byte arrays using NHibernate,
    /// because then retrieving an entity has a large overhead.
    /// Byte arrays are better managed with separate SQL,
    /// because then you only retrieve the bytes when you need them.
    /// But the consequence is that only flushed byte arrays can be retreived.
    /// Intermediate flushes not recommended, because they are dangerous, because in memory we often work with data that is not yet valid.
    /// And also they cost performance if we only rollback the transaction afterwards.
    /// We want to be able to retrieve non-flushed, uncommitted sample bytes because business logic is so flexibly usable
    /// (e.g. Sample_OperatorWrapper.SampleBytes might be used any time.)
    /// That is why we want to cache the byte arrays in memory.
    /// 
    /// We cannot cache in repositories, because we need to purge cache entries upon rollback or upon deletion.
    /// Such transactionality can only be managed in an IContext implementation, not in a repository.
    /// 
    /// In a stateless environment you might be able to get away with not purging cached byte arrays,
    /// but Synthesizer is a stateful application and the context is long lived and the repositories are long lived,
    /// so the cached data must also be deleted.
    /// 
    /// If we didn't we would also introduce a MEMORY LEAK, because all samples ever user while you ran the application
    /// would have been kept in memory, even ones that not even belong to the currently opened document.
    /// 
    /// So to be as flexible as to be able to retrieve byte arrays that are not yet flushed,
    /// and not to send byte arrays to SQL server unless we commit,
    /// we need to manage that in an alternative IContext implementation.
    /// 
    /// This also improves performance preventing needless SQL statements when bytes were already read.
    /// </summary>
    public class SynthesizerContext : NHibernateContext
    {
        public SynthesizerContext(string connectionString, Assembly modelAssembly, Assembly mappingAssembly, string dialect)
            : base(connectionString, modelAssembly, mappingAssembly, dialect)
        { }

        private readonly object _sampleBytesLock = new object();
        private readonly Dictionary<int, byte[]> _sampleBytesReadDictionary = new Dictionary<int, byte[]>();
        private readonly Dictionary<int, byte[]> _sampleBytesToSaveDictionary = new Dictionary<int, byte[]>();

        public byte[] TryGetSampleBytes(int sampleID)
        {
            lock (_sampleBytesLock)
            {
                byte[] sampleBytes;
                if (_sampleBytesReadDictionary.TryGetValue(sampleID, out sampleBytes))
                {
                    return sampleBytes;
                }

                SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(this);
                sampleBytes = sqlExecutor.Sample_TryGetBytes(sampleID);
                _sampleBytesReadDictionary[sampleID] = sampleBytes;
                return sampleBytes;
            }
        }

        public void SetSampleBytes(int sampleID, byte[] sampleBytes)
        {
            lock (_sampleBytesLock)
            {
                _sampleBytesToSaveDictionary[sampleID] = sampleBytes;
                _sampleBytesReadDictionary[sampleID] = sampleBytes;
            }
        }

        public override void Delete(object entity)
        {
            base.Delete(entity);

            if (entity is Sample sample)
            {
                lock (_sampleBytesLock)
                {
                    if (_sampleBytesReadDictionary.ContainsKey(sample.ID))
                    {
                        _sampleBytesReadDictionary.Remove(sample.ID);
                    }

                    if (_sampleBytesToSaveDictionary.ContainsKey(sample.ID))
                    {
                        _sampleBytesToSaveDictionary.Remove(sample.ID);
                    }
                }
            }
        }

        public override void Flush()
        {
            base.Flush();

            lock (_sampleBytesLock)
            {
                foreach (var entry in _sampleBytesToSaveDictionary)
                {
                    SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(this);
                    sqlExecutor.Sample_TrySetBytes(entry.Key, entry.Value);
                }

                _sampleBytesToSaveDictionary.Clear();
            }
        }

        public override void Rollback()
        {
            base.Rollback();

            lock (_sampleBytesLock)
            {
                _sampleBytesToSaveDictionary.Clear();
                _sampleBytesReadDictionary.Clear();
            }
        }

        public override void Commit()
        {
            try
            {
                base.Commit();
            }
            catch
            {
                // When Commit fails in any way (which includes Flush), you want to start with a clean slate.
                lock (_sampleBytesLock)
                {
                    _sampleBytesToSaveDictionary.Clear();
                    _sampleBytesReadDictionary.Clear();
                }

                throw;
            }
        }
    }
}
